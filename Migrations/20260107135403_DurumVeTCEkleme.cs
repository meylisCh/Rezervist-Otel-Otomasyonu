using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rezervist.Migrations
{
    /// <inheritdoc />
    public partial class DurumVeTCEkleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Durum",
                table: "Rezervasyonlar",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Durum",
                table: "Rezervasyonlar");
        }
    }
}
