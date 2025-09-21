using WeatherAccuracyMonitorBackend.Domain.Entities;
using WeatherAccuracyMonitorBackend.Domain.Repositories;

namespace WeatherAccuracyMonitorLib.Infra.AppDbContext.Repositories
{
    public class ForecastInfoDayRepository(AppDbContext appDbContext) : RepositoryBase<ForecastInfoDay>(appDbContext), IForecastInfoDayRepository
    {
    }
}
