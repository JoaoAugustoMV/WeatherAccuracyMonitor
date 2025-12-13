using Microsoft.AspNetCore.Mvc;
using WeatherAccuracyMonitor.Application.Interfaces;
using WeatherAccuracyMonitor.Application.ValueModels.ResponsesBFF;
using WeatherAccuracyMonitorLib.Domain.Services;

namespace WeatherAccuracyMonitorBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfoDataController(ILogger<ForecastInfoDayController> logger, IInfoDataService infoDataService) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<InfoDataLine>> Get(DateTime minDate, DateTime maxDate)
        {
            logger.LogInformation("InfoData - Get");
            return await infoDataService.GetInfo(minDate, maxDate);
        }

        [HttpGet("CurrentWeek")]        
        public async Task<InfoCurrentWeek> GetInfoCurrentWeek()
        {
            logger.LogInformation("InfoData - Get");
            return await infoDataService.GetInfoCurrentWeek();
        }
    }
}
