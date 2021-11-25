using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Simply_Watered.Migrations
{
    public partial class FixedTypoIrrigSchedules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SheduleStartDate",
                table: "IrrigationSchedules");

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduleStartDate",
                table: "IrrigationSchedules",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleStartDate",
                table: "IrrigationSchedules");

            migrationBuilder.AddColumn<DateTime>(
                name: "SheduleStartDate",
                table: "IrrigationSchedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
