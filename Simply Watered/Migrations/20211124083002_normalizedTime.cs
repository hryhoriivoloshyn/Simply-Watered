using Microsoft.EntityFrameworkCore.Migrations;

namespace Simply_Watered.Migrations
{
    public partial class normalizedTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedDateTime",
                table: "DeviceReadings");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedDate",
                table: "DeviceReadings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedTime",
                table: "DeviceReadings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedDate",
                table: "DeviceReadings");

            migrationBuilder.DropColumn(
                name: "NormalizedTime",
                table: "DeviceReadings");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedDateTime",
                table: "DeviceReadings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
