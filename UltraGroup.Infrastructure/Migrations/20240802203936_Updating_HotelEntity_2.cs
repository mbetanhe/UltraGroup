using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UltraGroup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updating_HotelEntity_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "Hotels",
                newName: "Htl_IsAvailable");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Htl_IsAvailable",
                table: "Hotels",
                newName: "IsAvailable");
        }
    }
}
