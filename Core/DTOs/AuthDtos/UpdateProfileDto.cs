using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.AuthDtos;

/// <summary>
/// Kullanıcı profil bilgisi güncelleme isteği
/// </summary>
public class UpdateProfileDto
{
    [Required(ErrorMessage = "Ad Soyad gereklidir")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Ad Soyad 2-100 karakter arasında olmalıdır")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta gereklidir")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;
}
