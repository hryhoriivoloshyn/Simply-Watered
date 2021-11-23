using Microsoft.EntityFrameworkCore.Migrations;

namespace Simply_Watered.Migrations
{
    public partial class SerialNumberAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Devices",
                maxLength: 450,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Devices");
        }
    }
}
