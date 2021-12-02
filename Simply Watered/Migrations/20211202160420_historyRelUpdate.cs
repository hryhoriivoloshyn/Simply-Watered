using Microsoft.EntityFrameworkCore.Migrations;

namespace Simply_Watered.Migrations
{
    public partial class historyRelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IrrigationHistory_IrrigationModes",
                table: "IrrigationHistory");

            migrationBuilder.AlterColumn<long>(
                name: "IrrigModeId",
                table: "IrrigationHistory",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_IrrigationHistory_IrrigationModes",
                table: "IrrigationHistory",
                column: "IrrigModeId",
                principalTable: "IrrigationModes",
                principalColumn: "IrrigModeId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IrrigationHistory_IrrigationModes",
                table: "IrrigationHistory");

            migrationBuilder.AlterColumn<long>(
                name: "IrrigModeId",
                table: "IrrigationHistory",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IrrigationHistory_IrrigationModes",
                table: "IrrigationHistory",
                column: "IrrigModeId",
                principalTable: "IrrigationModes",
                principalColumn: "IrrigModeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
