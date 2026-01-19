using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Rezervist.Migrations
{
    /// <inheritdoc />
    public partial class BuyukGuncelleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResimUrl",
                table: "Odalar",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telefon",
                table: "Misafirler",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "AdSoyad",
                table: "Misafirler",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "KaraListedeMi",
                table: "Misafirler",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OzelNotlar",
                table: "Misafirler",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Harcamalar",
                columns: table => new
                {
                    HarcamaID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UrunAdi = table.Column<string>(type: "text", nullable: true),
                    Tutar = table.Column<decimal>(type: "numeric", nullable: false),
                    IslemTarihi = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RezervasyonID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Harcamalar", x => x.HarcamaID);
                    table.ForeignKey(
                        name: "FK_Harcamalar_Rezervasyonlar_RezervasyonID",
                        column: x => x.RezervasyonID,
                        principalTable: "Rezervasyonlar",
                        principalColumn: "RezervasyonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Harcamalar_RezervasyonID",
                table: "Harcamalar",
                column: "RezervasyonID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Harcamalar");

            migrationBuilder.DropColumn(
                name: "ResimUrl",
                table: "Odalar");

            migrationBuilder.DropColumn(
                name: "KaraListedeMi",
                table: "Misafirler");

            migrationBuilder.DropColumn(
                name: "OzelNotlar",
                table: "Misafirler");

            migrationBuilder.AlterColumn<string>(
                name: "Telefon",
                table: "Misafirler",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdSoyad",
                table: "Misafirler",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
