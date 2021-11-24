using Microsoft.EntityFrameworkCore.Migrations;

namespace Simply_Watered.Migrations
{
    public partial class DeviceTypesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceDescription",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "DeviceName",
                table: "Devices");

            migrationBuilder.AddColumn<long>(
                name: "TypeId",
                table: "Devices",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeviceTypes",
                columns: table => new
                {
                    TypeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceName = table.Column<string>(maxLength: 450, nullable: false),
                    DeviceDescription = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypes", x => x.TypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_TypeId",
                table: "Devices",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceTypes",
                table: "Devices",
                column: "TypeId",
                principalTable: "DeviceTypes",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceTypes",
                table: "Devices");

            migrationBuilder.DropTable(
                name: "DeviceTypes");

            migrationBuilder.DropIndex(
                name: "IX_Devices_TypeId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Devices");

            migrationBuilder.AddColumn<string>(
                name: "DeviceDescription",
                table: "Devices",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceName",
                table: "Devices",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");
        }
    }
}
