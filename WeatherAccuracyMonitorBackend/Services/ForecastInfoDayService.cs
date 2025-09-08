using WeatherAccuracyMonitorBackend.Domain.Entities;
using WeatherAccuracyMonitorBackend.Domain.Repositories;
using WeatherAccuracyMonitorLib.Domain.Services.ForecastServices;

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
