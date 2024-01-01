using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExploreParks.Migrations
{
    /// <inheritdoc />
    public partial class ContentUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentUrls",
                table: "Descriptions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Descriptions",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentUrls",
                table: "Descriptions");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Descriptions");
        }
    }
}
