using WeatherAccuracyMonitor.Domain.Entities;
using WeatherAccuracyMonitorBackend.Domain.Repositories;

namespace WeatherAccuracyMonitor.Data.Repositories
{
    public class ForecastInfoDayRepository(AppDbContext appDbContext) : RepositoryBase<ForecastInfoDay>(appDbContext), IForecastInfoDayRepository
    {
    }
}
