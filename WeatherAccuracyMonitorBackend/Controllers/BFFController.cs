using Microsoft.AspNetCore.Mvc;
using WeatherAccuracyMonitorBackend.Domain.Entities;
using WeatherAccuracyMonitorBackend.Domain.Repositories;
using WeatherAccuracyMonitorLib.Domain.Models.ValueModels.ResponsesBFF;
using WeatherAccuracyMonitorLib.Domain.Services;
using WeatherAccuracyMonitorLib.Domain.Services.ForecastServices;

namespace WeatherAccuracyMonitorBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BFFController(ILogger<BFFController> logger, IInfoDataService bffService) : ControllerBase
    {

        [HttpGet]
        public async Task<InfoCurrentWeek> GetCurrentWeek()
        {
            return await bffService.GetInfoCurrentWeek();
        }

    }
}
