using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.ImageDtos;

/// <summary>
/// Görsel yükleme isteği
/// </summary>
public class UploadImageDto
{
    /// <summary>
    /// Mevcut resmin ID'si (varsa base64 gerekmez, resim korunur)
    /// </summary>
    public Guid? ExistingImageId { get; set; }

    /// <summary>
    /// Base64 encoded görsel verisi (yeni resim için)
    /// </summary>
    public string? Base64Data { get; set; }

    /// <summary>
    /// Dosya adı (uzantı dahil, yeni resim için)
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// Sıralama (opsiyonel)
    /// </summary>
    public int? Order { get; set; }
}
