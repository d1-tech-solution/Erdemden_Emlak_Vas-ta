using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAcessLayer.SeedData
{
    public static class SeedDatabase
    {
        public static async Task InitializeAsync(Context context)
        {
            // Lookup verilerini seed et (sıralama önemli - VehicleTypes önce olmalı çünkü BodyTypes'a bağlı)
            await SeedVehicleTypes.SeedAsync(context);
            await SeedVehicleTypes.EnsureBodyTypesAsync(context); // Eksik BodyType kayıtlarını oluştur
            await SeedVehicleTypes.MigrateLegacyMinivanPanelvanAsync(context); // Eski "Minivan & Panelvan" -> "Ticari Araçlar > Minivan"
            await SeedBrands.SeedAsync(context);
            await SeedFuelTypes.SeedAsync(context);
            await SeedTransmissionTypes.SeedAsync(context);
            await SeedCities.SeedAsync(context);
            await SeedCities.AddMissingDistrictsAndNeighborhoodsAsync(context);
            await SeedHousingTypes.SeedAsync(context);

            // Mevcut markalara eksik modelleri ekle
            await SeedBrands.AddMissingModelsAsync(context);

            // Paketleri seed et (Brands/Models seed edildikten sonra çalışmalı)
            await SeedPackages.SeedAsync(context);

            // Mevcut modellere eksik paketleri ekle
            await SeedPackages.AddMissingPackagesAsync(context);

            // EN SON: Tüm modellere doğru BodyTypeId ata (AddMissingModels sonrası yeni modeller de düzelsin)
            await SeedBrands.UpdateModelBodyTypesAsync(context);

            // Admin kullanıcı oluştur
            await SeedAdminUser(context);

            // Test kullanıcısı oluştur
            await SeedTestUser(context);
        }

        private static async Task SeedAdminUser(Context context)
        {
            // Admin var mı kontrol et
            if (!await context.Users.AnyAsync(u => u.Role == UserRole.Admin))
            {
                var adminUser = new User
                {
                    Name = "Sistem Yöneticisi",
                    Email = "admin@erdemden.com",
                    PasswordHash = HashPassword("Admin123!"),
                    Role = UserRole.Admin,
                    IsActive = true,
                    IsEmailVerified = true,
                    CreatedAt = DateTime.UtcNow
                };

                context.Users.Add(adminUser);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedTestUser(Context context)
        {
            // Test kullanıcısı var mı kontrol et
            if (!await context.Users.AnyAsync(u => u.Email == "test@erdemden.com"))
            {
                var testUser = new User
                {
                    Name = "Test Kullanıcı",
                    Email = "test@erdemden.com",
                    PasswordHash = HashPassword("Test123!"),
                    Role = UserRole.User,
                    IsActive = true,
                    IsEmailVerified = true,
                    CreatedAt = DateTime.UtcNow
                };

                context.Users.Add(testUser);
                await context.SaveChangesAsync();
            }
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
        }
    }
}
