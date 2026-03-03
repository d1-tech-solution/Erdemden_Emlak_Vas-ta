using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.SeedData
{
    public static class SeedPackages
    {
        /// <summary>
        /// Tüm modellere ait paketleri seed eder (Türkiye pazarı, son 10-15 yıl)
        /// </summary>
        public static async Task SeedAsync(Context context)
        {
            if (await context.Set<Package>().AnyAsync())
                return;

            var models = await context.Set<Model>()
                .Include(m => m.Brand)
                .ToListAsync();

            // Model ismine göre paket eşleştirmesi (Türkiye pazarı, 2011-2026)
            var modelPackages = new Dictionary<string, string[]>
            {
                // ==================== BMW ====================
                { "1 Serisi", new[] {
                    "Standart", "Comfort", "Joy", "Joy Plus",
                    "Urban Line", "Sport Line", "First Edition Sport Line",
                    "M Sport", "M Sport Pro", "M Plus", "Edition M Sport Shadow"
                } },
                { "3 Serisi", new[] {
                    "Standart", "Comfort", "Modern", "Luxury", "Luxury Line",
                    "Sport", "Sport Line", "First Edition Sport Line",
                    "Technology", "M Sport", "M Sport Pro", "M Plus",
                    "Edition M Sport Shadow"
                } },
                { "5 Serisi", new[] {
                    "Standart", "Comfort", "Modern", "Luxury", "Luxury Line",
                    "Sport Line", "Executive", "Joy", "Joy Plus",
                    "M Sport", "M Sport Pro", "M Plus", "Technology",
                    "Edition M Sport Shadow", "Sport", "Sport Pro"
                } },
                { "X1", new[] {
                    "Standart", "Comfort", "Joy", "Joy Plus",
                    "xLine", "Sport Line", "M Sport", "M Sport Pro",
                    "Edition M Sport Shadow"
                } },
                { "X3", new[] {
                    "Standart", "Comfort", "Joy", "Joy Plus",
                    "xLine", "Sport Line", "Luxury Line",
                    "M Sport", "M Sport Pro", "M Plus",
                    "Edition M Sport Shadow"
                } },
                { "X5", new[] {
                    "Standart", "Comfort", "xLine",
                    "M Sport", "M Sport Pro", "M Plus",
                    "Edition Black Vermilion"
                } },
                { "iX", new[] {
                    "xLine", "Sport", "M Sport", "M Sport Pro", "M60"
                } },

                // ==================== Mercedes ====================
                { "A Serisi", new[] {
                    "Style", "Urban", "Progressive",
                    "AMG Line", "Edition 1", "AMG"
                } },
                { "C Serisi", new[] {
                    "Classic", "Comfort", "Elegance", "Avantgarde", "Exclusive",
                    "AMG Line", "AMG Line Premium", "BlueEfficiency",
                    "Edition 1", "Edition C", "Night Edition", "Night Paket",
                    "Designo", "AMG"
                } },
                { "E Serisi", new[] {
                    "Elegance", "Avantgarde", "Exclusive",
                    "AMG Line", "AMG Line Premium", "BlueEfficiency",
                    "Edition 1", "Edition E", "Night Edition", "Night Paket",
                    "Designo", "AMG"
                } },
                { "S Serisi", new[] {
                    "BlueEfficiency", "AMG Line", "AMG Line Premium",
                    "Exclusive", "Night Paket", "Designo", "AMG"
                } },
                { "CLA", new[] {
                    "Style", "Urban", "Progressive",
                    "AMG Line", "Edition 1", "Edition Orange Art",
                    "Night Paket", "AMG"
                } },
                { "G Serisi", new[] {
                    "Professional", "Designo", "Edition 1", "Edition 35",
                    "Stronger Than Time Edition", "Night Paket",
                    "AMG Grand Edition", "Magno", "AMG"
                } },
                { "EQE", new[] {
                    "Electric Art", "AMG Line", "AMG Line Premium",
                    "Night Paket", "Edition 1", "AMG"
                } },
                { "EQS", new[] {
                    "Electric Art", "AMG Line", "AMG Line Premium",
                    "Night Paket", "Edition 1", "Designo", "Maybach", "AMG"
                } },

                // ==================== Audi ====================
                { "A1", new[] {
                    "Attraction", "Ambition", "Ambiente",
                    "S Line", "Sport", "Admired", "Citycarver",
                    "Advanced", "S Line Style Edition"
                } },
                { "A3", new[] {
                    "Attraction", "Ambition", "Ambiente",
                    "S Line", "Sport", "Design", "Advanced",
                    "Dynamic", "Edition", "Style Edition"
                } },
                { "A4", new[] {
                    "Attraction", "Ambition", "Ambiente",
                    "S Line", "Sport", "Design", "Advanced",
                    "Launch Edition", "Dynamic", "Business", "Premium"
                } },
                { "A6", new[] {
                    "S Line", "Design", "Sport", "Advanced",
                    "Business", "Premium", "Launch Edition", "Dynamic"
                } },
                { "Q3", new[] {
                    "Attraction", "Ambition", "S Line", "Sport",
                    "Design", "Advanced", "Edition One", "Dynamic"
                } },
                { "Q5", new[] {
                    "Attraction", "Ambition", "S Line", "Sport",
                    "Design", "Advanced", "Edition One", "Dynamic", "Premium"
                } },
                { "Q7", new[] {
                    "S Line", "Design", "Sport", "Advanced",
                    "Edition", "Premium", "Dynamic"
                } },
                { "Q8", new[] {
                    "S Line", "Design", "Advanced",
                    "Edition One", "Premium", "Dynamic"
                } },
                { "e-tron", new[] {
                    "Edition One", "S Line", "Advanced", "S", "GT"
                } },

                // ==================== Volkswagen ====================
                { "Golf", new[] {
                    "Trendline", "Comfortline", "Highline",
                    "R-Line", "Impression", "Life", "Style", "Move",
                    "R", "GTI", "GTI Performance", "GTD", "GTE",
                    "Alltrack", "Edition"
                } },
                { "Polo", new[] {
                    "Trendline", "Comfortline", "Highline",
                    "R-Line", "Impression", "Life", "Style", "Move",
                    "GTI", "Match", "Edition", "Beats"
                } },
                { "Passat", new[] {
                    "Trendline", "Comfortline", "Highline",
                    "R-Line", "Impression", "Business", "Elegance",
                    "Life", "Style", "GTE", "Edition"
                } },
                { "Tiguan", new[] {
                    "Trendline", "Comfortline", "Highline",
                    "R-Line", "Impression", "Life", "Style",
                    "Elegance", "R", "Edition"
                } },
                { "Touareg", new[] {
                    "Comfortline", "Highline", "R-Line",
                    "Elegance", "Atmosphere", "R",
                    "Edition", "Executive"
                } },
                { "Amarok", new[] {
                    "Trendline", "Comfortline", "Highline",
                    "Canyon", "Aventura", "Style", "PanAmericana",
                    "Life", "Dark Label", "Edition"
                } },

                // ==================== Toyota ====================
                { "Corolla", new[] {
                    "Dream", "Vision", "Flame", "Passion", "Passion X-Pack",
                    "GR Sport", "Life", "Comfort", "Advance",
                    "Elegant", "Premium"
                } },
                { "Yaris", new[] {
                    "Dream", "Vision", "Flame", "Passion",
                    "GR Sport", "Life", "Fun", "Cool",
                    "Style", "Skypack"
                } },
                { "C-HR", new[] {
                    "Dream", "Vision", "Flame", "Passion", "Passion X-Pack",
                    "GR Sport", "Life", "Fun", "Advance"
                } },
                { "RAV4", new[] {
                    "Dream", "Vision", "Flame", "Passion", "Passion X-Pack",
                    "Adventure", "Life", "Comfort", "Advance",
                    "Premium", "Elegant"
                } },
                { "Hilux", new[] {
                    "Dream", "Vision", "Flame", "Passion",
                    "GR Sport", "Life", "Comfort", "Advance",
                    "Premium", "Elegant", "High",
                    "Hi-Cruiser", "Adventure", "Invincible"
                } },
                { "Land Cruiser", new[] {
                    "Dream", "Vision", "Flame", "Passion",
                    "GR Sport", "First Edition", "Life", "Comfort",
                    "Advance", "Premium", "Elegant", "VX"
                } },

                // ==================== Volvo ====================
                { "S60", new[] {
                    "Kinetic", "Momentum", "Summum", "Inscription",
                    "R-Design", "Plus", "Ultimate", "Core",
                    "Polestar", "Polestar Engineered"
                } },
                { "S90", new[] {
                    "Momentum", "Inscription", "R-Design",
                    "Excellence", "Core", "Plus", "Ultimate"
                } },
                { "XC40", new[] {
                    "Momentum", "Inscription", "R-Design",
                    "Core", "Plus", "Ultimate"
                } },
                { "XC60", new[] {
                    "Kinetic", "Momentum", "Summum", "Inscription",
                    "R-Design", "Core", "Plus", "Ultimate",
                    "Ocean Race", "Polestar Engineered"
                } },
                { "XC90", new[] {
                    "Kinetic", "Momentum", "Summum", "Inscription",
                    "R-Design", "Core", "Plus", "Ultimate",
                    "Excellence", "Executive"
                } },

                // ==================== Tesla ====================
                { "Model 3", new[] {
                    "Standard Range Plus", "Long Range",
                    "Long Range AWD", "Performance"
                } },
                { "Model Y", new[] {
                    "RWD", "Long Range",
                    "Long Range AWD", "Performance"
                } },
                { "Model S", new[] { "Long Range", "Plaid" } },
                { "Model X", new[] { "Long Range", "Plaid" } },

                // ==================== Honda ====================
                { "Civic", new[] {
                    "Elegance", "Executive", "Executive Plus",
                    "Sport", "Sport Plus", "Eco", "Eco Elegance",
                    "Dream", "Premium", "RS",
                    "LPG Elegance", "LPG Executive"
                } },
                { "City", new[] {
                    "Comfort", "Elegance", "Executive",
                    "S", "V", "Dream"
                } },
                { "HR-V", new[] {
                    "Comfort", "Elegance", "Executive",
                    "Sport", "Executive Plus", "Advance"
                } },
                { "CR-V", new[] {
                    "Comfort", "Elegance", "Executive",
                    "Lifestyle", "Executive Plus", "Advance", "Sport Line"
                } },
                { "Jazz", new[] {
                    "Cool", "Comfort", "Elegance", "Executive",
                    "Sport", "S", "Dynamic",
                    "1.2 Trend", "1.2 Trend X",
                    "1.3 Comfort", "1.3 Comfort X", "1.3 Fun", "1.3 Fun X",
                    "1.3 S", "1.3 Cool", "1.3 Elegance", "1.3 Executive", "1.3 Sport",
                    "1.5 Dynamic", "1.5 Elegance", "1.5 Executive",
                    "1.5 Crosstar", "1.5 Crosstar Advance",
                    "1.5 e:HEV Elegance", "1.5 e:HEV Executive", "1.5 e:HEV Crosstar"
                } },
                { "Accord", new[] {
                    "Comfort", "Elegance", "Executive",
                    "Executive Plus", "Lifestyle", "Premium",
                    "2.0 Elegance", "2.0 Executive", "2.0 Premium",
                    "2.4 Executive", "2.4 Premium",
                    "1.5T Elegance", "1.5T Executive", "1.5T Sport",
                    "2.0i-VTEC Executive", "2.0i-VTEC Premium",
                    "2.0 Hybrid", "2.0 Hybrid Advance", "2.0 Hybrid EX"
                } },

                // ==================== Ford ====================
                { "Focus", new[] {
                    "Ambiente", "Trend", "Trend X", "Titanium", "Titanium X",
                    "ST-Line", "ST-Line X", "Vignale", "Ghia",
                    "Style", "Active", "Active X", "Collection"
                } },
                { "Fiesta", new[] {
                    "Ambiente", "Trend", "Trend X", "Titanium", "Titanium X",
                    "ST-Line", "ST-Line X", "Vignale",
                    "Style", "Active", "Collection"
                } },
                { "Puma", new[] {
                    "Trend", "Titanium", "ST-Line", "ST-Line X",
                    "ST-Line Vignale", "Vignale", "Active", "Active X"
                } },
                { "Kuga", new[] {
                    "Ambiente", "Trend", "Trend X", "Titanium", "Titanium X",
                    "ST-Line", "ST-Line X", "Vignale",
                    "Business", "Business Class", "Zetec"
                } },
                { "Ranger", new[] {
                    "XL", "XLT", "Limited", "Wildtrak", "Raptor",
                    "Trend", "Titanium", "Sport"
                } },

                // ==================== Hyundai ====================
                { "i10", new[] {
                    "Jump", "Team", "Style", "Style Plus",
                    "Prime", "Elite", "Classic", "Comfort"
                } },
                { "i20", new[] {
                    "Jump", "Team", "Style", "Style Plus",
                    "Prime", "Elite", "N Line", "Comfort",
                    "Passion", "MPI Style", "MPI Elite", "MPI Prime"
                } },
                { "i30", new[] {
                    "Team", "Style", "Style Plus", "Prime", "Elite",
                    "N Line", "Comfort", "Jump", "Design", "Edition Plus"
                } },
                { "Elantra", new[] {
                    "Style", "Style Plus", "Prime", "Elite",
                    "N Line", "Comfort", "Team", "Jump", "Design"
                } },
                { "Tucson", new[] {
                    "Style", "Style Plus", "Prime", "Elite",
                    "N Line", "Comfort", "Jump", "Design",
                    "Premium", "Executive", "Adventure"
                } },
                { "Bayon", new[] {
                    "Style", "Style Plus", "Prime", "Elite",
                    "N Line", "Comfort", "Jump", "Team", "Design"
                } },
                { "IONIQ 5", new[] {
                    "Style", "Prime", "N Line", "Vertex",
                    "Edition", "Top", "Inspiration",
                    "Long Range", "Standard Range"
                } },

                // ==================== Renault ====================
                { "Megane", new[] {
                    "Joy", "Touch", "Icon", "Bose",
                    "R.S. Line", "Intens", "Zen", "Life",
                    "Techno", "Equilibre", "Evolution", "Iconic"
                } },
                { "Talisman", new[] {
                    "Joy", "Touch", "Icon", "Initiale Paris",
                    "Intens", "Zen", "Life", "Techno"
                } },
                { "Captur", new[] {
                    "Joy", "Touch", "Icon", "Bose",
                    "R.S. Line", "Intens", "Zen", "Life",
                    "Techno", "Equilibre", "Evolution", "Iconic"
                } },
                { "Kadjar", new[] {
                    "Joy", "Touch", "Icon", "Bose",
                    "Intens", "Zen", "Life", "Techno"
                } },
                { "Austral", new[] {
                    "Equilibre", "Techno", "Iconic", "Esprit Alpine",
                    "Evolution", "Techno Esprit Alpine"
                } },

                // ==================== Kia ====================
                { "Rio", new[] {
                    "Cool", "Comfort", "Elegance", "Premium",
                    "Concept", "Concept Plus", "Dream", "Style"
                } },
                { "Ceed", new[] {
                    "Cool", "Comfort", "Elegance", "Premium",
                    "Concept", "Concept Plus", "Dream", "GT-Line",
                    "Style", "Business Line"
                } },
                { "EV6", new[] {
                    "Air", "Wind", "Earth", "GT-Line",
                    "GT", "Long Range", "Standard Range"
                } },
                { "Sportage", new[] {
                    "Cool", "Comfort", "Elegance", "Premium",
                    "Concept", "Concept Plus", "Dream", "GT-Line",
                    "Style", "Business Line"
                } },

                // ==================== Peugeot ====================
                { "Boxer", new[] {
                    "L1H1", "L2H1", "L2H2", "L3H2", "L3H3", "L4H2", "L4H3",
                    "Premium", "Pro", "Asphalt", "Grip",
                    "BlueHDi 120", "BlueHDi 140", "BlueHDi 160",
                    "Minibüs", "Kamyonet", "Şasi Kabin"
                } }
            };

            foreach (var model in models)
            {
                if (modelPackages.TryGetValue(model.Name, out var packages))
                {
                    foreach (var packageName in packages)
                    {
                        var package = new Package
                        {
                            Id = Guid.NewGuid(),
                            ModelId = model.Id,
                            Name = packageName,
                            CreatedAt = DateTime.UtcNow
                        };

                        context.Set<Package>().Add(package);
                    }
                }
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Mevcut modellere eksik paketleri ekler (son 10-15 yıl Türkiye pazarı)
        /// </summary>
        public static async Task AddMissingPackagesAsync(Context context)
        {
            var models = await context.Set<Model>()
                .Include(m => m.Brand)
                .Include(m => m.Packages)
                .ToListAsync();

            var newPackages = new List<(string BrandName, string ModelName, string[] Packages)>
            {
                // BMW
                ("BMW", "1 Serisi", new[] { "Standart", "Comfort", "Joy", "Joy Plus", "Urban Line", "Sport Line", "First Edition Sport Line", "M Sport", "M Sport Pro", "M Plus", "Edition M Sport Shadow" }),
                ("BMW", "3 Serisi", new[] { "Standart", "Comfort", "Modern", "Luxury", "Luxury Line", "Sport", "Sport Line", "First Edition Sport Line", "Technology", "M Sport", "M Sport Pro", "M Plus", "Edition M Sport Shadow" }),
                ("BMW", "5 Serisi", new[] { "Standart", "Comfort", "Modern", "Luxury", "Luxury Line", "Sport Line", "Executive", "Joy", "Joy Plus", "M Sport", "M Sport Pro", "M Plus", "Technology", "Edition M Sport Shadow", "Sport", "Sport Pro" }),
                ("BMW", "X1", new[] { "Standart", "Comfort", "Joy", "Joy Plus", "xLine", "Sport Line", "M Sport", "M Sport Pro", "Edition M Sport Shadow" }),
                ("BMW", "X3", new[] { "Standart", "Comfort", "Joy", "Joy Plus", "xLine", "Sport Line", "Luxury Line", "M Sport", "M Sport Pro", "M Plus", "Edition M Sport Shadow" }),
                ("BMW", "X5", new[] { "Standart", "Comfort", "xLine", "M Sport", "M Sport Pro", "M Plus", "Edition Black Vermilion" }),
                ("BMW", "iX", new[] { "xLine", "Sport", "M Sport", "M Sport Pro", "M60" }),

                // Mercedes
                ("Mercedes", "A Serisi", new[] { "Style", "Urban", "Progressive", "AMG Line", "Edition 1", "AMG" }),
                ("Mercedes", "C Serisi", new[] { "Classic", "Comfort", "Elegance", "Avantgarde", "Exclusive", "AMG Line", "AMG Line Premium", "BlueEfficiency", "Edition 1", "Edition C", "Night Edition", "Night Paket", "Designo", "AMG" }),
                ("Mercedes", "E Serisi", new[] { "Elegance", "Avantgarde", "Exclusive", "AMG Line", "AMG Line Premium", "BlueEfficiency", "Edition 1", "Edition E", "Night Edition", "Night Paket", "Designo", "AMG" }),
                ("Mercedes", "S Serisi", new[] { "BlueEfficiency", "AMG Line", "AMG Line Premium", "Exclusive", "Night Paket", "Designo", "AMG" }),
                ("Mercedes", "CLA", new[] { "Style", "Urban", "Progressive", "AMG Line", "Edition 1", "Edition Orange Art", "Night Paket", "AMG" }),
                ("Mercedes", "G Serisi", new[] { "Professional", "Designo", "Edition 1", "Edition 35", "Stronger Than Time Edition", "Night Paket", "AMG Grand Edition", "Magno", "AMG" }),
                ("Mercedes", "EQE", new[] { "Electric Art", "AMG Line", "AMG Line Premium", "Night Paket", "Edition 1", "AMG" }),
                ("Mercedes", "EQS", new[] { "Electric Art", "AMG Line", "AMG Line Premium", "Night Paket", "Edition 1", "Designo", "Maybach", "AMG" }),

                // Audi
                ("Audi", "A1", new[] { "Attraction", "Ambition", "Ambiente", "S Line", "Sport", "Admired", "Citycarver", "Advanced", "S Line Style Edition" }),
                ("Audi", "A3", new[] { "Attraction", "Ambition", "Ambiente", "S Line", "Sport", "Design", "Advanced", "Dynamic", "Edition", "Style Edition" }),
                ("Audi", "A4", new[] { "Attraction", "Ambition", "Ambiente", "S Line", "Sport", "Design", "Advanced", "Launch Edition", "Dynamic", "Business", "Premium" }),
                ("Audi", "A6", new[] { "S Line", "Design", "Sport", "Advanced", "Business", "Premium", "Launch Edition", "Dynamic" }),
                ("Audi", "Q3", new[] { "Attraction", "Ambition", "S Line", "Sport", "Design", "Advanced", "Edition One", "Dynamic" }),
                ("Audi", "Q5", new[] { "Attraction", "Ambition", "S Line", "Sport", "Design", "Advanced", "Edition One", "Dynamic", "Premium" }),
                ("Audi", "Q7", new[] { "S Line", "Design", "Sport", "Advanced", "Edition", "Premium", "Dynamic" }),
                ("Audi", "Q8", new[] { "S Line", "Design", "Advanced", "Edition One", "Premium", "Dynamic" }),
                ("Audi", "e-tron", new[] { "Edition One", "S Line", "Advanced", "S", "GT" }),

                // Volkswagen
                ("Volkswagen", "Golf", new[] { "Trendline", "Comfortline", "Highline", "R-Line", "Impression", "Life", "Style", "Move", "R", "GTI", "GTI Performance", "GTD", "GTE", "Alltrack", "Edition" }),
                ("Volkswagen", "Polo", new[] { "Trendline", "Comfortline", "Highline", "R-Line", "Impression", "Life", "Style", "Move", "GTI", "Match", "Edition", "Beats" }),
                ("Volkswagen", "Passat", new[] { "Trendline", "Comfortline", "Highline", "R-Line", "Impression", "Business", "Elegance", "Life", "Style", "GTE", "Edition" }),
                ("Volkswagen", "Tiguan", new[] { "Trendline", "Comfortline", "Highline", "R-Line", "Impression", "Life", "Style", "Elegance", "R", "Edition" }),
                ("Volkswagen", "Touareg", new[] { "Comfortline", "Highline", "R-Line", "Elegance", "Atmosphere", "R", "Edition", "Executive" }),
                ("Volkswagen", "Amarok", new[] { "Trendline", "Comfortline", "Highline", "Canyon", "Aventura", "Style", "PanAmericana", "Life", "Dark Label", "Edition" }),

                // Toyota
                ("Toyota", "Corolla", new[] { "Dream", "Vision", "Flame", "Passion", "Passion X-Pack", "GR Sport", "Life", "Comfort", "Advance", "Elegant", "Premium" }),
                ("Toyota", "Yaris", new[] { "Dream", "Vision", "Flame", "Passion", "GR Sport", "Life", "Fun", "Cool", "Style", "Skypack" }),
                ("Toyota", "C-HR", new[] { "Dream", "Vision", "Flame", "Passion", "Passion X-Pack", "GR Sport", "Life", "Fun", "Advance" }),
                ("Toyota", "RAV4", new[] { "Dream", "Vision", "Flame", "Passion", "Passion X-Pack", "Adventure", "Life", "Comfort", "Advance", "Premium", "Elegant" }),
                ("Toyota", "Hilux", new[] { "Dream", "Vision", "Flame", "Passion", "GR Sport", "Life", "Comfort", "Advance", "Premium", "Elegant", "High", "Hi-Cruiser", "Adventure", "Invincible" }),
                ("Toyota", "Land Cruiser", new[] { "Dream", "Vision", "Flame", "Passion", "GR Sport", "First Edition", "Life", "Comfort", "Advance", "Premium", "Elegant", "VX" }),

                // Volvo
                ("Volvo", "S60", new[] { "Kinetic", "Momentum", "Summum", "Inscription", "R-Design", "Plus", "Ultimate", "Core", "Polestar", "Polestar Engineered" }),
                ("Volvo", "S90", new[] { "Momentum", "Inscription", "R-Design", "Excellence", "Core", "Plus", "Ultimate" }),
                ("Volvo", "XC40", new[] { "Momentum", "Inscription", "R-Design", "Core", "Plus", "Ultimate" }),
                ("Volvo", "XC60", new[] { "Kinetic", "Momentum", "Summum", "Inscription", "R-Design", "Core", "Plus", "Ultimate", "Ocean Race", "Polestar Engineered" }),
                ("Volvo", "XC90", new[] { "Kinetic", "Momentum", "Summum", "Inscription", "R-Design", "Core", "Plus", "Ultimate", "Excellence", "Executive" }),

                // Tesla
                ("Tesla", "Model 3", new[] { "Standard Range Plus", "Long Range", "Long Range AWD", "Performance" }),
                ("Tesla", "Model Y", new[] { "RWD", "Long Range", "Long Range AWD", "Performance" }),
                ("Tesla", "Model S", new[] { "Long Range", "Plaid" }),
                ("Tesla", "Model X", new[] { "Long Range", "Plaid" }),

                // Honda
                ("Honda", "Civic", new[] { "Elegance", "Executive", "Executive Plus", "Sport", "Sport Plus", "Eco", "Eco Elegance", "Dream", "Premium", "RS", "LPG Elegance", "LPG Executive" }),
                ("Honda", "City", new[] { "Comfort", "Elegance", "Executive", "S", "V", "Dream" }),
                ("Honda", "HR-V", new[] { "Comfort", "Elegance", "Executive", "Sport", "Executive Plus", "Advance" }),
                ("Honda", "CR-V", new[] { "Comfort", "Elegance", "Executive", "Lifestyle", "Executive Plus", "Advance", "Sport Line" }),
                ("Honda", "Jazz", new[] {
                    "Cool", "Comfort", "Elegance", "Executive", "Sport", "S", "Dynamic",
                    "1.2 Trend", "1.2 Trend X",
                    "1.3 Comfort", "1.3 Comfort X", "1.3 Fun", "1.3 Fun X",
                    "1.3 S", "1.3 Cool", "1.3 Elegance", "1.3 Executive", "1.3 Sport",
                    "1.5 Dynamic", "1.5 Elegance", "1.5 Executive",
                    "1.5 Crosstar", "1.5 Crosstar Advance",
                    "1.5 e:HEV Elegance", "1.5 e:HEV Executive", "1.5 e:HEV Crosstar"
                }),
                ("Honda", "Accord", new[] {
                    "Comfort", "Elegance", "Executive", "Executive Plus", "Lifestyle", "Premium",
                    "2.0 Elegance", "2.0 Executive", "2.0 Premium",
                    "2.4 Executive", "2.4 Premium",
                    "1.5T Elegance", "1.5T Executive", "1.5T Sport",
                    "2.0i-VTEC Executive", "2.0i-VTEC Premium",
                    "2.0 Hybrid", "2.0 Hybrid Advance", "2.0 Hybrid EX"
                }),

                // Ford
                ("Ford", "Focus", new[] { "Ambiente", "Trend", "Trend X", "Titanium", "Titanium X", "ST-Line", "ST-Line X", "Vignale", "Ghia", "Style", "Active", "Active X", "Collection" }),
                ("Ford", "Fiesta", new[] { "Ambiente", "Trend", "Trend X", "Titanium", "Titanium X", "ST-Line", "ST-Line X", "Vignale", "Style", "Active", "Collection" }),
                ("Ford", "Puma", new[] { "Trend", "Titanium", "ST-Line", "ST-Line X", "ST-Line Vignale", "Vignale", "Active", "Active X" }),
                ("Ford", "Kuga", new[] { "Ambiente", "Trend", "Trend X", "Titanium", "Titanium X", "ST-Line", "ST-Line X", "Vignale", "Business", "Business Class", "Zetec" }),
                ("Ford", "Ranger", new[] { "XL", "XLT", "Limited", "Wildtrak", "Raptor", "Trend", "Titanium", "Sport" }),

                // Hyundai
                ("Hyundai", "i10", new[] { "Jump", "Team", "Style", "Style Plus", "Prime", "Elite", "Classic", "Comfort" }),
                ("Hyundai", "i20", new[] { "Jump", "Team", "Style", "Style Plus", "Prime", "Elite", "N Line", "Comfort", "Passion", "MPI Style", "MPI Elite", "MPI Prime" }),
                ("Hyundai", "i30", new[] { "Team", "Style", "Style Plus", "Prime", "Elite", "N Line", "Comfort", "Jump", "Design", "Edition Plus" }),
                ("Hyundai", "Elantra", new[] { "Style", "Style Plus", "Prime", "Elite", "N Line", "Comfort", "Team", "Jump", "Design" }),
                ("Hyundai", "Tucson", new[] { "Style", "Style Plus", "Prime", "Elite", "N Line", "Comfort", "Jump", "Design", "Premium", "Executive", "Adventure" }),
                ("Hyundai", "Bayon", new[] { "Style", "Style Plus", "Prime", "Elite", "N Line", "Comfort", "Jump", "Team", "Design" }),
                ("Hyundai", "IONIQ 5", new[] { "Style", "Prime", "N Line", "Vertex", "Edition", "Top", "Inspiration", "Long Range", "Standard Range" }),

                // Renault
                ("Renault", "Megane", new[] { "Joy", "Touch", "Icon", "Bose", "R.S. Line", "Intens", "Zen", "Life", "Techno", "Equilibre", "Evolution", "Iconic" }),
                ("Renault", "Talisman", new[] { "Joy", "Touch", "Icon", "Initiale Paris", "Intens", "Zen", "Life", "Techno" }),
                ("Renault", "Captur", new[] { "Joy", "Touch", "Icon", "Bose", "R.S. Line", "Intens", "Zen", "Life", "Techno", "Equilibre", "Evolution", "Iconic" }),
                ("Renault", "Kadjar", new[] { "Joy", "Touch", "Icon", "Bose", "Intens", "Zen", "Life", "Techno" }),
                ("Renault", "Austral", new[] { "Equilibre", "Techno", "Iconic", "Esprit Alpine", "Evolution", "Techno Esprit Alpine" }),

                // Kia
                ("Kia", "Rio", new[] { "Cool", "Comfort", "Elegance", "Premium", "Concept", "Concept Plus", "Dream", "Style" }),
                ("Kia", "Ceed", new[] { "Cool", "Comfort", "Elegance", "Premium", "Concept", "Concept Plus", "Dream", "GT-Line", "Style", "Business Line" }),
                ("Kia", "EV6", new[] { "Air", "Wind", "Earth", "GT-Line", "GT", "Long Range", "Standard Range" }),
                ("Kia", "Sportage", new[] { "Cool", "Comfort", "Elegance", "Premium", "Concept", "Concept Plus", "Dream", "GT-Line", "Style", "Business Line" }),

                // Peugeot
                ("Peugeot", "Boxer", new[] { "L1H1", "L2H1", "L2H2", "L3H2", "L3H3", "L4H2", "L4H3", "Premium", "Pro", "Asphalt", "Grip", "BlueHDi 120", "BlueHDi 140", "BlueHDi 160", "Minibüs", "Kamyonet", "Şasi Kabin" })
            };

            var added = false;

            foreach (var (brandName, modelName, packages) in newPackages)
            {
                var model = models.FirstOrDefault(m => m.Name == modelName && m.Brand.Name == brandName);
                if (model == null) continue;

                var existingPackageNames = model.Packages.Select(p => p.Name).ToHashSet();

                foreach (var packageName in packages)
                {
                    if (existingPackageNames.Contains(packageName)) continue;

                    context.Set<Package>().Add(new Package
                    {
                        Id = Guid.NewGuid(),
                        ModelId = model.Id,
                        Name = packageName,
                        CreatedAt = DateTime.UtcNow
                    });

                    added = true;
                }
            }

            if (added)
                await context.SaveChangesAsync();
        }
    }
}
