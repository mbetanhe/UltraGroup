using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UltraGroup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatesEntities4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Rooms");
        }
    }
}
