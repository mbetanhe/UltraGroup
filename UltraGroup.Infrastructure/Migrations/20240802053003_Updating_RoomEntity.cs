using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UltraGroup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updating_RoomEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_TP_Rooms_TP_RoomEntityID",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "TP_RoomEntityID",
                table: "Rooms",
                newName: "Hotel_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_TP_RoomEntityID",
                table: "Rooms",
                newName: "IX_Rooms_Hotel_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_TpRoom_ID",
                table: "Rooms",
                column: "TpRoom_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Hotels_Hotel_ID",
                table: "Rooms",
                column: "Hotel_ID",
                principalTable: "Hotels",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_TP_Rooms_TpRoom_ID",
                table: "Rooms",
                column: "TpRoom_ID",
                principalTable: "TP_Rooms",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Hotels_Hotel_ID",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_TP_Rooms_TpRoom_ID",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_TpRoom_ID",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "Hotel_ID",
                table: "Rooms",
                newName: "TP_RoomEntityID");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_Hotel_ID",
                table: "Rooms",
                newName: "IX_Rooms_TP_RoomEntityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_TP_Rooms_TP_RoomEntityID",
                table: "Rooms",
                column: "TP_RoomEntityID",
                principalTable: "TP_Rooms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
