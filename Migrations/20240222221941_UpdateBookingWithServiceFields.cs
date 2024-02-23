using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assign1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingWithServiceFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings");
            
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Bookings_BookingId",
                table: "Flights");
            
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Bookings_BookingId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Bookings_BookingID",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_BookingID",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_BookingId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Flights_BookingId",
                table: "Flights");
            
            migrationBuilder.DropColumn(
                name: "BookingID",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "CostPerDay",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Flights");
            */
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            /*
            migrationBuilder.AddColumn<int>(
                name: "ServiceID",
                table: "Bookings",
                type: "int",
                nullable: true);
            
            migrationBuilder.AddColumn<string>(
                name: "ServiceType",
                table: "Bookings",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
            */
            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //name: "FK_Bookings_Users_UserId",
            //table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ServiceID",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "BookingID",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CostPerDay",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Hotels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Flights",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_BookingID",
                table: "Rentals",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_BookingId",
                table: "Hotels",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_BookingId",
                table: "Flights",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Bookings_BookingId",
                table: "Flights",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Bookings_BookingId",
                table: "Hotels",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Bookings_BookingID",
                table: "Rentals",
                column: "BookingID",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
