using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayer.Migrations
{
    /// <inheritdoc />
    public partial class migemlak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "QuoteRequests",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "QuoteRequests",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DesiredMaxPrice",
                table: "QuoteRequests",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DesiredMinPrice",
                table: "QuoteRequests",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "QuoteRequests",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Neighborhood",
                table: "QuoteRequests",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "QuoteRequests",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RealEstateCategory",
                table: "QuoteRequests",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RealEstateListingType",
                table: "QuoteRequests",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RealEstateTitle",
                table: "QuoteRequests",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestType",
                table: "QuoteRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RoomCount",
                table: "QuoteRequests",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "QuoteRequests",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequests_RequestType",
                table: "QuoteRequests",
                column: "RequestType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuoteRequests_RequestType",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "City",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "DesiredMaxPrice",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "DesiredMinPrice",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "District",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "Neighborhood",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "RealEstateCategory",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "RealEstateListingType",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "RealEstateTitle",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "RequestType",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "RoomCount",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "QuoteRequests");
        }
    }
}
