using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Simply_Watered.Migrations
{
    public partial class IrrigationHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IrrigationHistory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    NormalizedStartDate = table.Column<string>(nullable: true),
                    NormalizedStartTime = table.Column<string>(nullable: true),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    NormalizedEndDate = table.Column<string>(nullable: true),
                    NormalizedEndTime = table.Column<string>(nullable: true),
                    ReadingStartTemp = table.Column<double>(nullable: true),
                    ReadingStartHumidity = table.Column<double>(nullable: true),
                    ReadingEndTemp = table.Column<double>(nullable: true),
                    ReadingEndHumidity = table.Column<double>(nullable: true),
                    DeviceId = table.Column<long>(nullable: false),
                    IrrigModeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IrrigationHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IrrigationHistory_Devices",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IrrigationHistory_IrrigationModes",
                        column: x => x.IrrigModeId,
                        principalTable: "IrrigationModes",
                        principalColumn: "IrrigModeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IrrigationHistory_DeviceId",
                table: "IrrigationHistory",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_IrrigationHistory_IrrigModeId",
                table: "IrrigationHistory",
                column: "IrrigModeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IrrigationHistory");
        }
    }
}
