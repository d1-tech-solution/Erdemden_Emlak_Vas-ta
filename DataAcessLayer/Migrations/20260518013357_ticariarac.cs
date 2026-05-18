using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayer.Migrations
{
    /// <inheritdoc />
    public partial class ticariarac : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuildingAge",
                table: "QuoteRequests",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessType",
                table: "QuoteRequests",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LandZoningStatus",
                table: "QuoteRequests",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OccupancyPermitStatus",
                table: "QuoteRequests",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildingAge",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "BusinessType",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "LandZoningStatus",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "OccupancyPermitStatus",
                table: "QuoteRequests");
        }
    }
}
