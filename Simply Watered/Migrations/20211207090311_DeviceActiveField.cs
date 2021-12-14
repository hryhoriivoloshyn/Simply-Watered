using Microsoft.EntityFrameworkCore.Migrations;

namespace Simply_Watered.Migrations
{
    public partial class DeviceActiveField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Devices",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Devices");
        }
    }
}
