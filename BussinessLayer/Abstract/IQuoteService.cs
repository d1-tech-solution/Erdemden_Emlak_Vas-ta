using Core.DTOs.Common;
using Core.DTOs.QuoteRequestDtos;

namespace BussinessLayer.Abstract;

/// <summary>
/// Teklif talebi yönetimi servisi
/// </summary>
public interface IQuoteService
{
    /// <summary>
    /// Yeni teklif talebi oluştur
    /// </summary>
    Task<ApiResponseDto<QuoteRequestDto>> CreateQuoteAsync(CreateQuoteRequestDto dto, List<FileUploadDto>? expertReports = null, Guid? userId = null);

    /// <summary>
    /// Tüm teklif taleplerini getir (Admin için)
    /// </summary>
    Task<ApiResponseDto<List<QuoteRequestDto>>> GetAllQuotesAsync();

    /// <summary>
    /// Kullanıcının teklif taleplerini getir (Email ile - eski kayıtlar için fallback)
    /// </summary>
    Task<ApiResponseDto<List<QuoteRequestDto>>> GetQuotesByEmailAsync(string email);

    /// <summary>
    /// Kullanıcının teklif taleplerini getir (UserId ile - yeni kayıtlar) + email fallback
    /// </summary>
    Task<ApiResponseDto<List<QuoteRequestDto>>> GetMyQuotesAsync(Guid userId, string? email);

    /// <summary>
    /// Teklif talebini ID ile getir
    /// </summary>
    Task<ApiResponseDto<QuoteRequestDto>> GetQuoteByIdAsync(Guid id);

    /// <summary>
    /// Teklif talebini okundu olarak işaretle
    /// </summary>
    Task<ApiResponseDto> MarkAsReadAsync(Guid id);

    /// <summary>
    /// Teklif talebini sil
    /// </summary>
    Task<ApiResponseDto> DeleteQuoteAsync(Guid id);

    /// <summary>
    /// Teklif talebine medya dosyaları yükle (fotoğraf/video)
    /// </summary>
    Task<ApiResponseDto> UploadMediaAsync(Guid quoteId, List<MediaFileUploadDto> files);

    /// <summary>
    /// Medya dosyasını getir (download için)
    /// </summary>
    Task<(byte[]? Data, string FileName, string ContentType)?> GetMediaFileAsync(Guid mediaId);

    /// <summary>
    /// Admin teklif ver (fiyat aralığı belirle)
    /// </summary>
    Task<ApiResponseDto<QuoteRequestDto>> SubmitOfferAsync(Guid id, SubmitOfferDto dto);

    /// <summary>
    /// Müşteri teklifi kabul/ret et
    /// </summary>
    Task<ApiResponseDto<QuoteRequestDto>> RespondToOfferAsync(Guid id, Guid userId, string? customerEmail, RespondToOfferDto dto);
}

/// <summary>
/// Dosya yükleme DTO
/// </summary>
public class FileUploadDto
{
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string Base64Data { get; set; } = string.Empty;
}

/// <summary>
/// Medya dosyası yükleme DTO
/// </summary>
public class MediaFileUploadDto
{
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string MediaType { get; set; } = string.Empty; // "Photo" veya "Video"
    public Stream FileStream { get; set; } = null!;
    public long FileSize { get; set; }
}
