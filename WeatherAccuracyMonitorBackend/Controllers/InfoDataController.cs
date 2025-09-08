using Microsoft.AspNetCore.Mvc;
using WeatherAccuracyMonitorLib.Domain.Models.ValueModels.ResponsesBFF;
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
            return await infoDataService.GetInfo(minDate, maxDate);
        }
    }
}
