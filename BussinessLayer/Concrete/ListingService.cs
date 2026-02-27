using BussinessLayer.Abstract;
using Core.DTOs.Common;
using Core.DTOs.DocumentDtos;
using Core.DTOs.ListingDtos;
using Core.DTOs.VehicleDtos;
using Core.DTOs.RealEstateDtos;
using Core.DTOs.ImageDtos;
using DataAcessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Concrete;

/// <summary>
/// İlan yönetimi servisi implementasyonu
/// </summary>
public class ListingService : IListingService
{
    private readonly IUnitOfWork _unitOfWork;

    public ListingService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Araç ilanı oluştur (Listing + Vehicle birlikte)
    /// </summary>
    public async Task<ApiResponseDto<ListingDto>> CreateVehicleListingAsync(CreateListingDto listingDto, CreateVehicleDto vehicleDto)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            // Listing oluştur
            var listing = new Listing
            {
                Title = listingDto.Title,
                Price = listingDto.Price,
                Currency = listingDto.Currency,
                Description = listingDto.Description,
                Category = ListingCategory.Vehicle,
                Status = listingDto.Status,
                CityId = listingDto.CityId,
                DistrictId = listingDto.DistrictId,
                PurchasePrice = listingDto.PurchasePrice,
                Expenses = listingDto.Expenses,
                ListingDate = DateTime.UtcNow
            };

            // Ekspertiz raporu (PDF)
            if (listingDto.ExpertiseReport != null && !string.IsNullOrEmpty(listingDto.ExpertiseReport.Base64Data))
            {
                var base64 = listingDto.ExpertiseReport.Base64Data;
                if (base64.Contains(","))
                    base64 = base64.Split(',')[1];

                listing.ExpertiseReportData = Convert.FromBase64String(base64);
                listing.ExpertiseReportContentType = "application/pdf";
                listing.ExpertiseReportFileName = listingDto.ExpertiseReport.FileName;
            }

            await _unitOfWork.Repository<Listing>().AddAsync(listing);
            await _unitOfWork.SaveChangesAsync();

            // Vehicle oluştur (Listing ID'yi kullanarak)
            var vehicle = new Vehicle
            {
                ListingId = listing.Id,
                Year = vehicleDto.Year,
                Km = vehicleDto.Km,
                EnginePower = vehicleDto.EnginePower,
                EngineDisplacement = vehicleDto.EngineDisplacement,
                Color = vehicleDto.Color,
                DamageStatus = vehicleDto.DamageStatus,
                TramerStatus = vehicleDto.TramerStatus,
                TramerAmount = vehicleDto.TramerAmount,
                VehicleTypeId = vehicleDto.VehicleTypeId,
                BrandId = vehicleDto.BrandId,
                ModelId = vehicleDto.ModelId,
                FuelTypeId = vehicleDto.FuelTypeId,
                TransmissionTypeId = vehicleDto.TransmissionTypeId,
                BodyTypeId = vehicleDto.BodyTypeId,
                PackageId = vehicleDto.PackageId,

                // ==================== GÜVENLİK ====================
                HasABS = vehicleDto.HasABS, HasESP = vehicleDto.HasESP, HasAirbag = vehicleDto.HasAirbag,
                HasRearCamera = vehicleDto.HasRearCamera, HasParkingSensor = vehicleDto.HasParkingSensor,
                HasLaneAssist = vehicleDto.HasLaneAssist, HasBlindSpotWarning = vehicleDto.HasBlindSpotWarning,
                HasCentralLock = vehicleDto.HasCentralLock, HasImmobilizer = vehicleDto.HasImmobilizer,
                HasIsofix = vehicleDto.HasIsofix, HasAEB = vehicleDto.HasAEB, HasBAS = vehicleDto.HasBAS,
                HasDistronic = vehicleDto.HasDistronic, HasNightVision = vehicleDto.HasNightVision,
                HasDriverAirbag = vehicleDto.HasDriverAirbag, HasPassengerAirbag = vehicleDto.HasPassengerAirbag,
                HasChildLock = vehicleDto.HasChildLock, HasHillAssist = vehicleDto.HasHillAssist,
                HasFatigueDetection = vehicleDto.HasFatigueDetection, HasArmoredVehicle = vehicleDto.HasArmoredVehicle,

                // ==================== İÇ DONANIM ====================
                HasAirConditioning = vehicleDto.HasAirConditioning, HasDigitalAC = vehicleDto.HasDigitalAC,
                HasLeatherSeats = vehicleDto.HasLeatherSeats, HasSeatHeating = vehicleDto.HasSeatHeating,
                HasElectricWindows = vehicleDto.HasElectricWindows, HasElectricMirrors = vehicleDto.HasElectricMirrors,
                HasSunroof = vehicleDto.HasSunroof, HasCruiseControl = vehicleDto.HasCruiseControl,
                HasSteeringWheelHeating = vehicleDto.HasSteeringWheelHeating, HasStartStop = vehicleDto.HasStartStop,
                HasAdaptiveCruiseControl = vehicleDto.HasAdaptiveCruiseControl, HasKeylessEntry = vehicleDto.HasKeylessEntry,
                HasFunctionalSteering = vehicleDto.HasFunctionalSteering, HasHeatedSteering = vehicleDto.HasHeatedSteering,
                HasHydraulicSteering = vehicleDto.HasHydraulicSteering, HasHeadUpDisplay = vehicleDto.HasHeadUpDisplay,
                HasSpeedLimiter = vehicleDto.HasSpeedLimiter, HasMemorySeats = vehicleDto.HasMemorySeats,
                HasSeatCooling = vehicleDto.HasSeatCooling, HasFabricSeats = vehicleDto.HasFabricSeats,
                HasElectricSeats = vehicleDto.HasElectricSeats, HasAutoDimmingMirror = vehicleDto.HasAutoDimmingMirror,
                HasFrontCamera = vehicleDto.HasFrontCamera, HasArmrest = vehicleDto.HasArmrest,
                HasCooledGlovebox = vehicleDto.HasCooledGlovebox, HasThirdRowSeats = vehicleDto.HasThirdRowSeats,
                HasTripComputer = vehicleDto.HasTripComputer,

                // ==================== DIŞ DONANIM ====================
                HasFootTrunkOpener = vehicleDto.HasFootTrunkOpener, HasHardtop = vehicleDto.HasHardtop,
                HasAdaptiveLights = vehicleDto.HasAdaptiveLights, HasElectricFoldMirrors = vehicleDto.HasElectricFoldMirrors,
                HasHeatedMirrors = vehicleDto.HasHeatedMirrors, HasMemoryMirrors = vehicleDto.HasMemoryMirrors,
                HasRearParkSensor = vehicleDto.HasRearParkSensor, HasFrontParkSensor = vehicleDto.HasFrontParkSensor,
                HasParkAssist = vehicleDto.HasParkAssist, HasSmartTrunk = vehicleDto.HasSmartTrunk,
                HasPanoramicRoof = vehicleDto.HasPanoramicRoof, HasTowBar = vehicleDto.HasTowBar,

                // ==================== MULTİMEDYA ====================
                HasBluetooth = vehicleDto.HasBluetooth, HasUSB = vehicleDto.HasUSB, HasAUX = vehicleDto.HasAUX,
                HasNavigation = vehicleDto.HasNavigation, HasTouchScreen = vehicleDto.HasTouchScreen,
                HasCarPlay = vehicleDto.HasCarPlay, HasRearEntertainment = vehicleDto.HasRearEntertainment,
                HasPremiumSound = vehicleDto.HasPremiumSound, HasAndroidAuto = vehicleDto.HasAndroidAuto
            };

