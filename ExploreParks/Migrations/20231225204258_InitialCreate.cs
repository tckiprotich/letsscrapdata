using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExploreParks.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Continents",
                columns: table => new
                {
                    ContinentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContinentName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continents", x => x.ContinentId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CountryName = table.Column<string>(type: "TEXT", nullable: true),
                    ContinentModelContinentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                    table.ForeignKey(
                        name: "FK_Countries_Continents_ContinentModelContinentId",
                        column: x => x.ContinentModelContinentId,
                        principalTable: "Continents",
                        principalColumn: "ContinentId");
                });

            migrationBuilder.CreateTable(
                name: "Parks",
                columns: table => new
                {
                    ParkId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParkName = table.Column<string>(type: "TEXT", nullable: true),
                    ParkDescription = table.Column<string>(type: "TEXT", nullable: true),
                    ParkLocation = table.Column<string>(type: "TEXT", nullable: true),
                    ParkImage = table.Column<string>(type: "TEXT", nullable: true),
                    URL = table.Column<string>(type: "TEXT", nullable: true),
                    ParkArea = table.Column<float>(type: "REAL", nullable: false),
                    Latitude = table.Column<string>(type: "TEXT", nullable: true),
                    Longitude = table.Column<string>(type: "TEXT", nullable: true),
                    ParkEstablished = table.Column<int>(type: "INTEGER", nullable: false),
                    CountryModelCountryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parks", x => x.ParkId);
                    table.ForeignKey(
                        name: "FK_Parks_Countries_CountryModelCountryId",
                        column: x => x.CountryModelCountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_ContinentModelContinentId",
                table: "Countries",
                column: "ContinentModelContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_Parks_CountryModelCountryId",
                table: "Parks",
                column: "CountryModelCountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parks");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Continents");
        }
    }
}
