using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace scrapper.Migrations
{
    /// <inheritdoc />
    public partial class Initmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "parkName",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "parkName",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Established",
                table: "parkName",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "parkName",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "parkName");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "parkName");

            migrationBuilder.DropColumn(
                name: "Established",
                table: "parkName");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "parkName");
        }
    }
}
