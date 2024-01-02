using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExploreParks.Migrations
{
    /// <inheritdoc />
    public partial class FinalMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Continents",
                columns: table => new
                {
                    ContinentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContinentName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continents", x => x.ContinentId);
                });

            migrationBuilder.CreateTable(
                name: "Descriptions",
                columns: table => new
                {
                    ParkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParkName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkLatitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkLongitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NearestCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentUrls = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descriptions", x => x.ParkId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContinentModelContinentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    ParkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParkName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NearestCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkEstablished = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryModelCountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                name: "Descriptions");

            migrationBuilder.DropTable(
                name: "Parks");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Continents");
        }
    }
}
