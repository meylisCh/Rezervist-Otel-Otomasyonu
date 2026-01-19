using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rezervist.Migrations
{
    /// <inheritdoc />
    public partial class OdemeSistemi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OdemeTuru",
                table: "Rezervasyonlar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OdendiMi",
                table: "Rezervasyonlar",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "ToplamTutar",
                table: "Rezervasyonlar",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OdemeTuru",
                table: "Rezervasyonlar");

            migrationBuilder.DropColumn(
                name: "OdendiMi",
                table: "Rezervasyonlar");

            migrationBuilder.DropColumn(
                name: "ToplamTutar",
                table: "Rezervasyonlar");
        }
    }
}
