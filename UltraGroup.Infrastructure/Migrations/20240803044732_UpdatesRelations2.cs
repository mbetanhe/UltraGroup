using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UltraGroup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatesRelations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_TP_Documents_TP_DocumentTypeEntityID",
                table: "Clients");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Hotels_Locations_LocationId",
            //    table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Clients_TP_DocumentTypeEntityID",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "TP_DocumentTypeEntityID",
                table: "Clients");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Hotels_Locations_LocationId",
            //    table: "Hotels",
            //    column: "LocationId",
            //    principalTable: "Locations",
            //    principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Locations_LocationId",
                table: "Hotels");

            migrationBuilder.AddColumn<int>(
                name: "TP_DocumentTypeEntityID",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_TP_DocumentTypeEntityID",
                table: "Clients",
                column: "TP_DocumentTypeEntityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_TP_Documents_TP_DocumentTypeEntityID",
                table: "Clients",
                column: "TP_DocumentTypeEntityID",
                principalTable: "TP_Documents",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Locations_LocationId",
                table: "Hotels",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
