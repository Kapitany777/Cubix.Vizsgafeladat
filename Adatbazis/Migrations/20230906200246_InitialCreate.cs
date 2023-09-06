using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adatbazis.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dolgozok",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VezetekNev = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KeresztNev = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dolgozok", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JavitasTipusok",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipusNev = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JavitasTipusok", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bejelentesek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iranyitoszam = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Varos = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Cim = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HibaLeiras = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    BejelentesDatuma = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JavitasDatuma = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DolgozoId = table.Column<int>(type: "int", nullable: true),
                    JavitasTipusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bejelentesek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bejelentesek_Dolgozok_DolgozoId",
                        column: x => x.DolgozoId,
                        principalTable: "Dolgozok",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bejelentesek_JavitasTipusok_JavitasTipusId",
                        column: x => x.JavitasTipusId,
                        principalTable: "JavitasTipusok",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bejelentesek_DolgozoId",
                table: "Bejelentesek",
                column: "DolgozoId");

            migrationBuilder.CreateIndex(
                name: "IX_Bejelentesek_JavitasTipusId",
                table: "Bejelentesek",
                column: "JavitasTipusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bejelentesek");

            migrationBuilder.DropTable(
                name: "Dolgozok");

            migrationBuilder.DropTable(
                name: "JavitasTipusok");
        }
    }
}
