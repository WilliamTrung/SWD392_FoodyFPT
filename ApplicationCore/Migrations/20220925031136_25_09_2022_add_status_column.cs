using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationCore.Migrations
{
    public partial class _25_09_2022_add_status_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "Store",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "Slot",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "Shipper",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "Location",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Slot");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Shipper");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Location");
        }
    }
}
