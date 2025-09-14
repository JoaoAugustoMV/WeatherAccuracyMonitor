using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WeatherAccuracyMonitorBackend.Domain.Entities;
using WeatherAccuracyMonitorBackend.Domain.Repositories;
using WeatherAccuracyMonitorLib.Domain.Enums;


namespace WeatherAccuracyMonitorLib.Domain.Services.ForecastServices
{
    public abstract class WeatherForecastServiceBase
    {
        protected abstract Sources _source { get; }

        protected readonly ILogger logger;
        protected readonly IHttpClientFactory httpClientFactory;

        private readonly string URL_SAVE;

        public WeatherForecastServiceBase(ILoggerFactory logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger.CreateLogger<WeatherForecastServiceBase>();
            this.httpClientFactory = httpClientFactory;

            URL_SAVE = configuration["BE_URL_POST"];
        }

        public async Task ExecuteTemperaturesPredictions()
        {
            try
            {
                IEnumerable<ForecastInfoDay> forecastInfoDays = await RetrieveForecasts();
                await SaveTemperaturesPredictions(forecastInfoDays);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error on save predictions");
            }
        }

        private async Task SaveTemperaturesPredictions(IEnumerable<ForecastInfoDay> forecastInfoDays)
        {
            try
            {
                HttpClient client = httpClientFactory.CreateClient();                

                string json = JsonSerializer.Serialize(forecastInfoDays);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(URL_SAVE, content);

                if (response.IsSuccessStatusCode) 
                {
                    logger.LogInformation($"SaveTemperaturesPredictions - {_source.ToString()} - Sucess");
                }
                else
                {
                    logger.LogInformation($"SaveTemperaturesPredictions - {_source.ToString()} - Failed - {response.Content}");
                }
            }
            catch (Exception ex) 
            {
                logger.LogError(ex, "SaveTemperaturesPredictions - Error");
            }
        }

        protected abstract Task<IEnumerable<ForecastInfoDay>> RetrieveForecasts();        

        
    }
}
