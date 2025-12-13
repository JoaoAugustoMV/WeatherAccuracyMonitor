using WeatherAccuracyMonitor.Domain.Entities;
using WeatherAccuracyMonitor.Domain.Services.ForecastServices;
using WeatherAccuracyMonitorBackend.Domain.Repositories;

namespace WeatherAccuracyMonitorBackend.Services
{
    public class ForecastInfoDayService(IForecastInfoDayRepository forecastInfoDayRepository) : IForecastInfoDayService
    {
        public ForecastInfoDay Update(string id, ForecastInfoDay forecastInfoDay)
        {
            forecastInfoDay.Id = id;
            return forecastInfoDayRepository.Update(forecastInfoDay);
        }

        public bool Delete(string id) 
        {
            return forecastInfoDayRepository.Delete(new ForecastInfoDay() { Id = id });
        }

    }
}
