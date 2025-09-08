using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherAccuracyMonitorBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "forecast_info_day",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    DayCode = table.Column<int>(type: "integer", nullable: false),
                    DayX = table.Column<int>(type: "integer", nullable: false),
                    DayForecastMadeLessX = table.Column<int>(type: "integer", nullable: false),
                    MinTemperatureForecastMadeMinusX = table.Column<int>(type: "integer", nullable: false),
                    MaxTemperatureForecastMadeMinusX = table.Column<int>(type: "integer", nullable: false),
                    RealTemperatureMin = table.Column<int>(type: "integer", nullable: false),
                    RealTemperatureMax = table.Column<int>(type: "integer", nullable: false),
                    Source = table.Column<int>(type: "integer", nullable: false),
                    City = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_forecast_info_day", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {            
        }
    }
}
