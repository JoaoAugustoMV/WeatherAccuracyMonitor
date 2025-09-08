using System.ComponentModel.DataAnnotations.Schema;
using UUIDNext;
using WeatherAccuracyMonitorLib.Domain.Enums;

namespace WeatherAccuracyMonitorBackend.Domain.Entities
{
    public class ForecastInfoDay
    {        
        public string Id { get; set; } = Uuid.NewSequential().ToString();
        public int DayCode { get; set; }
        public int DayX { get; set; }
        public DateTime ForecastDate { get; set; }
        public double MinTemperatureForecastMadeMinusX { get; set; }
        public double MaxTemperatureForecastMadeMinusX { get; set; }
        public double RealTemperatureMin { get; set; }
        public double RealTemperatureMax { get; set;}
        public Sources Source { get; set; }
        public string? City { get; set; }
        public string? Description { get; set; }
    }
}
