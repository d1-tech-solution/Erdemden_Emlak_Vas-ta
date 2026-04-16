using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.SeedData
{
    public static class SeedPackages
    {
        private static readonly List<(string BrandName, string ModelName, string[] Packages)> MotorcyclePackageEntries = new()
        {
            ("BMW", "R 1250 GS", new[] { "Standart", "Style Triple Black", "GS Trophy", "Adventure", "Option 719" }),
            ("BMW", "F 850 GS", new[] { "Standart", "Adventure", "Rallye", "Style Triple Black" }),
            ("BMW", "S 1000 RR", new[] { "Standart", "M Package", "Race Package" }),
            ("BMW", "G 310 R", new[] { "Standart", "Sport", "Style Passion" }),
            ("BMW", "C 400 X", new[] { "Standart", "Style Triple Black", "Ride Pro" }),
            ("BMW", "C 400 GT", new[] { "Standart", "Exclusive", "Comfort Package" }),
            ("BMW", "CE 04", new[] { "Standart", "Avantgarde", "Exclusive" }),
            ("Honda", "PCX 125", new[] { "CBS", "ABS", "DX" }),
            ("Honda", "PCX 160", new[] { "CBS", "ABS", "DX" }),
            ("Honda", "Forza 250", new[] { "Standart", "ABS", "Smart Top Box" }),
            ("Honda", "Forza 750", new[] { "Standart", "DCT", "Travel Pack" }),
            ("Honda", "ADV 350", new[] { "Standart", "ABS", "Smart Top Box" }),
            ("Honda", "CBR 650R", new[] { "Standart", "E-Clutch" }),
            ("Honda", "CB 650R", new[] { "Standart", "E-Clutch" }),
            ("Honda", "Hornet 750", new[] { "Standart", "ABS" }),
            ("Honda", "Africa Twin", new[] { "Standart", "Adventure Sports", "DCT", "ES" }),
            ("Honda", "Gold Wing", new[] { "Bagger", "Tour DCT", "Tour Airbag DCT" }),
            ("Honda", "NC750X", new[] { "Standart", "DCT", "Travel Edition" }),
            ("Honda", "SH125i", new[] { "Standart", "ABS" }),
            ("Yamaha", "NMAX 155", new[] { "Standart", "Connected", "Tech Max" }),
            ("Yamaha", "XMAX 250", new[] { "Standart", "Tech Max" }),
            ("Yamaha", "XMAX 300", new[] { "Standart", "Tech Max" }),
            ("Yamaha", "R25", new[] { "Standart", "ABS" }),
            ("Yamaha", "R3", new[] { "Standart", "Monster Energy" }),
            ("Yamaha", "R7", new[] { "Standart" }),
            ("Yamaha", "MT-07", new[] { "Standart", "Pure", "ABS" }),
            ("Yamaha", "MT-09", new[] { "Standart", "SP" }),
            ("Yamaha", "MT-25", new[] { "Standart", "ABS" }),
            ("Yamaha", "Tenere 700", new[] { "Standart", "World Raid", "Explore" }),
            ("Yamaha", "Tracer 9", new[] { "Standart", "GT", "GT+" }),
            ("Yamaha", "Aerox 155", new[] { "Standart", "Connected" }),
            ("Kawasaki", "Ninja 250", new[] { "Standart", "KRT Edition" }),
            ("Kawasaki", "Ninja 650", new[] { "Standart", "KRT Edition", "Performance" }),
            ("Kawasaki", "Ninja ZX-6R", new[] { "Standart", "KRT Edition" }),
            ("Kawasaki", "Z650", new[] { "Standart", "Performance" }),
            ("Kawasaki", "Z900", new[] { "Standart", "SE" }),
            ("Kawasaki", "Versys 650", new[] { "Standart", "Tourer Plus" }),
            ("Kawasaki", "KLR 650", new[] { "Standart", "Adventure" }),
            ("Suzuki", "GSX-R1000", new[] { "Standart", "R", "MotoGP Edition" }),
            ("Suzuki", "GSX-S750", new[] { "Standart", "Yugen" }),
            ("Suzuki", "GSX-8S", new[] { "Standart", "Tech Pack" }),
            ("Suzuki", "Hayabusa", new[] { "Standart", "25th Anniversary" }),
            ("Suzuki", "V-Strom 650", new[] { "Standart", "XT", "Adventure" }),
            ("Suzuki", "V-Strom 800DE", new[] { "Standart", "Adventure" }),
            ("Suzuki", "Burgman 400", new[] { "Standart", "Executive" }),
            ("KTM", "390 Duke", new[] { "Standart", "GP" }),
            ("KTM", "250 Duke", new[] { "Standart" }),
            ("KTM", "790 Adventure", new[] { "Standart", "R" }),
            ("KTM", "1290 Super Adventure S", new[] { "Standart", "Tech Pack" }),
            ("KTM", "RC 390", new[] { "Standart", "GP" }),
            ("KTM", "690 SMC R", new[] { "Standart" }),
            ("Ducati", "Panigale V2", new[] { "Standart", "Bayliss" }),
            ("Ducati", "Monster", new[] { "Standart", "Plus", "SP" }),
            ("Ducati", "Multistrada V4", new[] { "Standart", "S", "Pikes Peak", "Rally" }),
            ("Ducati", "Scrambler Icon", new[] { "Icon", "Full Throttle", "Nightshift" }),
            ("Ducati", "Diavel", new[] { "Standart", "V4", "Lamborghini" }),
            ("Ducati", "Hypermotard 950", new[] { "Standart", "SP", "RVE" }),
            ("Harley-Davidson", "Sportster S", new[] { "Standart" }),
            ("Harley-Davidson", "Iron 883", new[] { "Standart" }),
            ("Harley-Davidson", "Fat Bob 114", new[] { "Standart" }),
            ("Harley-Davidson", "Street Glide", new[] { "Special", "ST" }),
            ("Harley-Davidson", "Pan America 1250", new[] { "Standart", "Special" }),
            ("Triumph", "Street Triple RS", new[] { "R", "RS", "Moto2 Edition" }),
            ("Triumph", "Trident 660", new[] { "Standart", "Triple Tribute" }),
            ("Triumph", "Tiger 900", new[] { "GT", "GT Pro", "Rally Pro" }),
            ("Triumph", "Bonneville T120", new[] { "Standart", "Black" }),
            ("Triumph", "Rocket 3", new[] { "R", "GT" }),
            ("Triumph", "Scrambler 900", new[] { "Standart" }),
            ("Vespa", "Primavera 150", new[] { "Standart", "S", "RED" }),
            ("Vespa", "Sprint 150", new[] { "Standart", "S" }),
            ("Vespa", "GTS 300", new[] { "Standart", "Super", "SuperSport" }),
            ("Vespa", "Elettrica", new[] { "45 km/h", "70 km/h" }),
            ("Piaggio", "Liberty 125", new[] { "Standart", "S" }),
            ("Piaggio", "Beverly 400", new[] { "Standart", "S" }),
            ("Piaggio", "MP3 400", new[] { "Sport", "Exclusive" }),
            ("Piaggio", "Medley 150", new[] { "Standart", "S" }),
            ("Aprilia", "RS 660", new[] { "Standart", "Extrema" }),
            ("Aprilia", "Tuono 660", new[] { "Standart", "Factory" }),
            ("Aprilia", "SR GT 200", new[] { "Standart", "Sport" }),
            ("Aprilia", "Tuareg 660", new[] { "Standart", "Rally" }),
            ("Aprilia", "RSV4", new[] { "Standart", "Factory" }),
            ("Bajaj", "Pulsar NS200", new[] { "Standart", "UG" }),
            ("Bajaj", "Pulsar N250", new[] { "Standart", "Dual Channel ABS" }),
            ("Bajaj", "Dominar 400", new[] { "Standart", "Touring" }),
            ("TVS", "Apache RTR 200", new[] { "Standart", "4V", "Ride Modes" }),
            ("TVS", "Jupiter 125", new[] { "Drum", "Disc" }),
            ("TVS", "NTorq 125", new[] { "Standart", "Race XP", "XT" }),
            ("TVS", "Apache RR 310", new[] { "Standart", "BTO" }),
            ("Royal Enfield", "Classic 350", new[] { "Redditch", "Halcyon", "Signals", "Dark", "Chrome" }),
            ("Royal Enfield", "Meteor 350", new[] { "Fireball", "Stellar", "Supernova" }),
            ("Royal Enfield", "Hunter 350", new[] { "Retro", "Metro", "Metro Rebel" }),
            ("Royal Enfield", "Himalayan 450", new[] { "Base", "Pass", "Summit" }),
            ("Royal Enfield", "Interceptor 650", new[] { "Standart", "Black Ray", "Barcelona Blue" }),
            ("Royal Enfield", "Super Meteor 650", new[] { "Astral", "Interstellar", "Celestial" }),
            ("Benelli", "TNT 125", new[] { "Standart" }),
            ("Benelli", "302S", new[] { "Standart" }),
            ("Benelli", "Leoncino 250", new[] { "Standart" }),
            ("Benelli", "TRK 502X", new[] { "Standart" }),
            ("Benelli", "752S", new[] { "Standart" }),
            ("CFMOTO", "250NK", new[] { "Standart" }),
            ("CFMOTO", "450SR", new[] { "Standart", "S" }),
            ("CFMOTO", "650MT", new[] { "Standart", "Touring" }),
            ("CFMOTO", "700CL-X", new[] { "Heritage", "Sport", "Adventure" }),
            ("CFMOTO", "800MT", new[] { "Sport", "Touring", "Explore" }),
            ("QJMotor", "SRK 250", new[] { "Standart" }),
            ("QJMotor", "SRK 550", new[] { "Standart" }),
            ("QJMotor", "SRT 550", new[] { "Standart" }),
            ("SYM", "Joymax Z 250", new[] { "Standart", "Plus" }),
            ("SYM", "Jet X 125", new[] { "Standart" }),
            ("SYM", "Fiddle 125", new[] { "Standart" }),
            ("SYM", "Cruisym 250", new[] { "Standart" }),
            ("Kymco", "Agility 125", new[] { "Standart" }),
            ("Kymco", "Like 125", new[] { "Standart" }),
            ("Kymco", "Xciting VS 400", new[] { "Standart" }),
            ("Kymco", "Downtown 250i", new[] { "Standart" }),
            ("Husqvarna", "Svartpilen 401", new[] { "Standart" }),
            ("Husqvarna", "Vitpilen 401", new[] { "Standart" }),
            ("Husqvarna", "Norden 901", new[] { "Standart", "Expedition" }),
            ("Husqvarna", "701 Supermoto", new[] { "Standart" }),
            ("Mondial", "Drift L", new[] { "Standart" }),
            ("Mondial", "X-Treme Max 200i", new[] { "Standart" }),
            ("Mondial", "SMX 125", new[] { "Standart" }),
            ("Mondial", "Turismo 350i", new[] { "Standart" }),
            ("Mondial", "Pagani 250i", new[] { "Standart" }),
            ("Kuba", "Superlight 125", new[] { "Standart" }),
            ("Kuba", "Bluebird", new[] { "Standart" }),
            ("Kuba", "TK03", new[] { "Standart" }),
            ("Kuba", "VN50 Pro", new[] { "Standart" }),
            ("Kuba", "ATR 125", new[] { "Standart" }),
            ("NIU", "NQi GTS", new[] { "Standart", "Sport" }),
            ("NIU", "MQi GT", new[] { "Standart", "EVO" })
        };

        private static void MergeMotorcyclePackages(Dictionary<string, string[]> modelPackages)
        {
            foreach (var (_, modelName, packages) in MotorcyclePackageEntries)
            {
                if (modelPackages.TryGetValue(modelName, out var existingPackages))
                {
                    modelPackages[modelName] = existingPackages.Concat(packages).Distinct().ToArray();
                    continue;
                }

                modelPackages[modelName] = packages;
            }
        }

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

                // BMW - Yeni modeller
                { "2 Serisi Active Tourer", new[] {
                    "Standart", "Comfort", "Joy", "Joy Plus",
                    "Sport Line", "Luxury Line", "M Sport", "M Sport Pro",
                    "Edition M Sport Shadow"
                } },
                { "2 Serisi Gran Coupe", new[] {
                    "Standart", "Sport Line", "Luxury Line",
                    "M Sport", "M Sport Pro", "M Sport Edition",
                    "Edition M Sport Shadow"
                } },
                { "2 Serisi Coupe", new[] {
                    "Standart", "Sport Line", "M Sport", "M Sport Pro"
                } },
                { "4 Serisi Gran Coupe", new[] {
                    "Standart", "Sport Line", "Luxury Line",
                    "M Sport", "M Sport Pro", "M Sport Pro Edition",
                    "Edition M Sport Shadow"
                } },
                { "4 Serisi Coupe", new[] {
                    "Standart", "Sport Line", "Luxury Line",
                    "M Sport", "M Sport Pro", "Edition M Sport Shadow"
                } },
                { "4 Serisi Cabrio", new[] {
                    "Standart", "Sport Line", "Luxury Line",
                    "M Sport", "M Sport Pro"
                } },
                { "7 Serisi", new[] {
                    "Standart", "Comfort", "Modern", "Luxury",
                    "Executive", "Executive Lounge",
                    "M Sport", "M Sport Pro", "M Plus",
                    "Edition M Sport Shadow", "Pure Excellence"
                } },
                { "8 Serisi Gran Coupe", new[] {
                    "Standart", "M Sport", "M Sport Pro",
                    "M Sport Edition", "Edition Golden Thunder"
                } },
                { "8 Serisi Coupe", new[] {
                    "Standart", "M Sport", "M Sport Pro",
                    "Edition Golden Thunder"
                } },
                { "M2", new[] {
                    "Standart", "M Heritage Edition"
                } },
                { "M4", new[] {
                    "Standart", "Competition", "Competition xDrive",
                    "CS", "CSL", "Heritage Edition"
                } },
                { "Z4", new[] {
                    "sDrive20i", "sDrive30i", "M40i",
                    "Sport Line", "M Sport", "M Sport Pro",
                    "First Edition"
                } },
                { "X2", new[] {
                    "Standart", "Advantage", "Sport Line",
                    "M Sport", "M Sport X", "M Sport Pro",
                    "Edition M Sport Shadow"
                } },
                { "X4", new[] {
                    "Standart", "xLine", "Sport Line", "Luxury Line",
                    "M Sport", "M Sport Pro", "M Sport X",
                    "Edition M Sport Shadow"
                } },
                { "X6", new[] {
                    "Standart", "xLine", "M Sport", "M Sport Pro",
                    "M Sport Edition", "Edition Black Vermilion"
                } },
                { "X7", new[] {
                    "Standart", "xLine", "M Sport", "M Sport Pro",
                    "Edition Dark Shadow", "Frozen Black"
                } },
                { "i4", new[] {
                    "eDrive35", "eDrive40", "xDrive40", "M50",
                    "Sport Line", "M Sport", "M Sport Pro"
                } },
                { "i5", new[] {
                    "eDrive40", "xDrive40", "M60 xDrive",
                    "M Sport", "M Sport Pro"
                } },
                { "i7", new[] {
                    "xDrive60", "M70 xDrive",
                    "M Sport", "M Sport Pro", "Excellence"
                } },
                { "iX1", new[] {
                    "xDrive30", "eDrive20",
                    "xLine", "M Sport", "M Sport Pro"
                } },
                { "iX2", new[] {
                    "eDrive20", "xDrive30",
                    "M Sport", "M Sport Pro"
                } },
                { "iX3", new[] {
                    "Impressive", "M Sport", "M Sport Pro", "Inspiring"
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
                { "B Serisi", new[] {
                    "Style", "Urban", "Progressive",
                    "AMG Line", "BlueEfficiency", "Edition 1"
                } },
                { "CLS", new[] {
                    "Elegance", "Avantgarde", "Exclusive",
                    "AMG Line", "AMG Line Premium", "BlueEfficiency",
                    "Edition 1", "Night Edition", "Night Paket",
                    "Designo", "AMG", "Shooting Brake"
                } },
                { "AMG GT 4 Kapı", new[] {
                    "AMG GT 43", "AMG GT 53", "AMG GT 63", "AMG GT 63 S",
                    "Edition 1", "Night Paket", "Designo"
                } },
                { "CLA Coupe", new[] {
                    "Style", "Urban", "Progressive",
                    "AMG Line", "Edition 1", "Night Paket", "AMG"
                } },
                { "C Serisi Coupe", new[] {
                    "Elegance", "Avantgarde", "Exclusive",
                    "AMG Line", "AMG Line Premium",
                    "Edition 1", "Night Edition", "Night Paket",
                    "Designo", "AMG"
                } },
                { "E Serisi Coupe", new[] {
                    "Elegance", "Avantgarde", "Exclusive",
                    "AMG Line", "AMG Line Premium",
                    "Edition 1", "Night Edition", "Night Paket",
                    "Designo", "AMG"
                } },
                { "S Serisi Coupe", new[] {
                    "AMG Line", "AMG Line Premium", "Exclusive",
                    "Night Paket", "Designo", "AMG"
                } },
                { "AMG GT Coupe", new[] {
                    "AMG GT", "AMG GT S", "AMG GT R", "AMG GT R Pro",
                    "AMG GT Black Series", "Edition 1", "Night Paket"
                } },
                { "C Serisi Cabrio", new[] {
                    "Avantgarde", "Exclusive",
                    "AMG Line", "AMG Line Premium",
                    "Edition 1", "Night Paket", "Designo", "AMG"
                } },
                { "E Serisi Cabrio", new[] {
                    "Avantgarde", "Exclusive",
                    "AMG Line", "AMG Line Premium",
                    "Edition 1", "Night Edition", "Designo", "AMG"
                } },
                { "S Serisi Cabrio", new[] {
                    "AMG Line", "Exclusive",
                    "Night Paket", "Designo", "AMG"
                } },
                { "SL", new[] {
                    "AMG SL 43", "AMG SL 55", "AMG SL 63",
                    "Edition 1", "Night Paket", "Designo"
                } },
                { "SLC", new[] {
                    "Style", "Progressive", "AMG Line",
                    "BlueEfficiency", "Night Paket", "AMG SLC 43"
                } },
                { "SLK", new[] {
                    "BlueEfficiency", "AMG Line",
                    "Edition 1", "Designo", "AMG"
                } },
                { "AMG GT Roadster", new[] {
                    "AMG GT", "AMG GT S", "AMG GT R", "AMG GT C",
                    "Edition 1", "Night Paket"
                } },
                { "C Serisi Estate", new[] {
                    "Elegance", "Avantgarde", "Exclusive",
                    "AMG Line", "AMG Line Premium", "BlueEfficiency",
                    "Night Edition", "Night Paket", "All-Terrain", "AMG"
                } },
                { "E Serisi Estate", new[] {
                    "Elegance", "Avantgarde", "Exclusive",
                    "AMG Line", "AMG Line Premium", "BlueEfficiency",
                    "Night Edition", "Night Paket", "All-Terrain", "Designo", "AMG"
                } },
                { "GLA", new[] {
                    "Style", "Urban", "Progressive",
                    "AMG Line", "Edition 1", "Night Paket", "AMG"
                } },
                { "GLB", new[] {
                    "Style", "Progressive",
                    "AMG Line", "Edition 1", "Night Paket", "AMG"
                } },
                { "GLC", new[] {
                    "Exclusive", "Avantgarde",
                    "AMG Line", "AMG Line Premium",
                    "Edition 1", "Night Paket", "Designo", "AMG"
                } },
                { "GLC Coupe", new[] {
                    "Exclusive", "Avantgarde",
                    "AMG Line", "AMG Line Premium",
                    "Edition 1", "Night Paket", "Designo", "AMG"
                } },
                { "GLE", new[] {
                    "Exclusive", "Avantgarde",
                    "AMG Line", "AMG Line Premium",
                    "Edition 1", "Night Paket", "Designo", "AMG"
                } },
                { "GLE Coupe", new[] {
                    "Exclusive", "AMG Line", "AMG Line Premium",
                    "Edition 1", "Night Paket", "Designo", "AMG"
                } },
                { "GLS", new[] {
                    "AMG Line", "AMG Line Premium", "Exclusive",
                    "Night Paket", "Designo", "Maybach", "AMG"
                } },
                { "EQA", new[] {
                    "Electric Art", "AMG Line", "Progressive",
                    "Night Paket", "Edition 1"
                } },
                { "EQB", new[] {
                    "Electric Art", "AMG Line", "Progressive",
                    "Night Paket", "Edition 1"
                } },
                { "EQC", new[] {
                    "Electric Art", "AMG Line", "Progressive",
                    "Night Paket", "Edition 1", "Edition 1886"
                } },
                { "Vito", new[] {
                    "Base", "Pro", "Select", "Sport",
                    "Tourer Pro", "Tourer Select",
                    "Panel Van", "Mixto"
                } },
                { "V Serisi", new[] {
                    "Style", "Avantgarde", "Exclusive",
                    "AMG Line", "Edition 1", "Marco Polo",
                    "Marco Polo Horizon", "Night Edition"
                } },
                { "Sprinter", new[] {
                    "Panel Van", "Kombi", "Minibüs",
                    "Kamyonet", "Travel 75", "Travel 65"
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
                { "A5 Sportback", new[] {
                    "S Line", "Sport", "Design", "Advanced",
                    "Dynamic", "Business", "Premium", "Launch Edition"
                } },
                { "A7 Sportback", new[] {
                    "S Line", "Design", "Sport", "Advanced",
                    "Business", "Premium", "Launch Edition", "Dynamic"
                } },
                { "A8", new[] {
                    "S Line", "Design", "Sport", "Advanced",
                    "Premium", "Long", "Dynamic"
                } },
                { "A5 Coupe", new[] {
                    "S Line", "Sport", "Design", "Advanced",
                    "Dynamic", "Edition One", "S5"
                } },
                { "TT Coupe", new[] {
                    "S Line", "Sport", "Design",
                    "TTS", "TT RS", "Edition One", "Bronze Selection"
                } },
                { "R8 Coupe", new[] {
                    "V10", "V10 Plus", "V10 Performance",
                    "V10 Performance Quattro", "RWS", "GT",
                    "Decennium", "Green Hell"
                } },
                { "A5 Cabrio", new[] {
                    "S Line", "Sport", "Design", "Advanced",
                    "Dynamic", "S5 Cabrio"
                } },
                { "A3 Cabrio", new[] {
                    "Attraction", "Ambition", "Ambiente",
                    "S Line", "Sport", "Design", "Advanced"
                } },
                { "TT Roadster", new[] {
                    "S Line", "Sport", "Design",
                    "TTS Roadster", "TT RS Roadster", "Edition One"
                } },
                { "R8 Spyder", new[] {
                    "V10", "V10 Plus", "V10 Performance",
                    "V10 Performance Quattro", "RWS", "GT"
                } },
                { "A4 Avant", new[] {
                    "Attraction", "Ambition", "Ambiente",
                    "S Line", "Sport", "Design", "Advanced",
                    "Dynamic", "Business", "Allroad", "S4 Avant"
                } },
                { "A6 Avant", new[] {
                    "S Line", "Design", "Sport", "Advanced",
                    "Business", "Premium", "Dynamic",
                    "Allroad", "S6 Avant", "RS6 Avant"
                } },
                { "Q2", new[] {
                    "S Line", "Sport", "Design", "Advanced",
                    "Dynamic", "Edition One", "Business"
                } },
                { "Q3 Sportback", new[] {
                    "S Line", "Sport", "Design", "Advanced",
                    "Dynamic", "Edition One", "RS Q3 Sportback"
                } },
                { "Q5 Sportback", new[] {
                    "S Line", "Sport", "Design", "Advanced",
                    "Dynamic", "Edition One", "Premium", "SQ5 Sportback"
                } },
                { "Q8 e-tron", new[] {
                    "S Line", "Advanced", "Edition One",
                    "SQ8 e-tron", "Sportback"
                } },
                { "e-tron GT", new[] {
                    "S Line", "Edition One", "RS e-tron GT",
                    "Performance", "Dynamic"
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
                // Hatchback
                { "Civic HB", new[] {
                    "Elegance", "Executive", "Executive Plus",
                    "Sport", "Sport Plus", "Eco", "Eco Elegance",
                    "Dream", "Premium", "RS",
                    "LPG Elegance", "LPG Executive"
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
                { "Honda e", new[] {
                    "Honda e", "Honda e Advance"
                } },

                // Sedan
                { "Civic Sedan", new[] {
                    "Elegance", "Executive", "Executive Plus",
                    "Sport", "Sport Plus", "Eco", "Eco Elegance",
                    "Dream", "Premium", "RS",
                    "LPG Elegance", "LPG Executive"
                } },
                { "City", new[] {
                    "Comfort", "Elegance", "Executive",
                    "S", "V", "Dream"
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

                // SUV
                { "HR-V", new[] {
                    "Comfort", "Elegance", "Executive",
                    "Sport", "Executive Plus", "Advance",
                    "e:HEV Elegance", "e:HEV Advance"
                } },
                { "CR-V", new[] {
                    "Comfort", "Elegance", "Executive",
                    "Lifestyle", "Executive Plus", "Advance", "Sport Line",
                    "e:HEV Elegance", "e:HEV Advance", "e:PHEV Advance"
                } },
                { "ZR-V", new[] {
                    "Elegance", "Executive", "Advance",
                    "Sport", "e:HEV Elegance", "e:HEV Advance", "e:HEV Sport"
                } },
                { "e:Ny1", new[] {
                    "Elegance", "Advance"
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
                { "IONIQ HB", new[] {
                    "Style", "Elite", "Prime", "Premium",
                    "Hybrid", "Plug-in Hybrid", "Electric"
                } },
                { "IONIQ 6", new[] {
                    "Style", "Prime", "N Line", "Vertex",
                    "Top", "Inspiration",
                    "Long Range", "Standard Range"
                } },
                { "Sonata", new[] {
                    "Style", "Style Plus", "Prime", "Elite",
                    "Premium", "Executive", "Comfort", "Design"
                } },
                { "Accent", new[] {
                    "Mode", "Team", "Style", "Prime",
                    "Elite", "Era", "Active", "Blue",
                    "Cool", "Comfort"
                } },
                { "Accent Blue", new[] {
                    "Mode", "Team", "Style", "Prime",
                    "Elite", "Era", "Active",
                    "Cool", "Comfort", "Jump"
                } },
                { "Kona", new[] {
                    "Style", "Style Plus", "Prime", "Elite",
                    "N Line", "Comfort", "Jump", "Design",
                    "Premium", "Adventure"
                } },
                { "Kona Electric", new[] {
                    "Style", "Prime", "Elite", "Premium",
                    "Long Range", "Standard Range", "N Line"
                } },
                { "Santa Fe", new[] {
                    "Style", "Style Plus", "Prime", "Elite",
                    "N Line", "Premium", "Executive",
                    "Comfort", "Design", "Adventure",
                    "Calligraphy"
                } },
                { "ix35", new[] {
                    "Style", "Style Plus", "Prime", "Elite",
                    "Comfort", "Jump", "Team",
                    "Executive", "Premium"
                } },
                { "Venue", new[] {
                    "Style", "Style Plus", "Prime", "Elite",
                    "Comfort", "Jump", "Team", "Design"
                } },
                { "i20 Active", new[] {
                    "Style", "Style Plus", "Prime", "Elite",
                    "Comfort", "Jump", "Team"
                } },
                { "Staria", new[] {
                    "Style", "Prime", "Elite", "Premium",
                    "Executive", "Calligraphy",
                    "9 Kişilik", "5 Kişilik"
                } },
                { "H-1", new[] {
                    "Style", "Prime", "Elite", "Premium",
                    "Executive", "8+1 Kişilik", "Panel Van"
                } },

                // ==================== Renault ====================
                // Hatchback
                { "Clio", new[] {
                    "Joy", "Touch", "Icon", "Bose",
                    "R.S. Line", "Intens", "Zen", "Life",
                    "Techno", "Equilibre", "Evolution", "Iconic",
                    "Initiale", "Limited", "S Edition"
                } },
                { "Megane HB", new[] {
                    "Joy", "Touch", "Icon", "Bose",
                    "R.S. Line", "Intens", "Zen", "Life",
                    "Techno", "Equilibre", "Evolution", "Iconic"
                } },
                { "Zoe", new[] {
                    "Life", "Zen", "Intens", "Iconic",
                    "R110", "R135", "Edition One", "Riviera"
                } },

                // Sedan
                { "Megane Sedan", new[] {
                    "Joy", "Touch", "Icon", "Bose",
                    "R.S. Line", "Intens", "Zen", "Life",
                    "Techno", "Equilibre", "Evolution", "Iconic"
                } },
                { "Fluence", new[] {
                    "Joy", "Touch", "Icon", "Privilege",
                    "Extreme", "Business", "Expression",
                    "Dynamique"
                } },
                { "Talisman", new[] {
                    "Joy", "Touch", "Icon", "Initiale Paris",
                    "Intens", "Zen", "Life", "Techno"
                } },
                { "Symbol", new[] {
                    "Joy", "Touch", "Expression",
                    "Authentique", "Dynamique"
                } },
                { "Latitude", new[] {
                    "Expression", "Dynamique", "Privilege",
                    "Executive", "Initiale", "Business"
                } },

                // Station Wagon
                { "Megane Sport Tourer", new[] {
                    "Joy", "Touch", "Icon", "Bose",
                    "R.S. Line", "Intens", "Zen", "Life",
                    "Techno", "Equilibre", "Evolution", "Iconic"
                } },

                // Crossover
                { "Arkana", new[] {
                    "Joy", "Touch", "Icon", "Bose",
                    "R.S. Line", "Intens",
                    "Techno", "Equilibre", "Evolution", "Iconic",
                    "Esprit Alpine"
                } },

                // SUV
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
                { "Koleos", new[] {
                    "Joy", "Touch", "Icon", "Bose",
                    "Initiale Paris", "Intens", "Zen", "Life"
                } },

                // Minivan & Panelvan
                { "Kangoo", new[] {
                    "Joy", "Touch", "Zen", "Intens",
                    "Life", "Equilibre", "Techno",
                    "Multix", "Multix Joy", "Multix Touch"
                } },
                { "Scenic", new[] {
                    "Joy", "Touch", "Icon", "Bose",
                    "Intens", "Zen", "Life", "Techno",
                    "Initiale Paris", "Limited"
                } },
                { "Grand Scenic", new[] {
                    "Joy", "Touch", "Icon", "Bose",
                    "Intens", "Zen", "Life", "Techno",
                    "Initiale Paris"
                } },
                { "Kangoo Express", new[] {
                    "Joy", "Confort", "Maxi Joy", "Maxi Confort",
                    "Compact", "Extra"
                } },
                { "Master", new[] {
                    "L1H1", "L2H2", "L3H2",
                    "Confort", "Grand Confort",
                    "Extra", "Business"
                } },
                { "Trafic", new[] {
                    "Joy", "Confort", "Grand Confort",
                    "Grand Trafic", "SpaceClass",
                    "Extra", "Business"
                } },

                // ==================== Kia ====================
                // Hatchback
                { "Picanto", new[] {
                    "Cool", "Comfort", "Elegance", "Premium",
                    "Concept", "Concept Plus", "Dream", "Style",
                    "GT-Line", "X-Line"
                } },
                { "Rio", new[] {
                    "Cool", "Comfort", "Elegance", "Premium",
                    "Concept", "Concept Plus", "Dream", "Style",
                    "GT-Line"
                } },
                { "Ceed", new[] {
                    "Cool", "Comfort", "Elegance", "Premium",
                    "Concept", "Concept Plus", "Dream", "GT-Line",
                    "Style", "Business Line"
                } },
                { "ProCeed", new[] {
                    "Comfort", "Elegance", "Premium",
                    "GT-Line", "GT"
                } },

                // Sedan
                { "Cerato", new[] {
                    "Cool", "Comfort", "Elegance", "Premium",
                    "Concept", "Concept Plus", "Dream", "GT-Line",
                    "Style"
                } },
                { "Stinger", new[] {
                    "Comfort", "Elegance", "Premium",
                    "GT-Line", "GT"
                } },
                { "K5", new[] {
                    "Comfort", "Elegance", "Premium",
                    "GT-Line", "GT", "Style"
                } },

                // Station Wagon
                { "Ceed SW", new[] {
                    "Cool", "Comfort", "Elegance", "Premium",
                    "Concept", "Concept Plus", "Dream", "GT-Line",
                    "Style", "Business Line"
                } },

                // Crossover
                { "XCeed", new[] {
                    "Cool", "Comfort", "Elegance", "Premium",
                    "Concept", "Concept Plus", "Dream", "GT-Line",
                    "Style"
                } },

                // SUV
                { "Sportage", new[] {
                    "Cool", "Comfort", "Elegance", "Premium",
                    "Concept", "Concept Plus", "Dream", "GT-Line",
                    "Style", "Business Line"
                } },
                { "Sorento", new[] {
                    "Cool", "Comfort", "Elegance", "Premium",
                    "Concept", "Concept Plus", "Dream", "GT-Line",
                    "Style", "Business Line"
                } },
                { "Niro", new[] {
                    "Cool", "Comfort", "Elegance", "Premium",
                    "Concept", "Dream", "Style"
                } },
                { "Stonic", new[] {
                    "Cool", "Comfort", "Elegance", "Premium",
                    "Concept", "Concept Plus", "Dream", "GT-Line",
                    "Style"
                } },

                // Elektrikli SUV
                { "EV6", new[] {
                    "Air", "Wind", "Earth", "GT-Line",
                    "GT", "Long Range", "Standard Range"
                } },
                { "EV9", new[] {
                    "Air", "Earth", "GT-Line",
                    "GT", "Long Range"
                } },
                { "Niro EV", new[] {
                    "Cool", "Comfort", "Elegance", "Premium",
                    "Concept", "Dream", "Style",
                    "Long Range"
                } },

                // ==================== Land Rover ====================
                { "Range Rover", new[] {
                    "Vogue", "HSE", "Autobiography",
                    "Autobiography Long Wheelbase", "SV Autobiography",
                    "SVAutobiography Dynamic", "First Edition", "SE"
                } },
                { "Range Rover Sport", new[] {
                    "S", "SE", "HSE", "HSE Dynamic",
                    "Autobiography Dynamic", "SVR", "First Edition",
                    "Dynamic SE", "Dynamic HSE"
                } },
                { "Range Rover Velar", new[] {
                    "S", "SE", "HSE",
                    "R-Dynamic S", "R-Dynamic SE", "R-Dynamic HSE",
                    "First Edition", "SVAutobiography Dynamic Edition"
                } },
                { "Range Rover Evoque", new[] {
                    "S", "SE", "HSE",
                    "R-Dynamic S", "R-Dynamic SE", "R-Dynamic HSE",
                    "First Edition", "Autobiography", "Dynamic"
                } },
                { "Discovery", new[] {
                    "S", "SE", "HSE", "HSE Luxury",
                    "Landmark Edition", "First Edition",
                    "R-Dynamic S", "R-Dynamic SE", "R-Dynamic HSE",
                    "Metropolitan Edition"
                } },
                { "Discovery Sport", new[] {
                    "S", "SE", "HSE", "HSE Luxury",
                    "R-Dynamic S", "R-Dynamic SE", "R-Dynamic HSE",
                    "Landmark Edition", "First Edition"
                } },
                { "Defender", new[] {
                    "S", "SE", "HSE",
                    "X-Dynamic S", "X-Dynamic SE", "X-Dynamic HSE",
                    "X", "V8", "First Edition", "75th Limited Edition"
                } },
                { "Freelander 2", new[] {
                    "S", "SE", "HSE", "HSE Luxury",
                    "Dynamic", "SD4 HSE", "TD4 SE"
                } },

                // ==================== BYD ====================
                { "Dolphin", new[] {
                    "Comfort", "Design", "Boost",
                    "Active", "Freedom", "Glory"
                } },
                { "Seal", new[] {
                    "Comfort", "Design", "Excellence",
                    "AWD", "Performance", "Premium"
                } },
                { "Han", new[] {
                    "EV Glory Edition", "EV Premium",
                    "EV Flagship", "EV AWD",
                    "DM-i Premium", "DM-i Flagship"
                } },
                { "Atto 3", new[] {
                    "Comfort", "Design", "Active",
                    "Boost", "Award", "Excellence"
                } },
                { "Seal U", new[] {
                    "Comfort", "Design", "Excellence",
                    "Premium", "DM-i Comfort", "DM-i Design",
                    "DM-i Excellence"
                } },
                { "Tang", new[] {
                    "EV Glory Edition", "EV Flagship",
                    "EV AWD", "DM-i Premium",
                    "DM-i Flagship"
                } },

                // ==================== Chery ====================
                { "Arrizo 5", new[] {
                    "Comfort", "Luxury", "Elite",
                    "Premium", "Sport"
                } },
                { "Tiggo 4 Pro", new[] {
                    "Comfort", "Luxury", "Elite",
                    "Premium", "Distinction"
                } },
                { "Tiggo 7 Pro", new[] {
                    "Comfort", "Luxury", "Elite",
                    "Premium", "Distinction", "Max"
                } },
                { "Tiggo 8 Pro", new[] {
                    "Comfort", "Luxury", "Elite",
                    "Premium", "Distinction", "Max",
                    "Champion"
                } },
                { "Omoda 5", new[] {
                    "Comfort", "Luxury", "Elite",
                    "Premium", "Distinction", "Sport"
                } },

                // ==================== Nissan ====================
                { "Micra", new[] {
                    "Visia", "Acenta", "Tekna", "N-Sport",
                    "N-Connecta", "Bose Personal Edition",
                    "IG-T Visia", "IG-T Acenta"
                } },
                { "Note", new[] {
                    "Visia", "Acenta", "Tekna", "N-Tec",
                    "Comfort", "Premium"
                } },
                { "Pulsar", new[] {
                    "Visia", "Acenta", "Tekna", "N-Connecta",
                    "Business Edition"
                } },
                { "Leaf", new[] {
                    "Visia", "Acenta", "Tekna", "N-Connecta",
                    "e+", "e+ Tekna", "e+ N-Tec"
                } },
                { "Juke", new[] {
                    "Visia", "Acenta", "Tekna", "N-Connecta",
                    "N-Design", "N-Sport", "Bose Personal Edition",
                    "Premiere Edition", "Enigma"
                } },
                { "Qashqai", new[] {
                    "Visia", "Acenta", "Tekna", "Tekna+",
                    "N-Connecta", "N-Design", "N-Sport",
                    "Premiere Edition", "Business Edition",
                    "e-Power Acenta", "e-Power Tekna"
                } },
                { "X-Trail", new[] {
                    "Visia", "Acenta", "Tekna", "Tekna+",
                    "N-Connecta", "N-Design",
                    "Premiere Edition", "Adventure",
                    "e-Power Acenta", "e-Power Tekna",
                    "e-4orce Tekna"
                } },
                { "Navara", new[] {
                    "Visia", "Acenta", "Tekna", "N-Guard",
                    "N-Connecta", "Off-Roader AT32",
                    "King Cab", "Double Cab", "PRO-4X"
                } },
                { "NV300", new[] {
                    "Visia", "Acenta", "Comfort",
                    "Panel Van", "Kombi", "L1H1", "L2H1"
                } },
                { "NV400", new[] {
                    "Visia", "Acenta", "Comfort",
                    "Panel Van", "Kamyonet", "L2H2", "L3H2"
                } },

                // ==================== Fiat ====================
                // Hatchback
                { "Punto", new[] {
                    "Active", "Dynamic", "Emotion", "Sporting",
                    "Easy", "Pop", "Lounge", "S", "Street",
                    "1.2", "1.3 Multijet", "1.4"
                } },
                { "Grande Punto", new[] {
                    "Active", "Dynamic", "Emotion", "Sporting",
                    "1.2", "1.3 Multijet", "1.4", "1.4 T-Jet"
                } },
                { "Punto Evo", new[] {
                    "Active", "Dynamic", "Emotion", "Sporting",
                    "MyLife", "1.2", "1.3 Multijet", "1.4"
                } },
                { "Bravo", new[] {
                    "Active", "Dynamic", "Emotion", "Sport",
                    "1.4", "1.4 T-Jet", "1.6 Multijet"
                } },
                { "Tipo HB", new[] {
                    "Easy", "Lounge", "Sport", "Cross",
                    "City Life", "Life", "Club",
                    "1.4", "1.3 Multijet", "1.6 Multijet",
                    "Mirror", "Street", "S-Design"
                } },
                { "Palio", new[] {
                    "Active", "Dynamic", "Emotion",
                    "Sole", "Weekend", "1.2", "1.3 Multijet"
                } },
                { "Stilo", new[] {
                    "Active", "Dynamic", "Emotion", "Actual",
                    "Schumacher", "1.4", "1.6", "1.9 JTD"
                } },
                { "500", new[] {
                    "Pop", "Lounge", "Sport", "Icon",
                    "Rockstar", "Star", "Dolcevita",
                    "Club", "Anniversario", "Collezione",
                    "1.0 Hybrid", "1.2", "0.9 TwinAir",
                    "Abarth 595", "Abarth 695"
                } },
                { "500e", new[] {
                    "Action", "Passion", "Icon", "La Prima",
                    "RED", "Inspired By", "Giorgio Armani"
                } },
                { "Fiat 600e", new[] {
                    "RED", "La Prima", "Elettrica"
                } },
                // Sedan
                { "Linea", new[] {
                    "Active", "Active Plus", "Dynamic",
                    "Emotion", "Actual", "Pop", "Easy", "Lounge",
                    "1.3 Multijet", "1.4", "1.4 T-Jet", "1.6 Multijet"
                } },
                { "Albea", new[] {
                    "Active", "Dynamic", "Emotion", "Sole",
                    "1.2", "1.3 Multijet", "1.4"
                } },
                { "Tipo Sedan", new[] {
                    "Easy", "Lounge", "Sport", "Cross",
                    "City Life", "Life", "Club",
                    "1.4", "1.3 Multijet", "1.6 Multijet",
                    "Mirror", "Street", "S-Design"
                } },
                { "Marea", new[] {
                    "SX", "ELX", "HLX", "Sporting",
                    "1.6", "1.9 JTD", "2.0 HLX"
                } },
                // Cabrio
                { "500C", new[] {
                    "Pop", "Lounge", "Sport", "Icon",
                    "Rockstar", "Star", "Dolcevita",
                    "Club", "Collezione",
                    "1.0 Hybrid", "1.2", "0.9 TwinAir"
                } },
                // Station Wagon
                { "Tipo Station Wagon", new[] {
                    "Easy", "Lounge", "Sport", "Cross",
                    "City Life", "Life", "Club",
                    "1.4", "1.3 Multijet", "1.6 Multijet",
                    "Mirror", "Street", "S-Design"
                } },
                { "Marea Weekend", new[] {
                    "SX", "ELX", "HLX", "Sporting",
                    "1.6", "1.9 JTD", "2.0 HLX"
                } },
                // Crossover
                { "500X Cross", new[] {
                    "Pop", "Pop Star", "Lounge", "Cross",
                    "Cross Plus", "Sport", "Club",
                    "City Cross", "1.0 T3", "1.3 T4", "1.6 Multijet"
                } },
                // SUV
                { "500X", new[] {
                    "Pop", "Pop Star", "Lounge", "Cross",
                    "Cross Plus", "Sport", "Club",
                    "City Cross", "Dolcevita",
                    "1.0 T3", "1.3 T4", "1.6 Multijet"
                } },
                { "Freemont", new[] {
                    "Urban", "Lounge", "Cross", "AWD",
                    "2.0 Multijet", "Black Code"
                } },
                // Pickup
                { "Fullback", new[] {
                    "SX", "LX", "Cross", "Adventure",
                    "Single Cab", "Double Cab", "2.4D"
                } },
                // Minivan/Ticari
                { "Doblo", new[] {
                    "Active", "Dynamic", "Emotion",
                    "Easy", "Lounge", "Safeline",
                    "Cargo", "Maxi", "Premio",
                    "1.3 Multijet", "1.6 Multijet"
                } },
                { "Fiorino", new[] {
                    "Cargo", "Panorama", "Combi",
                    "Pop", "Easy", "Premio", "Safeline",
                    "1.3 Multijet", "1.4"
                } },
                { "Ducato", new[] {
                    "Cargo", "Panorama", "Kombi",
                    "Maxi", "Van", "Kamyonet",
                    "L1H1", "L2H1", "L2H2", "L3H2", "L4H2",
                    "2.3 Multijet", "Multijet II"
                } },
                { "Scudo", new[] {
                    "Cargo", "Panorama", "Kombi",
                    "L1H1", "L2H1",
                    "1.6 Multijet", "2.0 Multijet"
                } },
                { "Doblo Combi", new[] {
                    "Active", "Dynamic", "Emotion",
                    "Easy", "Lounge", "Safeline", "Premio",
                    "1.3 Multijet", "1.6 Multijet"
                } },
                { "Fiorino Combi", new[] {
                    "Pop", "Easy", "Premio", "Safeline",
                    "Combi", "1.3 Multijet", "1.4"
                } },
                { "Egea MultiWagon", new[] {
                    "Easy", "Lounge", "Cross", "Sport",
                    "City Life", "Life", "Club",
                    "1.4", "1.3 Multijet", "1.6 Multijet",
                    "Mirror", "S-Design"
                } },

                // ==================== SKODA ====================
                { "Fabia", new[] { "Active", "Ambition", "Style", "Monte Carlo", "Comfort", "1.0 MPI", "1.0 TSI", "1.5 TSI" } },
                { "Scala", new[] { "Active", "Ambition", "Style", "Monte Carlo", "1.0 TSI", "1.5 TSI" } },
                { "Rapid", new[] { "Active", "Ambition", "Style", "Monte Carlo", "Elegance", "Joy", "1.0 TSI", "1.4 TSI", "1.6 TDI" } },
                { "Octavia", new[] { "Active", "Ambition", "Style", "Elegance", "L&K", "RS", "Scout", "Joy", "Premium", "1.0 TSI", "1.4 TSI", "1.5 TSI", "2.0 TSI", "1.6 TDI", "2.0 TDI" } },
                { "Superb", new[] { "Active", "Ambition", "Style", "Elegance", "L&K", "SportLine", "Scout", "Premium", "1.5 TSI", "2.0 TSI", "1.6 TDI", "2.0 TDI" } },
                { "Octavia Combi", new[] { "Active", "Ambition", "Style", "Elegance", "L&K", "RS", "Scout", "Premium", "1.0 TSI", "1.5 TSI", "2.0 TSI", "1.6 TDI", "2.0 TDI" } },
                { "Superb Combi", new[] { "Active", "Ambition", "Style", "Elegance", "L&K", "SportLine", "Scout", "Premium", "1.5 TSI", "2.0 TSI", "1.6 TDI", "2.0 TDI" } },
                { "Kamiq", new[] { "Active", "Ambition", "Style", "Monte Carlo", "1.0 TSI", "1.5 TSI" } },
                { "Karoq", new[] { "Active", "Ambition", "Style", "SportLine", "Scout", "1.0 TSI", "1.5 TSI", "2.0 TSI", "1.6 TDI", "2.0 TDI" } },
                { "Kodiaq", new[] { "Active", "Ambition", "Style", "L&K", "SportLine", "Scout", "RS", "1.5 TSI", "2.0 TSI", "2.0 TDI" } },

                // ==================== OPEL ====================
                { "Corsa", new[] { "Essentia", "Edition", "Elegance", "GS Line", "Ultimate", "1.2", "1.2 Turbo", "1.5 Dizel", "1.4", "1.4 Turbo" } },
                { "Corsa-e", new[] { "Edition", "Elegance", "GS Line", "Ultimate" } },
                { "Astra", new[] { "Essentia", "Edition", "Elegance", "GS Line", "Ultimate", "1.2 Turbo", "1.4 Turbo", "1.5 Dizel", "1.6 Turbo" } },
                { "Astra HB", new[] { "Essentia", "Edition", "Elegance", "GS Line", "Ultimate", "1.2 Turbo", "1.4 Turbo", "1.5 Dizel" } },
                { "Astra Sedan", new[] { "Essentia", "Edition", "Elegance", "Design", "Sport", "Cosmo", "1.4", "1.4 Turbo", "1.6", "1.6 CDTI" } },
                { "Insignia", new[] { "Essentia", "Edition", "Elegance", "GS Line", "Ultimate", "Sport", "Cosmo", "1.5 Turbo", "1.6 CDTI", "2.0 CDTI", "2.0 Turbo" } },
                { "Astra Sports Tourer", new[] { "Essentia", "Edition", "Elegance", "GS Line", "Ultimate", "1.2 Turbo", "1.5 Dizel" } },
                { "Insignia Sports Tourer", new[] { "Essentia", "Edition", "Elegance", "GS Line", "Ultimate", "1.5 Turbo", "1.6 CDTI", "2.0 CDTI" } },
                { "Mokka", new[] { "Essentia", "Edition", "Elegance", "GS Line", "Ultimate", "1.2 Turbo", "1.5 Dizel" } },
                { "Mokka-e", new[] { "Edition", "Elegance", "GS Line", "Ultimate" } },
                { "Grandland", new[] { "Essentia", "Edition", "Elegance", "GS Line", "Ultimate", "1.2 Turbo", "1.5 Dizel", "1.6 Turbo", "Hybrid" } },
                { "Crossland", new[] { "Essentia", "Edition", "Elegance", "GS Line", "Ultimate", "1.2 Turbo", "1.5 Dizel" } },
                { "Combo", new[] { "Essentia", "Edition", "Elegance", "Cargo", "L1", "L2", "1.5 Dizel" } },
                { "Combo Life", new[] { "Essentia", "Edition", "Elegance", "1.2 Turbo", "1.5 Dizel" } },
                { "Vivaro", new[] { "Essentia", "Edition", "Cargo", "Kombi", "L1H1", "L2H1", "1.5 Dizel", "2.0 Dizel" } },
                { "Movano", new[] { "Essentia", "Edition", "Cargo", "Kamyonet", "L2H2", "L3H2", "L4H3", "2.2 Dizel" } },

                // ==================== PEUGEOT ====================
                // Hatchback
                { "208", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "Style", "Like", "Roadtrip", "e-208", "1.2 PureTech", "1.5 BlueHDi" } },
                { "308", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "Style", "Roadtrip", "1.2 PureTech", "1.5 BlueHDi", "PHEV" } },
                // Sedan
                { "301", new[] { "Active", "Allure", "Access", "1.2 PureTech", "1.5 BlueHDi", "1.6 HDi", "1.6 VTi" } },
                { "508", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "First Edition", "1.5 BlueHDi", "1.6 PureTech", "2.0 BlueHDi", "PHEV" } },
                // Station Wagon
                { "308 SW", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "1.2 PureTech", "1.5 BlueHDi", "PHEV" } },
                { "508 SW", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "First Edition", "1.5 BlueHDi", "1.6 PureTech", "2.0 BlueHDi", "PHEV" } },
                // SUV
                { "2008", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "Style", "Roadtrip", "e-2008", "1.2 PureTech", "1.5 BlueHDi" } },
                { "3008", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "Style", "Roadtrip", "1.2 PureTech", "1.5 BlueHDi", "1.6 PureTech", "PHEV", "PHEV4" } },
                { "5008", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "Style", "1.2 PureTech", "1.5 BlueHDi", "1.6 PureTech", "PHEV" } },
                // Minivan/Ticari
                { "Boxer", new[] { "Premium", "Pro", "Avantage", "L1H1", "L2H2", "L3H2", "L4H3", "2.2 BlueHDi", "Kamyonet", "Cargo" } },
                { "Rifter", new[] { "Active", "Allure", "GT Line", "Style", "1.2 PureTech", "1.5 BlueHDi", "Long" } },
                { "Partner", new[] { "Active", "Allure", "Premium", "Tepee", "1.6 HDi", "1.6 VTi", "Cargo" } },
                { "Expert", new[] { "Premium", "Pro", "Avantage", "L1", "L2", "L3", "1.5 BlueHDi", "2.0 BlueHDi", "Cargo", "Kombi" } },
                { "Traveller", new[] { "Active", "Allure", "Business", "VIP", "L2", "L3", "1.5 BlueHDi", "2.0 BlueHDi" } },
                { "108", new[] { "Active", "Allure", "Style", "Top!", "1.0 VTi", "1.2 PureTech" } },
                { "206", new[] { "XR", "XS", "XT", "Premium", "Quiksilver", "1.4", "1.4 HDi", "1.6", "1.6 HDi", "2.0 HDi", "CC", "SW" } },
                { "207", new[] { "Comfort", "Premium", "Sportium", "Envy", "1.4", "1.4 HDi", "1.6", "1.6 HDi", "1.6 THP", "CC", "SW" } },
                { "307", new[] { "XR", "XS", "XT", "Premium", "Sportium", "1.4", "1.4 HDi", "1.6", "1.6 HDi", "2.0", "2.0 HDi" } },
                { "407", new[] { "Comfort", "Premium", "Executive", "Feline", "ST", "1.6 HDi", "2.0", "2.0 HDi", "2.2", "2.2 HDi", "3.0 V6" } },
                { "408", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "1.2 PureTech", "1.5 BlueHDi", "PHEV" } },
                { "e-208", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "50 kWh" } },
                { "e-2008", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "50 kWh", "54 kWh" } },
                { "307 SW", new[] { "XR", "XS", "XT", "Premium", "1.6", "1.6 HDi", "2.0", "2.0 HDi" } },
                { "407 SW", new[] { "Comfort", "Premium", "Executive", "Feline", "1.6 HDi", "2.0", "2.0 HDi", "2.2", "2.2 HDi" } },
                { "Bipper", new[] { "Comfort", "Premium", "Tepee", "1.3 HDi", "1.4 HDi", "Cargo" } },

                // ==================== SEAT ====================
                // Hatchback
                { "Ibiza", new[] { "Reference", "Style", "Xcellence", "FR", "FR Sport", "Cupra", "1.0 TSI", "1.0 EcoTSI", "1.5 TSI", "1.6 TDI" } },
                { "Leon", new[] { "Reference", "Style", "Xcellence", "FR", "FR Sport", "Cupra", "1.0 TSI", "1.4 TSI", "1.5 TSI", "2.0 TSI", "1.6 TDI", "2.0 TDI", "e-Hybrid" } },
                { "Leon HB", new[] { "Reference", "Style", "Xcellence", "FR", "FR Sport", "1.0 TSI", "1.5 TSI", "2.0 TSI", "1.6 TDI" } },
                // Sedan
                { "Toledo", new[] { "Reference", "Style", "Xcellence", "1.0 TSI", "1.4 TSI", "1.6 TDI", "1.2 TSI" } },
                // Station Wagon
                { "Leon ST", new[] { "Reference", "Style", "Xcellence", "FR", "FR Sport", "Cupra", "1.0 TSI", "1.5 TSI", "2.0 TSI", "1.6 TDI", "2.0 TDI" } },
                // Crossover
                { "Arona", new[] { "Reference", "Style", "Xcellence", "FR", "FR Sport", "1.0 TSI", "1.0 EcoTSI", "1.5 TSI", "1.6 TDI" } },
                // SUV
                { "Ateca", new[] { "Reference", "Style", "Xcellence", "FR", "FR Sport", "Cupra", "1.0 TSI", "1.5 TSI", "2.0 TSI", "1.6 TDI", "2.0 TDI" } },
                { "Tarraco", new[] { "Reference", "Style", "Xcellence", "FR", "FR Sport", "1.5 TSI", "2.0 TSI", "2.0 TDI", "e-Hybrid" } },
                // Minivan
                { "Alhambra", new[] { "Reference", "Style", "Xcellence", "FR", "1.4 TSI", "2.0 TSI", "2.0 TDI" } },

                // ==================== CUPRA ====================
                { "Born", new[] { "V1", "V2", "V3", "VZ", "58 kWh", "77 kWh", "e-Boost" } },
                { "Leon Sportstourer", new[] { "VZ", "VZe", "1.4 e-Hybrid", "1.5 TSI", "2.0 TSI" } },
                { "Formentor", new[] { "V1", "V2", "VZ", "VZe", "VZ5", "1.5 TSI", "2.0 TSI", "1.4 e-Hybrid", "2.5 TSI" } },
                { "Terramar", new[] { "V1", "V2", "VZ", "VZe", "1.5 TSI", "2.0 TSI", "1.4 e-Hybrid" } },

                // ==================== TOGG ====================
                { "T10F", new[] { "Standart Menzil", "Uzun Menzil" } },
                { "T10X", new[] { "Standart Menzil", "Uzun Menzil" } },

                // ==================== CITROEN ====================
                // Hatchback
                { "C3", new[] { "Live", "Feel", "Feel Pack", "Shine", "Shine Pack", "C-Series", "You!", "Max", "1.2 PureTech", "1.5 BlueHDi" } },
                { "C4", new[] { "Live", "Feel", "Feel Pack", "Shine", "Shine Pack", "C-Series", "e-C4", "1.2 PureTech", "1.5 BlueHDi" } },
                { "C4 X", new[] { "Feel", "Feel Pack", "Shine", "Shine Pack", "e-C4 X", "1.2 PureTech", "1.5 BlueHDi" } },
                // Sedan
                { "C-Elysee", new[] { "Live", "Feel", "Shine", "Exclusive", "1.2 PureTech", "1.5 BlueHDi", "1.6 HDi", "1.6 VTi" } },
                { "C4 Sedan", new[] { "Live", "Feel", "Shine", "Exclusive", "1.6 HDi", "1.6 VTi", "1.6 THP" } },
                // Station Wagon
                { "C5 Tourer", new[] { "Attraction", "Confort", "Exclusive", "1.6 HDi", "1.6 THP", "2.0 HDi", "2.0 BlueHDi" } },
                // SUV
                { "C3 Aircross", new[] { "Live", "Feel", "Feel Pack", "Shine", "Shine Pack", "C-Series", "1.2 PureTech", "1.5 BlueHDi" } },
                { "C5 Aircross", new[] { "Live", "Feel", "Feel Pack", "Shine", "Shine Pack", "C-Series", "1.2 PureTech", "1.5 BlueHDi", "1.6 PureTech", "Hybrid", "Plug-in Hybrid" } },
                // Minivan/Ticari
                { "Berlingo", new[] { "Live", "Feel", "Shine", "XL", "M", "1.2 PureTech", "1.5 BlueHDi", "Cargo" } },
                { "SpaceTourer", new[] { "Live", "Feel", "Shine", "Business", "M", "XL", "1.5 BlueHDi", "2.0 BlueHDi" } },
                { "Jumpy", new[] { "Pro", "Business", "Club", "M", "XL", "XS", "1.5 BlueHDi", "2.0 BlueHDi", "Cargo", "Kombi" } },
                { "Jumper", new[] { "Pro", "Business", "Club", "L1H1", "L2H2", "L3H2", "L4H3", "2.2 BlueHDi", "Cargo", "Kamyonet" } },

                // ==================== JAGUAR ====================
                { "XE", new[] { "S", "SE", "HSE", "R-Dynamic S", "R-Dynamic SE", "R-Dynamic HSE", "R-Sport", "Portfolio", "P250", "P300", "D200" } },
                { "XF", new[] { "S", "SE", "HSE", "R-Dynamic S", "R-Dynamic SE", "R-Dynamic HSE", "R-Sport", "Portfolio", "P250", "P300", "D200" } },
                { "F-Type Coupe", new[] { "S", "R", "R-Dynamic", "SVR", "P300", "P380", "P450", "P575", "First Edition", "75" } },
                { "F-Type Cabrio", new[] { "S", "R", "R-Dynamic", "SVR", "P300", "P380", "P450", "P575", "First Edition", "75" } },
                { "F-Type Roadster", new[] { "S", "R", "R-Dynamic", "SVR", "P300", "P380", "P450", "P575", "First Edition" } },
                { "F-Pace", new[] { "S", "SE", "HSE", "R-Dynamic S", "R-Dynamic SE", "R-Dynamic HSE", "SVR", "P250", "P400", "P400e", "D200" } },
                { "E-Pace", new[] { "S", "SE", "HSE", "R-Dynamic S", "R-Dynamic SE", "R-Dynamic HSE", "P200", "P250", "P300", "D165", "D200" } },
                { "I-Pace", new[] { "S", "SE", "HSE", "R-Dynamic S", "R-Dynamic SE", "R-Dynamic HSE", "EV320", "EV400", "Black", "First Edition" } },

                // ==================== LEXUS ====================
                { "CT", new[] { "Executive", "Luxury", "F Sport", "200h" } },
                { "IS", new[] { "Executive", "Luxury", "F Sport", "300h", "350", "500 F" } },
                { "ES", new[] { "Executive", "Luxury", "F Sport", "250", "300h", "350" } },
                { "GS", new[] { "Executive", "Luxury", "F Sport", "300h", "350", "450h" } },
                { "LS", new[] { "Executive", "Luxury", "F Sport", "500h", "350" } },
                { "RC", new[] { "Luxury", "F Sport", "300h", "350", "F" } },
                { "LC", new[] { "Luxury", "Sport", "Sport+", "500", "500h" } },
                { "LC Cabrio", new[] { "Luxury", "Sport", "Sport+", "500" } },
                { "NX", new[] { "Executive", "Luxury", "F Sport", "250", "300h", "350h", "450h+" } },
                { "RX", new[] { "Executive", "Luxury", "F Sport", "350", "350h", "450h", "450h+", "500h" } },
                { "UX", new[] { "Executive", "Luxury", "F Sport", "250h", "300e" } },
                { "LX", new[] { "Executive", "Luxury", "F Sport", "600" } },

                // ==================== MASERATI ====================
                { "Ghibli", new[] { "GT", "Modena", "Trofeo", "S", "S Q4", "Diesel", "GranSport", "GranLusso", "Ribelle", "F Tributo" } },
                { "Quattroporte", new[] { "GT", "Modena", "Trofeo", "S", "S Q4", "GranSport", "GranLusso", "Diesel" } },
                { "MC20", new[] { "Cielo", "GT2", "Icona", "Leggera" } },
                { "GranTurismo", new[] { "Modena", "Trofeo", "Folgore", "Sport", "S", "MC" } },
                { "GranCabrio", new[] { "Modena", "Trofeo", "Folgore", "Sport", "MC" } },
                { "MC20 Cielo", new[] { "Standart", "PrimaSerie", "Icona" } },
                { "Levante", new[] { "GT", "Modena", "Trofeo", "S", "GranSport", "GranLusso", "Diesel", "Hybrid" } },
                { "Grecale", new[] { "GT", "Modena", "Trofeo", "Folgore", "PrimaSerie" } },

                // ==================== PORSCHE ====================
                { "Panamera", new[] { "4", "4S", "GTS", "Turbo", "Turbo S", "4 E-Hybrid", "4S E-Hybrid", "Turbo S E-Hybrid", "Executive", "Sport Turismo" } },
                { "Taycan", new[] { "4S", "GTS", "Turbo", "Turbo S", "Cross Turismo", "Sport Turismo", "Base", "Performance Battery", "Performance Battery Plus" } },
                { "911", new[] { "Carrera", "Carrera S", "Carrera 4", "Carrera 4S", "Carrera T", "GTS", "GT3", "GT3 RS", "GT3 Touring", "Turbo", "Turbo S", "Sport Classic", "Dakar" } },
                { "718 Cayman", new[] { "Base", "S", "T", "GTS 4.0", "GT4", "GT4 RS" } },
                { "911 Cabrio", new[] { "Carrera", "Carrera S", "Carrera 4", "Carrera 4S", "GTS", "Turbo", "Turbo S", "Speedster" } },
                { "718 Boxster", new[] { "Base", "S", "T", "GTS 4.0", "Spyder", "25 Years" } },
                { "911 Targa", new[] { "4", "4S", "4 GTS", "Heritage Design Edition" } },
                { "Cayenne", new[] { "Base", "S", "GTS", "Turbo", "Turbo GT", "E-Hybrid", "S E-Hybrid", "Turbo S E-Hybrid", "Coupe", "Coupe GTS", "Coupe Turbo GT" } },
                { "Macan", new[] { "Base", "S", "GTS", "Turbo", "T", "Electric", "4 Electric", "Turbo Electric" } },

                // ==================== MINI ====================
                { "Cooper", new[] { "Classic", "Salt", "Pepper", "Chili", "Yours", "John Cooper Works", "One", "1.5", "2.0", "Electric" } },
                { "Cooper S", new[] { "Classic", "Salt", "Pepper", "Chili", "Yours", "John Cooper Works", "2.0" } },
                { "Cooper SE", new[] { "Classic", "Yours", "Electric Collection" } },
                { "Cooper Cabrio", new[] { "Classic", "Salt", "Pepper", "Chili", "Yours", "John Cooper Works", "Sidewalk" } },
                { "Cooper S Cabrio", new[] { "Classic", "Salt", "Pepper", "Chili", "Yours", "John Cooper Works", "Sidewalk" } },
                { "Clubman", new[] { "Classic", "Salt", "Pepper", "Chili", "Yours", "John Cooper Works", "One", "Cooper", "Cooper S", "Cooper D" } },
                { "Countryman", new[] { "Classic", "Salt", "Pepper", "Chili", "Yours", "John Cooper Works", "One", "Cooper", "Cooper S", "Cooper D", "Cooper SD" } },
                { "Countryman SE", new[] { "Classic", "Yours", "John Cooper Works", "ALL4" } },

                // ==================== SUZUKI ====================
                { "Swift", new[] { "GL", "GLX", "GLS", "Sport", "Hybrid", "1.2", "1.0 Boosterjet", "1.4 Boosterjet" } },
                { "Baleno", new[] { "GL", "GLX", "1.0 Boosterjet", "1.2 DualJet", "Hybrid" } },
                { "Celerio", new[] { "GL", "GLX", "1.0" } },
                { "Ignis", new[] { "GL", "GLX", "1.2 DualJet", "Hybrid", "AllGrip" } },
                { "Vitara", new[] { "GL", "GLX", "GL+", "GLX+", "1.4 Boosterjet", "1.5", "Hybrid", "AllGrip" } },
                { "S-Cross", new[] { "GL", "GLX", "GL+", "GLX+", "1.4 Boosterjet", "1.5", "Hybrid", "AllGrip" } },
                { "Jimny", new[] { "GL", "GLX", "1.5", "AllGrip", "Sierra" } },

                // ==================== DS ====================
                { "DS 3", new[] { "Chic", "So Chic", "Performance", "Sport Chic", "Connected Chic", "Cafe Racer", "1.2 PureTech", "1.6 THP", "1.6 BlueHDi" } },
                { "DS 4", new[] { "Chic", "So Chic", "Performance Line", "Rivoli", "Cross", "1.2 PureTech", "1.5 BlueHDi", "1.6 PureTech", "PHEV" } },
                { "DS 9", new[] { "Performance Line", "Rivoli", "Opera", "1.6 PureTech", "E-Tense", "PHEV" } },
                { "DS 3 Crossback", new[] { "Chic", "So Chic", "Performance Line", "Grand Chic", "Rivoli", "E-Tense", "1.2 PureTech", "1.5 BlueHDi" } },
                { "DS 7", new[] { "Performance Line", "Rivoli", "Opera", "La Premiere", "1.5 BlueHDi", "1.6 PureTech", "E-Tense", "E-Tense 4x4" } },
                { "DS 7 Crossback", new[] { "Chic", "So Chic", "Performance Line", "Grand Chic", "Rivoli", "Opera", "La Premiere", "1.5 BlueHDi", "1.6 PureTech", "2.0 BlueHDi", "E-Tense" } },

                // ==================== CHEVROLET ====================
                { "Aveo", new[] { "LS", "LT", "LTZ", "1.2", "1.4", "1.3D" } },
                { "Cruze HB", new[] { "LS", "LT", "LTZ", "Sport", "1.4 Turbo", "1.6", "1.6D", "2.0D" } },
                { "Spark", new[] { "LS", "LT", "LTZ", "1.0", "1.2", "Activ" } },
                { "Cruze", new[] { "LS", "LT", "LTZ", "Sport", "1.4 Turbo", "1.6", "1.6D", "2.0D" } },
                { "Malibu", new[] { "LS", "LT", "LTZ", "1.5 Turbo", "2.0 Turbo", "Hybrid" } },
                { "Lacetti", new[] { "SE", "SX", "CDX", "1.4", "1.6", "1.8", "2.0D" } },
                { "Captiva", new[] { "LS", "LT", "LTZ", "High", "2.0D", "2.4", "AWD" } },
                { "Trax", new[] { "LS", "LT", "LTZ", "1.4 Turbo", "1.6", "1.7D" } },
                { "Trailblazer", new[] { "LS", "LT", "LTZ", "Premier", "1.2 Turbo", "1.3 Turbo", "Activ" } }
            };

            MergeMotorcyclePackages(modelPackages);

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

                // BMW - Yeni modeller
                ("BMW", "2 Serisi Active Tourer", new[] { "Standart", "Comfort", "Joy", "Joy Plus", "Sport Line", "Luxury Line", "M Sport", "M Sport Pro", "Edition M Sport Shadow" }),
                ("BMW", "2 Serisi Gran Coupe", new[] { "Standart", "Sport Line", "Luxury Line", "M Sport", "M Sport Pro", "M Sport Edition", "Edition M Sport Shadow" }),
                ("BMW", "2 Serisi Coupe", new[] { "Standart", "Sport Line", "M Sport", "M Sport Pro" }),
                ("BMW", "4 Serisi Gran Coupe", new[] { "Standart", "Sport Line", "Luxury Line", "M Sport", "M Sport Pro", "M Sport Pro Edition", "Edition M Sport Shadow" }),
                ("BMW", "4 Serisi Coupe", new[] { "Standart", "Sport Line", "Luxury Line", "M Sport", "M Sport Pro", "Edition M Sport Shadow" }),
                ("BMW", "4 Serisi Cabrio", new[] { "Standart", "Sport Line", "Luxury Line", "M Sport", "M Sport Pro" }),
                ("BMW", "7 Serisi", new[] { "Standart", "Comfort", "Modern", "Luxury", "Executive", "Executive Lounge", "M Sport", "M Sport Pro", "M Plus", "Edition M Sport Shadow", "Pure Excellence" }),
                ("BMW", "8 Serisi Gran Coupe", new[] { "Standart", "M Sport", "M Sport Pro", "M Sport Edition", "Edition Golden Thunder" }),
                ("BMW", "8 Serisi Coupe", new[] { "Standart", "M Sport", "M Sport Pro", "Edition Golden Thunder" }),
                ("BMW", "M2", new[] { "Standart", "M Heritage Edition" }),
                ("BMW", "M4", new[] { "Standart", "Competition", "Competition xDrive", "CS", "CSL", "Heritage Edition" }),
                ("BMW", "Z4", new[] { "sDrive20i", "sDrive30i", "M40i", "Sport Line", "M Sport", "M Sport Pro", "First Edition" }),
                ("BMW", "X2", new[] { "Standart", "Advantage", "Sport Line", "M Sport", "M Sport X", "M Sport Pro", "Edition M Sport Shadow" }),
                ("BMW", "X4", new[] { "Standart", "xLine", "Sport Line", "Luxury Line", "M Sport", "M Sport Pro", "M Sport X", "Edition M Sport Shadow" }),
                ("BMW", "X6", new[] { "Standart", "xLine", "M Sport", "M Sport Pro", "M Sport Edition", "Edition Black Vermilion" }),
                ("BMW", "X7", new[] { "Standart", "xLine", "M Sport", "M Sport Pro", "Edition Dark Shadow", "Frozen Black" }),
                ("BMW", "i4", new[] { "eDrive35", "eDrive40", "xDrive40", "M50", "Sport Line", "M Sport", "M Sport Pro" }),
                ("BMW", "i5", new[] { "eDrive40", "xDrive40", "M60 xDrive", "M Sport", "M Sport Pro" }),
                ("BMW", "i7", new[] { "xDrive60", "M70 xDrive", "M Sport", "M Sport Pro", "Excellence" }),
                ("BMW", "iX1", new[] { "xDrive30", "eDrive20", "xLine", "M Sport", "M Sport Pro" }),
                ("BMW", "iX2", new[] { "eDrive20", "xDrive30", "M Sport", "M Sport Pro" }),
                ("BMW", "iX3", new[] { "Impressive", "M Sport", "M Sport Pro", "Inspiring" }),

                // Mercedes
                ("Mercedes", "A Serisi", new[] { "Style", "Urban", "Progressive", "AMG Line", "Edition 1", "AMG" }),
                ("Mercedes", "C Serisi", new[] { "Classic", "Comfort", "Elegance", "Avantgarde", "Exclusive", "AMG Line", "AMG Line Premium", "BlueEfficiency", "Edition 1", "Edition C", "Night Edition", "Night Paket", "Designo", "AMG" }),
                ("Mercedes", "E Serisi", new[] { "Elegance", "Avantgarde", "Exclusive", "AMG Line", "AMG Line Premium", "BlueEfficiency", "Edition 1", "Edition E", "Night Edition", "Night Paket", "Designo", "AMG" }),
                ("Mercedes", "S Serisi", new[] { "BlueEfficiency", "AMG Line", "AMG Line Premium", "Exclusive", "Night Paket", "Designo", "AMG" }),
                ("Mercedes", "CLA", new[] { "Style", "Urban", "Progressive", "AMG Line", "Edition 1", "Edition Orange Art", "Night Paket", "AMG" }),
                ("Mercedes", "G Serisi", new[] { "Professional", "Designo", "Edition 1", "Edition 35", "Stronger Than Time Edition", "Night Paket", "AMG Grand Edition", "Magno", "AMG" }),
                ("Mercedes", "EQE", new[] { "Electric Art", "AMG Line", "AMG Line Premium", "Night Paket", "Edition 1", "AMG" }),
                ("Mercedes", "EQS", new[] { "Electric Art", "AMG Line", "AMG Line Premium", "Night Paket", "Edition 1", "Designo", "Maybach", "AMG" }),
                ("Mercedes", "B Serisi", new[] { "Style", "Urban", "Progressive", "AMG Line", "BlueEfficiency", "Edition 1" }),
                ("Mercedes", "CLS", new[] { "Elegance", "Avantgarde", "Exclusive", "AMG Line", "AMG Line Premium", "BlueEfficiency", "Edition 1", "Night Edition", "Night Paket", "Designo", "AMG", "Shooting Brake" }),
                ("Mercedes", "AMG GT 4 Kapı", new[] { "AMG GT 43", "AMG GT 53", "AMG GT 63", "AMG GT 63 S", "Edition 1", "Night Paket", "Designo" }),
                ("Mercedes", "CLA Coupe", new[] { "Style", "Urban", "Progressive", "AMG Line", "Edition 1", "Night Paket", "AMG" }),
                ("Mercedes", "C Serisi Coupe", new[] { "Elegance", "Avantgarde", "Exclusive", "AMG Line", "AMG Line Premium", "Edition 1", "Night Edition", "Night Paket", "Designo", "AMG" }),
                ("Mercedes", "E Serisi Coupe", new[] { "Elegance", "Avantgarde", "Exclusive", "AMG Line", "AMG Line Premium", "Edition 1", "Night Edition", "Night Paket", "Designo", "AMG" }),
                ("Mercedes", "S Serisi Coupe", new[] { "AMG Line", "AMG Line Premium", "Exclusive", "Night Paket", "Designo", "AMG" }),
                ("Mercedes", "AMG GT Coupe", new[] { "AMG GT", "AMG GT S", "AMG GT R", "AMG GT R Pro", "AMG GT Black Series", "Edition 1", "Night Paket" }),
                ("Mercedes", "C Serisi Cabrio", new[] { "Avantgarde", "Exclusive", "AMG Line", "AMG Line Premium", "Edition 1", "Night Paket", "Designo", "AMG" }),
                ("Mercedes", "E Serisi Cabrio", new[] { "Avantgarde", "Exclusive", "AMG Line", "AMG Line Premium", "Edition 1", "Night Edition", "Designo", "AMG" }),
                ("Mercedes", "S Serisi Cabrio", new[] { "AMG Line", "Exclusive", "Night Paket", "Designo", "AMG" }),
                ("Mercedes", "SL", new[] { "AMG SL 43", "AMG SL 55", "AMG SL 63", "Edition 1", "Night Paket", "Designo" }),
                ("Mercedes", "SLC", new[] { "Style", "Progressive", "AMG Line", "BlueEfficiency", "Night Paket", "AMG SLC 43" }),
                ("Mercedes", "SLK", new[] { "BlueEfficiency", "AMG Line", "Edition 1", "Designo", "AMG" }),
                ("Mercedes", "AMG GT Roadster", new[] { "AMG GT", "AMG GT S", "AMG GT R", "AMG GT C", "Edition 1", "Night Paket" }),
                ("Mercedes", "C Serisi Estate", new[] { "Elegance", "Avantgarde", "Exclusive", "AMG Line", "AMG Line Premium", "BlueEfficiency", "Night Edition", "Night Paket", "All-Terrain", "AMG" }),
                ("Mercedes", "E Serisi Estate", new[] { "Elegance", "Avantgarde", "Exclusive", "AMG Line", "AMG Line Premium", "BlueEfficiency", "Night Edition", "Night Paket", "All-Terrain", "Designo", "AMG" }),
                ("Mercedes", "GLA", new[] { "Style", "Urban", "Progressive", "AMG Line", "Edition 1", "Night Paket", "AMG" }),
                ("Mercedes", "GLB", new[] { "Style", "Progressive", "AMG Line", "Edition 1", "Night Paket", "AMG" }),
                ("Mercedes", "GLC", new[] { "Exclusive", "Avantgarde", "AMG Line", "AMG Line Premium", "Edition 1", "Night Paket", "Designo", "AMG" }),
                ("Mercedes", "GLC Coupe", new[] { "Exclusive", "Avantgarde", "AMG Line", "AMG Line Premium", "Edition 1", "Night Paket", "Designo", "AMG" }),
                ("Mercedes", "GLE", new[] { "Exclusive", "Avantgarde", "AMG Line", "AMG Line Premium", "Edition 1", "Night Paket", "Designo", "AMG" }),
                ("Mercedes", "GLE Coupe", new[] { "Exclusive", "AMG Line", "AMG Line Premium", "Edition 1", "Night Paket", "Designo", "AMG" }),
                ("Mercedes", "GLS", new[] { "AMG Line", "AMG Line Premium", "Exclusive", "Night Paket", "Designo", "Maybach", "AMG" }),
                ("Mercedes", "EQA", new[] { "Electric Art", "AMG Line", "Progressive", "Night Paket", "Edition 1" }),
                ("Mercedes", "EQB", new[] { "Electric Art", "AMG Line", "Progressive", "Night Paket", "Edition 1" }),
                ("Mercedes", "EQC", new[] { "Electric Art", "AMG Line", "Progressive", "Night Paket", "Edition 1", "Edition 1886" }),
                ("Mercedes", "Vito", new[] { "Base", "Pro", "Select", "Sport", "Tourer Pro", "Tourer Select", "Panel Van", "Mixto" }),
                ("Mercedes", "V Serisi", new[] { "Style", "Avantgarde", "Exclusive", "AMG Line", "Edition 1", "Marco Polo", "Marco Polo Horizon", "Night Edition" }),
                ("Mercedes", "Sprinter", new[] { "Panel Van", "Kombi", "Minibüs", "Kamyonet", "Travel 75", "Travel 65" }),

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
                ("Audi", "A5 Sportback", new[] { "S Line", "Sport", "Design", "Advanced", "Dynamic", "Business", "Premium", "Launch Edition" }),
                ("Audi", "A7 Sportback", new[] { "S Line", "Design", "Sport", "Advanced", "Business", "Premium", "Launch Edition", "Dynamic" }),
                ("Audi", "A8", new[] { "S Line", "Design", "Sport", "Advanced", "Premium", "Long", "Dynamic" }),
                ("Audi", "A5 Coupe", new[] { "S Line", "Sport", "Design", "Advanced", "Dynamic", "Edition One", "S5" }),
                ("Audi", "TT Coupe", new[] { "S Line", "Sport", "Design", "TTS", "TT RS", "Edition One", "Bronze Selection" }),
                ("Audi", "R8 Coupe", new[] { "V10", "V10 Plus", "V10 Performance", "V10 Performance Quattro", "RWS", "GT", "Decennium", "Green Hell" }),
                ("Audi", "A5 Cabrio", new[] { "S Line", "Sport", "Design", "Advanced", "Dynamic", "S5 Cabrio" }),
                ("Audi", "A3 Cabrio", new[] { "Attraction", "Ambition", "Ambiente", "S Line", "Sport", "Design", "Advanced" }),
                ("Audi", "TT Roadster", new[] { "S Line", "Sport", "Design", "TTS Roadster", "TT RS Roadster", "Edition One" }),
                ("Audi", "R8 Spyder", new[] { "V10", "V10 Plus", "V10 Performance", "V10 Performance Quattro", "RWS", "GT" }),
                ("Audi", "A4 Avant", new[] { "Attraction", "Ambition", "Ambiente", "S Line", "Sport", "Design", "Advanced", "Dynamic", "Business", "Allroad", "S4 Avant" }),
                ("Audi", "A6 Avant", new[] { "S Line", "Design", "Sport", "Advanced", "Business", "Premium", "Dynamic", "Allroad", "S6 Avant", "RS6 Avant" }),
                ("Audi", "Q2", new[] { "S Line", "Sport", "Design", "Advanced", "Dynamic", "Edition One", "Business" }),
                ("Audi", "Q3 Sportback", new[] { "S Line", "Sport", "Design", "Advanced", "Dynamic", "Edition One", "RS Q3 Sportback" }),
                ("Audi", "Q5 Sportback", new[] { "S Line", "Sport", "Design", "Advanced", "Dynamic", "Edition One", "Premium", "SQ5 Sportback" }),
                ("Audi", "Q8 e-tron", new[] { "S Line", "Advanced", "Edition One", "SQ8 e-tron", "Sportback" }),
                ("Audi", "e-tron GT", new[] { "S Line", "Edition One", "RS e-tron GT", "Performance", "Dynamic" }),

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

                // Honda - Hatchback
                ("Honda", "Civic HB", new[] { "Elegance", "Executive", "Executive Plus", "Sport", "Sport Plus", "Eco", "Eco Elegance", "Dream", "Premium", "RS", "LPG Elegance", "LPG Executive" }),
                ("Honda", "Jazz", new[] {
                    "Cool", "Comfort", "Elegance", "Executive", "Sport", "S", "Dynamic",
                    "1.2 Trend", "1.2 Trend X",
                    "1.3 Comfort", "1.3 Comfort X", "1.3 Fun", "1.3 Fun X",
                    "1.3 S", "1.3 Cool", "1.3 Elegance", "1.3 Executive", "1.3 Sport",
                    "1.5 Dynamic", "1.5 Elegance", "1.5 Executive",
                    "1.5 Crosstar", "1.5 Crosstar Advance",
                    "1.5 e:HEV Elegance", "1.5 e:HEV Executive", "1.5 e:HEV Crosstar"
                }),
                ("Honda", "Honda e", new[] { "Honda e", "Honda e Advance" }),
                // Honda - Sedan
                ("Honda", "Civic Sedan", new[] { "Elegance", "Executive", "Executive Plus", "Sport", "Sport Plus", "Eco", "Eco Elegance", "Dream", "Premium", "RS", "LPG Elegance", "LPG Executive" }),
                ("Honda", "City", new[] { "Comfort", "Elegance", "Executive", "S", "V", "Dream" }),
                ("Honda", "Accord", new[] {
                    "Comfort", "Elegance", "Executive", "Executive Plus", "Lifestyle", "Premium",
                    "2.0 Elegance", "2.0 Executive", "2.0 Premium",
                    "2.4 Executive", "2.4 Premium",
                    "1.5T Elegance", "1.5T Executive", "1.5T Sport",
                    "2.0i-VTEC Executive", "2.0i-VTEC Premium",
                    "2.0 Hybrid", "2.0 Hybrid Advance", "2.0 Hybrid EX"
                }),
                // Honda - SUV
                ("Honda", "HR-V", new[] { "Comfort", "Elegance", "Executive", "Sport", "Executive Plus", "Advance", "e:HEV Elegance", "e:HEV Advance" }),
                ("Honda", "CR-V", new[] { "Comfort", "Elegance", "Executive", "Lifestyle", "Executive Plus", "Advance", "Sport Line", "e:HEV Elegance", "e:HEV Advance", "e:PHEV Advance" }),
                ("Honda", "ZR-V", new[] { "Elegance", "Executive", "Advance", "Sport", "e:HEV Elegance", "e:HEV Advance", "e:HEV Sport" }),
                ("Honda", "e:Ny1", new[] { "Elegance", "Advance" }),

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
                ("Hyundai", "IONIQ HB", new[] { "Style", "Elite", "Prime", "Premium", "Hybrid", "Plug-in Hybrid", "Electric" }),
                ("Hyundai", "IONIQ 6", new[] { "Style", "Prime", "N Line", "Vertex", "Top", "Inspiration", "Long Range", "Standard Range" }),
                ("Hyundai", "Sonata", new[] { "Style", "Style Plus", "Prime", "Elite", "Premium", "Executive", "Comfort", "Design" }),
                ("Hyundai", "Accent", new[] { "Mode", "Team", "Style", "Prime", "Elite", "Era", "Active", "Blue", "Cool", "Comfort" }),
                ("Hyundai", "Accent Blue", new[] { "Mode", "Team", "Style", "Prime", "Elite", "Era", "Active", "Cool", "Comfort", "Jump" }),
                ("Hyundai", "Kona", new[] { "Style", "Style Plus", "Prime", "Elite", "N Line", "Comfort", "Jump", "Design", "Premium", "Adventure" }),
                ("Hyundai", "Kona Electric", new[] { "Style", "Prime", "Elite", "Premium", "Long Range", "Standard Range", "N Line" }),
                ("Hyundai", "Santa Fe", new[] { "Style", "Style Plus", "Prime", "Elite", "N Line", "Premium", "Executive", "Comfort", "Design", "Adventure", "Calligraphy" }),
                ("Hyundai", "ix35", new[] { "Style", "Style Plus", "Prime", "Elite", "Comfort", "Jump", "Team", "Executive", "Premium" }),
                ("Hyundai", "Venue", new[] { "Style", "Style Plus", "Prime", "Elite", "Comfort", "Jump", "Team", "Design" }),
                ("Hyundai", "i20 Active", new[] { "Style", "Style Plus", "Prime", "Elite", "Comfort", "Jump", "Team" }),
                ("Hyundai", "Staria", new[] { "Style", "Prime", "Elite", "Premium", "Executive", "Calligraphy", "9 Kişilik", "5 Kişilik" }),
                ("Hyundai", "H-1", new[] { "Style", "Prime", "Elite", "Premium", "Executive", "8+1 Kişilik", "Panel Van" }),

                // Renault - Hatchback
                ("Renault", "Clio", new[] { "Joy", "Touch", "Icon", "Bose", "R.S. Line", "Intens", "Zen", "Life", "Techno", "Equilibre", "Evolution", "Iconic", "Initiale", "Limited", "S Edition" }),
                ("Renault", "Megane HB", new[] { "Joy", "Touch", "Icon", "Bose", "R.S. Line", "Intens", "Zen", "Life", "Techno", "Equilibre", "Evolution", "Iconic" }),
                ("Renault", "Zoe", new[] { "Life", "Zen", "Intens", "Iconic", "R110", "R135", "Edition One", "Riviera" }),
                // Renault - Sedan
                ("Renault", "Megane Sedan", new[] { "Joy", "Touch", "Icon", "Bose", "R.S. Line", "Intens", "Zen", "Life", "Techno", "Equilibre", "Evolution", "Iconic" }),
                ("Renault", "Fluence", new[] { "Joy", "Touch", "Icon", "Privilege", "Extreme", "Business", "Expression", "Dynamique" }),
                ("Renault", "Talisman", new[] { "Joy", "Touch", "Icon", "Initiale Paris", "Intens", "Zen", "Life", "Techno" }),
                ("Renault", "Symbol", new[] { "Joy", "Touch", "Expression", "Authentique", "Dynamique" }),
                ("Renault", "Latitude", new[] { "Expression", "Dynamique", "Privilege", "Executive", "Initiale", "Business" }),
                // Renault - Station Wagon
                ("Renault", "Megane Sport Tourer", new[] { "Joy", "Touch", "Icon", "Bose", "R.S. Line", "Intens", "Zen", "Life", "Techno", "Equilibre", "Evolution", "Iconic" }),
                // Renault - Crossover
                ("Renault", "Arkana", new[] { "Joy", "Touch", "Icon", "Bose", "R.S. Line", "Intens", "Techno", "Equilibre", "Evolution", "Iconic", "Esprit Alpine" }),
                // Renault - SUV
                ("Renault", "Captur", new[] { "Joy", "Touch", "Icon", "Bose", "R.S. Line", "Intens", "Zen", "Life", "Techno", "Equilibre", "Evolution", "Iconic" }),
                ("Renault", "Kadjar", new[] { "Joy", "Touch", "Icon", "Bose", "Intens", "Zen", "Life", "Techno" }),
                ("Renault", "Austral", new[] { "Equilibre", "Techno", "Iconic", "Esprit Alpine", "Evolution", "Techno Esprit Alpine" }),
                ("Renault", "Koleos", new[] { "Joy", "Touch", "Icon", "Bose", "Initiale Paris", "Intens", "Zen", "Life" }),
                // Renault - Minivan & Panelvan
                ("Renault", "Kangoo", new[] { "Joy", "Touch", "Zen", "Intens", "Life", "Equilibre", "Techno", "Multix", "Multix Joy", "Multix Touch" }),
                ("Renault", "Scenic", new[] { "Joy", "Touch", "Icon", "Bose", "Intens", "Zen", "Life", "Techno", "Initiale Paris", "Limited" }),
                ("Renault", "Grand Scenic", new[] { "Joy", "Touch", "Icon", "Bose", "Intens", "Zen", "Life", "Techno", "Initiale Paris" }),
                ("Renault", "Kangoo Express", new[] { "Joy", "Confort", "Maxi Joy", "Maxi Confort", "Compact", "Extra" }),
                ("Renault", "Master", new[] { "L1H1", "L2H2", "L3H2", "Confort", "Grand Confort", "Extra", "Business" }),
                ("Renault", "Trafic", new[] { "Joy", "Confort", "Grand Confort", "Grand Trafic", "SpaceClass", "Extra", "Business" }),

                // Kia - Hatchback
                ("Kia", "Picanto", new[] { "Cool", "Comfort", "Elegance", "Premium", "Concept", "Concept Plus", "Dream", "Style", "GT-Line", "X-Line" }),
                ("Kia", "Rio", new[] { "Cool", "Comfort", "Elegance", "Premium", "Concept", "Concept Plus", "Dream", "Style", "GT-Line" }),
                ("Kia", "Ceed", new[] { "Cool", "Comfort", "Elegance", "Premium", "Concept", "Concept Plus", "Dream", "GT-Line", "Style", "Business Line" }),
                ("Kia", "ProCeed", new[] { "Comfort", "Elegance", "Premium", "GT-Line", "GT" }),
                // Kia - Sedan
                ("Kia", "Cerato", new[] { "Cool", "Comfort", "Elegance", "Premium", "Concept", "Concept Plus", "Dream", "GT-Line", "Style" }),
                ("Kia", "Stinger", new[] { "Comfort", "Elegance", "Premium", "GT-Line", "GT" }),
                ("Kia", "K5", new[] { "Comfort", "Elegance", "Premium", "GT-Line", "GT", "Style" }),
                // Kia - Station Wagon
                ("Kia", "Ceed SW", new[] { "Cool", "Comfort", "Elegance", "Premium", "Concept", "Concept Plus", "Dream", "GT-Line", "Style", "Business Line" }),
                // Kia - Crossover
                ("Kia", "XCeed", new[] { "Cool", "Comfort", "Elegance", "Premium", "Concept", "Concept Plus", "Dream", "GT-Line", "Style" }),
                // Kia - SUV
                ("Kia", "Sportage", new[] { "Cool", "Comfort", "Elegance", "Premium", "Concept", "Concept Plus", "Dream", "GT-Line", "Style", "Business Line" }),
                ("Kia", "Sorento", new[] { "Cool", "Comfort", "Elegance", "Premium", "Concept", "Concept Plus", "Dream", "GT-Line", "Style", "Business Line" }),
                ("Kia", "Niro", new[] { "Cool", "Comfort", "Elegance", "Premium", "Concept", "Dream", "Style" }),
                ("Kia", "Stonic", new[] { "Cool", "Comfort", "Elegance", "Premium", "Concept", "Concept Plus", "Dream", "GT-Line", "Style" }),
                // Kia - Elektrikli SUV
                ("Kia", "EV6", new[] { "Air", "Wind", "Earth", "GT-Line", "GT", "Long Range", "Standard Range" }),
                ("Kia", "EV9", new[] { "Air", "Earth", "GT-Line", "GT", "Long Range" }),
                ("Kia", "Niro EV", new[] { "Cool", "Comfort", "Elegance", "Premium", "Concept", "Dream", "Style", "Long Range" }),

                // Land Rover
                ("Land Rover", "Range Rover", new[] { "Vogue", "HSE", "Autobiography", "Autobiography Long Wheelbase", "SV Autobiography", "SVAutobiography Dynamic", "First Edition", "SE" }),
                ("Land Rover", "Range Rover Sport", new[] { "S", "SE", "HSE", "HSE Dynamic", "Autobiography Dynamic", "SVR", "First Edition", "Dynamic SE", "Dynamic HSE" }),
                ("Land Rover", "Range Rover Velar", new[] { "S", "SE", "HSE", "R-Dynamic S", "R-Dynamic SE", "R-Dynamic HSE", "First Edition", "SVAutobiography Dynamic Edition" }),
                ("Land Rover", "Range Rover Evoque", new[] { "S", "SE", "HSE", "R-Dynamic S", "R-Dynamic SE", "R-Dynamic HSE", "First Edition", "Autobiography", "Dynamic" }),
                ("Land Rover", "Discovery", new[] { "S", "SE", "HSE", "HSE Luxury", "Landmark Edition", "First Edition", "R-Dynamic S", "R-Dynamic SE", "R-Dynamic HSE", "Metropolitan Edition" }),
                ("Land Rover", "Discovery Sport", new[] { "S", "SE", "HSE", "HSE Luxury", "R-Dynamic S", "R-Dynamic SE", "R-Dynamic HSE", "Landmark Edition", "First Edition" }),
                ("Land Rover", "Defender", new[] { "S", "SE", "HSE", "X-Dynamic S", "X-Dynamic SE", "X-Dynamic HSE", "X", "V8", "First Edition", "75th Limited Edition" }),
                ("Land Rover", "Freelander 2", new[] { "S", "SE", "HSE", "HSE Luxury", "Dynamic", "SD4 HSE", "TD4 SE" }),

                // BYD
                ("BYD", "Dolphin", new[] { "Comfort", "Design", "Boost", "Active", "Freedom", "Glory" }),
                ("BYD", "Seal", new[] { "Comfort", "Design", "Excellence", "AWD", "Performance", "Premium" }),
                ("BYD", "Han", new[] { "EV Glory Edition", "EV Premium", "EV Flagship", "EV AWD", "DM-i Premium", "DM-i Flagship" }),
                ("BYD", "Atto 3", new[] { "Comfort", "Design", "Active", "Boost", "Award", "Excellence" }),
                ("BYD", "Seal U", new[] { "Comfort", "Design", "Excellence", "Premium", "DM-i Comfort", "DM-i Design", "DM-i Excellence" }),
                ("BYD", "Tang", new[] { "EV Glory Edition", "EV Flagship", "EV AWD", "DM-i Premium", "DM-i Flagship" }),

                // Chery
                ("Chery", "Arrizo 5", new[] { "Comfort", "Luxury", "Elite", "Premium", "Sport" }),
                ("Chery", "Tiggo 4 Pro", new[] { "Comfort", "Luxury", "Elite", "Premium", "Distinction" }),
                ("Chery", "Tiggo 7 Pro", new[] { "Comfort", "Luxury", "Elite", "Premium", "Distinction", "Max" }),
                ("Chery", "Tiggo 8 Pro", new[] { "Comfort", "Luxury", "Elite", "Premium", "Distinction", "Max", "Champion" }),
                ("Chery", "Omoda 5", new[] { "Comfort", "Luxury", "Elite", "Premium", "Distinction", "Sport" }),

                // Nissan
                ("Nissan", "Micra", new[] { "Visia", "Acenta", "Tekna", "N-Sport", "N-Connecta", "Bose Personal Edition", "IG-T Visia", "IG-T Acenta" }),
                ("Nissan", "Note", new[] { "Visia", "Acenta", "Tekna", "N-Tec", "Comfort", "Premium" }),
                ("Nissan", "Pulsar", new[] { "Visia", "Acenta", "Tekna", "N-Connecta", "Business Edition" }),
                ("Nissan", "Leaf", new[] { "Visia", "Acenta", "Tekna", "N-Connecta", "e+", "e+ Tekna", "e+ N-Tec" }),
                ("Nissan", "Juke", new[] { "Visia", "Acenta", "Tekna", "N-Connecta", "N-Design", "N-Sport", "Bose Personal Edition", "Premiere Edition", "Enigma" }),
                ("Nissan", "Qashqai", new[] { "Visia", "Acenta", "Tekna", "Tekna+", "N-Connecta", "N-Design", "N-Sport", "Premiere Edition", "Business Edition", "e-Power Acenta", "e-Power Tekna" }),
                ("Nissan", "X-Trail", new[] { "Visia", "Acenta", "Tekna", "Tekna+", "N-Connecta", "N-Design", "Premiere Edition", "Adventure", "e-Power Acenta", "e-Power Tekna", "e-4orce Tekna" }),
                ("Nissan", "Navara", new[] { "Visia", "Acenta", "Tekna", "N-Guard", "N-Connecta", "Off-Roader AT32", "King Cab", "Double Cab", "PRO-4X" }),
                ("Nissan", "NV300", new[] { "Visia", "Acenta", "Comfort", "Panel Van", "Kombi", "L1H1", "L2H1" }),
                ("Nissan", "NV400", new[] { "Visia", "Acenta", "Comfort", "Panel Van", "Kamyonet", "L2H2", "L3H2" }),

                // Fiat - Hatchback
                ("Fiat", "Punto", new[] { "Active", "Dynamic", "Emotion", "Sporting", "Easy", "Pop", "Lounge", "S", "Street", "1.2", "1.3 Multijet", "1.4" }),
                ("Fiat", "Grande Punto", new[] { "Active", "Dynamic", "Emotion", "Sporting", "1.2", "1.3 Multijet", "1.4", "1.4 T-Jet" }),
                ("Fiat", "Punto Evo", new[] { "Active", "Dynamic", "Emotion", "Sporting", "MyLife", "1.2", "1.3 Multijet", "1.4" }),
                ("Fiat", "Bravo", new[] { "Active", "Dynamic", "Emotion", "Sport", "1.4", "1.4 T-Jet", "1.6 Multijet" }),
                ("Fiat", "Tipo HB", new[] { "Easy", "Lounge", "Sport", "Cross", "City Life", "Life", "Club", "1.4", "1.3 Multijet", "1.6 Multijet", "Mirror", "Street", "S-Design" }),
                ("Fiat", "Palio", new[] { "Active", "Dynamic", "Emotion", "Sole", "Weekend", "1.2", "1.3 Multijet" }),
                ("Fiat", "Stilo", new[] { "Active", "Dynamic", "Emotion", "Actual", "Schumacher", "1.4", "1.6", "1.9 JTD" }),
                ("Fiat", "500", new[] { "Pop", "Lounge", "Sport", "Icon", "Rockstar", "Star", "Dolcevita", "Club", "Anniversario", "Collezione", "1.0 Hybrid", "1.2", "0.9 TwinAir", "Abarth 595", "Abarth 695" }),
                ("Fiat", "500e", new[] { "Action", "Passion", "Icon", "La Prima", "RED", "Inspired By", "Giorgio Armani" }),
                ("Fiat", "Fiat 600e", new[] { "RED", "La Prima", "Elettrica" }),
                // Fiat - Sedan
                ("Fiat", "Linea", new[] { "Active", "Active Plus", "Dynamic", "Emotion", "Actual", "Pop", "Easy", "Lounge", "1.3 Multijet", "1.4", "1.4 T-Jet", "1.6 Multijet" }),
                ("Fiat", "Albea", new[] { "Active", "Dynamic", "Emotion", "Sole", "1.2", "1.3 Multijet", "1.4" }),
                ("Fiat", "Tipo Sedan", new[] { "Easy", "Lounge", "Sport", "Cross", "City Life", "Life", "Club", "1.4", "1.3 Multijet", "1.6 Multijet", "Mirror", "Street", "S-Design" }),
                ("Fiat", "Marea", new[] { "SX", "ELX", "HLX", "Sporting", "1.6", "1.9 JTD", "2.0 HLX" }),
                // Fiat - Cabrio
                ("Fiat", "500C", new[] { "Pop", "Lounge", "Sport", "Icon", "Rockstar", "Star", "Dolcevita", "Club", "Collezione", "1.0 Hybrid", "1.2", "0.9 TwinAir" }),
                // Fiat - Station Wagon
                ("Fiat", "Tipo Station Wagon", new[] { "Easy", "Lounge", "Sport", "Cross", "City Life", "Life", "Club", "1.4", "1.3 Multijet", "1.6 Multijet", "Mirror", "Street", "S-Design" }),
                ("Fiat", "Marea Weekend", new[] { "SX", "ELX", "HLX", "Sporting", "1.6", "1.9 JTD", "2.0 HLX" }),
                // Fiat - Crossover
                ("Fiat", "500X Cross", new[] { "Pop", "Pop Star", "Lounge", "Cross", "Cross Plus", "Sport", "Club", "City Cross", "1.0 T3", "1.3 T4", "1.6 Multijet" }),
                // Fiat - SUV
                ("Fiat", "500X", new[] { "Pop", "Pop Star", "Lounge", "Cross", "Cross Plus", "Sport", "Club", "City Cross", "Dolcevita", "1.0 T3", "1.3 T4", "1.6 Multijet" }),
                ("Fiat", "Freemont", new[] { "Urban", "Lounge", "Cross", "AWD", "2.0 Multijet", "Black Code" }),
                // Fiat - Pickup
                ("Fiat", "Fullback", new[] { "SX", "LX", "Cross", "Adventure", "Single Cab", "Double Cab", "2.4D" }),
                // Fiat - Minivan/Ticari
                ("Fiat", "Doblo", new[] { "Active", "Dynamic", "Emotion", "Easy", "Lounge", "Safeline", "Cargo", "Maxi", "Premio", "1.3 Multijet", "1.6 Multijet" }),
                ("Fiat", "Fiorino", new[] { "Cargo", "Panorama", "Combi", "Pop", "Easy", "Premio", "Safeline", "1.3 Multijet", "1.4" }),
                ("Fiat", "Ducato", new[] { "Cargo", "Panorama", "Kombi", "Maxi", "Van", "Kamyonet", "L1H1", "L2H1", "L2H2", "L3H2", "L4H2", "2.3 Multijet", "Multijet II" }),
                ("Fiat", "Scudo", new[] { "Cargo", "Panorama", "Kombi", "L1H1", "L2H1", "1.6 Multijet", "2.0 Multijet" }),
                ("Fiat", "Doblo Combi", new[] { "Active", "Dynamic", "Emotion", "Easy", "Lounge", "Safeline", "Premio", "1.3 Multijet", "1.6 Multijet" }),
                ("Fiat", "Fiorino Combi", new[] { "Pop", "Easy", "Premio", "Safeline", "Combi", "1.3 Multijet", "1.4" }),
                ("Fiat", "Egea MultiWagon", new[] { "Easy", "Lounge", "Cross", "Sport", "City Life", "Life", "Club", "1.4", "1.3 Multijet", "1.6 Multijet", "Mirror", "S-Design" }),

                // ==================== SKODA ====================
                ("Skoda", "Fabia", new[] { "Active", "Ambition", "Style", "Monte Carlo", "Comfort", "1.0 MPI", "1.0 TSI", "1.5 TSI" }),
                ("Skoda", "Scala", new[] { "Active", "Ambition", "Style", "Monte Carlo", "1.0 TSI", "1.5 TSI" }),
                ("Skoda", "Rapid", new[] { "Active", "Ambition", "Style", "Monte Carlo", "Elegance", "Joy", "1.0 TSI", "1.4 TSI", "1.6 TDI" }),
                ("Skoda", "Octavia", new[] { "Active", "Ambition", "Style", "Elegance", "L&K", "RS", "Scout", "Joy", "Premium", "1.0 TSI", "1.4 TSI", "1.5 TSI", "2.0 TSI", "1.6 TDI", "2.0 TDI" }),
                ("Skoda", "Superb", new[] { "Active", "Ambition", "Style", "Elegance", "L&K", "SportLine", "Scout", "Premium", "1.5 TSI", "2.0 TSI", "1.6 TDI", "2.0 TDI" }),
                ("Skoda", "Octavia Combi", new[] { "Active", "Ambition", "Style", "Elegance", "L&K", "RS", "Scout", "Premium", "1.0 TSI", "1.5 TSI", "2.0 TSI", "1.6 TDI", "2.0 TDI" }),
                ("Skoda", "Superb Combi", new[] { "Active", "Ambition", "Style", "Elegance", "L&K", "SportLine", "Scout", "Premium", "1.5 TSI", "2.0 TSI", "1.6 TDI", "2.0 TDI" }),
                ("Skoda", "Kamiq", new[] { "Active", "Ambition", "Style", "Monte Carlo", "1.0 TSI", "1.5 TSI" }),
                ("Skoda", "Karoq", new[] { "Active", "Ambition", "Style", "SportLine", "Scout", "1.0 TSI", "1.5 TSI", "2.0 TSI", "1.6 TDI", "2.0 TDI" }),
                ("Skoda", "Kodiaq", new[] { "Active", "Ambition", "Style", "L&K", "SportLine", "Scout", "RS", "1.5 TSI", "2.0 TSI", "2.0 TDI" }),

                // ==================== OPEL ====================
                ("Opel", "Corsa", new[] { "Essentia", "Edition", "Elegance", "GS Line", "Ultimate", "1.2", "1.2 Turbo", "1.5 Dizel", "1.4", "1.4 Turbo" }),
                ("Opel", "Corsa-e", new[] { "Edition", "Elegance", "GS Line", "Ultimate" }),
                ("Opel", "Astra", new[] { "Essentia", "Edition", "Elegance", "GS Line", "Ultimate", "1.2 Turbo", "1.4 Turbo", "1.5 Dizel", "1.6 Turbo" }),
                ("Opel", "Astra HB", new[] { "Essentia", "Edition", "Elegance", "GS Line", "Ultimate", "1.2 Turbo", "1.4 Turbo", "1.5 Dizel" }),
                ("Opel", "Astra Sedan", new[] { "Essentia", "Edition", "Elegance", "Design", "Sport", "Cosmo", "1.4", "1.4 Turbo", "1.6", "1.6 CDTI" }),
                ("Opel", "Insignia", new[] { "Essentia", "Edition", "Elegance", "GS Line", "Ultimate", "Sport", "Cosmo", "1.5 Turbo", "1.6 CDTI", "2.0 CDTI", "2.0 Turbo" }),
                ("Opel", "Astra Sports Tourer", new[] { "Essentia", "Edition", "Elegance", "GS Line", "Ultimate", "1.2 Turbo", "1.5 Dizel" }),
                ("Opel", "Insignia Sports Tourer", new[] { "Essentia", "Edition", "Elegance", "GS Line", "Ultimate", "1.5 Turbo", "1.6 CDTI", "2.0 CDTI" }),
                ("Opel", "Mokka", new[] { "Essentia", "Edition", "Elegance", "GS Line", "Ultimate", "1.2 Turbo", "1.5 Dizel" }),
                ("Opel", "Mokka-e", new[] { "Edition", "Elegance", "GS Line", "Ultimate" }),
                ("Opel", "Grandland", new[] { "Essentia", "Edition", "Elegance", "GS Line", "Ultimate", "1.2 Turbo", "1.5 Dizel", "1.6 Turbo", "Hybrid" }),
                ("Opel", "Crossland", new[] { "Essentia", "Edition", "Elegance", "GS Line", "Ultimate", "1.2 Turbo", "1.5 Dizel" }),
                ("Opel", "Combo", new[] { "Essentia", "Edition", "Elegance", "Cargo", "L1", "L2", "1.5 Dizel" }),
                ("Opel", "Combo Life", new[] { "Essentia", "Edition", "Elegance", "1.2 Turbo", "1.5 Dizel" }),
                ("Opel", "Vivaro", new[] { "Essentia", "Edition", "Cargo", "Kombi", "L1H1", "L2H1", "1.5 Dizel", "2.0 Dizel" }),
                ("Opel", "Movano", new[] { "Essentia", "Edition", "Cargo", "Kamyonet", "L2H2", "L3H2", "L4H3", "2.2 Dizel" }),

                // ==================== PEUGEOT ====================
                ("Peugeot", "208", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "Style", "Like", "Roadtrip", "e-208", "1.2 PureTech", "1.5 BlueHDi" }),
                ("Peugeot", "308", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "Style", "Roadtrip", "1.2 PureTech", "1.5 BlueHDi", "PHEV" }),
                ("Peugeot", "301", new[] { "Active", "Allure", "Access", "1.2 PureTech", "1.5 BlueHDi", "1.6 HDi", "1.6 VTi" }),
                ("Peugeot", "508", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "First Edition", "1.5 BlueHDi", "1.6 PureTech", "2.0 BlueHDi", "PHEV" }),
                ("Peugeot", "308 SW", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "1.2 PureTech", "1.5 BlueHDi", "PHEV" }),
                ("Peugeot", "508 SW", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "First Edition", "1.5 BlueHDi", "1.6 PureTech", "2.0 BlueHDi", "PHEV" }),
                ("Peugeot", "2008", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "Style", "Roadtrip", "e-2008", "1.2 PureTech", "1.5 BlueHDi" }),
                ("Peugeot", "3008", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "Style", "Roadtrip", "1.2 PureTech", "1.5 BlueHDi", "1.6 PureTech", "PHEV", "PHEV4" }),
                ("Peugeot", "5008", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "Style", "1.2 PureTech", "1.5 BlueHDi", "1.6 PureTech", "PHEV" }),
                ("Peugeot", "Boxer", new[] { "Premium", "Pro", "Avantage", "L1H1", "L2H2", "L3H2", "L4H3", "2.2 BlueHDi", "Kamyonet", "Cargo" }),
                ("Peugeot", "Rifter", new[] { "Active", "Allure", "GT Line", "Style", "1.2 PureTech", "1.5 BlueHDi", "Long" }),
                ("Peugeot", "Partner", new[] { "Active", "Allure", "Premium", "Tepee", "1.6 HDi", "1.6 VTi", "Cargo" }),
                ("Peugeot", "Expert", new[] { "Premium", "Pro", "Avantage", "L1", "L2", "L3", "1.5 BlueHDi", "2.0 BlueHDi", "Cargo", "Kombi" }),
                ("Peugeot", "Traveller", new[] { "Active", "Allure", "Business", "VIP", "L2", "L3", "1.5 BlueHDi", "2.0 BlueHDi" }),
                ("Peugeot", "108", new[] { "Active", "Allure", "Style", "Top!", "1.0 VTi", "1.2 PureTech" }),
                ("Peugeot", "206", new[] { "XR", "XS", "XT", "Premium", "Quiksilver", "1.4", "1.4 HDi", "1.6", "1.6 HDi", "2.0 HDi", "CC", "SW" }),
                ("Peugeot", "207", new[] { "Comfort", "Premium", "Sportium", "Envy", "1.4", "1.4 HDi", "1.6", "1.6 HDi", "1.6 THP", "CC", "SW" }),
                ("Peugeot", "307", new[] { "XR", "XS", "XT", "Premium", "Sportium", "1.4", "1.4 HDi", "1.6", "1.6 HDi", "2.0", "2.0 HDi" }),
                ("Peugeot", "407", new[] { "Comfort", "Premium", "Executive", "Feline", "ST", "1.6 HDi", "2.0", "2.0 HDi", "2.2", "2.2 HDi", "3.0 V6" }),
                ("Peugeot", "408", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "1.2 PureTech", "1.5 BlueHDi", "PHEV" }),
                ("Peugeot", "e-208", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "50 kWh" }),
                ("Peugeot", "e-2008", new[] { "Active", "Allure", "Allure Pack", "GT", "GT Pack", "50 kWh", "54 kWh" }),
                ("Peugeot", "307 SW", new[] { "XR", "XS", "XT", "Premium", "1.6", "1.6 HDi", "2.0", "2.0 HDi" }),
                ("Peugeot", "407 SW", new[] { "Comfort", "Premium", "Executive", "Feline", "1.6 HDi", "2.0", "2.0 HDi", "2.2", "2.2 HDi" }),
                ("Peugeot", "Bipper", new[] { "Comfort", "Premium", "Tepee", "1.3 HDi", "1.4 HDi", "Cargo" }),

                // ==================== SEAT ====================
                ("Seat", "Ibiza", new[] { "Reference", "Style", "Xcellence", "FR", "FR Sport", "Cupra", "1.0 TSI", "1.0 EcoTSI", "1.5 TSI", "1.6 TDI" }),
                ("Seat", "Leon", new[] { "Reference", "Style", "Xcellence", "FR", "FR Sport", "Cupra", "1.0 TSI", "1.4 TSI", "1.5 TSI", "2.0 TSI", "1.6 TDI", "2.0 TDI", "e-Hybrid" }),
                ("Seat", "Leon HB", new[] { "Reference", "Style", "Xcellence", "FR", "FR Sport", "1.0 TSI", "1.5 TSI", "2.0 TSI", "1.6 TDI" }),
                ("Seat", "Toledo", new[] { "Reference", "Style", "Xcellence", "1.0 TSI", "1.4 TSI", "1.6 TDI", "1.2 TSI" }),
                ("Seat", "Leon ST", new[] { "Reference", "Style", "Xcellence", "FR", "FR Sport", "Cupra", "1.0 TSI", "1.5 TSI", "2.0 TSI", "1.6 TDI", "2.0 TDI" }),
                ("Seat", "Arona", new[] { "Reference", "Style", "Xcellence", "FR", "FR Sport", "1.0 TSI", "1.0 EcoTSI", "1.5 TSI", "1.6 TDI" }),
                ("Seat", "Ateca", new[] { "Reference", "Style", "Xcellence", "FR", "FR Sport", "Cupra", "1.0 TSI", "1.5 TSI", "2.0 TSI", "1.6 TDI", "2.0 TDI" }),
                ("Seat", "Tarraco", new[] { "Reference", "Style", "Xcellence", "FR", "FR Sport", "1.5 TSI", "2.0 TSI", "2.0 TDI", "e-Hybrid" }),
                ("Seat", "Alhambra", new[] { "Reference", "Style", "Xcellence", "FR", "1.4 TSI", "2.0 TSI", "2.0 TDI" }),

                // ==================== CUPRA ====================
                ("Cupra", "Born", new[] { "V1", "V2", "V3", "VZ", "58 kWh", "77 kWh", "e-Boost" }),
                ("Cupra", "Leon Sportstourer", new[] { "VZ", "VZe", "1.4 e-Hybrid", "1.5 TSI", "2.0 TSI" }),
                ("Cupra", "Formentor", new[] { "V1", "V2", "VZ", "VZe", "VZ5", "1.5 TSI", "2.0 TSI", "1.4 e-Hybrid", "2.5 TSI" }),
                ("Cupra", "Terramar", new[] { "V1", "V2", "VZ", "VZe", "1.5 TSI", "2.0 TSI", "1.4 e-Hybrid" }),

                // ==================== TOGG ====================
                ("Togg", "T10F", new[] { "Standart Menzil", "Uzun Menzil" }),
                ("Togg", "T10X", new[] { "Standart Menzil", "Uzun Menzil" }),

                // ==================== CITROEN ====================
                ("Citroen", "C3", new[] { "Live", "Feel", "Feel Pack", "Shine", "Shine Pack", "C-Series", "You!", "Max", "1.2 PureTech", "1.5 BlueHDi" }),
                ("Citroen", "C4", new[] { "Live", "Feel", "Feel Pack", "Shine", "Shine Pack", "C-Series", "e-C4", "1.2 PureTech", "1.5 BlueHDi" }),
                ("Citroen", "C4 X", new[] { "Feel", "Feel Pack", "Shine", "Shine Pack", "e-C4 X", "1.2 PureTech", "1.5 BlueHDi" }),
                ("Citroen", "C-Elysee", new[] { "Live", "Feel", "Shine", "Exclusive", "1.2 PureTech", "1.5 BlueHDi", "1.6 HDi", "1.6 VTi" }),
                ("Citroen", "C4 Sedan", new[] { "Live", "Feel", "Shine", "Exclusive", "1.6 HDi", "1.6 VTi", "1.6 THP" }),
                ("Citroen", "C5 Tourer", new[] { "Attraction", "Confort", "Exclusive", "1.6 HDi", "1.6 THP", "2.0 HDi", "2.0 BlueHDi" }),
                ("Citroen", "C3 Aircross", new[] { "Live", "Feel", "Feel Pack", "Shine", "Shine Pack", "C-Series", "1.2 PureTech", "1.5 BlueHDi" }),
                ("Citroen", "C5 Aircross", new[] { "Live", "Feel", "Feel Pack", "Shine", "Shine Pack", "C-Series", "1.2 PureTech", "1.5 BlueHDi", "1.6 PureTech", "Hybrid", "Plug-in Hybrid" }),
                ("Citroen", "Berlingo", new[] { "Live", "Feel", "Shine", "XL", "M", "1.2 PureTech", "1.5 BlueHDi", "Cargo" }),
                ("Citroen", "SpaceTourer", new[] { "Live", "Feel", "Shine", "Business", "M", "XL", "1.5 BlueHDi", "2.0 BlueHDi" }),
                ("Citroen", "Jumpy", new[] { "Pro", "Business", "Club", "M", "XL", "XS", "1.5 BlueHDi", "2.0 BlueHDi", "Cargo", "Kombi" }),
                ("Citroen", "Jumper", new[] { "Pro", "Business", "Club", "L1H1", "L2H2", "L3H2", "L4H3", "2.2 BlueHDi", "Cargo", "Kamyonet" }),

                // ==================== JAGUAR ====================
                ("Jaguar", "XE", new[] { "S", "SE", "HSE", "R-Dynamic S", "R-Dynamic SE", "R-Dynamic HSE", "R-Sport", "Portfolio", "P250", "P300", "D200" }),
                ("Jaguar", "XF", new[] { "S", "SE", "HSE", "R-Dynamic S", "R-Dynamic SE", "R-Dynamic HSE", "R-Sport", "Portfolio", "P250", "P300", "D200" }),
                ("Jaguar", "F-Type Coupe", new[] { "S", "R", "R-Dynamic", "SVR", "P300", "P380", "P450", "P575", "First Edition", "75" }),
                ("Jaguar", "F-Type Cabrio", new[] { "S", "R", "R-Dynamic", "SVR", "P300", "P380", "P450", "P575", "First Edition", "75" }),
                ("Jaguar", "F-Type Roadster", new[] { "S", "R", "R-Dynamic", "SVR", "P300", "P380", "P450", "P575", "First Edition" }),
                ("Jaguar", "F-Pace", new[] { "S", "SE", "HSE", "R-Dynamic S", "R-Dynamic SE", "R-Dynamic HSE", "SVR", "P250", "P400", "P400e", "D200" }),
                ("Jaguar", "E-Pace", new[] { "S", "SE", "HSE", "R-Dynamic S", "R-Dynamic SE", "R-Dynamic HSE", "P200", "P250", "P300", "D165", "D200" }),
                ("Jaguar", "I-Pace", new[] { "S", "SE", "HSE", "R-Dynamic S", "R-Dynamic SE", "R-Dynamic HSE", "EV320", "EV400", "Black", "First Edition" }),

                // ==================== LEXUS ====================
                ("Lexus", "CT", new[] { "Executive", "Luxury", "F Sport", "200h" }),
                ("Lexus", "IS", new[] { "Executive", "Luxury", "F Sport", "300h", "350", "500 F" }),
                ("Lexus", "ES", new[] { "Executive", "Luxury", "F Sport", "250", "300h", "350" }),
                ("Lexus", "GS", new[] { "Executive", "Luxury", "F Sport", "300h", "350", "450h" }),
                ("Lexus", "LS", new[] { "Executive", "Luxury", "F Sport", "500h", "350" }),
                ("Lexus", "RC", new[] { "Luxury", "F Sport", "300h", "350", "F" }),
                ("Lexus", "LC", new[] { "Luxury", "Sport", "Sport+", "500", "500h" }),
                ("Lexus", "LC Cabrio", new[] { "Luxury", "Sport", "Sport+", "500" }),
                ("Lexus", "NX", new[] { "Executive", "Luxury", "F Sport", "250", "300h", "350h", "450h+" }),
                ("Lexus", "RX", new[] { "Executive", "Luxury", "F Sport", "350", "350h", "450h", "450h+", "500h" }),
                ("Lexus", "UX", new[] { "Executive", "Luxury", "F Sport", "250h", "300e" }),
                ("Lexus", "LX", new[] { "Executive", "Luxury", "F Sport", "600" }),

                // ==================== MASERATI ====================
                ("Maserati", "Ghibli", new[] { "GT", "Modena", "Trofeo", "S", "S Q4", "Diesel", "GranSport", "GranLusso", "Ribelle", "F Tributo" }),
                ("Maserati", "Quattroporte", new[] { "GT", "Modena", "Trofeo", "S", "S Q4", "GranSport", "GranLusso", "Diesel" }),
                ("Maserati", "MC20", new[] { "Cielo", "GT2", "Icona", "Leggera" }),
                ("Maserati", "GranTurismo", new[] { "Modena", "Trofeo", "Folgore", "Sport", "S", "MC" }),
                ("Maserati", "GranCabrio", new[] { "Modena", "Trofeo", "Folgore", "Sport", "MC" }),
                ("Maserati", "MC20 Cielo", new[] { "Standart", "PrimaSerie", "Icona" }),
                ("Maserati", "Levante", new[] { "GT", "Modena", "Trofeo", "S", "GranSport", "GranLusso", "Diesel", "Hybrid" }),
                ("Maserati", "Grecale", new[] { "GT", "Modena", "Trofeo", "Folgore", "PrimaSerie" }),

                // ==================== PORSCHE ====================
                ("Porsche", "Panamera", new[] { "4", "4S", "GTS", "Turbo", "Turbo S", "4 E-Hybrid", "4S E-Hybrid", "Turbo S E-Hybrid", "Executive", "Sport Turismo" }),
                ("Porsche", "Taycan", new[] { "4S", "GTS", "Turbo", "Turbo S", "Cross Turismo", "Sport Turismo", "Base", "Performance Battery", "Performance Battery Plus" }),
                ("Porsche", "911", new[] { "Carrera", "Carrera S", "Carrera 4", "Carrera 4S", "Carrera T", "GTS", "GT3", "GT3 RS", "GT3 Touring", "Turbo", "Turbo S", "Sport Classic", "Dakar" }),
                ("Porsche", "718 Cayman", new[] { "Base", "S", "T", "GTS 4.0", "GT4", "GT4 RS" }),
                ("Porsche", "911 Cabrio", new[] { "Carrera", "Carrera S", "Carrera 4", "Carrera 4S", "GTS", "Turbo", "Turbo S", "Speedster" }),
                ("Porsche", "718 Boxster", new[] { "Base", "S", "T", "GTS 4.0", "Spyder", "25 Years" }),
                ("Porsche", "911 Targa", new[] { "4", "4S", "4 GTS", "Heritage Design Edition" }),
                ("Porsche", "Cayenne", new[] { "Base", "S", "GTS", "Turbo", "Turbo GT", "E-Hybrid", "S E-Hybrid", "Turbo S E-Hybrid", "Coupe", "Coupe GTS", "Coupe Turbo GT" }),
                ("Porsche", "Macan", new[] { "Base", "S", "GTS", "Turbo", "T", "Electric", "4 Electric", "Turbo Electric" }),

                // ==================== MINI ====================
                ("Mini", "Cooper", new[] { "Classic", "Salt", "Pepper", "Chili", "Yours", "John Cooper Works", "One", "1.5", "2.0", "Electric" }),
                ("Mini", "Cooper S", new[] { "Classic", "Salt", "Pepper", "Chili", "Yours", "John Cooper Works", "2.0" }),
                ("Mini", "Cooper SE", new[] { "Classic", "Yours", "Electric Collection" }),
                ("Mini", "Cooper Cabrio", new[] { "Classic", "Salt", "Pepper", "Chili", "Yours", "John Cooper Works", "Sidewalk" }),
                ("Mini", "Cooper S Cabrio", new[] { "Classic", "Salt", "Pepper", "Chili", "Yours", "John Cooper Works", "Sidewalk" }),
                ("Mini", "Clubman", new[] { "Classic", "Salt", "Pepper", "Chili", "Yours", "John Cooper Works", "One", "Cooper", "Cooper S", "Cooper D" }),
                ("Mini", "Countryman", new[] { "Classic", "Salt", "Pepper", "Chili", "Yours", "John Cooper Works", "One", "Cooper", "Cooper S", "Cooper D", "Cooper SD" }),
                ("Mini", "Countryman SE", new[] { "Classic", "Yours", "John Cooper Works", "ALL4" }),

                // ==================== SUZUKI ====================
                ("Suzuki", "Swift", new[] { "GL", "GLX", "GLS", "Sport", "Hybrid", "1.2", "1.0 Boosterjet", "1.4 Boosterjet" }),
                ("Suzuki", "Baleno", new[] { "GL", "GLX", "1.0 Boosterjet", "1.2 DualJet", "Hybrid" }),
                ("Suzuki", "Celerio", new[] { "GL", "GLX", "1.0" }),
                ("Suzuki", "Ignis", new[] { "GL", "GLX", "1.2 DualJet", "Hybrid", "AllGrip" }),
                ("Suzuki", "Vitara", new[] { "GL", "GLX", "GL+", "GLX+", "1.4 Boosterjet", "1.5", "Hybrid", "AllGrip" }),
                ("Suzuki", "S-Cross", new[] { "GL", "GLX", "GL+", "GLX+", "1.4 Boosterjet", "1.5", "Hybrid", "AllGrip" }),
                ("Suzuki", "Jimny", new[] { "GL", "GLX", "1.5", "AllGrip", "Sierra" }),

                // ==================== DS ====================
                ("DS", "DS 3", new[] { "Chic", "So Chic", "Performance", "Sport Chic", "Connected Chic", "Cafe Racer", "1.2 PureTech", "1.6 THP", "1.6 BlueHDi" }),
                ("DS", "DS 4", new[] { "Chic", "So Chic", "Performance Line", "Rivoli", "Cross", "1.2 PureTech", "1.5 BlueHDi", "1.6 PureTech", "PHEV" }),
                ("DS", "DS 9", new[] { "Performance Line", "Rivoli", "Opera", "1.6 PureTech", "E-Tense", "PHEV" }),
                ("DS", "DS 3 Crossback", new[] { "Chic", "So Chic", "Performance Line", "Grand Chic", "Rivoli", "E-Tense", "1.2 PureTech", "1.5 BlueHDi" }),
                ("DS", "DS 7", new[] { "Performance Line", "Rivoli", "Opera", "La Premiere", "1.5 BlueHDi", "1.6 PureTech", "E-Tense", "E-Tense 4x4" }),
                ("DS", "DS 7 Crossback", new[] { "Chic", "So Chic", "Performance Line", "Grand Chic", "Rivoli", "Opera", "La Premiere", "1.5 BlueHDi", "1.6 PureTech", "2.0 BlueHDi", "E-Tense" }),

                // ==================== CHEVROLET ====================
                ("Chevrolet", "Aveo", new[] { "LS", "LT", "LTZ", "1.2", "1.4", "1.3D" }),
                ("Chevrolet", "Cruze HB", new[] { "LS", "LT", "LTZ", "Sport", "1.4 Turbo", "1.6", "1.6D", "2.0D" }),
                ("Chevrolet", "Spark", new[] { "LS", "LT", "LTZ", "1.0", "1.2", "Activ" }),
                ("Chevrolet", "Cruze", new[] { "LS", "LT", "LTZ", "Sport", "1.4 Turbo", "1.6", "1.6D", "2.0D" }),
                ("Chevrolet", "Malibu", new[] { "LS", "LT", "LTZ", "1.5 Turbo", "2.0 Turbo", "Hybrid" }),
                ("Chevrolet", "Lacetti", new[] { "SE", "SX", "CDX", "1.4", "1.6", "1.8", "2.0D" }),
                ("Chevrolet", "Captiva", new[] { "LS", "LT", "LTZ", "High", "2.0D", "2.4", "AWD" }),
                ("Chevrolet", "Trax", new[] { "LS", "LT", "LTZ", "1.4 Turbo", "1.6", "1.7D" }),
                ("Chevrolet", "Trailblazer", new[] { "LS", "LT", "LTZ", "Premier", "1.2 Turbo", "1.3 Turbo", "Activ" })
            };

            newPackages.AddRange(MotorcyclePackageEntries);

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
