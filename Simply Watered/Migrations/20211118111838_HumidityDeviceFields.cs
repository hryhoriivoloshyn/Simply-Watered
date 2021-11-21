using Microsoft.EntityFrameworkCore.Migrations;

namespace Simply_Watered.Migrations
{
    public partial class HumidityDeviceFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MaxHumidity",
                table: "Devices",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MinimalHumidity",
                table: "Devices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxHumidity",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "MinimalHumidity",
                table: "Devices");
        }
    }
}
