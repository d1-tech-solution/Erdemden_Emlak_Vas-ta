using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.Concrete
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        // DbSets
        public DbSet<Listing> Listings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Neighborhood> Neighborhoods { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<BodyType> BodyTypes { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<HousingType> HousingTypes { get; set; }
        public DbSet<ListingImage> ListingImages { get; set; }
        public DbSet<ExpertReport> ExpertReports { get; set; }
        public DbSet<NotaryDocument> NotaryDocuments { get; set; }
        public DbSet<UserFavorite> UserFavorites { get; set; }
        public DbSet<QuoteRequest> QuoteRequests { get; set; }
        public DbSet<QuoteMedia> QuoteMedia { get; set; }
        public DbSet<SiteContent> SiteContents { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==================== LISTING ====================
            modelBuilder.Entity<Listing>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Sık sorgulanan alanlar için indexler
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.Category);
                entity.HasIndex(e => e.CityId);
                entity.HasIndex(e => e.NeighborhoodId);
                entity.HasIndex(e => e.ListingDate);

                // City - Listing (1:N)
                entity.HasOne(e => e.City)
                    .WithMany(c => c.Listings)
                    .HasForeignKey(e => e.CityId)
                    .OnDelete(DeleteBehavior.Restrict);

                // District - Listing (1:N, optional)
                entity.HasOne(e => e.District)
                    .WithMany(d => d.Listings)
                    .HasForeignKey(e => e.DistrictId)
                    .OnDelete(DeleteBehavior.SetNull);

                // Neighborhood - Listing (1:N, optional)
                entity.HasOne(e => e.Neighborhood)
                    .WithMany(n => n.Listings)
                    .HasForeignKey(e => e.NeighborhoodId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // ==================== VEHICLE (1:1 with Listing) ====================
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.ListingId);

                entity.HasIndex(e => e.BrandId);
                entity.HasIndex(e => e.ModelId);

                entity.HasOne(e => e.Listing)
                    .WithOne(l => l.Vehicle)
                    .HasForeignKey<Vehicle>(e => e.ListingId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Brand)
                    .WithMany(b => b.Vehicles)
                    .HasForeignKey(e => e.BrandId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Model)
                    .WithMany(m => m.Vehicles)
                    .HasForeignKey(e => e.ModelId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.VehicleType)
                    .WithMany(v => v.Vehicles)
                    .HasForeignKey(e => e.VehicleTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.BodyType)
                    .WithMany(b => b.Vehicles)
                    .HasForeignKey(e => e.BodyTypeId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.FuelType)
                    .WithMany(f => f.Vehicles)
                    .HasForeignKey(e => e.FuelTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.TransmissionType)
                    .WithMany(t => t.Vehicles)
                    .HasForeignKey(e => e.TransmissionTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Package)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(e => e.PackageId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // ==================== PACKAGE - MODEL (1:N) ====================
            modelBuilder.Entity<Package>(entity =>
            {
                entity.HasOne(e => e.Model)
                    .WithMany(m => m.Packages)
                    .HasForeignKey(e => e.ModelId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ==================== REALESTATE (1:1 with Listing) ====================
            modelBuilder.Entity<RealEstate>(entity =>
            {
                entity.HasKey(e => e.ListingId);

                entity.HasOne(e => e.Listing)
                    .WithOne(l => l.RealEstate)
                    .HasForeignKey<RealEstate>(e => e.ListingId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.HousingType)
                    .WithMany(h => h.RealEstates)
                    .HasForeignKey(e => e.HousingTypeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ==================== MODEL - BRAND (1:N) ====================
            modelBuilder.Entity<Model>(entity =>
            {
                entity.HasOne(e => e.Brand)
                    .WithMany(b => b.Models)
                    .HasForeignKey(e => e.BrandId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ==================== DISTRICT - CITY (1:N) ====================
            modelBuilder.Entity<District>(entity =>
            {
                entity.HasOne(e => e.City)
                    .WithMany(c => c.Districts)
                    .HasForeignKey(e => e.CityId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ==================== NEIGHBORHOOD - DISTRICT (1:N) ====================
            modelBuilder.Entity<Neighborhood>(entity =>
            {
                entity.HasOne(e => e.District)
                    .WithMany(d => d.Neighborhoods)
                    .HasForeignKey(e => e.DistrictId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ==================== LISTING IMAGE (1:N) ====================
            modelBuilder.Entity<ListingImage>(entity =>
            {
                entity.HasOne(e => e.Listing)
                    .WithMany(l => l.Images)
                    .HasForeignKey(e => e.ListingId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ==================== NOTARY DOCUMENT (1:N) ====================
            modelBuilder.Entity<NotaryDocument>(entity =>
            {
                entity.HasOne(e => e.Listing)
                    .WithMany(l => l.NotaryDocuments)
                    .HasForeignKey(e => e.ListingId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ==================== QUOTE REQUEST ====================
            modelBuilder.Entity<QuoteRequest>(entity =>
            {
                entity.HasIndex(e => e.Email);
                entity.HasIndex(e => e.IsRead);
                entity.HasIndex(e => e.Date);
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.RequestType);
                entity.HasIndex(e => e.UserId);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false);
            });

            // ==================== EXPERT REPORT (1:N with QuoteRequest) ====================
            modelBuilder.Entity<ExpertReport>(entity =>
            {
                entity.HasOne(e => e.QuoteRequest)
                    .WithMany(q => q.ExpertReports)
                    .HasForeignKey(e => e.QuoteRequestId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ==================== QUOTE MEDIA (1:N with QuoteRequest) ====================
            modelBuilder.Entity<QuoteMedia>(entity =>
            {
                entity.HasOne(e => e.QuoteRequest)
                    .WithMany(q => q.Media)
                    .HasForeignKey(e => e.QuoteRequestId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ==================== USER FAVORITE (M:N) ====================
            modelBuilder.Entity<UserFavorite>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ListingId });

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Favorites)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Listing)
                    .WithMany(l => l.FavoritedBy)
                    .HasForeignKey(e => e.ListingId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ==================== USER ====================
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // ==================== REFRESH TOKEN (1:N with User) ====================
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasOne(e => e.User)
                    .WithMany(u => u.RefreshTokens)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.Token);
            });
        }
    }
}
