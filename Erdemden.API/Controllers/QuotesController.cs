using System.IO.Compression;
using BussinessLayer.Abstract;
using Core.DTOs.QuoteRequestDtos;
using DataAcessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Erdemden.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuotesController : ControllerBase
{
    private readonly IQuoteService _quoteService;
    private readonly IUnitOfWork _unitOfWork;

    public QuotesController(IQuoteService quoteService, IUnitOfWork unitOfWork)
    {
        _quoteService = quoteService;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Yeni teklif talebi oluştur
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateQuote([FromBody] CreateQuoteWithFilesDto request)
    {
        var expertReports = request.ExpertReports?.Select(r => new FileUploadDto
        {
            FileName = r.Name,
            ContentType = r.Type,
            Base64Data = r.Data.Contains(",") ? r.Data.Split(',')[1] : r.Data // Base64 data URI formatını handle et
        }).ToList();

        // Login kullanıcı varsa quote'u user'a bağla
        Guid? userId = null;
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(userIdClaim, out var parsed))
        {
            userId = parsed;
        }

        var result = await _quoteService.CreateQuoteAsync(request.Quote, expertReports, userId);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(GetQuoteById), new { id = result.Data!.Id }, result);
    }

    /// <summary>
    /// Teklif talebine medya dosyaları yükle (fotoğraf/video)
    /// </summary>
    [HttpPost("{quoteId:guid}/media")]
    [RequestSizeLimit(500 * 1024 * 1024)] // 500MB
    public async Task<IActionResult> UploadMedia(Guid quoteId, [FromForm] List<IFormFile> files)
    {
        if (files == null || files.Count == 0)
            return BadRequest(new { success = false, message = "Dosya seçilmedi." });

        var allowedPhotoTypes = new[] { "image/jpeg", "image/png", "image/webp" };
        var allowedVideoTypes = new[] { "video/mp4", "video/quicktime", "video/webm" };
        const long maxPhotoSize = 10 * 1024 * 1024; // 10MB
        const long maxVideoSize = 100 * 1024 * 1024; // 100MB

        var mediaFiles = new List<MediaFileUploadDto>();

        foreach (var file in files)
        {
            var contentType = file.ContentType.ToLower();
            string mediaType;

            if (allowedPhotoTypes.Contains(contentType))
            {
                if (file.Length > maxPhotoSize)
                    return BadRequest(new { success = false, message = $"Fotoğraf '{file.FileName}' 10MB'dan büyük olamaz." });
                mediaType = "Photo";
            }
            else if (allowedVideoTypes.Contains(contentType))
            {
                if (file.Length > maxVideoSize)
                    return BadRequest(new { success = false, message = $"Video '{file.FileName}' 100MB'dan büyük olamaz." });
                mediaType = "Video";
            }
            else
            {
                return BadRequest(new { success = false, message = $"'{file.FileName}' desteklenmeyen dosya formatı. Fotoğraf: jpg, png, webp - Video: mp4, mov, webm" });
            }

            mediaFiles.Add(new MediaFileUploadDto
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                MediaType = mediaType,
                FileStream = file.OpenReadStream(),
                FileSize = file.Length
            });
        }

        // Fotoğraf ve video sayı limitleri
        var photoCount = mediaFiles.Count(f => f.MediaType == "Photo");
        var videoCount = mediaFiles.Count(f => f.MediaType == "Video");

        if (photoCount > 10)
            return BadRequest(new { success = false, message = "En fazla 10 fotoğraf yüklenebilir." });
        if (videoCount > 3)
            return BadRequest(new { success = false, message = "En fazla 3 video yüklenebilir." });

        var result = await _quoteService.UploadMediaAsync(quoteId, mediaFiles);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    /// <summary>
    /// Tüm teklif taleplerini getir (Sadece Admin)
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<IActionResult> GetAllQuotes()
    {
        var result = await _quoteService.GetAllQuotesAsync();
        return Ok(result);
    }

    /// <summary>
    /// Kullanıcının kendi teklif taleplerini getir (UserId + eski kayıtlar için email fallback)
    /// </summary>
    [Authorize]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyQuotes()
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return BadRequest(new { success = false, message = "Kullanıcı bilgisi bulunamadı." });
        }

        var result = await _quoteService.GetMyQuotesAsync(userId, email);
        return Ok(result);
    }

    /// <summary>
    /// Teklif talebini ID ile getir (Sadece Admin)
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetQuoteById(Guid id)
    {
        var result = await _quoteService.GetQuoteByIdAsync(id);

        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Teklif talebini okundu olarak işaretle (Sadece Admin)
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id:guid}/read")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        var result = await _quoteService.MarkAsReadAsync(id);

        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Teklif talebini sil (Sadece Admin)
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteQuote(Guid id)
    {
        var result = await _quoteService.DeleteQuoteAsync(id);

        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Teklif talebi için fiyat teklifi ver (Sadece Admin)
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id:guid}/offer")]
    public async Task<IActionResult> SubmitOffer(Guid id, [FromBody] SubmitOfferDto dto)
    {
        var result = await _quoteService.SubmitOfferAsync(id, dto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Müşteri teklif yanıtı (Kabul/Red)
    /// </summary>
    [Authorize]
    [HttpPut("{id:guid}/respond")]
    public async Task<IActionResult> RespondToOffer(Guid id, [FromBody] RespondToOfferDto dto)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return BadRequest(new { success = false, message = "Kullanıcı bilgisi bulunamadı." });
        }

        var result = await _quoteService.RespondToOfferAsync(id, userId, email, dto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Ekspertiz raporu indir
    /// </summary>
    [Authorize]
    [HttpGet("reports/{reportId:guid}/download")]
    public async Task<IActionResult> DownloadExpertReport(Guid reportId)
    {
        var report = await _unitOfWork.Repository<ExpertReport>().GetByIdAsync(reportId);

        if (report == null || report.Data == null)
        {
            return NotFound(new { success = false, message = "Rapor bulunamadı." });
        }

        // Tarayıcıda yeni sekmede aç (inline), indirme yapmasın
        return new FileContentResult(report.Data, report.ContentType ?? "application/pdf")
        {
            EnableRangeProcessing = true
        };
    }

    /// <summary>
    /// Medya dosyası indir (fotoğraf/video)
    /// </summary>
    [Authorize]
    [HttpGet("media/{mediaId:guid}/download")]
    public async Task<IActionResult> DownloadMedia(Guid mediaId)
    {
        var result = await _quoteService.GetMediaFileAsync(mediaId);

        if (result == null || result.Value.Data == null)
        {
            return NotFound(new { success = false, message = "Medya dosyası bulunamadı." });
        }

        var (data, fileName, contentType) = result.Value;
        // Tarayıcıda yeni sekmede aç (inline), indirme yapmasın
        return new FileContentResult(data!, contentType)
        {
            EnableRangeProcessing = true
        };
    }

    /// <summary>
    /// Teklif talebinin tüm dosyalarını ZIP olarak indir (Sadece Admin)
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet("{quoteId:guid}/download-all")]
    public async Task DownloadAllFiles(Guid quoteId)
    {
        var quote = await _unitOfWork.Repository<QuoteRequest>()
            .Query()
            .Include(q => q.ExpertReports)
            .Include(q => q.Media)
            .FirstOrDefaultAsync(q => q.Id == quoteId);

        if (quote == null)
        {
            HttpContext.Response.StatusCode = 404;
            HttpContext.Response.ContentType = "application/json";
            await HttpContext.Response.WriteAsJsonAsync(new { success = false, message = "Teklif talebi bulunamadı." });
            return;
        }

        var zipName = $"{(string.IsNullOrEmpty(quote.Plate) ? quote.Id.ToString()[..8] : quote.Plate)}_Dosyalar.zip";

        HttpContext.Response.ContentType = "application/zip";
        HttpContext.Response.Headers.Append("Content-Disposition", $"attachment; filename=\"{zipName}\"");

        // ZIP'i doğrudan response stream'e yaz - RAM'de tamponlamadan
        using var archive = new ZipArchive(HttpContext.Response.Body, ZipArchiveMode.Create, leaveOpen: true);

        // Ekspertiz raporları
        foreach (var report in quote.ExpertReports)
        {
            if (report.Data != null)
            {
                var entry = archive.CreateEntry($"Raporlar/{report.Name}");
                using var entryStream = entry.Open();
                await entryStream.WriteAsync(report.Data);
            }
        }

        // Fotoğraflar
        foreach (var media in quote.Media.Where(m => m.MediaType == "Photo"))
        {
            if (media.Data != null)
            {
                var entry = archive.CreateEntry($"Fotograflar/{media.FileName}");
                using var entryStream = entry.Open();
                await entryStream.WriteAsync(media.Data);
            }
        }

        // Videolar - dosya sisteminden stream et
        foreach (var media in quote.Media.Where(m => m.MediaType == "Video"))
        {
            if (!string.IsNullOrEmpty(media.FilePath) && System.IO.File.Exists(media.FilePath))
            {
                var entry = archive.CreateEntry($"Videolar/{media.FileName}");
                using var entryStream = entry.Open();
                using var fileStream = System.IO.File.OpenRead(media.FilePath);
                await fileStream.CopyToAsync(entryStream);
            }
        }
    }
}

/// <summary>
/// Teklif oluşturma isteği (dosyalarla birlikte)
/// </summary>
public class CreateQuoteWithFilesDto
{
    public CreateQuoteRequestDto Quote { get; set; } = new();
    public List<ExpertReportUploadDto>? ExpertReports { get; set; }
}

/// <summary>
/// Ekspertiz raporu yükleme DTO
/// </summary>
public class ExpertReportUploadDto
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty; // Base64 encoded
}
