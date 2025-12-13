using WeatherAccuracyMonitor.Domain.Entities;

namespace WeatherAccuracyMonitor.Domain.Services.ForecastServices
{
    public interface IForecastInfoDayService
    {
        public ForecastInfoDay Update(string id, ForecastInfoDay forecastInfoDay);

        public bool Delete(string id);
    }
}
