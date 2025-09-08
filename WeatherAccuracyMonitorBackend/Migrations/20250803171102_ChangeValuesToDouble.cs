using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherAccuracyMonitorBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeValuesToDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "RealTemperatureMin",
                table: "forecast_info_day",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "RealTemperatureMax",
                table: "forecast_info_day",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "MinTemperatureForecastMadeMinusX",
                table: "forecast_info_day",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "MaxTemperatureForecastMadeMinusX",
                table: "forecast_info_day",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RealTemperatureMin",
                table: "forecast_info_day",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "RealTemperatureMax",
                table: "forecast_info_day",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "MinTemperatureForecastMadeMinusX",
                table: "forecast_info_day",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "MaxTemperatureForecastMadeMinusX",
                table: "forecast_info_day",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
