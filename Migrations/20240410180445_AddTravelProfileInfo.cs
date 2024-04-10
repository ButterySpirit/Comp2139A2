using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assign1.Migrations
{
    /// <inheritdoc />
    public partial class AddTravelProfileInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FrequentFlyerNumber",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HotelLoyaltyProgramId",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Preferences",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FrequentFlyerNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HotelLoyaltyProgramId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Preferences",
                table: "Users");
        }
    }
}
