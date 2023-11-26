using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace scrapper.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "parkModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    Area = table.Column<string>(type: "TEXT", nullable: true),
                    Established = table.Column<string>(type: "TEXT", nullable: true),
                    Latitude = table.Column<string>(type: "TEXT", nullable: true),
                    Longitude = table.Column<string>(type: "TEXT", nullable: true),
                    NearestCity = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Activities = table.Column<string>(type: "TEXT", nullable: true),
                    OperatingHours = table.Column<string>(type: "TEXT", nullable: true),
                    EntranceFees = table.Column<string>(type: "TEXT", nullable: true),
                    WeatherInfo = table.Column<string>(type: "TEXT", nullable: true),
                    DirectionsInfo = table.Column<string>(type: "TEXT", nullable: true),
                    DirectionsUrl = table.Column<string>(type: "TEXT", nullable: true),
                    ContactInfo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parkModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "parkName",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parkName", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "parkModels");

            migrationBuilder.DropTable(
                name: "parkName");
        }
    }
}
