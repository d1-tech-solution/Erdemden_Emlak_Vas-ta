using BussinessLayer.Abstract;
using BussinessLayer.Settings;
using Core.DTOs.AuthDtos;
using Core.DTOs.Common;
using Core.DTOs.UserDtos;
using DataAcessLayer.Abstract;
using EntityLayer.Entities;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BussinessLayer.Concrete;

/// <summary>
/// Kimlik doğrulama servisi implementasyonu
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly JwtSettings _jwtSettings;
    private readonly IEmailService _emailService;

    public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService, IOptions<JwtSettings> jwtSettings, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;
        _emailService = emailService;
    }

    /// <summary>
    /// Kullanıcı girişi
    /// </summary>
    public async Task<ApiResponseDto<AuthResponseDto>> LoginAsync(LoginDto loginDto)
    {
        var user = await _unitOfWork.Repository<User>()
            .GetAsync(u => u.Email == loginDto.Email);

        if (user == null)
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse("E-posta veya şifre hatalı");
        }

        if (string.IsNullOrEmpty(user.PasswordHash))
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse("Bu hesap Google ile oluşturuldu. Lütfen Google ile giriş yapın.");
        }

        if (!VerifyPassword(loginDto.Password, user.PasswordHash))
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse("E-posta veya şifre hatalı");
        }

        if (!user.IsActive)
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse("Hesabınız devre dışı bırakılmış");
        }

        // Token oluştur
        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken(user.Id);

        // Refresh token'ı kaydet
        await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);

        // Son giriş zamanını güncelle
        user.LastLoginAt = DateTime.UtcNow;
        _unitOfWork.Repository<User>().Update(user);

        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto<AuthResponseDto>.SuccessResponse(new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpiresAt = refreshToken.ExpiresAt,
            User = MapToUserDto(user)
        }, "Giriş başarılı");
    }

    /// <summary>
    /// Kullanıcı kaydı
    /// </summary>
    // İzin verilen gerçek e-posta domainleri
    private static readonly HashSet<string> AllowedEmailDomains = new(StringComparer.OrdinalIgnoreCase)
    {
        "gmail.com", "googlemail.com",
        "hotmail.com", "hotmail.com.tr", "outlook.com", "outlook.com.tr", "live.com", "msn.com",
        "yahoo.com", "yahoo.com.tr",
        "icloud.com", "me.com", "mac.com",
        "yandex.com", "yandex.com.tr",
        "protonmail.com", "proton.me",
        "aol.com",
        "mail.com",
        "zoho.com",
        "edu.tr", "k12.tr"
    };

    private static bool IsAllowedEmailDomain(string email)
    {
        var parts = email.Split('@');
        if (parts.Length != 2) return false;
        var domain = parts[1].ToLowerInvariant();

        // Direkt eşleşme
        if (AllowedEmailDomains.Contains(domain)) return true;

        // .edu.tr ve .k12.tr ile biten eğitim domainleri
        if (domain.EndsWith(".edu.tr") || domain.EndsWith(".k12.tr")) return true;

        return false;
    }

    public async Task<ApiResponseDto<AuthResponseDto>> RegisterAsync(RegisterDto registerDto)
    {
        // E-posta domain kontrolü
        if (!IsAllowedEmailDomain(registerDto.Email))
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse(
                "Lütfen geçerli bir e-posta adresi kullanın (Gmail, Hotmail, Outlook, Yahoo vb.)");
        }

        // E-posta kontrolü
        var existingUser = await _unitOfWork.Repository<User>()
            .ExistsAsync(u => u.Email == registerDto.Email);

        if (existingUser)
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse("Bu e-posta adresi zaten kullanılıyor");
        }

        // Yeni kullanıcı oluştur
        var user = new User
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            PasswordHash = HashPassword(registerDto.Password),
            Role = UserRole.User,
            IsActive = true,
            IsEmailVerified = true
        };

        await _unitOfWork.Repository<User>().AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // Token oluştur - direkt giriş yap
        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken(user.Id);

        await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);

        user.LastLoginAt = DateTime.UtcNow;
        _unitOfWork.Repository<User>().Update(user);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto<AuthResponseDto>.SuccessResponse(new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpiresAt = refreshToken.ExpiresAt,
            User = MapToUserDto(user)
        }, "Kayıt başarılı");
    }

    /// <summary>
    /// Token yenileme
    /// </summary>
    public async Task<ApiResponseDto<AuthResponseDto>> RefreshTokenAsync(string accessToken, string refreshToken)
    {
        // Access token'dan user ID al
        var userId = _tokenService.GetUserIdFromToken(accessToken);
        if (userId == null)
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse("Geçersiz token");
        }

        // Refresh token'ı bul
        var storedToken = await _unitOfWork.Repository<RefreshToken>()
            .Query()
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == refreshToken && r.UserId == userId);

        if (storedToken == null || !storedToken.IsActive)
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse("Geçersiz veya süresi dolmuş refresh token");
        }

        var user = storedToken.User;

        if (!user.IsActive)
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse("Hesabınız devre dışı bırakılmış");
        }

        // Eski refresh token'ı iptal et
        storedToken.IsRevoked = true;
        storedToken.RevokedAt = DateTime.UtcNow;

        // Yeni tokenlar oluştur
        var newAccessToken = _tokenService.GenerateAccessToken(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken(user.Id);

        storedToken.ReplacedByToken = newRefreshToken.Token;

        await _unitOfWork.Repository<RefreshToken>().AddAsync(newRefreshToken);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto<AuthResponseDto>.SuccessResponse(new AuthResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token,
            ExpiresAt = newRefreshToken.ExpiresAt,
            User = MapToUserDto(user)
        }, "Token yenilendi");
    }

    /// <summary>
    /// Çıkış yapma
    /// </summary>
    public async Task<ApiResponseDto> LogoutAsync(Guid userId, string refreshToken)
    {
        var storedToken = await _unitOfWork.Repository<RefreshToken>()
            .GetAsync(r => r.Token == refreshToken && r.UserId == userId);

        if (storedToken != null && storedToken.IsActive)
        {
            storedToken.IsRevoked = true;
            storedToken.RevokedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();
        }

        return ApiResponseDto.SuccessResponse("Çıkış başarılı");
    }

    /// <summary>
    /// Şifre değiştirme
    /// </summary>
    public async Task<ApiResponseDto> ChangePasswordAsync(Guid userId, ChangePasswordDto changePasswordDto)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId);

        if (user == null)
        {
            return ApiResponseDto.FailResponse("Kullanıcı bulunamadı");
        }

        if (string.IsNullOrEmpty(user.PasswordHash))
        {
            return ApiResponseDto.FailResponse("Google hesapları için şifre değiştirilemez");
        }

        if (!VerifyPassword(changePasswordDto.CurrentPassword, user.PasswordHash))
        {
            return ApiResponseDto.FailResponse("Mevcut şifre hatalı");
        }

        user.PasswordHash = HashPassword(changePasswordDto.NewPassword);
        _unitOfWork.Repository<User>().Update(user);

        // Tüm refresh tokenları iptal et (güvenlik için)
        await RevokeAllUserTokens(userId);

        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto.SuccessResponse("Şifre başarıyla değiştirildi");
    }

    /// <summary>
    /// Profil bilgilerini güncelle (Ad, E-posta)
    /// </summary>
    public async Task<ApiResponseDto<object>> UpdateProfileAsync(Guid userId, UpdateProfileDto dto)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId);
        if (user == null)
        {
            return ApiResponseDto<object>.FailResponse("Kullanıcı bulunamadı");
        }

        var name = dto.Name?.Trim() ?? string.Empty;
        var email = dto.Email?.Trim().ToLowerInvariant() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
        {
            return ApiResponseDto<object>.FailResponse("Geçerli bir ad soyad giriniz");
        }
        if (string.IsNullOrWhiteSpace(email))
        {
            return ApiResponseDto<object>.FailResponse("Geçerli bir e-posta giriniz");
        }

        // E-posta değişiyorsa benzersizlik kontrolü
        if (!string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase))
        {
            var existing = await _unitOfWork.Repository<User>().GetAsync(u => u.Email == email && u.Id != userId);
            if (existing != null)
            {
                return ApiResponseDto<object>.FailResponse("Bu e-posta adresi başka bir hesap tarafından kullanılıyor");
            }

            user.Email = email;
            // E-posta değiştiğinde doğrulamayı sıfırla (var ise)
            user.IsEmailVerified = false;
        }

        user.Name = name;
        _unitOfWork.Repository<User>().Update(user);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto<object>.SuccessResponse(new
        {
            user.Id,
            user.Name,
            user.Email,
            Role = user.Role.ToString()
        }, "Profil bilgileri güncellendi");
    }

    /// <summary>
    /// Kullanıcının tüm oturumlarını sonlandır
    /// </summary>
    public async Task<ApiResponseDto> RevokeAllTokensAsync(Guid userId)
    {
        await RevokeAllUserTokens(userId);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto.SuccessResponse("Tüm oturumlar sonlandırıldı");
    }

    /// <summary>
    /// Google ile giriş
    /// </summary>
    public async Task<ApiResponseDto<AuthResponseDto>> GoogleLoginAsync(GoogleLoginDto googleLoginDto)
    {
        try
        {
            // 1. Google ID token'ı doğrula
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _jwtSettings.GoogleClientId }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(googleLoginDto.IdToken, settings);

            var email = payload.Email;
            var name = payload.Name;
            var googleId = payload.Subject;

            if (string.IsNullOrEmpty(email))
            {
                return ApiResponseDto<AuthResponseDto>.FailResponse("Google hesabında e-posta bulunamadı");
            }

            // 2. Kullanıcıyı email ile bul
            var user = await _unitOfWork.Repository<User>()
                .GetAsync(u => u.Email == email);

            // 3. Kullanıcı yoksa oluştur
            if (user == null)
            {
                user = new User
                {
                    Name = name ?? email.Split('@')[0],
                    Email = email,
                    PasswordHash = null,
                    GoogleId = googleId,
                    AuthProvider = "Google",
                    Role = UserRole.User,
                    IsActive = true,
                    IsEmailVerified = true
                };

                await _unitOfWork.Repository<User>().AddAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                // Mevcut kullanıcıya Google ID bağla (ilk Google login)
                if (string.IsNullOrEmpty(user.GoogleId))
                {
                    user.GoogleId = googleId;
                    if (string.IsNullOrEmpty(user.AuthProvider))
                        user.AuthProvider = string.IsNullOrEmpty(user.PasswordHash) ? "Google" : "Local";
                }

                user.LastLoginAt = DateTime.UtcNow;
                _unitOfWork.Repository<User>().Update(user);
                await _unitOfWork.SaveChangesAsync();
            }

            // 4. Hesap aktif mi kontrol et
            if (!user.IsActive)
            {
                return ApiResponseDto<AuthResponseDto>.FailResponse("Hesabınız devre dışı bırakılmış");
            }

            // 5. JWT token oluştur (normal login ile aynı)
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user.Id);

            await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponseDto<AuthResponseDto>.SuccessResponse(new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = refreshToken.ExpiresAt,
                User = MapToUserDto(user)
            }, "Google ile giriş başarılı");
        }
        catch (InvalidJwtException)
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse("Geçersiz Google token");
        }
    }

    /// <summary>
    /// Şifremi unuttum - e-posta ile şifre sıfırlama kodu gönderir
    /// </summary>
    public async Task<ApiResponseDto> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
    {
        var user = await _unitOfWork.Repository<User>()
            .GetAsync(u => u.Email == forgotPasswordDto.Email);

        // Güvenlik: Kullanıcı bulunamasa bile başarılı mesajı dön (email enumeration önleme)
        if (user == null)
        {
            return ApiResponseDto.SuccessResponse(
                "Eğer bu e-posta adresi kayıtlıysa, şifre sıfırlama kodu gönderildi.");
        }

        // Google-only hesaplar için şifre sıfırlama yapılmaz
        if (string.IsNullOrEmpty(user.PasswordHash))
        {
            return ApiResponseDto.SuccessResponse(
                "Eğer bu e-posta adresi kayıtlıysa, şifre sıfırlama kodu gönderildi.");
        }

        // 6 haneli rastgele kod oluştur
        var random = new Random();
        var resetCode = random.Next(100000, 999999).ToString();

        // Mevcut alanları kullan
        user.EmailVerificationCode = resetCode;
        user.EmailVerificationCodeExpiry = DateTime.UtcNow.AddMinutes(15);

        _unitOfWork.Repository<User>().Update(user);
        await _unitOfWork.SaveChangesAsync();

        // E-posta gönder
        try
        {
            await _emailService.SendPasswordResetCodeEmailAsync(
                user.Email, user.Name, resetCode);
        }
        catch
        {
            // E-posta gönderilemese bile hata verme (güvenlik)
        }

        return ApiResponseDto.SuccessResponse(
            "Eğer bu e-posta adresi kayıtlıysa, şifre sıfırlama kodu gönderildi.");
    }

    /// <summary>
    /// Şifre sıfırlama - kod ile doğrulama
    /// </summary>
    public async Task<ApiResponseDto> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        var user = await _unitOfWork.Repository<User>()
            .GetAsync(u => u.Email == resetPasswordDto.Email);

        if (user == null)
        {
            return ApiResponseDto.FailResponse("Geçersiz veya süresi dolmuş kod.");
        }

        // Kod kontrolü
        if (user.EmailVerificationCode != resetPasswordDto.Code)
        {
            return ApiResponseDto.FailResponse("Geçersiz veya süresi dolmuş kod.");
        }

        // Süre kontrolü
        if (user.EmailVerificationCodeExpiry == null ||
            user.EmailVerificationCodeExpiry < DateTime.UtcNow)
        {
            // Süresi dolmuş kodu temizle
            user.EmailVerificationCode = null;
            user.EmailVerificationCodeExpiry = null;
            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponseDto.FailResponse("Kodun süresi dolmuş. Lütfen yeni kod talep edin.");
        }

        // Şifre güncelle
        user.PasswordHash = HashPassword(resetPasswordDto.NewPassword);

        // Kodu temizle (tek kullanımlık)
        user.EmailVerificationCode = null;
        user.EmailVerificationCodeExpiry = null;

        _unitOfWork.Repository<User>().Update(user);

        // Tüm refresh tokenları iptal et (güvenlik için)
        await RevokeAllUserTokens(user.Id);

        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto.SuccessResponse("Şifreniz başarıyla sıfırlandı. Yeni şifrenizle giriş yapabilirsiniz.");
    }

    #region Private Methods

    private async Task RevokeAllUserTokens(Guid userId)
    {
        var activeTokens = await _unitOfWork.Repository<RefreshToken>()
            .GetAllAsync(r => r.UserId == userId && !r.IsRevoked);

        foreach (var token in activeTokens)
        {
            token.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;
        }
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
    }

    private static bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

    private static UserDto MapToUserDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            IsActive = user.IsActive,
            LastLoginAt = user.LastLoginAt,
            CreatedAt = user.CreatedAt
        };
    }

    #endregion
}
