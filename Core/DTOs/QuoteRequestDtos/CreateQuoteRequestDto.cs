using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.QuoteRequestDtos;

/// <summary>
/// Teklif talebi olusturma.
/// </summary>
public class CreateQuoteRequestDto
{
    public string RequestType { get; set; } = "Vehicle";

    // Arac bilgileri
    [StringLength(20)]
    public string? Plate { get; set; }

    [StringLength(100)]
    public string? Brand { get; set; }

    [StringLength(100)]
    public string? Model { get; set; }

    [StringLength(10)]
    public string? Year { get; set; }

    [StringLength(20)]
    public string? Km { get; set; }

    [StringLength(50)]
    public string? Gear { get; set; }

    [StringLength(50)]
    public string? Fuel { get; set; }

    public string? Damage { get; set; }

    // Emlak bilgileri
    public int? RealEstateCategory { get; set; }
    public int? RealEstateListingType { get; set; }

    [StringLength(200)]
    public string? RealEstateTitle { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    [StringLength(100)]
    public string? District { get; set; }

    [StringLength(100)]
    public string? Neighborhood { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    public int? Size { get; set; }

    [StringLength(50)]
    public string? RoomCount { get; set; }

    public int? BuildingAge { get; set; }

    [StringLength(50)]
    public string? OccupancyPermitStatus { get; set; }

    [StringLength(100)]
    public string? BusinessType { get; set; }

    [StringLength(50)]
    public string? LandZoningStatus { get; set; }

    public decimal? DesiredMinPrice { get; set; }
    public decimal? DesiredMaxPrice { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }

    // Iletisim bilgileri
    [Required(ErrorMessage = "Ad gereklidir")]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Soyad gereklidir")]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefon gereklidir")]
    [Phone(ErrorMessage = "Gecerli bir telefon numarasi giriniz")]
    public string Phone { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Gecerli bir e-posta adresi giriniz")]
    public string? Email { get; set; }
}
