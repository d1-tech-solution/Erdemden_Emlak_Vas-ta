using Core.DTOs.DocumentDtos;

namespace Core.DTOs.QuoteRequestDtos;

/// <summary>
/// Teklif talebi yanıtı (Liste görünümü)
/// </summary>
public class QuoteRequestDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsRead { get; set; }
    public string RequestType { get; set; } = "Vehicle";

    // Araç bilgileri
    public string Plate { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string? Year { get; set; }
    public string? Km { get; set; }
    public string? Gear { get; set; }
    public string? Fuel { get; set; }
    public string? Damage { get; set; }

    // Emlak bilgileri
    public int? RealEstateCategory { get; set; }
    public string? RealEstateCategoryText { get; set; }
    public int? RealEstateListingType { get; set; }
    public string? RealEstateListingTypeText { get; set; }
    public string? RealEstateTitle { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? Neighborhood { get; set; }
    public string? Address { get; set; }
    public int? Size { get; set; }
    public string? RoomCount { get; set; }
    public decimal? DesiredMinPrice { get; set; }
    public decimal? DesiredMaxPrice { get; set; }
    public string? Notes { get; set; }

    // İletişim bilgileri
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }

    // Admin teklif bilgisi
    public string Status { get; set; } = "Pending";
    public decimal? OfferMinPrice { get; set; }
    public decimal? OfferMaxPrice { get; set; }
    public DateTime? OfferDate { get; set; }
    public DateTime? ResponseDate { get; set; }

    // Ekspertiz raporları
    public List<DocumentDto> ExpertReports { get; set; } = new();

    // Medya dosyaları
    public List<MediaDocumentDto> Photos { get; set; } = new();
    public List<MediaDocumentDto> Videos { get; set; } = new();
}
