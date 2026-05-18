using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayer.Migrations
{
    /// <inheritdoc />
    public partial class SeedMissingVehicleTypeLookupData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                INSERT INTO "VehicleTypes" ("Id", "Name", "CreatedAt")
                SELECT '6ce711e9-df4d-4a7f-a7b4-3a066eb3e064'::uuid, 'Elektrikli Otomobil', NOW() AT TIME ZONE 'UTC'
                WHERE NOT EXISTS (
                    SELECT 1 FROM "VehicleTypes" WHERE "Name" = 'Elektrikli Otomobil'
                );
                """);

            migrationBuilder.Sql("""
                INSERT INTO "VehicleTypes" ("Id", "Name", "CreatedAt")
                SELECT '0f7c4e4b-1538-4014-80d2-607bda1dcf61'::uuid, 'Minivan & Panelvan', NOW() AT TIME ZONE 'UTC'
                WHERE NOT EXISTS (
                    SELECT 1 FROM "VehicleTypes" WHERE "Name" = 'Minivan & Panelvan'
                );
                """);

            migrationBuilder.Sql("""
                INSERT INTO "VehicleTypes" ("Id", "Name", "CreatedAt")
                SELECT '1f172bdd-c455-4fdd-8f97-9a35ea05e26f'::uuid, 'Ticari Araçlar', NOW() AT TIME ZONE 'UTC'
                WHERE NOT EXISTS (
                    SELECT 1 FROM "VehicleTypes" WHERE "Name" = 'Ticari Araçlar'
                );
                """);

            InsertBodyType(migrationBuilder, "Elektrikli Otomobil", "Elektrikli", "868db741-0e45-41e7-8d18-dde503517ca8");

            InsertBodyType(migrationBuilder, "Minivan & Panelvan", "Minivan", "43ee1a34-94c6-44bd-a5d4-8521776ca11c");
            InsertBodyType(migrationBuilder, "Minivan & Panelvan", "Panelvan", "bf3e239d-2e53-48d6-8363-4ed42c7bc246");

            InsertBodyType(migrationBuilder, "Ticari Araçlar", "Minibüs & Midibüs", "a2506207-6d42-4b9f-8d5d-8fa7d9c48b03");
            InsertBodyType(migrationBuilder, "Ticari Araçlar", "Otobüs", "a8528538-ce3b-4aeb-b151-08718bbd745a");
            InsertBodyType(migrationBuilder, "Ticari Araçlar", "Kamyon & Kamyonet", "4d84b7b2-ed3b-4fc7-8238-87e83186821a");
            InsertBodyType(migrationBuilder, "Ticari Araçlar", "Çekici", "cd931070-52a2-4cd2-a4a3-926161d98c98");
            InsertBodyType(migrationBuilder, "Ticari Araçlar", "Dorse", "fc64496c-0c18-4b78-b5fd-dd6cf902978a");
            InsertBodyType(migrationBuilder, "Ticari Araçlar", "Römork", "5ff50a26-ec79-4955-9e5a-5fd54d55a112");
            InsertBodyType(migrationBuilder, "Ticari Araçlar", "Karoser & Üst Yapı", "b64eaac9-4678-4d29-bce5-0d30c4ab8cef");
            InsertBodyType(migrationBuilder, "Ticari Araçlar", "Oto Kurtarıcı & Taşıyıcı", "7d4ea2fb-543d-4b8d-9daf-902a82d995fb");
            InsertBodyType(migrationBuilder, "Ticari Araçlar", "Ticari Hat & Ticari Plaka", "e7e1c8a5-4a78-486e-bb59-013c281a3d15");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Lookup data may be referenced by vehicles/models, so rollback intentionally leaves it in place.
        }

        private static void InsertBodyType(MigrationBuilder migrationBuilder, string vehicleTypeName, string bodyTypeName, string id)
        {
            migrationBuilder.Sql($"""
                INSERT INTO "BodyTypes" ("Id", "VehicleTypeId", "Name", "CreatedAt")
                SELECT '{id}'::uuid, vt."Id", '{bodyTypeName}', NOW() AT TIME ZONE 'UTC'
                FROM "VehicleTypes" vt
                WHERE vt."Name" = '{vehicleTypeName}'
                  AND NOT EXISTS (
                      SELECT 1
                      FROM "BodyTypes" bt
                      WHERE bt."VehicleTypeId" = vt."Id"
                        AND bt."Name" = '{bodyTypeName}'
                  );
                """);
        }
    }
}
