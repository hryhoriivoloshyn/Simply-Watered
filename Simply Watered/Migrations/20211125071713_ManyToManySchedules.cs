using Microsoft.EntityFrameworkCore.Migrations;

namespace Simply_Watered.Migrations
{
    public partial class ManyToManySchedules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IrrigationSchedules_Devices",
                table: "IrrigationSchedules");

            migrationBuilder.DropIndex(
                name: "IX_IrrigationSchedules_DeviceId",
                table: "IrrigationSchedules");

            migrationBuilder.CreateTable(
                name: "DevicesSchedules",
                columns: table => new
                {
                    DeviceId = table.Column<long>(nullable: false),
                    ScheduleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevicesSchedules", x => new { x.DeviceId, x.ScheduleId });
                    table.ForeignKey(
                        name: "FK_DevicesSchedules_Devices",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DevicesSchedules_IrrigationSchedules",
                        column: x => x.ScheduleId,
                        principalTable: "IrrigationSchedules",
                        principalColumn: "IrrigScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevicesSchedules_ScheduleId",
                table: "DevicesSchedules",
                column: "ScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DevicesSchedules");

            migrationBuilder.CreateIndex(
                name: "IX_IrrigationSchedules_DeviceId",
                table: "IrrigationSchedules",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_IrrigationSchedules_Devices",
                table: "IrrigationSchedules",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
