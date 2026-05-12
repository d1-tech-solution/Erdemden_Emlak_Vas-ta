using Core.DTOs.AuthDtos;
using Core.DTOs.Common;

namespace BussinessLayer.Abstract;

/// <summary>
/// Kimlik doğrulama servisi
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Kullanıcı girişi
    /// </summary>
    Task<ApiResponseDto<AuthResponseDto>> LoginAsync(LoginDto loginDto);

    /// <summary>
    /// Kullanıcı kaydı
    /// </summary>
    Task<ApiResponseDto<AuthResponseDto>> RegisterAsync(RegisterDto registerDto);

    /// <summary>
    /// Token yenileme
    /// </summary>
    Task<ApiResponseDto<AuthResponseDto>> RefreshTokenAsync(string accessToken, string refreshToken);

    /// <summary>
    /// Çıkış yapma (refresh token'ı iptal eder)
    /// </summary>
    Task<ApiResponseDto> LogoutAsync(Guid userId, string refreshToken);

    /// <summary>
    /// Şifre değiştirme
    /// </summary>
    Task<ApiResponseDto> ChangePasswordAsync(Guid userId, ChangePasswordDto changePasswordDto);

    /// <summary>
    /// Profil bilgilerini güncelle (Ad, E-posta)
    /// </summary>
    Task<ApiResponseDto<object>> UpdateProfileAsync(Guid userId, UpdateProfileDto updateProfileDto);

    /// <summary>
    /// Kullanıcının tüm oturumlarını sonlandır
    /// </summary>
    Task<ApiResponseDto> RevokeAllTokensAsync(Guid userId);

    /// <summary>
    /// Google ile giriş (ID token doğrulama)
    /// </summary>
    Task<ApiResponseDto<AuthResponseDto>> GoogleLoginAsync(GoogleLoginDto googleLoginDto);

    /// <summary>
    /// Şifremi unuttum - e-posta ile şifre sıfırlama kodu gönderir
    /// </summary>
    Task<ApiResponseDto> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);

    /// <summary>
    /// Şifre sıfırlama - kod ile yeni şifre belirlenir
    /// </summary>
    Task<ApiResponseDto> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
}
