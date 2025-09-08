using WeatherAccuracyMonitorBackend.Domain.Entities;

namespace WeatherAccuracyMonitorLib.Domain.Services.ForecastServices
{
    public interface IForecastInfoDayService
    {
        public ForecastInfoDay Update(string id, ForecastInfoDay forecastInfoDay);

        public bool Delete(string id);
    }
}
