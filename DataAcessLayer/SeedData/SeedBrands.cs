using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.SeedData
{
    public static class SeedBrands
    {
        // SUV modelleri - kasa tipi SUV olacak
        private static readonly HashSet<string> SuvModels = new()
        {
            "X1", "X3", "X4", "X5", "X6", "X7",                // BMW SUV
            "iX", "iX1", "iX3",                                 // BMW Elektrikli SUV
            "GLA", "GLB", "GLC", "GLE", "GLS", "G Serisi",   // Mercedes SUV
            "GLC Coupe", "GLE Coupe",                            // Mercedes SUV Coupe
            "EQA", "EQB", "EQC",                                 // Mercedes Elektrikli SUV
            "Q2", "Q3", "Q3 Sportback", "Q5", "Q5 Sportback", // Audi SUV
            "Q7", "Q8", "Q8 e-tron",                            // Audi SUV
            "e-tron", "e-tron GT",                               // Audi Elektrikli
            "Tiguan", "Touareg", "T-Roc",                    // VW
            "C-HR", "RAV4", "Land Cruiser",                  // Toyota
            "XC40", "XC60", "XC90",                          // Volvo
            "Model Y", "Model X",                            // Tesla
            "HR-V", "CR-V", "ZR-V", "e:Ny1",                   // Honda SUV
            "Puma", "Kuga", "Explorer",                      // Ford
            "Tucson", "Kona", "Kona Electric", "Santa Fe",      // Hyundai SUV
            "Bayon", "IONIQ 5", "ix35", "Venue",                  // Hyundai SUV
            "Captur", "Kadjar", "Austral", "Koleos",             // Renault SUV
            "Sportage", "Sorento", "Niro", "Stonic",            // Kia SUV
            "EV6", "EV9", "Niro EV",                             // Kia Elektrikli SUV
            "Range Rover", "Range Rover Sport", "Range Rover Velar",   // Land Rover
            "Range Rover Evoque", "Discovery", "Discovery Sport",       // Land Rover
            "Defender", "Freelander 2",                                  // Land Rover
            "Atto 3", "Seal U", "Tang",                                  // BYD SUV
            "Tiggo 4 Pro", "Tiggo 7 Pro", "Tiggo 8 Pro",                // Chery SUV
            "Omoda 5",                                                   // Chery SUV
            "Qashqai", "X-Trail",                                        // Nissan SUV
            "500X", "Freemont",                                          // Fiat SUV
            "Karoq", "Kodiaq",                                           // Skoda SUV
            "Mokka", "Mokka-e", "Grandland", "Crossland",               // Opel SUV
            "2008", "3008", "5008", "e-2008",                              // Peugeot SUV
            "Ateca", "Tarraco",                                          // Seat SUV
            "Formentor", "Terramar",                                     // Cupra SUV
            "T10X",                                                      // Togg SUV
            "C3 Aircross", "C5 Aircross",                               // Citroen SUV
            "F-Pace", "E-Pace", "I-Pace",                               // Jaguar SUV
            "NX", "RX", "UX", "LX",                                     // Lexus SUV
            "Levante", "Grecale",                                        // Maserati SUV
            "Cayenne", "Macan",                                          // Porsche SUV
            "Countryman", "Countryman SE",                               // Mini SUV
            "Vitara", "S-Cross", "Jimny",                                // Suzuki SUV
            "DS 3 Crossback", "DS 7", "DS 7 Crossback",                 // DS SUV
            "Captiva", "Trax", "Trailblazer"                             // Chevrolet SUV
        };

        // Sedan modelleri - kasa tipi Sedan olacak
        private static readonly HashSet<string> SedanModels = new()
        {
            "2 Serisi Gran Coupe", "3 Serisi", "4 Serisi Gran Coupe", // BMW Sedan/Gran Coupe
            "5 Serisi", "7 Serisi", "8 Serisi Gran Coupe",  // BMW Sedan/Gran Coupe
            "i4", "i5", "i7",                                // BMW Elektrikli Sedan
            "C Serisi", "E Serisi", "S Serisi", "CLA", "EQE", "EQS", // Mercedes Sedan
            "CLS", "AMG GT 4 Kapı",                              // Mercedes Sedan
            "A4", "A5 Sportback", "A6", "A7 Sportback", "A8", // Audi Sedan
            "Passat", "Jetta", "Arteon",                     // VW
            "Corolla", "Camry",                              // Toyota
            "S60", "S90",                                    // Volvo
            "Model 3", "Model S",                            // Tesla
            "Civic Sedan", "City", "Accord",                   // Honda Sedan
            "Focus", "Mondeo",                               // Ford
            "Elantra", "Sonata", "Accent", "Accent Blue",      // Hyundai Sedan
            "IONIQ 6",                                           // Hyundai Elektrikli Sedan
            "Megane Sedan", "Fluence", "Talisman",               // Renault Sedan
            "Symbol", "Latitude",                                // Renault Sedan
            "Cerato", "Stinger", "K5",                           // Kia Sedan
            "Seal", "Han",                                        // BYD Sedan
            "Arrizo 5",                                           // Chery Sedan
            "Linea", "Albea", "Tipo Sedan", "Marea",             // Fiat Sedan
            "Octavia", "Superb",                                     // Skoda Sedan
            "Insignia", "Astra Sedan",                              // Opel Sedan
            "301", "407", "408", "508",                                 // Peugeot Sedan
            "Toledo",                                               // Seat Sedan
            "C-Elysee", "C4 Sedan",                                // Citroen Sedan
            "XE", "XF",                                            // Jaguar Sedan
            "IS", "ES", "GS", "LS",                                // Lexus Sedan
            "Ghibli", "Quattroporte",                              // Maserati Sedan
            "Panamera", "Taycan",                                  // Porsche Sedan
            "DS 9",                                                // DS Sedan
            "Cruze", "Malibu", "Lacetti"                           // Chevrolet Sedan
        };

        // Hatchback modelleri - kasa tipi Hatchback olacak
        private static readonly HashSet<string> HatchbackModels = new()
        {
            "1 Serisi", "2 Serisi Active Tourer",               // BMW
            "A Serisi", "B Serisi",                              // Mercedes Hatchback
            "A1", "A3",                                      // Audi
            "Golf", "Polo",                                  // VW
            "Yaris",                                         // Toyota
            "Civic HB", "Jazz", "Honda e",                       // Honda Hatchback
            "Fiesta",                                        // Ford
            "i10", "i20", "i30", "IONIQ HB",                   // Hyundai Hatchback
            "Clio", "Megane HB", "Zoe",                          // Renault Hatchback
            "Picanto", "Rio", "Ceed", "ProCeed",                 // Kia Hatchback
            "Dolphin",                                            // BYD Hatchback
            "Micra", "Note", "Pulsar", "Leaf",                   // Nissan Hatchback
            "Punto", "Grande Punto", "Punto Evo", "Bravo",       // Fiat Hatchback
            "Tipo HB", "Palio", "Stilo",                         // Fiat Hatchback
            "500", "500e", "Fiat 600e",                           // Fiat Hatchback/Mini
            "Fabia", "Scala", "Rapid",                            // Skoda Hatchback
            "Corsa", "Corsa-e", "Astra", "Astra HB",             // Opel Hatchback
            "108", "206", "207", "208", "307", "308", "e-208",      // Peugeot Hatchback
            "Ibiza", "Leon", "Leon HB",                           // Seat Hatchback
            "Born",                                                // Cupra Hatchback
            "T10F",                                                // Togg Hatchback/Fastback
            "C3", "C4", "C4 X",                                   // Citroen Hatchback
            "CT",                                                  // Lexus Hatchback
            "Cooper", "Cooper S", "Cooper SE",                     // Mini Hatchback
            "Swift", "Baleno", "Celerio", "Ignis",                 // Suzuki Hatchback
            "DS 3", "DS 4",                                        // DS Hatchback
            "Aveo", "Cruze HB", "Spark"                            // Chevrolet Hatchback
        };

        // Pickup modelleri - kasa tipi Pickup olacak
        private static readonly HashSet<string> PickupModels = new()
        {
            "Amarok",                                        // VW
            "Hilux",                                         // Toyota
            "Ranger",                                        // Ford
            "Navara",                                        // Nissan Pickup
            "Fullback"                                       // Fiat Pickup
        };

        // Coupe modelleri - kasa tipi Coupe olacak
        private static readonly HashSet<string> CoupeModels = new()
        {
            "2 Serisi Coupe", "4 Serisi Coupe", "8 Serisi Coupe", "M2", "M4", // BMW
            "CLA Coupe",                                     // Mercedes
            "C Serisi Coupe", "E Serisi Coupe", "S Serisi Coupe", // Mercedes Coupe
            "AMG GT Coupe",                                       // Mercedes AMG
            "A5 Coupe", "TT Coupe", "R8 Coupe",                  // Audi Coupe
            "F-Type Coupe",                                        // Jaguar Coupe
            "RC", "LC",                                            // Lexus Coupe
            "MC20", "GranTurismo",                                 // Maserati Coupe
            "911", "718 Cayman"                                    // Porsche Coupe
        };

        // Cabrio modelleri - kasa tipi Cabrio olacak
        private static readonly HashSet<string> CabrioModels = new()
        {
            "4 Serisi Cabrio",                               // BMW
            "C Serisi Cabrio", "E Serisi Cabrio", "S Serisi Cabrio", // Mercedes Cabrio
            "A5 Cabrio", "A3 Cabrio",                             // Audi Cabrio
            "500C",                                               // Fiat Cabrio
            "F-Type Cabrio",                                       // Jaguar Cabrio
            "LC Cabrio",                                           // Lexus Cabrio
            "GranCabrio",                                          // Maserati Cabrio
            "911 Cabrio",                                          // Porsche Cabrio
            "Cooper Cabrio", "Cooper S Cabrio"                     // Mini Cabrio
        };

        // Roadster modelleri - kasa tipi Roadster olacak
        private static readonly HashSet<string> RoadsterModels = new()
        {
            "Z4",                                            // BMW
            "SL", "SLC", "SLK", "AMG GT Roadster",              // Mercedes Roadster
            "TT Roadster", "R8 Spyder",                           // Audi Roadster
            "F-Type Roadster",                                     // Jaguar Roadster
            "MC20 Cielo",                                          // Maserati Roadster
            "718 Boxster", "911 Targa"                             // Porsche Roadster
        };

        // Station Wagon modelleri - kasa tipi Station Wagon olacak
        private static readonly HashSet<string> StationWagonModels = new()
        {
            "Megane Sport Tourer",                           // Renault
            "Ceed SW",                                           // Kia
            "C Serisi Estate", "E Serisi Estate",                // Mercedes Station Wagon
            "A4 Avant", "A6 Avant",                               // Audi Station Wagon
            "Tipo Station Wagon", "Marea Weekend",                // Fiat Station Wagon
            "Octavia Combi", "Superb Combi",                      // Skoda Station Wagon
            "Astra Sports Tourer", "Insignia Sports Tourer",      // Opel Station Wagon
            "307 SW", "308 SW", "407 SW", "508 SW",                  // Peugeot Station Wagon
            "Leon ST",                                            // Seat Station Wagon
            "Leon Sportstourer",                                  // Cupra Station Wagon
            "C5 Tourer",                                          // Citroen Station Wagon
            "Clubman"                                              // Mini Station Wagon
        };

        // Crossover modelleri - kasa tipi Crossover olacak
        private static readonly HashSet<string> CrossoverModels = new()
        {
            "X2", "iX2",                                     // BMW
            "Arkana",                                            // Renault
            "XCeed",                                             // Kia
            "i20 Active",                                            // Hyundai
            "Juke",                                                  // Nissan Crossover
            "500X Cross",                                                // Fiat Crossover
            "Kamiq",                                                     // Skoda Crossover
            "Arona",                                                     // Seat Crossover
            "C3 Aircross Crossover"                                      // Citroen Crossover
        };

        // Minivan & Panelvan modelleri
        private static readonly HashSet<string> MinivanPanelvanModels = new()
        {
            "Boxer", "Rifter", "Partner", "Expert", "Traveller", "Bipper", // Peugeot Ticari/Minivan
            "Berlingo", "SpaceTourer", "Jumpy", "Jumper",        // Citroen Ticari/Minivan
            "Alhambra",                                           // Seat Minivan
            "Kangoo", "Scenic", "Grand Scenic",                  // Renault Minivan
            "Kangoo Express", "Master", "Trafic",                // Renault Panelvan/Ticari
            "Staria", "H-1",                                         // Hyundai Minivan
            "Vito", "V Serisi", "Sprinter",                          // Mercedes Minivan/Ticari
            "NV300", "NV400",                                            // Nissan Ticari
            "Doblo", "Fiorino", "Ducato", "Scudo",                      // Fiat Ticari/Minivan
            "Doblo Combi", "Fiorino Combi", "Egea MultiWagon",          // Fiat Minivan/SW
            "Combo", "Combo Life", "Vivaro", "Movano"                    // Opel Ticari
        };

        private static readonly HashSet<string> ScooterModels = new()
        {
            "PCX 125", "PCX 160", "Activa 125", "NMAX 155", "Address 125", "Jupiter 125",
            "Primavera 150", "Sprint 150", "Liberty 125", "SR GT 200", "Bluebird",
            "SH125i", "Aerox 155", "Medley 150", "NTorq 125", "Jet X 125",
            "Fiddle 125", "Agility 125", "Like 125", "VN50 Pro"
        };

        private static readonly HashSet<string> MaxiScooterModels = new()
        {
            "Forza 250", "Forza 750", "ADV 350", "XMAX 250", "XMAX 300",
            "Burgman 400", "GTS 300", "Beverly 400", "MP3 400", "C 400 X",
            "C 400 GT", "Joymax Z 250", "Cruisym 250", "Downtown 250i", "X-Treme Max 200i"
        };

        private static readonly HashSet<string> NakedModels = new()
        {
            "CB 650R", "Hornet 750", "MT-07", "MT-09", "MT-25", "Z650", "Z900",
            "390 Duke", "250 Duke", "Monster", "Street Triple RS", "Trident 660",
            "Apache RTR 200", "G 310 R", "F 900 R", "GSX-S750", "GSX-8S",
            "Tuono 660", "Pulsar NS200", "Pulsar N250", "TNT 125", "302S",
            "752S", "250NK", "SRK 250", "SRK 550", "Pagani 250i", "Speed Twin 900"
        };

        private static readonly HashSet<string> SportModels = new()
        {
            "S 1000 RR", "CBR 650R", "CBR 500R", "R25", "R3", "R6", "R7",
            "Ninja 250", "Ninja 650", "Ninja ZX-6R", "GSX-R1000", "Hayabusa",
            "RC 390", "Panigale V2", "RS 660", "RSV4", "450SR", "250SR", "Apache RR 310"
        };

        private static readonly HashSet<string> TouringModels = new()
        {
            "Gold Wing", "Street Glide", "Rocket 3", "Tracer 9"
        };

        private static readonly HashSet<string> AdventureEnduroModels = new()
        {
            "R 1250 GS", "F 850 GS", "G 310 GS", "Africa Twin", "Tenere 700", "Versys 650",
            "V-Strom 650", "V-Strom 800DE", "790 Adventure", "1290 Super Adventure S",
            "Multistrada V4", "Tiger 900", "Pan America 1250", "Tuareg 660",
            "NC750X", "KLR 650", "Himalayan 450", "TRK 502X", "650MT", "800MT",
            "SRT 550", "Norden 901"
        };

        private static readonly HashSet<string> ChopperCruiserModels = new()
        {
            "R 18", "Diavel", "Sportster S", "Iron 883", "Fat Bob 114",
            "Bonneville T120", "Superlight 125", "Forty-Eight", "Classic 350",
            "Meteor 350", "Super Meteor 650"
        };

        private static readonly HashSet<string> CrossMotocrossModels = new()
        {
            "KX 250", "RM-Z450", "SX-F 450", "SMX 125", "TK03", "ATR 125"
        };

        private static readonly HashSet<string> CafeRacerScramblerModels = new()
        {
            "Scrambler Icon", "Svartpilen 401", "Vitpilen 401", "Drift L",
            "Scrambler 900", "Interceptor 650", "700CL-X", "Leoncino 250"
        };

        private static readonly HashSet<string> UnderboneCubModels = new()
        {
            "Super Cub C125", "Crypton S"
        };

        private static readonly HashSet<string> ElectricMotorcycleModels = new()
        {
            "CE 04", "Elettrica", "NQi GTS", "MQi GT"
        };

        private static readonly HashSet<string> SupermotoModels = new()
        {
            "690 SMC R", "701 Supermoto", "DR-Z400SM", "Hypermotard 950"
        };

        private static readonly Dictionary<string, string[]> MotorcycleBrandModels = new()
        {
            { "BMW", new[] { "R 1250 GS", "F 850 GS", "G 310 GS", "S 1000 RR", "G 310 R", "F 900 R", "C 400 X", "C 400 GT", "CE 04", "R 18" } },
            { "Honda", new[] { "CBR 650R", "CBR 500R", "CB 650R", "Hornet 750", "PCX 125", "PCX 160", "Forza 250", "Forza 750", "ADV 350", "Africa Twin", "Gold Wing", "NC750X", "Activa 125", "SH125i", "Super Cub C125" } },
            { "Yamaha", new[] { "R25", "R3", "R6", "R7", "MT-07", "MT-09", "MT-25", "NMAX 155", "XMAX 250", "XMAX 300", "Tenere 700", "Tracer 9", "Aerox 155", "Crypton S" } },
            { "Kawasaki", new[] { "Ninja 250", "Ninja 650", "Ninja ZX-6R", "Z650", "Z900", "Versys 650", "KLR 650", "KX 250" } },
            { "Suzuki", new[] { "GSX-R1000", "GSX-S750", "GSX-8S", "Hayabusa", "V-Strom 650", "V-Strom 800DE", "Burgman 400", "Address 125", "RM-Z450", "DR-Z400SM" } },
            { "KTM", new[] { "390 Duke", "250 Duke", "790 Adventure", "1290 Super Adventure S", "RC 390", "690 SMC R", "SX-F 450" } },
            { "Ducati", new[] { "Panigale V2", "Monster", "Multistrada V4", "Scrambler Icon", "Diavel", "Hypermotard 950" } },
            { "Harley-Davidson", new[] { "Sportster S", "Iron 883", "Fat Bob 114", "Street Glide", "Pan America 1250", "Forty-Eight" } },
            { "Triumph", new[] { "Street Triple RS", "Trident 660", "Tiger 900", "Bonneville T120", "Rocket 3", "Scrambler 900", "Speed Twin 900" } },
            { "Vespa", new[] { "Primavera 150", "Sprint 150", "GTS 300", "Elettrica" } },
            { "Piaggio", new[] { "Liberty 125", "Beverly 400", "MP3 400", "Medley 150" } },
            { "Aprilia", new[] { "RS 660", "RSV4", "Tuono 660", "SR GT 200", "Tuareg 660" } },
            { "Bajaj", new[] { "Pulsar NS200", "Pulsar N250", "Dominar 400" } },
            { "TVS", new[] { "Apache RTR 200", "Apache RR 310", "Jupiter 125", "NTorq 125" } },
            { "Mondial", new[] { "Drift L", "X-Treme Max 200i", "SMX 125", "Turismo 350i", "Pagani 250i" } },
            { "Kuba", new[] { "Superlight 125", "Bluebird", "TK03", "VN50 Pro", "ATR 125" } },
            { "Husqvarna", new[] { "Svartpilen 401", "Vitpilen 401", "Norden 901", "701 Supermoto" } },
            { "Royal Enfield", new[] { "Classic 350", "Meteor 350", "Hunter 350", "Himalayan 450", "Interceptor 650", "Super Meteor 650" } },
            { "Benelli", new[] { "TNT 125", "302S", "Leoncino 250", "TRK 502X", "752S" } },
            { "CFMOTO", new[] { "250NK", "450SR", "650MT", "700CL-X", "800MT" } },
            { "QJMotor", new[] { "SRK 250", "SRK 550", "SRT 550" } },
            { "SYM", new[] { "Joymax Z 250", "Jet X 125", "Fiddle 125", "Cruisym 250" } },
            { "Kymco", new[] { "Agility 125", "Like 125", "Xciting VS 400", "Downtown 250i" } },
            { "NIU", new[] { "NQi GTS", "MQi GT" } }
        };

        private static void MergeMotorcycleBrandModels(Dictionary<string, string[]> brandModels)
        {
            foreach (var entry in MotorcycleBrandModels)
            {
                if (brandModels.TryGetValue(entry.Key, out var existingModels))
                {
                    brandModels[entry.Key] = existingModels.Concat(entry.Value).Distinct().ToArray();
                    continue;
                }

                brandModels[entry.Key] = entry.Value;
            }
        }

        private static Guid? ResolveBodyTypeId(string modelName, IReadOnlyDictionary<string, Guid?> bodyTypeMap)
        {
            if (SuvModels.Contains(modelName))
                return bodyTypeMap["SUV"];
            if (SedanModels.Contains(modelName))
                return bodyTypeMap["Sedan"];
            if (HatchbackModels.Contains(modelName))
                return bodyTypeMap["Hatchback"];
            if (CoupeModels.Contains(modelName))
                return bodyTypeMap["Coupe"];
            if (CabrioModels.Contains(modelName))
                return bodyTypeMap["Cabrio"];
            if (RoadsterModels.Contains(modelName))
                return bodyTypeMap["Roadster"];
            if (StationWagonModels.Contains(modelName))
                return bodyTypeMap["Station Wagon"];
            if (CrossoverModels.Contains(modelName))
                return bodyTypeMap["Crossover"];
            if (PickupModels.Contains(modelName))
                return bodyTypeMap["Pickup"];
            if (MinivanPanelvanModels.Contains(modelName))
                return bodyTypeMap["Minivan & Panelvan"];
            if (ScooterModels.Contains(modelName))
                return bodyTypeMap["Scooter"];
            if (MaxiScooterModels.Contains(modelName))
                return bodyTypeMap["Maxi Scooter"];
            if (NakedModels.Contains(modelName))
                return bodyTypeMap["Naked"];
            if (SportModels.Contains(modelName))
                return bodyTypeMap["Sport"];
            if (TouringModels.Contains(modelName))
                return bodyTypeMap["Touring"];
            if (AdventureEnduroModels.Contains(modelName))
                return bodyTypeMap["Enduro / Adventure"];
            if (ChopperCruiserModels.Contains(modelName))
                return bodyTypeMap["Chopper / Cruiser"];
            if (CrossMotocrossModels.Contains(modelName))
                return bodyTypeMap["Cross / Motocross"];
            if (CafeRacerScramblerModels.Contains(modelName))
                return bodyTypeMap["Cafe Racer / Scrambler"];
            if (UnderboneCubModels.Contains(modelName))
                return bodyTypeMap["Underbone / Cub"];
            if (ElectricMotorcycleModels.Contains(modelName))
                return bodyTypeMap["Elektrikli"];
            if (SupermotoModels.Contains(modelName))
                return bodyTypeMap["Supermoto"];

            return null;
        }

        public static async Task SeedAsync(Context context)
        {
            if (await context.Set<Brand>().AnyAsync())
                return;

            // BodyType'ları al
            var bodyTypes = await context.Set<BodyType>().ToListAsync();
            var suvBodyType = bodyTypes.FirstOrDefault(b => b.Name == "SUV");
            var sedanBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Sedan");
            var hatchbackBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Hatchback");
            var coupeBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Coupe");
            var cabrioBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Cabrio");
            var roadsterBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Roadster");
            var stationWagonBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Station Wagon");
            var crossoverBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Crossover");
            var pickupBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Pickup");
            var minivanPanelvanBodyType2 = bodyTypes.FirstOrDefault(b => b.Name == "Minivan & Panelvan");
            var scooterBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Scooter");
            var maxiScooterBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Maxi Scooter");
            var nakedBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Naked");
            var sportBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Sport");
            var touringBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Touring");
            var adventureBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Enduro / Adventure");
            var cruiserBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Chopper / Cruiser");
            var crossBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Cross / Motocross");
            var scramblerBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Cafe Racer / Scrambler");
            var underboneBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Underbone / Cub");
            var electricMotorcycleBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Elektrikli");
            var supermotoBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Supermoto");

            var brandModels = new Dictionary<string, string[]>
            {
                { "BMW", new[] {
                    // Hatchback
                    "1 Serisi", "2 Serisi Active Tourer",
                    // Sedan / Gran Coupe
                    "2 Serisi Gran Coupe", "3 Serisi", "4 Serisi Gran Coupe",
                    "5 Serisi", "7 Serisi", "8 Serisi Gran Coupe",
                    // Coupe
                    "2 Serisi Coupe", "4 Serisi Coupe", "8 Serisi Coupe", "M2", "M4",
                    // Cabrio
                    "4 Serisi Cabrio",
                    // Roadster
                    "Z4",
                    // Crossover
                    "X2", "iX2",
                    // SUV
                    "X1", "X3", "X4", "X5", "X6", "X7",
                    // Elektrikli
                    "i4", "i5", "i7", "iX", "iX1", "iX3"
                } },
                { "Mercedes", new[] {
                    // Hatchback
                    "A Serisi", "B Serisi",
                    // Sedan
                    "C Serisi", "E Serisi", "S Serisi", "CLA", "CLS", "AMG GT 4 Kapı", "EQE", "EQS",
                    // Coupe
                    "CLA Coupe", "C Serisi Coupe", "E Serisi Coupe", "S Serisi Coupe", "AMG GT Coupe",
                    // Cabrio
                    "C Serisi Cabrio", "E Serisi Cabrio", "S Serisi Cabrio",
                    // Roadster
                    "SL", "SLC", "SLK", "AMG GT Roadster",
                    // Station Wagon
                    "C Serisi Estate", "E Serisi Estate",
                    // SUV
                    "GLA", "GLB", "GLC", "GLC Coupe", "GLE", "GLE Coupe", "GLS", "G Serisi",
                    "EQA", "EQB", "EQC",
                    // Minivan & Ticari
                    "Vito", "V Serisi", "Sprinter"
                } },
                { "Audi", new[] {
                    // Hatchback
                    "A1", "A3",
                    // Sedan
                    "A4", "A5 Sportback", "A6", "A7 Sportback", "A8",
                    // Coupe
                    "A5 Coupe", "TT Coupe", "R8 Coupe",
                    // Cabrio
                    "A5 Cabrio", "A3 Cabrio",
                    // Roadster
                    "TT Roadster", "R8 Spyder",
                    // Station Wagon
                    "A4 Avant", "A6 Avant",
                    // SUV
                    "Q2", "Q3", "Q3 Sportback", "Q5", "Q5 Sportback",
                    "Q7", "Q8", "Q8 e-tron", "e-tron", "e-tron GT"
                } },
                { "Volkswagen", new[] { "Golf", "Polo", "Passat", "Tiguan", "Touareg", "Amarok" } },
                { "Toyota", new[] { "Corolla", "Yaris", "C-HR", "RAV4", "Hilux", "Land Cruiser" } },
                { "Volvo", new[] { "S60", "S90", "XC40", "XC60", "XC90" } },
                { "Tesla", new[] { "Model 3", "Model Y", "Model S", "Model X" } },
                { "Honda", new[] {
                    // Hatchback
                    "Civic HB", "Jazz", "Honda e",
                    // Sedan
                    "Civic Sedan", "City", "Accord",
                    // SUV
                    "HR-V", "CR-V", "ZR-V", "e:Ny1"
                } },
                { "Ford", new[] { "Focus", "Fiesta", "Puma", "Kuga", "Ranger" } },
                { "Hyundai", new[] {
                    // Hatchback
                    "i10", "i20", "i30", "IONIQ HB",
                    // Sedan
                    "Elantra", "Sonata", "Accent", "Accent Blue", "IONIQ 6",
                    // Crossover
                    "i20 Active",
                    // SUV
                    "Tucson", "Kona", "Kona Electric", "Santa Fe", "Bayon", "IONIQ 5", "ix35", "Venue",
                    // Minivan
                    "Staria", "H-1"
                } },
                { "Renault", new[] {
                    // Hatchback
                    "Clio", "Megane HB", "Zoe",
                    // Sedan
                    "Megane Sedan", "Fluence", "Talisman", "Symbol", "Latitude",
                    // Station Wagon
                    "Megane Sport Tourer",
                    // Crossover
                    "Arkana",
                    // SUV
                    "Captur", "Kadjar", "Austral", "Koleos",
                    // Minivan & Panelvan
                    "Kangoo", "Scenic", "Grand Scenic",
                    "Kangoo Express", "Master", "Trafic"
                } },
                { "Kia", new[] {
                    // Hatchback
                    "Picanto", "Rio", "Ceed", "ProCeed",
                    // Sedan
                    "Cerato", "Stinger", "K5",
                    // Station Wagon
                    "Ceed SW",
                    // Crossover
                    "XCeed",
                    // SUV
                    "Sportage", "Sorento", "Niro", "Stonic",
                    // Elektrikli SUV
                    "EV6", "EV9", "Niro EV"
                } },
                { "Peugeot", new[] {
                    // Hatchback
                    "108", "206", "207", "208", "307", "308", "e-208",
                    // Sedan
                    "301", "407", "408", "508",
                    // Station Wagon
                    "307 SW", "308 SW", "407 SW", "508 SW",
                    // SUV
                    "2008", "3008", "5008", "e-2008",
                    // Minivan/Ticari
                    "Boxer", "Rifter", "Partner", "Expert", "Traveller", "Bipper"
                } },
                { "Land Rover", new[] {
                    // SUV
                    "Range Rover", "Range Rover Sport", "Range Rover Velar",
                    "Range Rover Evoque", "Discovery", "Discovery Sport",
                    "Defender", "Freelander 2"
                } },
                { "BYD", new[] {
                    // Hatchback
                    "Dolphin",
                    // Sedan
                    "Seal", "Han",
                    // SUV
                    "Atto 3", "Seal U", "Tang"
                } },
                { "Chery", new[] {
                    // Sedan
                    "Arrizo 5",
                    // SUV
                    "Tiggo 4 Pro", "Tiggo 7 Pro", "Tiggo 8 Pro",
                    "Omoda 5"
                } },
                { "Nissan", new[] {
                    // Hatchback
                    "Micra", "Note", "Pulsar", "Leaf",
                    // Crossover
                    "Juke",
                    // SUV
                    "Qashqai", "X-Trail",
                    // Pickup
                    "Navara",
                    // Minivan/Ticari
                    "NV300", "NV400"
                } },
                { "Fiat", new[] {
                    // Hatchback
                    "Punto", "Grande Punto", "Punto Evo", "Bravo",
                    "Tipo HB", "Palio", "Stilo",
                    "500", "500e", "Fiat 600e",
                    // Sedan
                    "Linea", "Albea", "Tipo Sedan", "Marea",
                    // Cabrio
                    "500C",
                    // Station Wagon
                    "Tipo Station Wagon", "Marea Weekend",
                    // Crossover
                    "500X Cross",
                    // SUV
                    "500X", "Freemont",
                    // Pickup
                    "Fullback",
                    // Minivan/Ticari
                    "Doblo", "Fiorino", "Ducato", "Scudo",
                    "Doblo Combi", "Fiorino Combi", "Egea MultiWagon"
                } },
                { "Skoda", new[] {
                    // Hatchback
                    "Fabia", "Scala", "Rapid",
                    // Sedan
                    "Octavia", "Superb",
                    // Station Wagon
                    "Octavia Combi", "Superb Combi",
                    // Crossover
                    "Kamiq",
                    // SUV
                    "Karoq", "Kodiaq"
                } },
                { "Opel", new[] {
                    // Hatchback
                    "Corsa", "Corsa-e", "Astra", "Astra HB",
                    // Sedan
                    "Insignia", "Astra Sedan",
                    // Station Wagon
                    "Astra Sports Tourer", "Insignia Sports Tourer",
                    // SUV
                    "Mokka", "Mokka-e", "Grandland", "Crossland",
                    // Minivan/Ticari
                    "Combo", "Combo Life", "Vivaro", "Movano"
                } },
                { "Seat", new[] {
                    // Hatchback
                    "Ibiza", "Leon", "Leon HB",
                    // Sedan
                    "Toledo",
                    // Station Wagon
                    "Leon ST",
                    // Crossover
                    "Arona",
                    // SUV
                    "Ateca", "Tarraco",
                    // Minivan
                    "Alhambra"
                } },
                { "Cupra", new[] {
                    // Hatchback
                    "Born",
                    // Station Wagon
                    "Leon Sportstourer",
                    // SUV
                    "Formentor", "Terramar"
                } },
                { "Togg", new[] {
                    // Hatchback/Fastback
                    "T10F",
                    // SUV
                    "T10X"
                } },
                { "Citroen", new[] {
                    // Hatchback
                    "C3", "C4", "C4 X",
                    // Sedan
                    "C-Elysee", "C4 Sedan",
                    // Station Wagon
                    "C5 Tourer",
                    // SUV
                    "C3 Aircross", "C5 Aircross",
                    // Minivan/Ticari
                    "Berlingo", "SpaceTourer", "Jumpy", "Jumper"
                } },
                { "Jaguar", new[] {
                    // Sedan
                    "XE", "XF",
                    // Coupe
                    "F-Type Coupe",
                    // Cabrio
                    "F-Type Cabrio",
                    // Roadster
                    "F-Type Roadster",
                    // SUV
                    "F-Pace", "E-Pace", "I-Pace"
                } },
                { "Lexus", new[] {
                    // Hatchback
                    "CT",
                    // Sedan
                    "IS", "ES", "GS", "LS",
                    // Coupe
                    "RC", "LC",
                    // Cabrio
                    "LC Cabrio",
                    // SUV
                    "NX", "RX", "UX", "LX"
                } },
                { "Maserati", new[] {
                    // Sedan
                    "Ghibli", "Quattroporte",
                    // Coupe
                    "MC20", "GranTurismo",
                    // Cabrio
                    "GranCabrio",
                    // Roadster
                    "MC20 Cielo",
                    // SUV
                    "Levante", "Grecale"
                } },
                { "Porsche", new[] {
                    // Sedan
                    "Panamera", "Taycan",
                    // Coupe
                    "911", "718 Cayman",
                    // Cabrio
                    "911 Cabrio",
                    // Roadster
                    "718 Boxster", "911 Targa",
                    // SUV
                    "Cayenne", "Macan"
                } },
                { "Mini", new[] {
                    // Hatchback
                    "Cooper", "Cooper S", "Cooper SE",
                    // Cabrio
                    "Cooper Cabrio", "Cooper S Cabrio",
                    // Station Wagon
                    "Clubman",
                    // SUV
                    "Countryman", "Countryman SE"
                } },
                { "Suzuki", new[] {
                    // Hatchback
                    "Swift", "Baleno", "Celerio", "Ignis",
                    // SUV
                    "Vitara", "S-Cross", "Jimny"
                } },
                { "DS", new[] {
                    // Hatchback
                    "DS 3", "DS 4",
                    // Sedan
                    "DS 9",
                    // SUV
                    "DS 3 Crossback", "DS 7", "DS 7 Crossback"
                } },
                { "Chevrolet", new[] {
                    // Hatchback
                    "Aveo", "Cruze HB", "Spark",
                    // Sedan
                    "Cruze", "Malibu", "Lacetti",
                    // SUV
                    "Captiva", "Trax", "Trailblazer"
                } }
            };

            MergeMotorcycleBrandModels(brandModels);

            var bodyTypeMap = new Dictionary<string, Guid?>
            {
                ["SUV"] = suvBodyType?.Id,
                ["Sedan"] = sedanBodyType?.Id,
                ["Hatchback"] = hatchbackBodyType?.Id,
                ["Coupe"] = coupeBodyType?.Id,
                ["Cabrio"] = cabrioBodyType?.Id,
                ["Roadster"] = roadsterBodyType?.Id,
                ["Station Wagon"] = stationWagonBodyType?.Id,
                ["Crossover"] = crossoverBodyType?.Id,
                ["Pickup"] = pickupBodyType?.Id,
                ["Minivan & Panelvan"] = minivanPanelvanBodyType2?.Id,
                ["Scooter"] = scooterBodyType?.Id,
                ["Maxi Scooter"] = maxiScooterBodyType?.Id,
                ["Naked"] = nakedBodyType?.Id,
                ["Sport"] = sportBodyType?.Id,
                ["Touring"] = touringBodyType?.Id,
                ["Enduro / Adventure"] = adventureBodyType?.Id,
                ["Chopper / Cruiser"] = cruiserBodyType?.Id,
                ["Cross / Motocross"] = crossBodyType?.Id,
                ["Cafe Racer / Scrambler"] = scramblerBodyType?.Id,
                ["Underbone / Cub"] = underboneBodyType?.Id,
                ["Elektrikli"] = electricMotorcycleBodyType?.Id,
                ["Supermoto"] = supermotoBodyType?.Id
            };

            foreach (var brandData in brandModels)
            {
                var brand = new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = brandData.Key,
                    CreatedAt = DateTime.UtcNow
                };

                context.Set<Brand>().Add(brand);

                foreach (var modelName in brandData.Value)
                {
                    var bodyTypeId = ResolveBodyTypeId(modelName, bodyTypeMap);

                    var model = new Model
                    {
                        Id = Guid.NewGuid(),
                        BrandId = brand.Id,
                        BodyTypeId = bodyTypeId,
                        Name = modelName,
                        CreatedAt = DateTime.UtcNow
                    };

                    context.Set<Model>().Add(model);
                }
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Mevcut markalara eksik modelleri ekler (yeni model seed sonrası için)
        /// </summary>
        public static async Task AddMissingModelsAsync(Context context)
        {
            // VehicleType-aware BodyType seçimi (duplicate body type sorunu için)
            var vehicleTypes = await context.Set<VehicleType>().ToListAsync();
            var otomobilVT = vehicleTypes.FirstOrDefault(vt => vt.Name == "Otomobil");
            var motosikletVT = vehicleTypes.FirstOrDefault(vt => vt.Name == "Motosiklet");
            var suvAraziVT = vehicleTypes.FirstOrDefault(vt => vt.Name == "SUV & Arazi Araçları");

            var bodyTypes = await context.Set<BodyType>().ToListAsync();
            var suvBodyType = bodyTypes.FirstOrDefault(b => b.Name == "SUV" && b.VehicleTypeId == suvAraziVT?.Id);
            var sedanBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Sedan" && b.VehicleTypeId == otomobilVT?.Id);
            var hatchbackBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Hatchback" && b.VehicleTypeId == otomobilVT?.Id);
            var coupeBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Coupe" && b.VehicleTypeId == otomobilVT?.Id);
            var cabrioBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Cabrio" && b.VehicleTypeId == otomobilVT?.Id);
            var roadsterBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Roadster" && b.VehicleTypeId == otomobilVT?.Id);
            var stationWagonBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Station Wagon" && b.VehicleTypeId == otomobilVT?.Id);
            var crossoverBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Crossover" && b.VehicleTypeId == suvAraziVT?.Id);
            var pickupBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Pickup" && b.VehicleTypeId == suvAraziVT?.Id);
            var minivanPanelvanBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Minivan & Panelvan" && b.VehicleTypeId == suvAraziVT?.Id);
            var scooterBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Scooter" && b.VehicleTypeId == motosikletVT?.Id);
            var maxiScooterBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Maxi Scooter" && b.VehicleTypeId == motosikletVT?.Id);
            var nakedBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Naked" && b.VehicleTypeId == motosikletVT?.Id);
            var sportBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Sport" && b.VehicleTypeId == motosikletVT?.Id);
            var touringBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Touring" && b.VehicleTypeId == motosikletVT?.Id);
            var adventureBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Enduro / Adventure" && b.VehicleTypeId == motosikletVT?.Id);
            var cruiserBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Chopper / Cruiser" && b.VehicleTypeId == motosikletVT?.Id);
            var crossBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Cross / Motocross" && b.VehicleTypeId == motosikletVT?.Id);
            var scramblerBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Cafe Racer / Scrambler" && b.VehicleTypeId == motosikletVT?.Id);
            var underboneBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Underbone / Cub" && b.VehicleTypeId == motosikletVT?.Id);
            var electricMotorcycleBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Elektrikli" && b.VehicleTypeId == motosikletVT?.Id);
            var supermotoBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Supermoto" && b.VehicleTypeId == motosikletVT?.Id);

            var brandModels = new Dictionary<string, string[]>
            {
                { "BMW", new[] {
                    "1 Serisi", "2 Serisi Active Tourer",
                    "2 Serisi Gran Coupe", "7 Serisi", "4 Serisi Gran Coupe",
                    "8 Serisi Gran Coupe", "i4", "i5", "i7",
                    "2 Serisi Coupe", "4 Serisi Coupe", "8 Serisi Coupe", "M2", "M4",
                    "4 Serisi Cabrio", "Z4",
                    "X2", "iX2", "X4", "X6", "X7", "iX1", "iX3"
                } },
                { "Mercedes", new[] {
                    // Hatchback
                    "A Serisi", "B Serisi",
                    // Sedan
                    "C Serisi", "E Serisi", "S Serisi", "CLA", "CLS", "AMG GT 4 Kapı", "EQE", "EQS",
                    // Coupe
                    "CLA Coupe", "C Serisi Coupe", "E Serisi Coupe", "S Serisi Coupe", "AMG GT Coupe",
                    // Cabrio
                    "C Serisi Cabrio", "E Serisi Cabrio", "S Serisi Cabrio",
                    // Roadster
                    "SL", "SLC", "SLK", "AMG GT Roadster",
                    // Station Wagon
                    "C Serisi Estate", "E Serisi Estate",
                    // SUV
                    "GLA", "GLB", "GLC", "GLC Coupe", "GLE", "GLE Coupe", "GLS", "G Serisi",
                    "EQA", "EQB", "EQC",
                    // Minivan & Ticari
                    "Vito", "V Serisi", "Sprinter"
                } },
                { "Audi", new[] {
                    // Hatchback
                    "A1", "A3",
                    // Sedan
                    "A4", "A5 Sportback", "A6", "A7 Sportback", "A8",
                    // Coupe
                    "A5 Coupe", "TT Coupe", "R8 Coupe",
                    // Cabrio
                    "A5 Cabrio", "A3 Cabrio",
                    // Roadster
                    "TT Roadster", "R8 Spyder",
                    // Station Wagon
                    "A4 Avant", "A6 Avant",
                    // SUV
                    "Q2", "Q3", "Q3 Sportback", "Q5", "Q5 Sportback",
                    "Q7", "Q8", "Q8 e-tron", "e-tron", "e-tron GT"
                } },
                { "Honda", new[] { "Jazz", "Accord" } },
                { "Hyundai", new[] {
                    // Hatchback
                    "i10", "i20", "i30", "IONIQ HB",
                    // Sedan
                    "Elantra", "Sonata", "Accent", "Accent Blue", "IONIQ 6",
                    // Crossover
                    "i20 Active",
                    // SUV
                    "Tucson", "Kona", "Kona Electric", "Santa Fe", "Bayon", "IONIQ 5", "ix35", "Venue",
                    // Minivan
                    "Staria", "H-1"
                } },
                { "Renault", new[] {
                    // Hatchback
                    "Clio", "Megane HB", "Zoe",
                    // Sedan
                    "Megane Sedan", "Fluence", "Talisman", "Symbol", "Latitude",
                    // Station Wagon
                    "Megane Sport Tourer",
                    // Crossover
                    "Arkana",
                    // SUV
                    "Captur", "Kadjar", "Austral", "Koleos",
                    // Minivan & Panelvan
                    "Kangoo", "Scenic", "Grand Scenic",
                    "Kangoo Express", "Master", "Trafic"
                } },
                { "Kia", new[] {
                    // Hatchback
                    "Picanto", "Rio", "Ceed", "ProCeed",
                    // Sedan
                    "Cerato", "Stinger", "K5",
                    // Station Wagon
                    "Ceed SW",
                    // Crossover
                    "XCeed",
                    // SUV
                    "Sportage", "Sorento", "Niro", "Stonic",
                    // Elektrikli SUV
                    "EV6", "EV9", "Niro EV"
                } },
                { "Peugeot", new[] {
                    // Hatchback
                    "108", "206", "207", "208", "307", "308", "e-208",
                    // Sedan
                    "301", "407", "408", "508",
                    // Station Wagon
                    "307 SW", "308 SW", "407 SW", "508 SW",
                    // SUV
                    "2008", "3008", "5008", "e-2008",
                    // Minivan/Ticari
                    "Boxer", "Rifter", "Partner", "Expert", "Traveller", "Bipper"
                } },
                { "Land Rover", new[] {
                    // SUV
                    "Range Rover", "Range Rover Sport", "Range Rover Velar",
                    "Range Rover Evoque", "Discovery", "Discovery Sport",
                    "Defender", "Freelander 2"
                } },
                { "BYD", new[] {
                    // Hatchback
                    "Dolphin",
                    // Sedan
                    "Seal", "Han",
                    // SUV
                    "Atto 3", "Seal U", "Tang"
                } },
                { "Chery", new[] {
                    // Sedan
                    "Arrizo 5",
                    // SUV
                    "Tiggo 4 Pro", "Tiggo 7 Pro", "Tiggo 8 Pro",
                    "Omoda 5"
                } },
                { "Nissan", new[] {
                    // Hatchback
                    "Micra", "Note", "Pulsar", "Leaf",
                    // Crossover
                    "Juke",
                    // SUV
                    "Qashqai", "X-Trail",
                    // Pickup
                    "Navara",
                    // Minivan/Ticari
                    "NV300", "NV400"
                } },
                { "Fiat", new[] {
                    // Hatchback
                    "Punto", "Grande Punto", "Punto Evo", "Bravo",
                    "Tipo HB", "Palio", "Stilo",
                    "500", "500e", "Fiat 600e",
                    // Sedan
                    "Linea", "Albea", "Tipo Sedan", "Marea",
                    // Cabrio
                    "500C",
                    // Station Wagon
                    "Tipo Station Wagon", "Marea Weekend",
                    // Crossover
                    "500X Cross",
                    // SUV
                    "500X", "Freemont",
                    // Pickup
                    "Fullback",
                    // Minivan/Ticari
                    "Doblo", "Fiorino", "Ducato", "Scudo",
                    "Doblo Combi", "Fiorino Combi", "Egea MultiWagon"
                } },
                { "Skoda", new[] {
                    "Fabia", "Scala", "Rapid",
                    "Octavia", "Superb",
                    "Octavia Combi", "Superb Combi",
                    "Kamiq",
                    "Karoq", "Kodiaq"
                } },
                { "Opel", new[] {
                    "Corsa", "Corsa-e", "Astra", "Astra HB",
                    "Insignia", "Astra Sedan",
                    "Astra Sports Tourer", "Insignia Sports Tourer",
                    "Mokka", "Mokka-e", "Grandland", "Crossland",
                    "Combo", "Combo Life", "Vivaro", "Movano"
                } },
                { "Seat", new[] {
                    "Ibiza", "Leon", "Leon HB",
                    "Toledo",
                    "Leon ST",
                    "Arona",
                    "Ateca", "Tarraco",
                    "Alhambra"
                } },
                { "Cupra", new[] {
                    "Born",
                    "Leon Sportstourer",
                    "Formentor", "Terramar"
                } },
                { "Togg", new[] {
                    "T10F",
                    "T10X"
                } },
                { "Citroen", new[] {
                    "C3", "C4", "C4 X",
                    "C-Elysee", "C4 Sedan",
                    "C5 Tourer",
                    "C3 Aircross", "C5 Aircross",
                    "C3 Aircross Crossover",
                    "Berlingo", "SpaceTourer", "Jumpy", "Jumper"
                } },
                { "Jaguar", new[] {
                    "XE", "XF",
                    "F-Type Coupe",
                    "F-Type Cabrio",
                    "F-Type Roadster",
                    "F-Pace", "E-Pace", "I-Pace"
                } },
                { "Lexus", new[] {
                    "CT",
                    "IS", "ES", "GS", "LS",
                    "RC", "LC",
                    "LC Cabrio",
                    "NX", "RX", "UX", "LX"
                } },
                { "Maserati", new[] {
                    "Ghibli", "Quattroporte",
                    "MC20", "GranTurismo",
                    "GranCabrio",
                    "MC20 Cielo",
                    "Levante", "Grecale"
                } },
                { "Porsche", new[] {
                    "Panamera", "Taycan",
                    "911", "718 Cayman",
                    "911 Cabrio",
                    "718 Boxster", "911 Targa",
                    "Cayenne", "Macan"
                } },
                { "Mini", new[] {
                    "Cooper", "Cooper S", "Cooper SE",
                    "Cooper Cabrio", "Cooper S Cabrio",
                    "Clubman",
                    "Countryman", "Countryman SE"
                } },
                { "Suzuki", new[] {
                    "Swift", "Baleno", "Celerio", "Ignis",
                    "Vitara", "S-Cross", "Jimny"
                } },
                { "DS", new[] {
                    "DS 3", "DS 4",
                    "DS 9",
                    "DS 3 Crossback", "DS 7", "DS 7 Crossback"
                } },
                { "Chevrolet", new[] {
                    "Aveo", "Cruze HB", "Spark",
                    "Cruze", "Malibu", "Lacetti",
                    "Captiva", "Trax", "Trailblazer"
                } }
            };

            MergeMotorcycleBrandModels(brandModels);

            var bodyTypeMap = new Dictionary<string, Guid?>
            {
                ["SUV"] = suvBodyType?.Id,
                ["Sedan"] = sedanBodyType?.Id,
                ["Hatchback"] = hatchbackBodyType?.Id,
                ["Coupe"] = coupeBodyType?.Id,
                ["Cabrio"] = cabrioBodyType?.Id,
                ["Roadster"] = roadsterBodyType?.Id,
                ["Station Wagon"] = stationWagonBodyType?.Id,
                ["Crossover"] = crossoverBodyType?.Id,
                ["Pickup"] = pickupBodyType?.Id,
                ["Minivan & Panelvan"] = minivanPanelvanBodyType?.Id,
                ["Scooter"] = scooterBodyType?.Id,
                ["Maxi Scooter"] = maxiScooterBodyType?.Id,
                ["Naked"] = nakedBodyType?.Id,
                ["Sport"] = sportBodyType?.Id,
                ["Touring"] = touringBodyType?.Id,
                ["Enduro / Adventure"] = adventureBodyType?.Id,
                ["Chopper / Cruiser"] = cruiserBodyType?.Id,
                ["Cross / Motocross"] = crossBodyType?.Id,
                ["Cafe Racer / Scrambler"] = scramblerBodyType?.Id,
                ["Underbone / Cub"] = underboneBodyType?.Id,
                ["Elektrikli"] = electricMotorcycleBodyType?.Id,
                ["Supermoto"] = supermotoBodyType?.Id
            };

            var existingBrands = await context.Set<Brand>()
                .Include(b => b.Models)
                .ToListAsync();

            var added = false;

            foreach (var entry in brandModels)
            {
                var brand = existingBrands.FirstOrDefault(b => b.Name == entry.Key);
                if (brand == null)
                {
                    // Yeni marka oluştur
                    brand = new Brand
                    {
                        Id = Guid.NewGuid(),
                        Name = entry.Key,
                        CreatedAt = DateTime.UtcNow
                    };
                    context.Set<Brand>().Add(brand);
                    added = true;
                }

                var existingModelNames = brand.Models?.Select(m => m.Name).ToHashSet() ?? new HashSet<string>();

                foreach (var modelName in entry.Value)
                {
                    if (existingModelNames.Contains(modelName)) continue;

                    var bodyTypeId = ResolveBodyTypeId(modelName, bodyTypeMap);

                    context.Set<Model>().Add(new Model
                    {
                        Id = Guid.NewGuid(),
                        BrandId = brand.Id,
                        BodyTypeId = bodyTypeId,
                        Name = modelName,
                        CreatedAt = DateTime.UtcNow
                    });

                    added = true;
                }
            }

            if (added)
                await context.SaveChangesAsync();
        }

        /// <summary>
        /// Mevcut modellere BodyTypeId ataması yapar - RAW SQL ile (EF Core change tracking bypass)
        /// ÖNEMLI: BodyType'ları doğru VehicleType altından seçer (duplicate body type sorunu için)
        /// </summary>
        public static async Task UpdateModelBodyTypesAsync(Context context)
        {
            context.ChangeTracker.Clear();

            // VehicleType'ları al - doğru parent'ı bulmak için
            var vehicleTypes = await context.Set<VehicleType>().AsNoTracking().ToListAsync();
            var otomobilVT = vehicleTypes.FirstOrDefault(vt => vt.Name == "Otomobil");
            var motosikletVT = vehicleTypes.FirstOrDefault(vt => vt.Name == "Motosiklet");
            var suvAraziVT = vehicleTypes.FirstOrDefault(vt => vt.Name == "SUV & Arazi Araçları");

            Console.WriteLine($"[Seed] VehicleTypes -> Otomobil: {otomobilVT?.Id}, SUV & Arazi: {suvAraziVT?.Id}");

            var bodyTypes = await context.Set<BodyType>().AsNoTracking().ToListAsync();

            // Doğru VehicleType altındaki BodyType'ları seç
            // Sedan ve Hatchback -> "Otomobil" altında olmalı
            // SUV ve Pickup -> "SUV & Arazi Araçları" altında olmalı
            var sedanBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Sedan" && bt.VehicleTypeId == otomobilVT?.Id);
            var hatchbackBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Hatchback" && bt.VehicleTypeId == otomobilVT?.Id);
            var suvBT = bodyTypes.FirstOrDefault(bt => bt.Name == "SUV" && bt.VehicleTypeId == suvAraziVT?.Id);
            var pickupBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Pickup" && bt.VehicleTypeId == suvAraziVT?.Id);
            var minivanPanelvanBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Minivan & Panelvan" && bt.VehicleTypeId == suvAraziVT?.Id);

            Console.WriteLine($"[Seed] Correct BodyTypes -> Sedan: {sedanBT?.Id}, Hatchback: {hatchbackBT?.Id}, SUV: {suvBT?.Id}, Pickup: {pickupBT?.Id}, Minivan & Panelvan: {minivanPanelvanBT?.Id}");

            var coupeBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Coupe" && bt.VehicleTypeId == otomobilVT?.Id);
            var cabrioBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Cabrio" && bt.VehicleTypeId == otomobilVT?.Id);
            var roadsterBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Roadster" && bt.VehicleTypeId == otomobilVT?.Id);
            var stationWagonBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Station Wagon" && bt.VehicleTypeId == otomobilVT?.Id);
            var crossoverBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Crossover" && bt.VehicleTypeId == suvAraziVT?.Id);
            var scooterBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Scooter" && bt.VehicleTypeId == motosikletVT?.Id);
            var maxiScooterBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Maxi Scooter" && bt.VehicleTypeId == motosikletVT?.Id);
            var nakedBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Naked" && bt.VehicleTypeId == motosikletVT?.Id);
            var sportBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Sport" && bt.VehicleTypeId == motosikletVT?.Id);
            var touringBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Touring" && bt.VehicleTypeId == motosikletVT?.Id);
            var adventureBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Enduro / Adventure" && bt.VehicleTypeId == motosikletVT?.Id);
            var cruiserBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Chopper / Cruiser" && bt.VehicleTypeId == motosikletVT?.Id);
            var crossBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Cross / Motocross" && bt.VehicleTypeId == motosikletVT?.Id);
            var scramblerBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Cafe Racer / Scrambler" && bt.VehicleTypeId == motosikletVT?.Id);
            var underboneBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Underbone / Cub" && bt.VehicleTypeId == motosikletVT?.Id);
            var electricMotorcycleBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Elektrikli" && bt.VehicleTypeId == motosikletVT?.Id);
            var supermotoBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Supermoto" && bt.VehicleTypeId == motosikletVT?.Id);

            var updates = new Dictionary<string, (BodyType? bodyType, HashSet<string> models)>
            {
                { "Sedan", (sedanBT, SedanModels) },
                { "Hatchback", (hatchbackBT, HatchbackModels) },
                { "Coupe", (coupeBT, CoupeModels) },
                { "Cabrio", (cabrioBT, CabrioModels) },
                { "Roadster", (roadsterBT, RoadsterModels) },
                { "Station Wagon", (stationWagonBT, StationWagonModels) },
                { "Crossover", (crossoverBT, CrossoverModels) },
                { "SUV", (suvBT, SuvModels) },
                { "Pickup", (pickupBT, PickupModels) },
                { "Minivan & Panelvan", (minivanPanelvanBT, MinivanPanelvanModels) },
                { "Scooter", (scooterBT, ScooterModels) },
                { "Maxi Scooter", (maxiScooterBT, MaxiScooterModels) },
                { "Naked", (nakedBT, NakedModels) },
                { "Sport", (sportBT, SportModels) },
                { "Touring", (touringBT, TouringModels) },
                { "Enduro / Adventure", (adventureBT, AdventureEnduroModels) },
                { "Chopper / Cruiser", (cruiserBT, ChopperCruiserModels) },
                { "Cross / Motocross", (crossBT, CrossMotocrossModels) },
                { "Cafe Racer / Scrambler", (scramblerBT, CafeRacerScramblerModels) },
                { "Underbone / Cub", (underboneBT, UnderboneCubModels) },
                { "Elektrikli", (electricMotorcycleBT, ElectricMotorcycleModels) },
                { "Supermoto", (supermotoBT, SupermotoModels) }
            };

            var totalUpdated = 0;
            foreach (var entry in updates)
            {
                if (entry.Value.bodyType == null)
                {
                    Console.WriteLine($"[Seed] WARNING: BodyType '{entry.Key}' not found under correct VehicleType!");
                    continue;
                }

                var bodyTypeId = entry.Value.bodyType.Id;
                var inClause = string.Join(", ", entry.Value.models.Select(n => $"'{n.Replace("'", "''")}'"));
                var sql = $@"UPDATE ""Models"" SET ""BodyTypeId"" = '{bodyTypeId}' WHERE ""Name"" IN ({inClause}) AND (""BodyTypeId"" IS NULL OR ""BodyTypeId"" != '{bodyTypeId}')";

                var affected = await context.Database.ExecuteSqlRawAsync(sql);
                Console.WriteLine($"[Seed] {entry.Key} (Id={bodyTypeId}): {affected} models updated via raw SQL");
                totalUpdated += affected;
            }

            Console.WriteLine($"[Seed] === Total models updated: {totalUpdated} ===");

            // Yanlış VehicleType'a ait orphan BodyType'ları temizle (duplicate olanlar)
            await CleanupOrphanBodyTypesAsync(context, otomobilVT, suvAraziVT, motosikletVT);
        }

        /// <summary>
        /// Yanlış VehicleType altındaki duplicate BodyType kayıtlarını temizlemeye çalışır (hata olursa atlar)
        /// </summary>
        private static async Task CleanupOrphanBodyTypesAsync(Context context, VehicleType? otomobilVT, VehicleType? suvAraziVT, VehicleType? motosikletVT)
        {
            if (otomobilVT == null || suvAraziVT == null || motosikletVT == null) return;

            try
            {
                var validVehicleTypeIds = new[] { otomobilVT.Id, suvAraziVT.Id, motosikletVT.Id };
                var inClause = string.Join(", ", validVehicleTypeIds.Select(id => $"'{id}'"));

                // Hiçbir tabloda referans edilmeyen orphan BodyType'ları sil
                var sql = $@"
                    DELETE FROM ""BodyTypes""
                    WHERE ""VehicleTypeId"" NOT IN ({inClause})
                    AND ""Id"" NOT IN (SELECT DISTINCT ""BodyTypeId"" FROM ""Models"" WHERE ""BodyTypeId"" IS NOT NULL)
                    AND ""Id"" NOT IN (SELECT DISTINCT ""BodyTypeId"" FROM ""Vehicles"" WHERE ""BodyTypeId"" IS NOT NULL)";

                var deleted = await context.Database.ExecuteSqlRawAsync(sql);
                if (deleted > 0)
                    Console.WriteLine($"[Seed] {deleted} orphan BodyType records cleaned up.");

                // Hiçbir tabloda referans edilmeyen orphan VehicleType'ları sil
                var cleanupVT = $@"
                    DELETE FROM ""VehicleTypes""
                    WHERE ""Id"" NOT IN ({inClause})
                    AND ""Id"" NOT IN (SELECT DISTINCT ""VehicleTypeId"" FROM ""BodyTypes"")
                    AND ""Id"" NOT IN (SELECT DISTINCT ""VehicleTypeId"" FROM ""Vehicles"" WHERE ""VehicleTypeId"" IS NOT NULL)";

                var deletedVT = await context.Database.ExecuteSqlRawAsync(cleanupVT);
                if (deletedVT > 0)
                    Console.WriteLine($"[Seed] {deletedVT} orphan VehicleType records cleaned up.");
            }
            catch (Exception ex)
            {
                // FK constraint varsa silme işlemini atla - kritik değil
                Console.WriteLine($"[Seed] Orphan cleanup skipped (FK constraint): {ex.Message}");
            }
        }
    }
}
