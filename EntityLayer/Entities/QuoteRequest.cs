using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities;

public class QuoteRequest : BaseEntity
{
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public QuoteRequestType RequestType { get; set; } = QuoteRequestType.Vehicle;

    // Teklifi gönderen kullanıcı (login kullanıcıysa). Anonim/eski kayıtlarda null olabilir.
    public Guid? UserId { get; set; }
    public virtual User? User { get; set; }

    // Araç bilgisi
    [MaxLength(20)]
    public string? Plate { get; set; }

    [MaxLength(100)]
    public string? Brand { get; set; }

    [MaxLength(100)]
    public string? Model { get; set; }

    [MaxLength(10)]
    public string? Year { get; set; }

    [MaxLength(20)]
    public string? Km { get; set; }

    [MaxLength(50)]
    public string? Gear { get; set; }

    [MaxLength(50)]
    public string? Fuel { get; set; }

    [MaxLength(500)]
    public string? Damage { get; set; }

    public RealEstateCategory? RealEstateCategory { get; set; }
    public RealEstateListingType? RealEstateListingType { get; set; }

    [MaxLength(200)]
    public string? RealEstateTitle { get; set; }

    [MaxLength(100)]
    public string? City { get; set; }

    [MaxLength(100)]
    public string? District { get; set; }

    [MaxLength(100)]
    public string? Neighborhood { get; set; }

    [MaxLength(500)]
    public string? Address { get; set; }

    public int? Size { get; set; }

    [MaxLength(50)]
    public string? RoomCount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? DesiredMinPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? DesiredMaxPrice { get; set; }

    [MaxLength(1000)]
    public string? Notes { get; set; }

    // Müşteri bilgisi
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Phone { get; set; } = null!;

    [MaxLength(255)]
    public string? Email { get; set; }

    public bool IsRead { get; set; } = false;

    // Admin teklif bilgisi
    public QuoteStatus Status { get; set; } = QuoteStatus.Pending;

    [Column(TypeName = "decimal(18,2)")]
    public decimal? OfferMinPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? OfferMaxPrice { get; set; }

    public DateTime? OfferDate { get; set; }
    public DateTime? ResponseDate { get; set; }

    public virtual ICollection<ExpertReport> ExpertReports { get; set; } = new List<ExpertReport>();
    public virtual ICollection<QuoteMedia> Media { get; set; } = new List<QuoteMedia>();
}
