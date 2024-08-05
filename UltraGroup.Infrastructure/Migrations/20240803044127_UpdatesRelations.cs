using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UltraGroup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatesRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Hotels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TP_DocumentTypeEntityID",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeDocumentId",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TP_Documents",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TP_Documents", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_LocationId",
                table: "Hotels",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_TP_DocumentTypeEntityID",
                table: "Clients",
                column: "TP_DocumentTypeEntityID");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_TypeDocumentId",
                table: "Clients",
                column: "TypeDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_TP_Documents_TP_DocumentTypeEntityID",
                table: "Clients",
                column: "TP_DocumentTypeEntityID",
                principalTable: "TP_Documents",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_TP_Documents_TypeDocumentId",
                table: "Clients",
                column: "TypeDocumentId",
                principalTable: "TP_Documents",
                principalColumn: "ID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Hotels_Locations_LocationId",
            //    table: "Hotels",
            //    column: "LocationId",
            //    principalTable: "Locations",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_TP_Documents_TP_DocumentTypeEntityID",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_TP_Documents_TypeDocumentId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Locations_LocationId",
                table: "Hotels");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "TP_Documents");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_LocationId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Clients_TP_DocumentTypeEntityID",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_TypeDocumentId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "TP_DocumentTypeEntityID",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "TypeDocumentId",
                table: "Clients");
        }
    }
}
