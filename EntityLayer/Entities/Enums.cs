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

public enum QuoteRequestType
{
    Vehicle = 0,
    RealEstate = 1
}

public enum RealEstateCategory
{
    Konut = 0,
    IsYeri = 1,
    Arsa = 2
}

public enum HeatingType
{
    Yok = 0,
    Soba = 1,
    Dogalgaz = 2,
    Klima = 3,
    Merkezi = 4,
    Kombi = 5,
    YerdenIsitma = 6,
    VRV = 7
}

public enum BuildingCondition
{
    Sifir = 0,
    IkinciEl = 1,
    InsaatHalinde = 2
}

public enum UsageStatus
{
    Bos = 0,
    Kiracili = 1,
    MulkSahibi = 2
}

public enum BuildingType
{
    ApartmanIci = 0,
    MustakilBina = 1,
    Plaza = 2,
    IsHani = 3,
    AVM = 4
}

public enum ZoningStatus
{
    Konut = 0,
    Ticari = 1,
    Sanayi = 2,
    Turizm = 3,
    Karma = 4,
    Tarim = 5,
    Imarsiz = 6,
    Belirtilmemis = 7
}

public enum DeedStatus
{
    Mustakil = 0,
    Hisseli = 1,
    KatIrtifakli = 2,
    KatMulkiyetli = 3,
    TahsisBelgeli = 4,
    Belirsiz = 5
}
