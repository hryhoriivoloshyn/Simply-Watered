using Microsoft.EntityFrameworkCore.Migrations;

namespace Simply_Watered.Migrations
{
    public partial class DefaultHumidity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "MinimalHumidity",
                table: "Devices",
                nullable: false,
                defaultValue: 50.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MaxHumidity",
                table: "Devices",
                nullable: false,
                defaultValue: 80.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "IrrigModeId",
                table: "Devices",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldDefaultValueSql: "((1))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "MinimalHumidity",
                table: "Devices",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldDefaultValue: 50.0);

            migrationBuilder.AlterColumn<double>(
                name: "MaxHumidity",
                table: "Devices",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldDefaultValue: 80.0);

            migrationBuilder.AlterColumn<long>(
                name: "IrrigModeId",
                table: "Devices",
                type: "bigint",
                nullable: true,
                defaultValueSql: "((1))",
                oldClrType: typeof(long),
                oldDefaultValueSql: "((1))");
        }
    }
}
