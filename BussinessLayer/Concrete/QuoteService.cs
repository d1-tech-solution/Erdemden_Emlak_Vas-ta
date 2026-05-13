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
    private const string UploadBasePath = "uploads/quote-media";
    private const int MaxBase64FileSizeMB = 20; // Maksimum dosya boyutu (MB)

    public QuoteService(IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    /// <summary>
    /// Yeni teklif talebi oluştur
    /// </summary>
    public async Task<ApiResponseDto<QuoteRequestDto>> CreateQuoteAsync(CreateQuoteRequestDto dto, List<FileUploadDto>? expertReports = null, Guid? userId = null)
    {
        try
        {
            var requestType = ParseRequestType(dto.RequestType);
            var validationError = ValidateCreateQuote(dto, requestType);
            if (validationError != null)
                return ApiResponseDto<QuoteRequestDto>.FailResponse(validationError);

            await _unitOfWork.BeginTransactionAsync();

            // QuoteRequest oluştur
            var quoteRequest = new QuoteRequest
            {
                Date = DateTime.UtcNow,
                RequestType = requestType,
                UserId = userId,
                Plate = dto.Plate?.Trim(),
                Brand = dto.Brand?.Trim(),
                Model = dto.Model?.Trim(),
                Year = dto.Year,
                Km = dto.Km,
                Gear = dto.Gear,
                Fuel = dto.Fuel,
                Damage = dto.Damage,
                RealEstateCategory = dto.RealEstateCategory.HasValue ? (RealEstateCategory)dto.RealEstateCategory.Value : null,
                RealEstateListingType = dto.RealEstateListingType.HasValue ? (RealEstateListingType)dto.RealEstateListingType.Value : null,
                RealEstateTitle = dto.RealEstateTitle?.Trim(),
                City = dto.City?.Trim(),
                District = dto.District?.Trim(),
                Neighborhood = dto.Neighborhood?.Trim(),
                Address = dto.Address?.Trim(),
                Size = dto.Size,
                RoomCount = dto.RoomCount?.Trim(),
                DesiredMinPrice = dto.DesiredMinPrice,
                DesiredMaxPrice = dto.DesiredMaxPrice,
                Notes = dto.Notes?.Trim(),
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
                var vehicleInfo = GetQuoteInfo(quoteRequest);
                var customerName = $"{quoteRequest.FirstName} {quoteRequest.LastName}";
                await _emailService.SendNewQuoteNotificationToAdminAsync(
                    customerName, vehicleInfo, quoteRequest.Phone, quoteRequest.Email);

                // Müşteriye "teklif alındı" onay maili gönder
                if (!string.IsNullOrWhiteSpace(quoteRequest.Email))
                {
                    await _emailService.SendQuoteReceivedConfirmationEmailAsync(
                        quoteRequest.Email, customerName, vehicleInfo);
                }
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
    /// Kullanıcının teklif taleplerini getir (Email ile - eski kayıtlar için)
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
    /// Kullanıcının teklif taleplerini getir (UserId ile - yeni kayıtlar) + email fallback (eski kayıtlar için)
    /// </summary>
    public async Task<ApiResponseDto<List<QuoteRequestDto>>> GetMyQuotesAsync(Guid userId, string? email)
    {
        var normalizedEmail = email?.Trim().ToLower();
        var hasEmail = !string.IsNullOrEmpty(normalizedEmail);

        var quotes = await _unitOfWork.Repository<QuoteRequest>()
            .Query()
            .Include(q => q.ExpertReports)
            .Include(q => q.Media)
            .Where(q =>
                q.UserId == userId
                || (hasEmail && q.UserId == null && q.Email != null && q.Email.ToLower() == normalizedEmail))
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
        foreach (var media in quote.Media.Where(m => !string.IsNullOrEmpty(m.FilePath)))
        {
            var filePath = GetMediaPhysicalPath(media.FilePath!);
            if (File.Exists(filePath))
                File.Delete(filePath);
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

                if (file.MediaType == "Photo" && quote.RequestType == QuoteRequestType.Vehicle)
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

                    media.FilePath = ToRelativeMediaUrl(filePath);
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

        var data = media.Data;
        if (data == null)
        {
            if (string.IsNullOrEmpty(media.FilePath))
                return null;

            var filePath = GetMediaPhysicalPath(media.FilePath);
            if (!File.Exists(filePath))
                return null;

            data = await File.ReadAllBytesAsync(filePath);
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
                var vehicleInfo = GetQuoteInfo(quote);
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
    public async Task<ApiResponseDto<QuoteRequestDto>> RespondToOfferAsync(Guid id, Guid userId, string? customerEmail, RespondToOfferDto dto)
    {
        var quote = await _unitOfWork.Repository<QuoteRequest>()
            .Query()
            .Include(q => q.ExpertReports)
            .Include(q => q.Media)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (quote == null)
            return ApiResponseDto<QuoteRequestDto>.FailResponse("Teklif talebi bulunamadı.");

        // Teklif bu müşteriye mi ait kontrol et (UserId öncelikli, eski kayıtlar için email fallback)
        var ownsByUserId = quote.UserId.HasValue && quote.UserId.Value == userId;
        var ownsByEmail = !quote.UserId.HasValue
            && !string.IsNullOrEmpty(customerEmail)
            && !string.IsNullOrEmpty(quote.Email)
            && quote.Email.Equals(customerEmail, StringComparison.OrdinalIgnoreCase);

        if (!ownsByUserId && !ownsByEmail)
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
            var vehicleInfo = GetQuoteInfo(quote);
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
            RequestType = quote.RequestType.ToString(),
            Plate = quote.Plate ?? string.Empty,
            Brand = quote.Brand ?? string.Empty,
            Model = quote.Model ?? string.Empty,
            Year = quote.Year,
            Km = quote.Km,
            Gear = quote.Gear,
            Fuel = quote.Fuel,
            Damage = quote.Damage,
            RealEstateCategory = quote.RealEstateCategory.HasValue ? (int)quote.RealEstateCategory.Value : null,
            RealEstateCategoryText = quote.RealEstateCategory?.ToString(),
            RealEstateListingType = quote.RealEstateListingType.HasValue ? (int)quote.RealEstateListingType.Value : null,
            RealEstateListingTypeText = quote.RealEstateListingType?.ToString(),
            RealEstateTitle = quote.RealEstateTitle,
            City = quote.City,
            District = quote.District,
            Neighborhood = quote.Neighborhood,
            Address = quote.Address,
            Size = quote.Size,
            RoomCount = quote.RoomCount,
            DesiredMinPrice = quote.DesiredMinPrice,
            DesiredMaxPrice = quote.DesiredMaxPrice,
            Notes = quote.Notes,
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

    private static QuoteRequestType ParseRequestType(string? requestType)
    {
        return string.Equals(requestType, "RealEstate", StringComparison.OrdinalIgnoreCase)
            ? QuoteRequestType.RealEstate
            : QuoteRequestType.Vehicle;
    }

    private static string ToRelativeMediaUrl(string filePath)
    {
        return "/" + filePath.Replace("\\", "/").TrimStart('/');
    }

    private static string GetMediaPhysicalPath(string filePath)
    {
        if (Path.IsPathRooted(filePath) && !filePath.StartsWith('/'))
            return filePath;

        var relativePath = filePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString());
        return Path.Combine(Directory.GetCurrentDirectory(), relativePath);
    }

    private static string? ValidateCreateQuote(CreateQuoteRequestDto dto, QuoteRequestType requestType)
    {
        if (requestType == QuoteRequestType.Vehicle)
        {
            if (string.IsNullOrWhiteSpace(dto.Plate))
                return "Plaka gereklidir.";
            if (string.IsNullOrWhiteSpace(dto.Brand))
                return "Marka gereklidir.";
            if (string.IsNullOrWhiteSpace(dto.Model))
                return "Model gereklidir.";
            return null;
        }

        if (!dto.RealEstateCategory.HasValue || !Enum.IsDefined(typeof(RealEstateCategory), dto.RealEstateCategory.Value))
            return "Emlak kategorisi gereklidir.";
        if (!dto.RealEstateListingType.HasValue || !Enum.IsDefined(typeof(RealEstateListingType), dto.RealEstateListingType.Value))
            return "Satilik/kiralik bilgisi gereklidir.";
        if (!dto.DesiredMinPrice.HasValue || !dto.DesiredMaxPrice.HasValue || dto.DesiredMinPrice <= 0 || dto.DesiredMaxPrice <= 0)
            return "Istenen fiyat araligi gereklidir.";
        if (dto.DesiredMinPrice > dto.DesiredMaxPrice)
            return "Minimum fiyat, maksimum fiyattan buyuk olamaz.";

        return null;
    }

    private static string GetQuoteInfo(QuoteRequest quote)
    {
        if (quote.RequestType == QuoteRequestType.RealEstate)
        {
            var category = quote.RealEstateCategory?.ToString() ?? "Emlak";
            var listingType = quote.RealEstateListingType?.ToString();
            var location = string.Join(" / ", new[] { quote.Neighborhood, quote.District, quote.City }.Where(x => !string.IsNullOrWhiteSpace(x)));
            var title = string.IsNullOrWhiteSpace(quote.RealEstateTitle) ? category : quote.RealEstateTitle;
            var price = quote.DesiredMinPrice.HasValue && quote.DesiredMaxPrice.HasValue
                ? $"{quote.DesiredMinPrice.Value:N0} TL - {quote.DesiredMaxPrice.Value:N0} TL"
                : null;

            return string.Join(" - ", new[] { listingType, title, location, price }.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        return $"{quote.Year} {quote.Brand} {quote.Model} - {quote.Plate}".Trim();
    }
}
