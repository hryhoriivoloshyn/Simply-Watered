using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Simply_Watered.Migrations
{
    public partial class EndDateIsRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleEndDate",
                table: "IrrigationSchedules",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleEndDate",
                table: "IrrigationSchedules",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
