using Microsoft.EntityFrameworkCore.Migrations;

namespace Simply_Watered.Migrations
{
    public partial class RevertChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupPresets");

            migrationBuilder.AddColumn<long>(
                name: "IrrigModeId",
                table: "Devices",
                nullable: true,
                defaultValueSql: "((1))");

            migrationBuilder.AddColumn<double>(
                name: "MaxHumidity",
                table: "Devices",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MinimalHumidity",
                table: "Devices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_IrrigModeId",
                table: "Devices",
                column: "IrrigModeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_IrrigationModes",
                table: "Devices",
                column: "IrrigModeId",
                principalTable: "IrrigationModes",
                principalColumn: "IrrigModeId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_IrrigationModes",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_IrrigModeId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "IrrigModeId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "MaxHumidity",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "MinimalHumidity",
                table: "Devices");

            migrationBuilder.CreateTable(
                name: "GroupPresets",
                columns: table => new
                {
                    DeviceId = table.Column<long>(type: "bigint", nullable: false),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    IrrigModeId = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "((1))"),
                    MaxHumidity = table.Column<double>(type: "float", nullable: true),
                    MinimalHumidity = table.Column<double>(type: "float", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPresets", x => new { x.DeviceId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_RegionPresets_Devices",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegionPresets_RegionGroups",
                        column: x => x.GroupId,
                        principalTable: "RegionGroups",
                        principalColumn: "RegionGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Devices_IrrigationModes",
                        column: x => x.IrrigModeId,
                        principalTable: "IrrigationModes",
                        principalColumn: "IrrigModeId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupPresets_GroupId",
                table: "GroupPresets",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPresets_IrrigModeId",
                table: "GroupPresets",
                column: "IrrigModeId");
        }
    }
}
