using Core.DTOs.Common;
using Core.DTOs.DocumentDtos;
using Core.DTOs.ImageDtos;
using Core.DTOs.VehicleDtos;
using Core.DTOs.RealEstateDtos;
using EntityLayer.Entities;

namespace Core.DTOs.ListingDtos;

/// <summary>
/// İlan detay yanıtı
/// </summary>
public class ListingDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public ListingCategory Category { get; set; }
    public ListingStatus Status { get; set; }
    public DateTime ListingDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Konum
    public LookupDto? City { get; set; }
    public LookupWithParentDto? District { get; set; }

    // Görseller
    public List<ImageDto> Images { get; set; } = new();

    // Araç veya Emlak detayları
    public VehicleDto? Vehicle { get; set; }
    public RealEstateDto? RealEstate { get; set; }

    // Ekspertiz Raporu
    public string? ExpertiseReportUrl { get; set; }

    // Satış bilgileri (Admin için)
    public ListingSaleInfoDto? SaleInfo { get; set; }

    // Noter belgeleri
    public List<DocumentDto> NotaryDocuments { get; set; } = new();
}
