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
                name: "Bejelentesek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bejelentesek", x => x.Id);
                });

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
                name: "Javitasok",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JavitasTipus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Javitasok", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bejelentesek");

            migrationBuilder.DropTable(
                name: "Dolgozok");

            migrationBuilder.DropTable(
                name: "Javitasok");
        }
    }
}
