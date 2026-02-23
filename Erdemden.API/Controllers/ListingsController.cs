using BussinessLayer.Abstract;
using Core.DTOs.Common;
using Core.DTOs.ListingDtos;
using Core.DTOs.VehicleDtos;
using Core.DTOs.RealEstateDtos;
using DataAcessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Erdemden.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListingsController : ControllerBase
{
    private readonly IListingService _listingService;
    private readonly IUnitOfWork _unitOfWork;

    public ListingsController(IListingService listingService, IUnitOfWork unitOfWork)
    {
        _listingService = listingService;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Araç ilanı oluştur
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpPost("vehicle")]
    public async Task<IActionResult> CreateVehicleListing([FromBody] CreateVehicleListingRequest request)
    {
        var result = await _listingService.CreateVehicleListingAsync(request.Listing, request.Vehicle);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(GetListingById), new { id = result.Data!.Id }, result);
    }

    /// <summary>
    /// Emlak ilanı oluştur
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpPost("real-estate")]
    public async Task<IActionResult> CreateRealEstateListing([FromBody] CreateRealEstateListingRequest request)
    {
        var result = await _listingService.CreateRealEstateListingAsync(request.Listing, request.RealEstate);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(GetListingById), new { id = result.Data!.Id }, result);
    }

    /// <summary>
    /// İlanları listele (filtreleme + sayfalama)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetListings([FromQuery] ListingFilterDto filter)
    {
        var result = await _listingService.GetListingsAsync(filter);
        return Ok(result);
    }

    /// <summary>
    /// İlan detayı getir
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetListingById(Guid id)
    {
        var result = await _listingService.GetListingByIdAsync(id);

        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// İlan güncelle
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateListing(Guid id, [FromBody] UpdateListingDto updateDto)
    {
        var result = await _listingService.UpdateListingAsync(id, updateDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Araç bilgilerini güncelle
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id:guid}/vehicle")]
    public async Task<IActionResult> UpdateVehicle(Guid id, [FromBody] UpdateVehicleDto updateDto)
    {
        var result = await _listingService.UpdateVehicleAsync(id, updateDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Emlak bilgilerini güncelle
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id:guid}/real-estate")]
    public async Task<IActionResult> UpdateRealEstate(Guid id, [FromBody] UpdateRealEstateDto updateDto)
    {
        var result = await _listingService.UpdateRealEstateAsync(id, updateDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// İlan sil
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteListing(Guid id)
    {
        var result = await _listingService.DeleteListingAsync(id);

        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// İlanı pasife al
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpPatch("{id:guid}/set-passive")]
    public async Task<IActionResult> SetListingPassive(Guid id)
    {
        var result = await _listingService.SetListingPassiveAsync(id);

        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// İlanı aktif et
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpPatch("{id:guid}/set-active")]
    public async Task<IActionResult> SetListingActive(Guid id)
    {
        var result = await _listingService.SetListingActiveAsync(id);

        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Noter belgesi indir
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet("{id:guid}/notary-documents/{docId:guid}")]
    public async Task<IActionResult> DownloadNotaryDocument(Guid id, Guid docId)
    {
        var doc = await _unitOfWork.Repository<NotaryDocument>()
            .Query()
            .FirstOrDefaultAsync(d => d.Id == docId && d.ListingId == id);

        if (doc?.Data == null)
        {
            return NotFound(ApiResponseDto<object>.FailResponse("Belge bulunamadı"));
        }

        return new FileContentResult(doc.Data, doc.ContentType ?? "application/octet-stream")
        {
            FileDownloadName = doc.Name,
            EnableRangeProcessing = true
        };
    }

    /// <summary>
    /// Ekspertiz raporu indir (PDF)
    /// </summary>
    [HttpGet("{id:guid}/expertise-report")]
    public async Task<IActionResult> DownloadExpertiseReport(Guid id)
    {
        var listing = await _unitOfWork.Repository<Listing>()
            .Query()
            .FirstOrDefaultAsync(l => l.Id == id);

        if (listing?.ExpertiseReportData == null)
        {
            return NotFound(ApiResponseDto<object>.FailResponse("Ekspertiz raporu bulunamadı"));
        }

        return new FileContentResult(listing.ExpertiseReportData, listing.ExpertiseReportContentType ?? "application/pdf")
        {
            FileDownloadName = listing.ExpertiseReportFileName ?? "ekspertiz-raporu.pdf",
            EnableRangeProcessing = true
        };
    }
}

/// <summary>
/// Araç ilanı oluşturma isteği (Listing + Vehicle birlikte)
/// </summary>
public class CreateVehicleListingRequest
{
    public CreateListingDto Listing { get; set; } = null!;
    public CreateVehicleDto Vehicle { get; set; } = null!;
}

/// <summary>
/// Emlak ilanı oluşturma isteği (Listing + RealEstate birlikte)
/// </summary>
public class CreateRealEstateListingRequest
{
    public CreateListingDto Listing { get; set; } = null!;
    public CreateRealEstateDto RealEstate { get; set; } = null!;
}
