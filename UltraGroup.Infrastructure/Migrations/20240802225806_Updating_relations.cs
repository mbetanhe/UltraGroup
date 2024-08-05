using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UltraGroup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updating_relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Clients_ClientEntityID",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ClientEntityID",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ClientEntityID",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "Room_Location",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_BookingId",
                table: "Clients",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Bookings_BookingId",
                table: "Clients",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Bookings_BookingId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_BookingId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Room_Location",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Clients");

            migrationBuilder.AddColumn<int>(
                name: "ClientEntityID",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ClientEntityID",
                table: "Bookings",
                column: "ClientEntityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Clients_ClientEntityID",
                table: "Bookings",
                column: "ClientEntityID",
                principalTable: "Clients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
