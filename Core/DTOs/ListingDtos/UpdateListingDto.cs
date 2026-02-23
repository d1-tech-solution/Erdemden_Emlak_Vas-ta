using System.ComponentModel.DataAnnotations;
using Core.DTOs.DocumentDtos;
using EntityLayer.Entities;

namespace Core.DTOs.ListingDtos;

/// <summary>
/// İlan güncelleme isteği (Satış bilgileri dahil)
/// </summary>
public class UpdateListingDto
{
    [StringLength(200, MinimumLength = 5, ErrorMessage = "Başlık 5-200 karakter arasında olmalıdır")]
    public string? Title { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır")]
    public decimal? Price { get; set; }

    [StringLength(10)]
    public string? Currency { get; set; }

    [StringLength(2000)]
    public string? Description { get; set; }

    [EnumDataType(typeof(ListingStatus), ErrorMessage = "Geçersiz durum değeri")]
    public ListingStatus? Status { get; set; }

    public Guid? CityId { get; set; }

    public Guid? DistrictId { get; set; }

    // Admin satış bilgileri
    [Range(0, (double)decimal.MaxValue, ErrorMessage = "Alış fiyatı 0'dan büyük olmalıdır")]
    public decimal? PurchasePrice { get; set; }
    [Range(0, (double)decimal.MaxValue, ErrorMessage = "Masraf 0'dan büyük olmalıdır")]
    public decimal? Expenses { get; set; }
    [Range(0, (double)decimal.MaxValue, ErrorMessage = "Satış fiyatı 0'dan büyük olmalıdır")]
    public decimal? SalePrice { get; set; }
    public DateTime? SoldDate { get; set; }

    // Alıcı bilgileri
    [StringLength(255)]
    public string? SoldTo { get; set; }
    [StringLength(20)]
    public string? SoldToPhone { get; set; }
    [StringLength(255)]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    public string? SoldToEmail { get; set; }
    [EnumDataType(typeof(BuyerReason), ErrorMessage = "Geçersiz alıcı nedeni değeri")]
    public BuyerReason? BuyerReason { get; set; }

    // Noter belgeleri
    public List<UploadDocumentDto>? NotaryDocuments { get; set; }
}
