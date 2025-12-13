using Microsoft.AspNetCore.Mvc;
using WeatherAccuracyMonitor.Domain.Entities;
using WeatherAccuracyMonitor.Domain.Services.ForecastServices;
using WeatherAccuracyMonitorBackend.Domain.Repositories;

namespace WeatherAccuracyMonitorBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForecastInfoDayController(ILogger<ForecastInfoDayController> logger, IForecastInfoDayRepository forecastInfoDayRepository, IForecastInfoDayService forecastInfoDayService) : ControllerBase
    {
     
        [HttpGet]
        public async Task<IEnumerable<ForecastInfoDay>> Get()
        {
            return await forecastInfoDayRepository.GetAllAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ForecastInfoDay[] forecastsInfoDay)
        {
            try
            {
                await forecastInfoDayRepository.InsertAsync(forecastsInfoDay);
            
                return StatusCode(201);
                
            } catch (Exception ex) 
            { 
                return StatusCode(500, ex.Message);
            }           
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, ForecastInfoDay forecastInfoDay)
        {
            try
            {
                forecastInfoDayService.Update(id ,forecastInfoDay);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                forecastInfoDayService.Delete(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return Ok();
        }
    }
}
