using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExploreParks.Migrations
{
    /// <inheritdoc />
    public partial class UpdateParkModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ParkArea",
                table: "Parks",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ParkArea",
                table: "Parks",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
