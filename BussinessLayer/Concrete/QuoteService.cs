using BussinessLayer.Abstract;
using Core.DTOs.Common;
using Core.DTOs.DocumentDtos;
using Core.DTOs.QuoteRequestDtos;
using DataAcessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Concrete;

/// <summary>
/// Teklif talebi yönetimi servisi implementasyonu
/// </summary>
public class QuoteService : IQuoteService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private const string UploadBasePath = "Uploads/QuoteMedia";
    private const int MaxBase64FileSizeMB = 20; // Maksimum dosya boyutu (MB)

    public QuoteService(IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    /// <summary>
    /// Yeni teklif talebi oluştur
    /// </summary>
    public async Task<ApiResponseDto<QuoteRequestDto>> CreateQuoteAsync(CreateQuoteRequestDto dto, List<FileUploadDto>? expertReports = null)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            // QuoteRequest oluştur
            var quoteRequest = new QuoteRequest
            {
                Date = DateTime.UtcNow,
                Plate = dto.Plate,
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year,
                Km = dto.Km,
                Gear = dto.Gear,
                Fuel = dto.Fuel,
                Damage = dto.Damage,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone,
                Email = dto.Email,
                IsRead = false
            };

            await _unitOfWork.Repository<QuoteRequest>().AddAsync(quoteRequest);
            await _unitOfWork.SaveChangesAsync();

            // Ekspertiz raporlarını ekle
            if (expertReports != null && expertReports.Count > 0)
            {
                foreach (var report in expertReports)
                {
                    // Base64 boyut validasyonu (decode öncesi bellek koruması)
                    var estimatedSizeBytes = (report.Base64Data.Length * 3) / 4;
                    if (estimatedSizeBytes > MaxBase64FileSizeMB * 1024 * 1024)
                    {
                        await _unitOfWork.RollbackTransactionAsync();
                        return ApiResponseDto<QuoteRequestDto>.FailResponse(
                            $"'{report.FileName}' dosyası çok büyük. Maksimum {MaxBase64FileSizeMB}MB yüklenebilir.");
                    }

                    var expertReport = new ExpertReport
                    {
                        QuoteRequestId = quoteRequest.Id,
                        Name = report.FileName,
                        ContentType = report.ContentType,
                        Data = Convert.FromBase64String(report.Base64Data)
                    };

                    await _unitOfWork.Repository<ExpertReport>().AddAsync(expertReport);
                }

                await _unitOfWork.SaveChangesAsync();
            }

            await _unitOfWork.CommitTransactionAsync();

            // Admin'e yeni teklif bildirimi gönder
            try
            {
                var vehicleInfo = $"{quoteRequest.Year} {quoteRequest.Brand} {quoteRequest.Model} - {quoteRequest.Plate}";
                var customerName = $"{quoteRequest.FirstName} {quoteRequest.LastName}";
                await _emailService.SendNewQuoteNotificationToAdminAsync(
                    customerName, vehicleInfo, quoteRequest.Phone, quoteRequest.Email);
            }
            catch (Exception)
            {
                // E-posta hatası teklif oluşturmayı engellemesin
            }

            // Response DTO oluştur
            var responseDto = MapToDto(quoteRequest);
            return ApiResponseDto<QuoteRequestDto>.SuccessResponse(responseDto, "Teklif talebiniz başarıyla alındı.");
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return ApiResponseDto<QuoteRequestDto>.FailResponse($"Teklif oluşturulurken hata: {ex.Message}");
        }
    }

    /// <summary>
    /// Tüm teklif taleplerini getir (Admin için)
    /// </summary>
    public async Task<ApiResponseDto<List<QuoteRequestDto>>> GetAllQuotesAsync()
    {
        var quotes = await _unitOfWork.Repository<QuoteRequest>()
            .Query()
            .Include(q => q.ExpertReports)
            .Include(q => q.Media)
            .OrderByDescending(q => q.Date)
            .ToListAsync();

        var dtos = quotes.Select(MapToDto).ToList();
        return ApiResponseDto<List<QuoteRequestDto>>.SuccessResponse(dtos);
    }

    /// <summary>
    /// Kullanıcının teklif taleplerini getir (Email ile)
    /// </summary>
    public async Task<ApiResponseDto<List<QuoteRequestDto>>> GetQuotesByEmailAsync(string email)
    {
        var quotes = await _unitOfWork.Repository<QuoteRequest>()
            .Query()
            .Include(q => q.ExpertReports)
            .Include(q => q.Media)
            .Where(q => q.Email != null && q.Email.ToLower() == email.ToLower())
            .OrderByDescending(q => q.Date)
            .ToListAsync();

        var dtos = quotes.Select(MapToDto).ToList();
        return ApiResponseDto<List<QuoteRequestDto>>.SuccessResponse(dtos);
    }

    /// <summary>
    /// Teklif talebini ID ile getir
    /// </summary>
    public async Task<ApiResponseDto<QuoteRequestDto>> GetQuoteByIdAsync(Guid id)
    {
        var quote = await _unitOfWork.Repository<QuoteRequest>()
            .Query()
            .Include(q => q.ExpertReports)
            .Include(q => q.Media)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (quote == null)
            return ApiResponseDto<QuoteRequestDto>.FailResponse("Teklif talebi bulunamadı.");

        var dto = MapToDto(quote);
        return ApiResponseDto<QuoteRequestDto>.SuccessResponse(dto);
    }

    /// <summary>
    /// Teklif talebini okundu olarak işaretle
    /// </summary>
    public async Task<ApiResponseDto> MarkAsReadAsync(Guid id)
    {
        var quote = await _unitOfWork.Repository<QuoteRequest>().GetByIdAsync(id);

        if (quote == null)
            return ApiResponseDto.FailResponse("Teklif talebi bulunamadı.");

        quote.IsRead = true;
        _unitOfWork.Repository<QuoteRequest>().Update(quote);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto.SuccessResponse("Teklif okundu olarak işaretlendi.");
    }

    /// <summary>
    /// Teklif talebini sil
    /// </summary>
    public async Task<ApiResponseDto> DeleteQuoteAsync(Guid id)
    {
        var quote = await _unitOfWork.Repository<QuoteRequest>()
            .Query()
            .Include(q => q.ExpertReports)
            .Include(q => q.Media)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (quote == null)
            return ApiResponseDto.FailResponse("Teklif talebi bulunamadı.");

        // Video dosyalarını diskten sil
        foreach (var media in quote.Media.Where(m => m.MediaType == "Video" && !string.IsNullOrEmpty(m.FilePath)))
        {
            if (File.Exists(media.FilePath!))
                File.Delete(media.FilePath!);
        }

        // Quote klasörünü sil (varsa)
        var quoteMediaDir = Path.Combine(UploadBasePath, id.ToString());
        if (Directory.Exists(quoteMediaDir))
            Directory.Delete(quoteMediaDir, true);

        // Ekspertiz raporlarını sil (Cascade ile otomatik silinir ama explicit olsun)
        foreach (var report in quote.ExpertReports.ToList())
        {
            _unitOfWork.Repository<ExpertReport>().Delete(report);
        }

        foreach (var media in quote.Media.ToList())
        {
            _unitOfWork.Repository<QuoteMedia>().Delete(media);
        }

        _unitOfWork.Repository<QuoteRequest>().Delete(quote);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto.SuccessResponse("Teklif talebi silindi.");
    }

    /// <summary>
    /// Teklif talebine medya dosyaları yükle
    /// </summary>
    public async Task<ApiResponseDto> UploadMediaAsync(Guid quoteId, List<MediaFileUploadDto> files)
    {
        var quote = await _unitOfWork.Repository<QuoteRequest>().GetByIdAsync(quoteId);
        if (quote == null)
            return ApiResponseDto.FailResponse("Teklif talebi bulunamadı.");

        try
        {
            foreach (var file in files)
            {
                var media = new QuoteMedia
                {
                    QuoteRequestId = quoteId,
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    MediaType = file.MediaType,
                    FileSize = file.FileSize
                };

                if (file.MediaType == "Photo")
                {
                    // Fotoğrafları DB'de sakla
                    using var ms = new MemoryStream();
                    await file.FileStream.CopyToAsync(ms);
                    media.Data = ms.ToArray();
                }
                else
                {
                    // Videoları dosya sisteminde sakla
                    var quoteDir = Path.Combine(UploadBasePath, quoteId.ToString());
                    Directory.CreateDirectory(quoteDir);

                    var safeFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(quoteDir, safeFileName);

                    using var fs = new FileStream(filePath, FileMode.Create);
                    await file.FileStream.CopyToAsync(fs);

                    media.FilePath = filePath;
                }

                await _unitOfWork.Repository<QuoteMedia>().AddAsync(media);
            }

            await _unitOfWork.SaveChangesAsync();
            return ApiResponseDto.SuccessResponse("Medya dosyaları başarıyla yüklendi.");
        }
        catch (Exception ex)
        {
            return ApiResponseDto.FailResponse($"Medya yüklenirken hata: {ex.Message}");
        }
    }

    /// <summary>
    /// Medya dosyasını getir (download için)
    /// </summary>
    public async Task<(byte[]? Data, string FileName, string ContentType)?> GetMediaFileAsync(Guid mediaId)
    {
        var media = await _unitOfWork.Repository<QuoteMedia>().GetByIdAsync(mediaId);
        if (media == null) return null;

        byte[]? data;
        if (media.MediaType == "Photo")
        {
            data = media.Data;
        }
        else
        {
            if (string.IsNullOrEmpty(media.FilePath) || !File.Exists(media.FilePath))
                return null;

            data = await File.ReadAllBytesAsync(media.FilePath);
        }

        return (data, media.FileName, media.ContentType);
    }

    /// <summary>
    /// Admin teklif ver (fiyat aralığı belirle)
    /// </summary>
    public async Task<ApiResponseDto<QuoteRequestDto>> SubmitOfferAsync(Guid id, SubmitOfferDto dto)
    {
        if (dto.MinPrice > dto.MaxPrice)
            return ApiResponseDto<QuoteRequestDto>.FailResponse("Minimum fiyat, maksimum fiyattan büyük olamaz.");

        var quote = await _unitOfWork.Repository<QuoteRequest>()
            .Query()
            .Include(q => q.ExpertReports)
            .Include(q => q.Media)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (quote == null)
            return ApiResponseDto<QuoteRequestDto>.FailResponse("Teklif talebi bulunamadı.");

        quote.OfferMinPrice = dto.MinPrice;
        quote.OfferMaxPrice = dto.MaxPrice;
        quote.OfferDate = DateTime.UtcNow;
        quote.Status = QuoteStatus.OfferMade;
        quote.IsRead = true;
        quote.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Repository<QuoteRequest>().Update(quote);
        await _unitOfWork.SaveChangesAsync();

        // Müşteriye e-posta bildirimi gönder
        if (!string.IsNullOrEmpty(quote.Email))
        {
            try
            {
                var vehicleInfo = $"{quote.Year} {quote.Brand} {quote.Model} - {quote.Plate}";
                var customerName = $"{quote.FirstName} {quote.LastName}";
                await _emailService.SendOfferNotificationEmailAsync(
                    quote.Email, customerName, vehicleInfo,
                    dto.MinPrice, dto.MaxPrice);
            }
            catch (Exception ex)
            {
                // E-posta hatası teklif gönderimini engellemesin
                Console.WriteLine($"Email gönderme hatası: {ex.Message}");
            }
        }

        var responseDto = MapToDto(quote);
        return ApiResponseDto<QuoteRequestDto>.SuccessResponse(responseDto, "Teklif başarıyla gönderildi.");
    }

    /// <summary>
    /// Müşteri teklifi kabul/ret et
    /// </summary>
    public async Task<ApiResponseDto<QuoteRequestDto>> RespondToOfferAsync(Guid id, string customerEmail, RespondToOfferDto dto)
    {
        var quote = await _unitOfWork.Repository<QuoteRequest>()
            .Query()
            .Include(q => q.ExpertReports)
            .Include(q => q.Media)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (quote == null)
            return ApiResponseDto<QuoteRequestDto>.FailResponse("Teklif talebi bulunamadı.");

        // Teklif bu müşteriye mi ait kontrol et
        if (string.IsNullOrEmpty(quote.Email) || !quote.Email.Equals(customerEmail, StringComparison.OrdinalIgnoreCase))
            return ApiResponseDto<QuoteRequestDto>.FailResponse("Bu teklif size ait değil.");

        // Teklif verilmiş mi kontrol et
        if (quote.Status != QuoteStatus.OfferMade)
            return ApiResponseDto<QuoteRequestDto>.FailResponse("Bu teklif için henüz bir fiyat teklifi verilmemiş veya zaten yanıtlanmış.");

        quote.Status = dto.Accepted ? QuoteStatus.Accepted : QuoteStatus.Rejected;
        quote.ResponseDate = DateTime.UtcNow;
        quote.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Repository<QuoteRequest>().Update(quote);
        await _unitOfWork.SaveChangesAsync();

        // Admin'e kabul/red bildirimi gönder
        try
        {
            var vehicleInfo = $"{quote.Year} {quote.Brand} {quote.Model} - {quote.Plate}";
            var customerName = $"{quote.FirstName} {quote.LastName}";
            await _emailService.SendQuoteResponseNotificationToAdminAsync(
                customerName, vehicleInfo, dto.Accepted);
        }
        catch (Exception)
        {
            // E-posta hatası yanıt işlemini engellemesin
        }

        var statusText = dto.Accepted ? "kabul edildi" : "reddedildi";
        var responseDto = MapToDto(quote);
        return ApiResponseDto<QuoteRequestDto>.SuccessResponse(responseDto, $"Teklif {statusText}.");
    }

    /// <summary>
    /// Entity'yi DTO'ya map et
    /// </summary>
    private QuoteRequestDto MapToDto(QuoteRequest quote)
    {
        var dto = new QuoteRequestDto
        {
            Id = quote.Id,
            Date = quote.Date,
            IsRead = quote.IsRead,
            Plate = quote.Plate ?? string.Empty,
            Brand = quote.Brand ?? string.Empty,
            Model = quote.Model ?? string.Empty,
            Year = quote.Year,
            Km = quote.Km,
            Gear = quote.Gear,
            Fuel = quote.Fuel,
            Damage = quote.Damage,
            FirstName = quote.FirstName,
            LastName = quote.LastName,
            Phone = quote.Phone,
            Email = quote.Email,
            Status = quote.Status.ToString(),
            OfferMinPrice = quote.OfferMinPrice,
            OfferMaxPrice = quote.OfferMaxPrice,
            OfferDate = quote.OfferDate,
            ResponseDate = quote.ResponseDate,
            ExpertReports = quote.ExpertReports.Select(r => new DocumentDto
            {
                Id = r.Id,
                Name = r.Name,
                ContentType = r.ContentType ?? "application/octet-stream",
                DownloadUrl = $"/api/quotes/reports/{r.Id}/download"
            }).ToList(),
            Photos = quote.Media
                .Where(m => m.MediaType == "Photo")
                .Select(m => new MediaDocumentDto
                {
                    Id = m.Id,
                    FileName = m.FileName,
                    ContentType = m.ContentType,
                    MediaType = m.MediaType,
                    FileSize = m.FileSize,
                    DownloadUrl = $"/api/quotes/media/{m.Id}/download"
                }).ToList(),
            Videos = quote.Media
                .Where(m => m.MediaType == "Video")
                .Select(m => new MediaDocumentDto
                {
                    Id = m.Id,
                    FileName = m.FileName,
                    ContentType = m.ContentType,
                    MediaType = m.MediaType,
                    FileSize = m.FileSize,
                    DownloadUrl = $"/api/quotes/media/{m.Id}/download"
                }).ToList()
        };

        return dto;
    }
}
