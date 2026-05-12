using System.Security.Claims;
using BussinessLayer.Abstract;
using BussinessLayer.Settings;
using Core.DTOs.AuthDtos;
using Core.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Erdemden.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly JwtSettings _jwtSettings;
    private readonly IWebHostEnvironment _environment;

    public AuthController(IAuthService authService, IOptions<JwtSettings> jwtSettings, IWebHostEnvironment environment)
    {
        _authService = authService;
        _jwtSettings = jwtSettings.Value;
        _environment = environment;
    }

    /// <summary>
    /// Kullanıcı girişi - Cookie'ye token yazar
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        // Token'ları HttpOnly Cookie'ye yaz
        SetTokenCookies(result.Data!.AccessToken, result.Data.RefreshToken);

        // Response'da token döndürme (güvenlik için)
        return Ok(ApiResponseDto<object>.SuccessResponse(new
        {
            result.Data.User,
            result.Message
        }));
    }

    /// <summary>
    /// Kullanıcı kaydı - Cookie'ye token yazar
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var result = await _authService.RegisterAsync(registerDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        // Token'ları HttpOnly Cookie'ye yaz
        SetTokenCookies(result.Data!.AccessToken, result.Data.RefreshToken);

        return Ok(ApiResponseDto<object>.SuccessResponse(new
        {
            result.Data.User,
            result.Message
        }));
    }

    /// <summary>
    /// Google ile giriş - ID token doğrulama
    /// </summary>
    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto googleLoginDto)
    {
        var result = await _authService.GoogleLoginAsync(googleLoginDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        // Token'ları HttpOnly Cookie'ye yaz (normal login ile aynı)
        SetTokenCookies(result.Data!.AccessToken, result.Data.RefreshToken);

        return Ok(ApiResponseDto<object>.SuccessResponse(new
        {
            result.Data.User,
            result.Message
        }));
    }

    /// <summary>
    /// Token yenileme - Cookie'deki refresh token ile
    /// </summary>
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        var accessToken = Request.Cookies["accessToken"];
        var refreshToken = Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
        {
            return Unauthorized(ApiResponseDto.FailResponse("Token bulunamadı"));
        }

        var result = await _authService.RefreshTokenAsync(accessToken, refreshToken);

        if (!result.Success)
        {
            // Token geçersiz, cookie'leri sil
            DeleteTokenCookies();
            return Unauthorized(result);
        }

        // Yeni token'ları cookie'ye yaz
        SetTokenCookies(result.Data!.AccessToken, result.Data.RefreshToken);

        return Ok(ApiResponseDto<object>.SuccessResponse(new
        {
            result.Data.User,
            Message = "Token yenilendi"
        }));
    }

    /// <summary>
    /// Çıkış yapma - Cookie'leri siler
    /// </summary>
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var userId = GetCurrentUserId();
        var refreshToken = Request.Cookies["refreshToken"];

        if (userId.HasValue && !string.IsNullOrEmpty(refreshToken))
        {
            await _authService.LogoutAsync(userId.Value, refreshToken);
        }

        DeleteTokenCookies();

        return Ok(ApiResponseDto.SuccessResponse("Çıkış başarılı"));
    }

    /// <summary>
    /// Şifre değiştirme
    /// </summary>
    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var userId = GetCurrentUserId();

        if (!userId.HasValue)
        {
            return Unauthorized(ApiResponseDto.FailResponse("Kullanıcı bulunamadı"));
        }

        var result = await _authService.ChangePasswordAsync(userId.Value, changePasswordDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        // Şifre değiştiğinde cookie'leri sil (yeniden giriş gerekli)
        DeleteTokenCookies();

        return Ok(result);
    }

    /// <summary>
    /// Şifremi unuttum - e-posta ile kod gönderir
    /// </summary>
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
    {
        var result = await _authService.ForgotPasswordAsync(forgotPasswordDto);
        return Ok(result);
    }

    /// <summary>
    /// Şifre sıfırlama - kod ile yeni şifre belirlenir
    /// </summary>
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        var result = await _authService.ResetPasswordAsync(resetPasswordDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Profil bilgilerini güncelle (Ad, E-posta)
    /// </summary>
    [Authorize]
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto updateProfileDto)
    {
        var userId = GetCurrentUserId();

        if (!userId.HasValue)
        {
            return Unauthorized(ApiResponseDto.FailResponse("Kullanıcı bulunamadı"));
        }

        var result = await _authService.UpdateProfileAsync(userId.Value, updateProfileDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Mevcut kullanıcı bilgisi
    /// </summary>
    [Authorize]
    [HttpGet("me")]
    public IActionResult GetCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var name = User.FindFirst(ClaimTypes.Name)?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;

        return Ok(ApiResponseDto<object>.SuccessResponse(new
        {
            Id = userId,
            Email = email,
            Name = name,
            Role = role
        }));
    }

    #region Private Methods

    private void SetTokenCookies(string accessToken, string refreshToken)
    {
        // Development: SameSite=None + Secure=true (HTTPS localhost için çalışır)
        // Production: SameSite=Strict + Secure=true
        var isProduction = _environment.IsProduction();

        Response.Cookies.Append("accessToken", accessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true, // HTTPS kullandığımız için her zaman true
            SameSite = isProduction ? SameSiteMode.Strict : SameSiteMode.None,
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
            Path = "/"
        });

        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true, // HTTPS kullandığımız için her zaman true
            SameSite = isProduction ? SameSiteMode.Strict : SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
            Path = "/"
        });
    }

    private void DeleteTokenCookies()
    {
        Response.Cookies.Delete("accessToken");
        Response.Cookies.Delete("refreshToken");
    }

    private Guid? GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (Guid.TryParse(userIdClaim, out var userId))
        {
            return userId;
        }

        return null;
    }

    #endregion
}
