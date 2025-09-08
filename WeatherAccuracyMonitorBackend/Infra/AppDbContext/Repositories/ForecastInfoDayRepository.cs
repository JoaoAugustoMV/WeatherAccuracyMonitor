using WeatherAccuracyMonitorBackend.Domain.Entities;
using WeatherAccuracyMonitorBackend.Domain.Repositories;

namespace WeatherAccuracyMonitorBackend.Infra.AppDbContext.Repositories
{
    public class ForecastInfoDayRepository(AppDbContext appDbContext) : RepositoryBase<ForecastInfoDay>(appDbContext), IForecastInfoDayRepository
    {
    }
}
