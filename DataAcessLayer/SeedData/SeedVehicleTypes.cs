using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.SeedData
{
    public static class SeedVehicleTypes
    {
        private static readonly Dictionary<string, string[]> VehicleTypeBodyMap = new()
        {
            { "Otomobil", new[] { "Sedan", "Hatchback", "Station Wagon", "Coupe", "Cabrio", "Roadster" } },
            { "SUV & Arazi Araçları", new[] { "SUV", "Pickup", "Crossover", "Arazi Aracı" } },
            { "Motosiklet", new[] { "Scooter", "Maxi Scooter", "Naked", "Sport", "Touring", "Enduro / Adventure", "Chopper / Cruiser", "Cross / Motocross", "Cafe Racer / Scrambler", "Underbone / Cub", "Elektrikli", "Supermoto" } },
            { "Minivan & Panelvan", new[] { "Minivan", "Panelvan" } },
            { "Ticari Araçlar", new[] { "Minibüs & Midibüs", "Otobüs", "Kamyon & Kamyonet", "Çekici", "Dorse", "Römork", "Karoser & Üst Yapı", "Oto Kurtarıcı & Taşıyıcı", "Ticari Hat & Ticari Plaka" } }
        };

        public static async Task SeedAsync(Context context)
        {
            if (await context.Set<VehicleType>().AnyAsync())
                return;

            foreach (var vehicleTypeData in VehicleTypeBodyMap)
            {
                var vehicleType = new VehicleType
                {
                    Id = Guid.NewGuid(),
                    Name = vehicleTypeData.Key,
                    CreatedAt = DateTime.UtcNow
                };

                context.Set<VehicleType>().Add(vehicleType);

                foreach (var bodyTypeName in vehicleTypeData.Value)
                {
                    context.Set<BodyType>().Add(new BodyType
                    {
                        Id = Guid.NewGuid(),
                        VehicleTypeId = vehicleType.Id,
                        Name = bodyTypeName,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Eksik BodyType kayıtlarını oluşturur (VehicleTypes zaten varsa SeedAsync atlar, bu metot eksikleri tamamlar)
        /// </summary>
        public static async Task EnsureBodyTypesAsync(Context context)
        {
            var vehicleTypes = await context.Set<VehicleType>().ToListAsync();
            var existingBodyTypes = await context.Set<BodyType>().ToListAsync();
            var added = false;

            foreach (var entry in VehicleTypeBodyMap)
            {
                var vehicleType = vehicleTypes.FirstOrDefault(vt => vt.Name == entry.Key);
                if (vehicleType == null)
                {
                    vehicleType = new VehicleType
                    {
                        Id = Guid.NewGuid(),
                        Name = entry.Key,
                        CreatedAt = DateTime.UtcNow
                    };

                    context.Set<VehicleType>().Add(vehicleType);
                    vehicleTypes.Add(vehicleType);
                    added = true;
                }

                foreach (var bodyTypeName in entry.Value)
                {
                    var exists = existingBodyTypes.Any(bt => bt.Name == bodyTypeName && bt.VehicleTypeId == vehicleType.Id);
                    if (exists)
                        continue;

                    var bodyType = new BodyType
                    {
                        Id = Guid.NewGuid(),
                        VehicleTypeId = vehicleType.Id,
                        Name = bodyTypeName,
                        CreatedAt = DateTime.UtcNow
                    };

                    context.Set<BodyType>().Add(bodyType);
                    existingBodyTypes.Add(bodyType);
                    added = true;
                }
            }

            if (added)
                await context.SaveChangesAsync();
        }

        /// <summary>
        /// Eski "SUV & Arazi Araçları > Minivan & Panelvan" gövde tipini kullanan Vehicle ve Model kayıtlarını
        /// yeni "Minivan & Panelvan > Minivan" gövde tipine taşır, ardından eski kaydı siler.
        /// Idempotent — eski kayıt yoksa hiçbir şey yapmaz.
        /// </summary>
        public static async Task MigrateLegacyMinivanPanelvanAsync(Context context)
        {
            var legacyBodyType = await context.Set<BodyType>()
                .FirstOrDefaultAsync(bt => bt.Name == "Minivan & Panelvan");

            if (legacyBodyType == null)
                return;

            var minivanPanelvanVehicleType = await context.Set<VehicleType>()
                .FirstOrDefaultAsync(vt => vt.Name == "Minivan & Panelvan");
            if (minivanPanelvanVehicleType == null)
                return;

            var targetBodyType = await context.Set<BodyType>()
                .FirstOrDefaultAsync(bt => bt.VehicleTypeId == minivanPanelvanVehicleType.Id && bt.Name == "Minivan");
            if (targetBodyType == null)
                return;

            // Vehicle kayıtlarını yeni body type'a taşı + VehicleTypeId güncelle
            var affectedVehicles = await context.Set<Vehicle>()
                .Where(v => v.BodyTypeId == legacyBodyType.Id)
                .ToListAsync();
            foreach (var vehicle in affectedVehicles)
            {
                vehicle.BodyTypeId = targetBodyType.Id;
                vehicle.VehicleTypeId = minivanPanelvanVehicleType.Id;
            }

            // Model kayıtlarını da yeni body type'a taşı (FK_Models_BodyTypes_BodyTypeId yüzünden silmeden önce şart)
            var affectedModels = await context.Set<Model>()
                .Where(m => m.BodyTypeId == legacyBodyType.Id)
                .ToListAsync();
            foreach (var model in affectedModels)
            {
                model.BodyTypeId = targetBodyType.Id;
            }

            await context.SaveChangesAsync();

            context.Set<BodyType>().Remove(legacyBodyType);
            await context.SaveChangesAsync();
        }
    }
}
