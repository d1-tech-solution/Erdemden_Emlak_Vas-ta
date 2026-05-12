using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayer.Migrations
{
    /// <inheritdoc />
    public partial class useridteklfiler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "QuoteRequests",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequests_UserId",
                table: "QuoteRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequests_Users_UserId",
                table: "QuoteRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequests_Users_UserId",
                table: "QuoteRequests");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequests_UserId",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "QuoteRequests");
        }
    }
}
