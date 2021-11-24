using Microsoft.EntityFrameworkCore.Migrations;

namespace Simply_Watered.Migrations
{
    public partial class RegionsAndModesSetNullChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_IrrigationModes",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_RegionGroups",
                table: "Regions");

            migrationBuilder.AlterColumn<long>(
                name: "IrrigModeId",
                table: "Devices",
                nullable: true,
                defaultValueSql: "((1))",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValueSql: "((1))");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_IrrigationModes",
                table: "Devices",
                column: "IrrigModeId",
                principalTable: "IrrigationModes",
                principalColumn: "IrrigModeId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_RegionGroups",
                table: "Regions",
                column: "RegionGroupId",
                principalTable: "RegionGroups",
                principalColumn: "RegionGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_IrrigationModes",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_RegionGroups",
                table: "Regions");

            migrationBuilder.AlterColumn<long>(
                name: "IrrigModeId",
                table: "Devices",
                type: "bigint",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(long),
                oldNullable: true,
                oldDefaultValueSql: "((1))");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_IrrigationModes",
                table: "Devices",
                column: "IrrigModeId",
                principalTable: "IrrigationModes",
                principalColumn: "IrrigModeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_RegionGroups",
                table: "Regions",
                column: "RegionGroupId",
                principalTable: "RegionGroups",
                principalColumn: "RegionGroupId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
