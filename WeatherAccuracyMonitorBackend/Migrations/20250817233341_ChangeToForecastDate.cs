using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherAccuracyMonitorBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeToForecastDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayForecastMadeLessX",
                table: "forecast_info_day");

            migrationBuilder.AddColumn<DateTime>(
                name: "ForecastDate",
                table: "forecast_info_day",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForecastDate",
                table: "forecast_info_day");

            migrationBuilder.AddColumn<int>(
                name: "DayForecastMadeLessX",
                table: "forecast_info_day",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