            await _unitOfWork.Repository<Vehicle>().AddAsync(vehicle);
            await _unitOfWork.SaveChangesAsync();

            // Görselleri ekle
            if (listingDto.Images != null && listingDto.Images.Any())
            {
                var images = listingDto.Images.Select((img, index) => new ListingImage
                {
                    ListingId = listing.Id,
                    Base64Data = img.Base64Data,
                    FileName = img.FileName,
                    MimeType = GetMimeType(img.FileName),
                    IsCover = index == 0, // İlk görsel kapak
                    Order = img.Order ?? index
                }).ToList();

                foreach (var image in images)
                {
                    await _unitOfWork.Repository<ListingImage>().AddAsync(image);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            await _unitOfWork.CommitTransactionAsync();

            // Oluşturulan ilanı getir
            return await GetListingByIdAsync(listing.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            var errorMsg = ex.InnerException != null
                ? $"İlan oluşturulurken hata: {ex.InnerException.Message}"
                : $"İlan oluşturulurken hata: {ex.Message}";
            return ApiResponseDto<ListingDto>.FailResponse(errorMsg);
        }
    }

    /// <summary>
    /// Emlak ilanı oluştur (Listing + RealEstate birlikte)
    /// </summary>
    public async Task<ApiResponseDto<ListingDto>> CreateRealEstateListingAsync(CreateListingDto listingDto, CreateRealEstateDto realEstateDto)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            // Listing oluştur
            var listing = new Listing
            {
                Title = listingDto.Title,
                Price = listingDto.Price,
                Currency = listingDto.Currency,
                Description = listingDto.Description,
                Category = ListingCategory.RealEstate,
                Status = listingDto.Status,
                CityId = listingDto.CityId,
                DistrictId = listingDto.DistrictId,
                PurchasePrice = listingDto.PurchasePrice,
                Expenses = listingDto.Expenses,
                ListingDate = DateTime.UtcNow
            };

            await _unitOfWork.Repository<Listing>().AddAsync(listing);
            await _unitOfWork.SaveChangesAsync();

            // RealEstate oluştur (Listing ID'yi kullanarak)
            var realEstate = new RealEstate
            {
                ListingId = listing.Id,
                RoomCount = realEstateDto.RoomCount,
                Size = realEstateDto.Size,
                Floor = realEstateDto.Floor,
                TotalFloors = realEstateDto.TotalFloors,
                BuildingAge = realEstateDto.BuildingAge,
                HasElevator = realEstateDto.HasElevator,
                HasParking = realEstateDto.HasParking,
                IsFurnished = realEstateDto.IsFurnished,
                HousingTypeId = realEstateDto.HousingTypeId,
                ListingType = (RealEstateListingType)realEstateDto.ListingType,
                MonthlyRent = realEstateDto.MonthlyRent,
                Deposit = realEstateDto.Deposit,
                // İç Özellikler
                HasBalcony = realEstateDto.HasBalcony,
                HasTerrace = realEstateDto.HasTerrace,
                HasCellar = realEstateDto.HasCellar,
                HasStorageRoom = realEstateDto.HasStorageRoom,
                HasFireplace = realEstateDto.HasFireplace,
                HasAirConditioning = realEstateDto.HasAirConditioning,
                HasUnderfloorHeating = realEstateDto.HasUnderfloorHeating,
                HasBuiltInKitchen = realEstateDto.HasBuiltInKitchen,
                // Dış Özellikler
                HasGarden = realEstateDto.HasGarden,
                HasPool = realEstateDto.HasPool,
                HasCoveredParking = realEstateDto.HasCoveredParking,
                // Güvenlik
                HasSecurity = realEstateDto.HasSecurity,
                HasSteelDoor = realEstateDto.HasSteelDoor,
                HasVideoIntercom = realEstateDto.HasVideoIntercom,
                HasAlarm = realEstateDto.HasAlarm,
                // Altyapı
                HasSatellite = realEstateDto.HasSatellite,
                HasCableTv = realEstateDto.HasCableTv,
                HasInternet = realEstateDto.HasInternet,
                HasGenerator = realEstateDto.HasGenerator,
                HasNaturalGas = realEstateDto.HasNaturalGas
            };

            await _unitOfWork.
               Repository<RealEstate>().AddAsync(realEstate);
            await _unitOfWork.SaveChangesAsync();

            // Görselleri ekle
            if (listingDto.Images != null && listingDto.Images.Any())
            {
                var images = listingDto.Images.Select((img, index) => new ListingImage
                {
                    ListingId = listing.Id,
                    Base64Data = img.Base64Data,
                    FileName = img.FileName,
                    MimeType = GetMimeType(img.FileName),
                    IsCover = index == 0,
                    Order = img.Order ?? index
                }).ToList();

                foreach (var image in images)
                {
                    await _unitOfWork.Repository<ListingImage>().AddAsync(image);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            await _unitOfWork.CommitTransactionAsync();

            // Oluşturulan ilanı getir
            return await GetListingByIdAsync(listing.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return ApiResponseDto<ListingDto>.FailResponse($"Emlak ilanı oluşturulurken hata: {ex.Message}");
        }
    }

    /// <summary>
    /// İlan getir (ID ile)
    /// </summary>
    public async Task<ApiResponseDto<ListingDto>> GetListingByIdAsync(Guid id)
    {
        var listing = await _unitOfWork.Repository<Listing>()
            .Query()
            .Include(l => l.City)
            .Include(l => l.District)
            .Include(l => l.Images)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.VehicleType)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.Brand)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.Model)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.FuelType)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.TransmissionType)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.BodyType)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.Package)
            .Include(l => l.RealEstate)
                .ThenInclude(r => r!.HousingType)
            .Include(l => l.NotaryDocuments)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (listing == null)
        {
            return ApiResponseDto<ListingDto>.FailResponse("İlan bulunamadı");
        }

        return ApiResponseDto<ListingDto>.SuccessResponse(MapToListingDto(listing));
    }

    /// <summary>
    /// İlanları listele (filtreleme + sayfalama)
    /// </summary>
    public async Task<ApiResponseDto<PaginatedResponseDto<ListingDto>>> GetListingsAsync(ListingFilterDto filter)
    {
        var query = _unitOfWork.Repository<Listing>()
            .Query()
            .AsNoTracking()
            .Include(l => l.City)
            .Include(l => l.District)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.VehicleType)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.Brand)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.Model)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.FuelType)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.TransmissionType)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.BodyType)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.Package)
            .Include(l => l.RealEstate)
                .ThenInclude(r => r!.HousingType)
            .AsQueryable();

        // Filtreleme
        if (filter.Category.HasValue)
            query = query.Where(l => l.Category == filter.Category.Value);

        if (filter.Status.HasValue)
            query = query.Where(l => l.Status == filter.Status.Value);

        if (filter.MinPrice.HasValue)
            query = query.Where(l => l.Price >= filter.MinPrice.Value);

        if (filter.MaxPrice.HasValue)
            query = query.Where(l => l.Price <= filter.MaxPrice.Value);

        if (filter.CityId.HasValue)
            query = query.Where(l => l.CityId == filter.CityId.Value);

        if (filter.DistrictId.HasValue)
            query = query.Where(l => l.DistrictId == filter.DistrictId.Value);

        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            query = query.Where(l => l.Title.Contains(filter.SearchTerm) ||
                                     (l.Description != null && l.Description.Contains(filter.SearchTerm)));

        if (filter.FromDate.HasValue)
            query = query.Where(l => l.ListingDate >= filter.FromDate.Value);

        if (filter.ToDate.HasValue)
            query = query.Where(l => l.ListingDate <= filter.ToDate.Value);

        // ==================== Araç Filtreleri ====================
        if (filter.VehicleTypeId.HasValue)
            query = query.Where(l => l.Vehicle != null && l.Vehicle.VehicleTypeId == filter.VehicleTypeId.Value);

        if (filter.BrandId.HasValue)
            query = query.Where(l => l.Vehicle != null && l.Vehicle.BrandId == filter.BrandId.Value);

        if (filter.ModelId.HasValue)
            query = query.Where(l => l.Vehicle != null && l.Vehicle.ModelId == filter.ModelId.Value);

        if (filter.BodyTypeId.HasValue)
            query = query.Where(l => l.Vehicle != null && l.Vehicle.BodyTypeId == filter.BodyTypeId.Value);

        if (filter.PackageId.HasValue)
            query = query.Where(l => l.Vehicle != null && l.Vehicle.PackageId == filter.PackageId.Value);

        if (filter.FuelTypeId.HasValue)
            query = query.Where(l => l.Vehicle != null && l.Vehicle.FuelTypeId == filter.FuelTypeId.Value);

        if (filter.TransmissionTypeId.HasValue)
            query = query.Where(l => l.Vehicle != null && l.Vehicle.TransmissionTypeId == filter.TransmissionTypeId.Value);

        if (filter.MinYear.HasValue)
            query = query.Where(l => l.Vehicle != null && l.Vehicle.Year >= filter.MinYear.Value);

        if (filter.MaxYear.HasValue)
            query = query.Where(l => l.Vehicle != null && l.Vehicle.Year <= filter.MaxYear.Value);

        if (filter.MinKm.HasValue)
            query = query.Where(l => l.Vehicle != null && l.Vehicle.Km >= filter.MinKm.Value);

        if (filter.MaxKm.HasValue)
            query = query.Where(l => l.Vehicle != null && l.Vehicle.Km <= filter.MaxKm.Value);

        // ==================== Emlak Filtreleri ====================
        if (filter.HousingTypeId.HasValue)
            query = query.Where(l => l.RealEstate != null && l.RealEstate.HousingTypeId == filter.HousingTypeId.Value);

        if (!string.IsNullOrWhiteSpace(filter.RoomCount))
            query = query.Where(l => l.RealEstate != null && l.RealEstate.RoomCount == filter.RoomCount);

        if (filter.MinSize.HasValue)
            query = query.Where(l => l.RealEstate != null && l.RealEstate.Size >= filter.MinSize.Value);

        if (filter.MaxSize.HasValue)
            query = query.Where(l => l.RealEstate != null && l.RealEstate.Size <= filter.MaxSize.Value);

        // Toplam kayıt sayısı
        var totalCount = await query.CountAsync();

        // ==================== Sıralama ====================
        query = filter.SortBy switch
        {
            "price_asc" => query.OrderBy(l => l.Price),
            "price_desc" => query.OrderByDescending(l => l.Price),
            "oldest" => query.OrderBy(l => l.CreatedAt),
            _ => query.OrderByDescending(l => l.CreatedAt) // newest default
        };

        // Sayfalama
        var listings = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        // Sadece kapak resmi ID'lerini çek (base64 HARİÇ - performans için)
        var listingIds = listings.Select(l => l.Id).ToList();
        var coverImages = await _unitOfWork.Repository<ListingImage>()
            .Query()
            .AsNoTracking()
            .Where(i => listingIds.Contains(i.ListingId) && i.IsCover)
            .Select(i => new { i.Id, i.ListingId, i.Order })
            .ToListAsync();

        var listingDtos = listings.Select(l =>
        {
            var dto = MapToListingDto(l);
            // Liste görünümünde sadece kapak resmini URL olarak ata
            var cover = coverImages.FirstOrDefault(c => c.ListingId == l.Id);
            if (cover != null)
            {
                dto.Images = new List<Core.DTOs.ImageDtos.ImageDto>
                {
                    new() { Id = cover.Id, ImageUrl = $"/api/listings/{l.Id}/images/{cover.Id}", IsCover = true, Order = cover.Order }
                };
            }
            return dto;
        }).ToList();

        var result = new PaginatedResponseDto<ListingDto>
        {
            Items = listingDtos,
            TotalCount = totalCount,
            Page = filter.PageNumber,
            PageSize = filter.PageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
        };

        return ApiResponseDto<PaginatedResponseDto<ListingDto>>.SuccessResponse(result);
    }

    /// <summary>
    /// İlan güncelle
    /// </summary>
    public async Task<ApiResponseDto<ListingDto>> UpdateListingAsync(Guid id, UpdateListingDto updateDto)
    {
        var listing = await _unitOfWork.Repository<Listing>().GetByIdAsync(id);

        if (listing == null)
        {
            return ApiResponseDto<ListingDto>.FailResponse("İlan bulunamadı");
        }

        // Güncelleme
        if (updateDto.Title != null) listing.Title = updateDto.Title;
        if (updateDto.Price.HasValue) listing.Price = updateDto.Price.Value;
        if (updateDto.Currency != null) listing.Currency = updateDto.Currency;
        if (updateDto.Description != null) listing.Description = updateDto.Description;
        if (updateDto.Status.HasValue) listing.Status = updateDto.Status.Value;
        if (updateDto.ListingDate.HasValue) listing.ListingDate = updateDto.ListingDate.Value;
        if (updateDto.CityId.HasValue) listing.CityId = updateDto.CityId.Value;
        if (updateDto.DistrictId.HasValue) listing.DistrictId = updateDto.DistrictId;

        // Satış bilgileri
        if (updateDto.PurchasePrice.HasValue) listing.PurchasePrice = updateDto.PurchasePrice;
        if (updateDto.Expenses.HasValue) listing.Expenses = updateDto.Expenses;
        if (updateDto.SalePrice.HasValue) listing.SalePrice = updateDto.SalePrice;
        if (updateDto.SoldDate.HasValue) listing.SoldDate = updateDto.SoldDate;
        if (updateDto.SoldTo != null) listing.SoldTo = updateDto.SoldTo;
        if (updateDto.SoldToPhone != null) listing.SoldToPhone = updateDto.SoldToPhone;
        if (updateDto.SoldToEmail != null) listing.SoldToEmail = updateDto.SoldToEmail;
        if (updateDto.BuyerReason.HasValue) listing.BuyerReason = updateDto.BuyerReason;

        _unitOfWork.Repository<Listing>().Update(listing);

        // Görselleri güncelle
        if (updateDto.Images != null)
        {
            var existingImages = await _unitOfWork.Repository<ListingImage>()
                .Query().Where(i => i.ListingId == id).ToListAsync();

            // Korunacak mevcut resim ID'lerini belirle
            var keepImageIds = updateDto.Images
                .Where(i => i.ExistingImageId.HasValue)
                .Select(i => i.ExistingImageId!.Value)
                .ToHashSet();

            // Korunmayacak mevcut resimleri sil
            foreach (var img in existingImages.Where(i => !keepImageIds.Contains(i.Id)))
                _unitOfWork.Repository<ListingImage>().Delete(img);

            // Korunan resimlerin order ve isCover değerlerini güncelle
            foreach (var uploadImg in updateDto.Images.Where(i => i.ExistingImageId.HasValue))
            {
                var existing = existingImages.FirstOrDefault(i => i.Id == uploadImg.ExistingImageId!.Value);
                if (existing != null)
                {
                    var idx = updateDto.Images.IndexOf(uploadImg);
                    existing.Order = uploadImg.Order ?? idx;
                    existing.IsCover = idx == 0;
                    _unitOfWork.Repository<ListingImage>().Update(existing);
                }
            }

            // Yeni resimleri ekle (base64 olanlar)
            foreach (var item in updateDto.Images.Select((img, index) => new { img, index }))
            {
                if (!item.img.ExistingImageId.HasValue && !string.IsNullOrEmpty(item.img.Base64Data))
                {
                    var newImage = new ListingImage
                    {
                        ListingId = id,
                        Base64Data = item.img.Base64Data,
                        FileName = item.img.FileName ?? $"image_{item.index + 1}.jpg",
                        MimeType = GetMimeType(item.img.FileName ?? "image.jpg"),
                        IsCover = item.index == 0 && !keepImageIds.Any(),
                        Order = item.img.Order ?? item.index
                    };
                    await _unitOfWork.Repository<ListingImage>().AddAsync(newImage);
                }
            }
        }

        // Noter belgeleri güncelle
        if (updateDto.NotaryDocuments != null)
        {
            var existingDocs = await _unitOfWork.Repository<NotaryDocument>()
                .Query().Where(d => d.ListingId == id).ToListAsync();
            foreach (var doc in existingDocs)
                _unitOfWork.Repository<NotaryDocument>().Delete(doc);

            foreach (var docDto in updateDto.NotaryDocuments)
            {
                var base64 = docDto.Base64Data.Contains(",")
                    ? docDto.Base64Data.Split(',')[1]
                    : docDto.Base64Data;
                await _unitOfWork.Repository<NotaryDocument>().AddAsync(new NotaryDocument
                {
                    ListingId = id,
                    Name = docDto.FileName,
                    ContentType = docDto.ContentType,
                    Data = Convert.FromBase64String(base64)
                });
            }
        }

        await _unitOfWork.SaveChangesAsync();

        return await GetListingByIdAsync(id);
    }

    /// <summary>
    /// Araç bilgilerini güncelle
    /// </summary>
    public async Task<ApiResponseDto<VehicleDto>> UpdateVehicleAsync(Guid listingId, UpdateVehicleDto updateDto)
    {
        var vehicle = await _unitOfWork.Repository<Vehicle>()
            .Query()
            .Include(v => v.VehicleType)
            .Include(v => v.Brand)
            .Include(v => v.Model)
            .Include(v => v.FuelType)
            .Include(v => v.TransmissionType)
            .Include(v => v.BodyType)
            .Include(v => v.Package)
            .FirstOrDefaultAsync(v => v.ListingId == listingId);

        if (vehicle == null)
        {
            return ApiResponseDto<VehicleDto>.FailResponse("Araç bulunamadı");
        }

        // Güncelleme
        if (updateDto.Year.HasValue) vehicle.Year = updateDto.Year.Value;
        if (updateDto.Km.HasValue) vehicle.Km = updateDto.Km.Value;
        if (updateDto.EnginePower.HasValue) vehicle.EnginePower = updateDto.EnginePower.Value;
        if (updateDto.EngineDisplacement.HasValue) vehicle.EngineDisplacement = updateDto.EngineDisplacement.Value;
        if (updateDto.Color != null) vehicle.Color = updateDto.Color;
        if (updateDto.DamageStatus != null) vehicle.DamageStatus = updateDto.DamageStatus;
        if (updateDto.TramerStatus.HasValue) vehicle.TramerStatus = updateDto.TramerStatus.Value;
        if (updateDto.TramerAmount.HasValue) vehicle.TramerAmount = updateDto.TramerAmount;
        if (updateDto.VehicleTypeId.HasValue) vehicle.VehicleTypeId = updateDto.VehicleTypeId.Value;
        if (updateDto.BrandId.HasValue) vehicle.BrandId = updateDto.BrandId.Value;
        if (updateDto.ModelId.HasValue) vehicle.ModelId = updateDto.ModelId.Value;
        if (updateDto.FuelTypeId.HasValue) vehicle.FuelTypeId = updateDto.FuelTypeId.Value;
        if (updateDto.TransmissionTypeId.HasValue) vehicle.TransmissionTypeId = updateDto.TransmissionTypeId.Value;
        if (updateDto.BodyTypeId.HasValue) vehicle.BodyTypeId = updateDto.BodyTypeId;
        if (updateDto.PackageId.HasValue) vehicle.PackageId = updateDto.PackageId;

        // ==================== GÜVENLİK ====================
        if (updateDto.HasABS.HasValue) vehicle.HasABS = updateDto.HasABS.Value;
        if (updateDto.HasESP.HasValue) vehicle.HasESP = updateDto.HasESP.Value;
        if (updateDto.HasAirbag.HasValue) vehicle.HasAirbag = updateDto.HasAirbag.Value;
        if (updateDto.HasRearCamera.HasValue) vehicle.HasRearCamera = updateDto.HasRearCamera.Value;
        if (updateDto.HasParkingSensor.HasValue) vehicle.HasParkingSensor = updateDto.HasParkingSensor.Value;
        if (updateDto.HasLaneAssist.HasValue) vehicle.HasLaneAssist = updateDto.HasLaneAssist.Value;
        if (updateDto.HasBlindSpotWarning.HasValue) vehicle.HasBlindSpotWarning = updateDto.HasBlindSpotWarning.Value;
        if (updateDto.HasCentralLock.HasValue) vehicle.HasCentralLock = updateDto.HasCentralLock.Value;
        if (updateDto.HasImmobilizer.HasValue) vehicle.HasImmobilizer = updateDto.HasImmobilizer.Value;
        if (updateDto.HasIsofix.HasValue) vehicle.HasIsofix = updateDto.HasIsofix.Value;
        if (updateDto.HasAEB.HasValue) vehicle.HasAEB = updateDto.HasAEB.Value;
        if (updateDto.HasBAS.HasValue) vehicle.HasBAS = updateDto.HasBAS.Value;
        if (updateDto.HasDistronic.HasValue) vehicle.HasDistronic = updateDto.HasDistronic.Value;
        if (updateDto.HasNightVision.HasValue) vehicle.HasNightVision = updateDto.HasNightVision.Value;
        if (updateDto.HasDriverAirbag.HasValue) vehicle.HasDriverAirbag = updateDto.HasDriverAirbag.Value;
        if (updateDto.HasPassengerAirbag.HasValue) vehicle.HasPassengerAirbag = updateDto.HasPassengerAirbag.Value;
        if (updateDto.HasChildLock.HasValue) vehicle.HasChildLock = updateDto.HasChildLock.Value;
        if (updateDto.HasHillAssist.HasValue) vehicle.HasHillAssist = updateDto.HasHillAssist.Value;
        if (updateDto.HasFatigueDetection.HasValue) vehicle.HasFatigueDetection = updateDto.HasFatigueDetection.Value;
        if (updateDto.HasArmoredVehicle.HasValue) vehicle.HasArmoredVehicle = updateDto.HasArmoredVehicle.Value;

        // ==================== İÇ DONANIM ====================
        if (updateDto.HasAirConditioning.HasValue) vehicle.HasAirConditioning = updateDto.HasAirConditioning.Value;
        if (updateDto.HasDigitalAC.HasValue) vehicle.HasDigitalAC = updateDto.HasDigitalAC.Value;
        if (updateDto.HasLeatherSeats.HasValue) vehicle.HasLeatherSeats = updateDto.HasLeatherSeats.Value;
        if (updateDto.HasSeatHeating.HasValue) vehicle.HasSeatHeating = updateDto.HasSeatHeating.Value;
        if (updateDto.HasElectricWindows.HasValue) vehicle.HasElectricWindows = updateDto.HasElectricWindows.Value;
        if (updateDto.HasElectricMirrors.HasValue) vehicle.HasElectricMirrors = updateDto.HasElectricMirrors.Value;
        if (updateDto.HasSunroof.HasValue) vehicle.HasSunroof = updateDto.HasSunroof.Value;
        if (updateDto.HasCruiseControl.HasValue) vehicle.HasCruiseControl = updateDto.HasCruiseControl.Value;
        if (updateDto.HasSteeringWheelHeating.HasValue) vehicle.HasSteeringWheelHeating = updateDto.HasSteeringWheelHeating.Value;
        if (updateDto.HasStartStop.HasValue) vehicle.HasStartStop = updateDto.HasStartStop.Value;
        if (updateDto.HasAdaptiveCruiseControl.HasValue) vehicle.HasAdaptiveCruiseControl = updateDto.HasAdaptiveCruiseControl.Value;
        if (updateDto.HasKeylessEntry.HasValue) vehicle.HasKeylessEntry = updateDto.HasKeylessEntry.Value;
        if (updateDto.HasFunctionalSteering.HasValue) vehicle.HasFunctionalSteering = updateDto.HasFunctionalSteering.Value;
        if (updateDto.HasHeatedSteering.HasValue) vehicle.HasHeatedSteering = updateDto.HasHeatedSteering.Value;
        if (updateDto.HasHydraulicSteering.HasValue) vehicle.HasHydraulicSteering = updateDto.HasHydraulicSteering.Value;
        if (updateDto.HasHeadUpDisplay.HasValue) vehicle.HasHeadUpDisplay = updateDto.HasHeadUpDisplay.Value;
        if (updateDto.HasSpeedLimiter.HasValue) vehicle.HasSpeedLimiter = updateDto.HasSpeedLimiter.Value;
        if (updateDto.HasMemorySeats.HasValue) vehicle.HasMemorySeats = updateDto.HasMemorySeats.Value;
        if (updateDto.HasSeatCooling.HasValue) vehicle.HasSeatCooling = updateDto.HasSeatCooling.Value;
        if (updateDto.HasFabricSeats.HasValue) vehicle.HasFabricSeats = updateDto.HasFabricSeats.Value;
        if (updateDto.HasElectricSeats.HasValue) vehicle.HasElectricSeats = updateDto.HasElectricSeats.Value;
        if (updateDto.HasAutoDimmingMirror.HasValue) vehicle.HasAutoDimmingMirror = updateDto.HasAutoDimmingMirror.Value;
        if (updateDto.HasFrontCamera.HasValue) vehicle.HasFrontCamera = updateDto.HasFrontCamera.Value;
        if (updateDto.HasArmrest.HasValue) vehicle.HasArmrest = updateDto.HasArmrest.Value;
        if (updateDto.HasCooledGlovebox.HasValue) vehicle.HasCooledGlovebox = updateDto.HasCooledGlovebox.Value;
        if (updateDto.HasThirdRowSeats.HasValue) vehicle.HasThirdRowSeats = updateDto.HasThirdRowSeats.Value;
        if (updateDto.HasTripComputer.HasValue) vehicle.HasTripComputer = updateDto.HasTripComputer.Value;

        // ==================== DIŞ DONANIM ====================
        if (updateDto.HasFootTrunkOpener.HasValue) vehicle.HasFootTrunkOpener = updateDto.HasFootTrunkOpener.Value;
        if (updateDto.HasHardtop.HasValue) vehicle.HasHardtop = updateDto.HasHardtop.Value;
        if (updateDto.HasAdaptiveLights.HasValue) vehicle.HasAdaptiveLights = updateDto.HasAdaptiveLights.Value;
        if (updateDto.HasElectricFoldMirrors.HasValue) vehicle.HasElectricFoldMirrors = updateDto.HasElectricFoldMirrors.Value;
        if (updateDto.HasHeatedMirrors.HasValue) vehicle.HasHeatedMirrors = updateDto.HasHeatedMirrors.Value;
        if (updateDto.HasMemoryMirrors.HasValue) vehicle.HasMemoryMirrors = updateDto.HasMemoryMirrors.Value;
        if (updateDto.HasRearParkSensor.HasValue) vehicle.HasRearParkSensor = updateDto.HasRearParkSensor.Value;
        if (updateDto.HasFrontParkSensor.HasValue) vehicle.HasFrontParkSensor = updateDto.HasFrontParkSensor.Value;
        if (updateDto.HasParkAssist.HasValue) vehicle.HasParkAssist = updateDto.HasParkAssist.Value;
        if (updateDto.HasSmartTrunk.HasValue) vehicle.HasSmartTrunk = updateDto.HasSmartTrunk.Value;
        if (updateDto.HasPanoramicRoof.HasValue) vehicle.HasPanoramicRoof = updateDto.HasPanoramicRoof.Value;
        if (updateDto.HasTowBar.HasValue) vehicle.HasTowBar = updateDto.HasTowBar.Value;

        // ==================== MULTİMEDYA ====================
        if (updateDto.HasBluetooth.HasValue) vehicle.HasBluetooth = updateDto.HasBluetooth.Value;
        if (updateDto.HasUSB.HasValue) vehicle.HasUSB = updateDto.HasUSB.Value;
        if (updateDto.HasAUX.HasValue) vehicle.HasAUX = updateDto.HasAUX.Value;
        if (updateDto.HasNavigation.HasValue) vehicle.HasNavigation = updateDto.HasNavigation.Value;
        if (updateDto.HasTouchScreen.HasValue) vehicle.HasTouchScreen = updateDto.HasTouchScreen.Value;
        if (updateDto.HasCarPlay.HasValue) vehicle.HasCarPlay = updateDto.HasCarPlay.Value;
        if (updateDto.HasRearEntertainment.HasValue) vehicle.HasRearEntertainment = updateDto.HasRearEntertainment.Value;
        if (updateDto.HasPremiumSound.HasValue) vehicle.HasPremiumSound = updateDto.HasPremiumSound.Value;
        if (updateDto.HasAndroidAuto.HasValue) vehicle.HasAndroidAuto = updateDto.HasAndroidAuto.Value;

        _unitOfWork.Repository<Vehicle>().Update(vehicle);
        await _unitOfWork.SaveChangesAsync();

        // Güncel vehicle'ı tekrar getir
        vehicle = await _unitOfWork.Repository<Vehicle>()
            .Query()
            .Include(v => v.VehicleType)
            .Include(v => v.Brand)
            .Include(v => v.Model)
            .Include(v => v.FuelType)
            .Include(v => v.TransmissionType)
            .Include(v => v.BodyType)
            .Include(v => v.Package)
            .FirstOrDefaultAsync(v => v.ListingId == listingId);

        return ApiResponseDto<VehicleDto>.SuccessResponse(MapToVehicleDto(vehicle!));
    }

    /// <summary>
    /// Emlak bilgilerini güncelle
    /// </summary>
    public async Task<ApiResponseDto<RealEstateDto>> UpdateRealEstateAsync(Guid listingId, UpdateRealEstateDto updateDto)
    {
        var realEstate = await _unitOfWork.Repository<RealEstate>()
            .Query()
            .Include(r => r.HousingType)
            .FirstOrDefaultAsync(r => r.ListingId == listingId);

        if (realEstate == null)
        {
            return ApiResponseDto<RealEstateDto>.FailResponse("Emlak bulunamadı");
        }

        // Güncelleme
        if (updateDto.RoomCount != null) realEstate.RoomCount = updateDto.RoomCount;
        if (updateDto.Size.HasValue) realEstate.Size = updateDto.Size.Value;
        if (updateDto.Floor.HasValue) realEstate.Floor = updateDto.Floor;
        if (updateDto.TotalFloors.HasValue) realEstate.TotalFloors = updateDto.TotalFloors;
        if (updateDto.BuildingAge.HasValue) realEstate.BuildingAge = updateDto.BuildingAge;
        if (updateDto.HasElevator.HasValue) realEstate.HasElevator = updateDto.HasElevator.Value;
        if (updateDto.HasParking.HasValue) realEstate.HasParking = updateDto.HasParking.Value;
        if (updateDto.IsFurnished.HasValue) realEstate.IsFurnished = updateDto.IsFurnished.Value;
        if (updateDto.HousingTypeId.HasValue) realEstate.HousingTypeId = updateDto.HousingTypeId.Value;
        if (updateDto.ListingType.HasValue) realEstate.ListingType = (RealEstateListingType)updateDto.ListingType.Value;
        if (updateDto.MonthlyRent.HasValue) realEstate.MonthlyRent = updateDto.MonthlyRent;
        if (updateDto.Deposit.HasValue) realEstate.Deposit = updateDto.Deposit;
        // İç Özellikler
        if (updateDto.HasBalcony.HasValue) realEstate.HasBalcony = updateDto.HasBalcony.Value;
        if (updateDto.HasTerrace.HasValue) realEstate.HasTerrace = updateDto.HasTerrace.Value;
        if (updateDto.HasCellar.HasValue) realEstate.HasCellar = updateDto.HasCellar.Value;
        if (updateDto.HasStorageRoom.HasValue) realEstate.HasStorageRoom = updateDto.HasStorageRoom.Value;
        if (updateDto.HasFireplace.HasValue) realEstate.HasFireplace = updateDto.HasFireplace.Value;
        if (updateDto.HasAirConditioning.HasValue) realEstate.HasAirConditioning = updateDto.HasAirConditioning.Value;
        if (updateDto.HasUnderfloorHeating.HasValue) realEstate.HasUnderfloorHeating = updateDto.HasUnderfloorHeating.Value;
        if (updateDto.HasBuiltInKitchen.HasValue) realEstate.HasBuiltInKitchen = updateDto.HasBuiltInKitchen.Value;
        // Dış Özellikler
        if (updateDto.HasGarden.HasValue) realEstate.HasGarden = updateDto.HasGarden.Value;
        if (updateDto.HasPool.HasValue) realEstate.HasPool = updateDto.HasPool.Value;
        if (updateDto.HasCoveredParking.HasValue) realEstate.HasCoveredParking = updateDto.HasCoveredParking.Value;
        // Güvenlik
        if (updateDto.HasSecurity.HasValue) realEstate.HasSecurity = updateDto.HasSecurity.Value;
        if (updateDto.HasSteelDoor.HasValue) realEstate.HasSteelDoor = updateDto.HasSteelDoor.Value;
        if (updateDto.HasVideoIntercom.HasValue) realEstate.HasVideoIntercom = updateDto.HasVideoIntercom.Value;
        if (updateDto.HasAlarm.HasValue) realEstate.HasAlarm = updateDto.HasAlarm.Value;
        // Altyapı
        if (updateDto.HasSatellite.HasValue) realEstate.HasSatellite = updateDto.HasSatellite.Value;
        if (updateDto.HasCableTv.HasValue) realEstate.HasCableTv = updateDto.HasCableTv.Value;
        if (updateDto.HasInternet.HasValue) realEstate.HasInternet = updateDto.HasInternet.Value;
        if (updateDto.HasGenerator.HasValue) realEstate.HasGenerator = updateDto.HasGenerator.Value;
        if (updateDto.HasNaturalGas.HasValue) realEstate.HasNaturalGas = updateDto.HasNaturalGas.Value;

        _unitOfWork.Repository<RealEstate>().Update(realEstate);
        await _unitOfWork.SaveChangesAsync();

        // Güncel realEstate'i tekrar getir
        realEstate = await _unitOfWork.Repository<RealEstate>()
            .Query()
            .Include(r => r.HousingType)
            .FirstOrDefaultAsync(r => r.ListingId == listingId);

        return ApiResponseDto<RealEstateDto>.SuccessResponse(MapToRealEstateDto(realEstate!));
    }

    /// <summary>
    /// İlan sil
    /// </summary>
    public async Task<ApiResponseDto> DeleteListingAsync(Guid id)
    {
        var listing = await _unitOfWork.Repository<Listing>()
            .Query()
            .Include(l => l.Vehicle)
            .Include(l => l.RealEstate)
            .Include(l => l.Images)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (listing == null)
        {
            return ApiResponseDto.FailResponse("İlan bulunamadı");
        }

        _unitOfWork.Repository<Listing>().Delete(listing);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto.SuccessResponse("İlan başarıyla silindi");
    }

    /// <summary>
    /// İlanı pasife al
    /// </summary>
    public async Task<ApiResponseDto> SetListingPassiveAsync(Guid id)
    {
        var listing = await _unitOfWork.Repository<Listing>().GetByIdAsync(id);

        if (listing == null)
        {
            return ApiResponseDto.FailResponse("İlan bulunamadı");
        }

        listing.Status = ListingStatus.Pasif;
        _unitOfWork.Repository<Listing>().Update(listing);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto.SuccessResponse("İlan pasife alındı");
    }

    /// <summary>
    /// İlanı aktif et
    /// </summary>
    public async Task<ApiResponseDto> SetListingActiveAsync(Guid id)
    {
        var listing = await _unitOfWork.Repository<Listing>().GetByIdAsync(id);

        if (listing == null)
        {
            return ApiResponseDto.FailResponse("İlan bulunamadı");
        }

        listing.Status = ListingStatus.Satilik;
        _unitOfWork.Repository<Listing>().Update(listing);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto.SuccessResponse("İlan aktif edildi");
    }

    #region Private Methods

    private static ListingDto MapToListingDto(Listing listing)
    {
        // Kapak görseli veya ilk görseli al
        var coverImage = listing.Images?.FirstOrDefault(i => i.IsCover) ?? listing.Images?.FirstOrDefault();
        var coverImageUrl = coverImage != null ? $"/api/listings/{listing.Id}/images/{coverImage.Id}" : null;

        return new ListingDto
        {
            Id = listing.Id,
            Title = listing.Title,
            Price = listing.Price,
            Currency = listing.Currency,
            ImageUrl = coverImageUrl,
            Description = listing.Description,
            Category = listing.Category,
            Status = listing.Status,
            ListingDate = listing.ListingDate,
            CreatedAt = listing.CreatedAt,
            UpdatedAt = listing.UpdatedAt,
            City = listing.City != null ? new LookupDto { Id = listing.City.Id, Name = listing.City.Name } : null,
            District = listing.District != null ? new LookupWithParentDto
            {
                Id = listing.District.Id,
                Name = listing.District.Name,
                ParentId = listing.District.CityId,
                ParentName = listing.City?.Name ?? string.Empty
            } : null,
            Images = listing.Images?.OrderBy(i => i.Order).Select(i => new ImageDto
            {
                Id = i.Id,
                ImageUrl = $"/api/listings/{listing.Id}/images/{i.Id}",
                IsCover = i.IsCover,
                Order = i.Order
            }).ToList() ?? new List<ImageDto>(),
            ExpertiseReportUrl = listing.ExpertiseReportData != null
                ? $"/api/listings/{listing.Id}/expertise-report"
                : null,
            Vehicle = listing.Vehicle != null ? MapToVehicleDto(listing.Vehicle) : null,
            RealEstate = listing.RealEstate != null ? MapToRealEstateDto(listing.RealEstate) : null,
            SaleInfo = new ListingSaleInfoDto
            {
                PurchasePrice = listing.PurchasePrice,
                Expenses = listing.Expenses,
                SalePrice = listing.SalePrice,
                SoldDate = listing.SoldDate,
                SoldTo = listing.SoldTo,
                SoldToPhone = listing.SoldToPhone,
                SoldToEmail = listing.SoldToEmail,
                BuyerReason = listing.BuyerReason
            },
            NotaryDocuments = listing.NotaryDocuments?.Select(d => new DocumentDto
            {
                Id = d.Id,
                Name = d.Name,
                ContentType = d.ContentType ?? string.Empty,
                DownloadUrl = $"/api/listings/{listing.Id}/notary-documents/{d.Id}"
            }).ToList() ?? new List<DocumentDto>()
        };
    }

    private static VehicleDto MapToVehicleDto(Vehicle vehicle)
    {
        return new VehicleDto
        {
            Year = vehicle.Year ?? 0,
            Km = vehicle.Km ?? 0,
            EnginePower = vehicle.EnginePower,
            EngineDisplacement = vehicle.EngineDisplacement,
            Color = vehicle.Color,
            DamageStatus = vehicle.DamageStatus,
            TramerStatus = vehicle.TramerStatus,
            TramerAmount = vehicle.TramerAmount,
            VehicleType = new LookupDto { Id = vehicle.VehicleType.Id, Name = vehicle.VehicleType.Name },
            Brand = new LookupDto { Id = vehicle.Brand.Id, Name = vehicle.Brand.Name },
            Model = new LookupWithParentDto
            {
                Id = vehicle.Model.Id,
                Name = vehicle.Model.Name,
                ParentId = vehicle.Model.BrandId,
                ParentName = vehicle.Brand?.Name ?? string.Empty
            },
            FuelType = new LookupDto { Id = vehicle.FuelType.Id, Name = vehicle.FuelType.Name },
            TransmissionType = new LookupDto { Id = vehicle.TransmissionType.Id, Name = vehicle.TransmissionType.Name },
            BodyType = vehicle.BodyType != null ? new LookupWithParentDto
            {
                Id = vehicle.BodyType.Id,
                Name = vehicle.BodyType.Name,
                ParentId = vehicle.BodyType.VehicleTypeId,
                ParentName = vehicle.VehicleType?.Name ?? string.Empty
            } : null,
            Package = vehicle.Package != null ? new LookupWithParentDto
            {
                Id = vehicle.Package.Id,
                Name = vehicle.Package.Name,
                ParentId = vehicle.Package.ModelId,
                ParentName = vehicle.Model?.Name ?? string.Empty
            } : null,

            // ==================== GÜVENLİK ====================
            HasABS = vehicle.HasABS, HasESP = vehicle.HasESP, HasAirbag = vehicle.HasAirbag,
            HasRearCamera = vehicle.HasRearCamera, HasParkingSensor = vehicle.HasParkingSensor,
            HasLaneAssist = vehicle.HasLaneAssist, HasBlindSpotWarning = vehicle.HasBlindSpotWarning,
            HasCentralLock = vehicle.HasCentralLock, HasImmobilizer = vehicle.HasImmobilizer,
            HasIsofix = vehicle.HasIsofix, HasAEB = vehicle.HasAEB, HasBAS = vehicle.HasBAS,
            HasDistronic = vehicle.HasDistronic, HasNightVision = vehicle.HasNightVision,
            HasDriverAirbag = vehicle.HasDriverAirbag, HasPassengerAirbag = vehicle.HasPassengerAirbag,
            HasChildLock = vehicle.HasChildLock, HasHillAssist = vehicle.HasHillAssist,
            HasFatigueDetection = vehicle.HasFatigueDetection, HasArmoredVehicle = vehicle.HasArmoredVehicle,

            // ==================== İÇ DONANIM ====================
            HasAirConditioning = vehicle.HasAirConditioning, HasDigitalAC = vehicle.HasDigitalAC,
            HasLeatherSeats = vehicle.HasLeatherSeats, HasSeatHeating = vehicle.HasSeatHeating,
            HasElectricWindows = vehicle.HasElectricWindows, HasElectricMirrors = vehicle.HasElectricMirrors,
            HasSunroof = vehicle.HasSunroof, HasCruiseControl = vehicle.HasCruiseControl,
            HasSteeringWheelHeating = vehicle.HasSteeringWheelHeating, HasStartStop = vehicle.HasStartStop,
            HasAdaptiveCruiseControl = vehicle.HasAdaptiveCruiseControl, HasKeylessEntry = vehicle.HasKeylessEntry,
            HasFunctionalSteering = vehicle.HasFunctionalSteering, HasHeatedSteering = vehicle.HasHeatedSteering,
            HasHydraulicSteering = vehicle.HasHydraulicSteering, HasHeadUpDisplay = vehicle.HasHeadUpDisplay,
            HasSpeedLimiter = vehicle.HasSpeedLimiter, HasMemorySeats = vehicle.HasMemorySeats,
            HasSeatCooling = vehicle.HasSeatCooling, HasFabricSeats = vehicle.HasFabricSeats,
            HasElectricSeats = vehicle.HasElectricSeats, HasAutoDimmingMirror = vehicle.HasAutoDimmingMirror,
            HasFrontCamera = vehicle.HasFrontCamera, HasArmrest = vehicle.HasArmrest,
            HasCooledGlovebox = vehicle.HasCooledGlovebox, HasThirdRowSeats = vehicle.HasThirdRowSeats,
            HasTripComputer = vehicle.HasTripComputer,

            // ==================== DIŞ DONANIM ====================
            HasFootTrunkOpener = vehicle.HasFootTrunkOpener, HasHardtop = vehicle.HasHardtop,
            HasAdaptiveLights = vehicle.HasAdaptiveLights, HasElectricFoldMirrors = vehicle.HasElectricFoldMirrors,
            HasHeatedMirrors = vehicle.HasHeatedMirrors, HasMemoryMirrors = vehicle.HasMemoryMirrors,
            HasRearParkSensor = vehicle.HasRearParkSensor, HasFrontParkSensor = vehicle.HasFrontParkSensor,
            HasParkAssist = vehicle.HasParkAssist, HasSmartTrunk = vehicle.HasSmartTrunk,
            HasPanoramicRoof = vehicle.HasPanoramicRoof, HasTowBar = vehicle.HasTowBar,

            // ==================== MULTİMEDYA ====================
            HasBluetooth = vehicle.HasBluetooth, HasUSB = vehicle.HasUSB, HasAUX = vehicle.HasAUX,
            HasNavigation = vehicle.HasNavigation, HasTouchScreen = vehicle.HasTouchScreen,
            HasCarPlay = vehicle.HasCarPlay, HasRearEntertainment = vehicle.HasRearEntertainment,
            HasPremiumSound = vehicle.HasPremiumSound, HasAndroidAuto = vehicle.HasAndroidAuto
        };
    }

    private static RealEstateDto MapToRealEstateDto(RealEstate realEstate)
    {
        return new RealEstateDto
        {
            RoomCount = realEstate.RoomCount ?? string.Empty,
            Size = realEstate.Size ?? 0,
            Floor = realEstate.Floor,
            TotalFloors = realEstate.TotalFloors,
            BuildingAge = realEstate.BuildingAge,
            HasElevator = realEstate.HasElevator ?? false,
            HasParking = realEstate.HasParking ?? false,
            IsFurnished = realEstate.IsFurnished ?? false,
            HousingType = new LookupDto { Id = realEstate.HousingType.Id, Name = realEstate.HousingType.Name },
            ListingType = (int)realEstate.ListingType,
            MonthlyRent = realEstate.MonthlyRent,
            Deposit = realEstate.Deposit,
            // İç Özellikler
            HasBalcony = realEstate.HasBalcony ?? false,
            HasTerrace = realEstate.HasTerrace ?? false,
            HasCellar = realEstate.HasCellar ?? false,
            HasStorageRoom = realEstate.HasStorageRoom ?? false,
            HasFireplace = realEstate.HasFireplace ?? false,
            HasAirConditioning = realEstate.HasAirConditioning ?? false,
            HasUnderfloorHeating = realEstate.HasUnderfloorHeating ?? false,
            HasBuiltInKitchen = realEstate.HasBuiltInKitchen ?? false,
            // Dış Özellikler
            HasGarden = realEstate.HasGarden ?? false,
            HasPool = realEstate.HasPool ?? false,
            HasCoveredParking = realEstate.HasCoveredParking ?? false,
            // Güvenlik
            HasSecurity = realEstate.HasSecurity ?? false,
            HasSteelDoor = realEstate.HasSteelDoor ?? false,
            HasVideoIntercom = realEstate.HasVideoIntercom ?? false,
            HasAlarm = realEstate.HasAlarm ?? false,
            // Altyapı
            HasSatellite = realEstate.HasSatellite ?? false,
            HasCableTv = realEstate.HasCableTv ?? false,
            HasInternet = realEstate.HasInternet ?? false,
            HasGenerator = realEstate.HasGenerator ?? false,
            HasNaturalGas = realEstate.HasNaturalGas ?? false
        };
    }

    /// <summary>
    /// Dosya uzantısından MIME tipini belirle
    /// </summary>
    private static string GetMimeType(string fileName)
    {
        var extension = Path.GetExtension(fileName)?.ToLowerInvariant();
        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            ".bmp" => "image/bmp",
            _ => "image/jpeg" // Varsayılan
        };
    }

    #endregion
}
