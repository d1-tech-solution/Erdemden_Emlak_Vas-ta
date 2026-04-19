namespace EntityLayer.Entities;

public enum UserRole
{
    User,
    Admin
}

public enum ListingCategory
{
    RealEstate,
    Vehicle
}

public enum ListingStatus
{
    Satilik = 0,
    Satildi = 1,
    Opsiyonlu = 2,
    Pasif = 3,
    Kiralandi = 4
}

public enum BuyerReason
{
    Kendisi,
    Esi,
    Cocugu,
    Yatirimlik,
    SirketIcin,
    Diger
}

public enum RealEstateListingType
{
    Satilik,
    Kiralik
}

public enum QuoteStatus
{
    Pending,    // Beklemede
    OfferMade,  // Teklif Verildi
    Accepted,   // Kabul Edildi
    Rejected    // Reddedildi
}
