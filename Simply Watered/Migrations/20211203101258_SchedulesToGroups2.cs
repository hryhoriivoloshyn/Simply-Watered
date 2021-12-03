using Microsoft.EntityFrameworkCore.Migrations;

namespace Simply_Watered.Migrations
{
    public partial class SchedulesToGroups2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RegionGroupId",
                table: "IrrigationSchedules",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_IrrigationSchedules_RegionGroupId",
                table: "IrrigationSchedules",
                column: "RegionGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_IrrigationSchedules_RegionGroups",
                table: "IrrigationSchedules",
                column: "RegionGroupId",
                principalTable: "RegionGroups",
                principalColumn: "RegionGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IrrigationSchedules_RegionGroups",
                table: "IrrigationSchedules");

            migrationBuilder.DropIndex(
                name: "IX_IrrigationSchedules_RegionGroupId",
                table: "IrrigationSchedules");

            migrationBuilder.DropColumn(
                name: "RegionGroupId",
                table: "IrrigationSchedules");
        }
    }
}
