using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaService.DataLayer.Migrations
{
    public partial class UpdatedBookingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBooked",
                table: "Seats");

            migrationBuilder.AddColumn<bool>(
                name: "IsBooked",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBooked",
                table: "Bookings");

            migrationBuilder.AddColumn<bool>(
                name: "IsBooked",
                table: "Seats",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
