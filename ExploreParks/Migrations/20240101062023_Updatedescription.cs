using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExploreParks.Migrations
{
    /// <inheritdoc />
    public partial class Updatedescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NearestCity",
                table: "Descriptions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParkLatitude",
                table: "Descriptions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParkLongitude",
                table: "Descriptions",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NearestCity",
                table: "Descriptions");

            migrationBuilder.DropColumn(
                name: "ParkLatitude",
                table: "Descriptions");

            migrationBuilder.DropColumn(
                name: "ParkLongitude",
                table: "Descriptions");
        }
    }
}
