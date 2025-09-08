using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherAccuracyMonitorBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeForecastDateType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ForecastDate",
                table: "forecast_info_day",
                type: "date",                
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
