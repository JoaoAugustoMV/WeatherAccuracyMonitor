using System.Globalization;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WeatherAccuracyMonitor.Application;
using WeatherAccuracyMonitor.Data;
using WeatherAccuracyMonitor.Domain.Entities;
using WeatherAccuracyMonitor.Domain.Enums;
using WeatherAccuracyMonitor.Providers.SourcesResponses;
using WeatherAccuracyMonitorBackend.Domain.Repositories;

namespace WeatherAccuracyMonitor.Providers.ForecastSources
{
    public class HGBrasilForecastService : WeatherForecastServiceBase
    {        
        private readonly string URL_HGBrasil;

        protected override Sources _source => Sources.HGBrasil;

        public HGBrasilForecastService(ILogger<HGBrasilForecastService> logger, IHttpClientFactory httpClientFactory,
            IDbContextFactory<AppDbContext> dbConnFactory,
            IConfiguration configuration) : base(logger, httpClientFactory, dbConnFactory)
        {            
            URL_HGBrasil = configuration["HGBrasil_URL"]!;
        }

        protected override async Task<IEnumerable<ForecastInfoDay>> RetrieveForecasts()
        {
            try
            {
                HGBrasilResponse? hgBrResponse = await RequestForecastAsync();

                if (hgBrResponse != null) 
                {
                    return MapForecasts(hgBrResponse);                   
                }

                logger.LogError("HGBrasil Error Request");
            }
            catch (Exception ex) 
            {
                logger.LogError(ex, "Error on Request");
            }

            return [];
            
        }

        private async Task<HGBrasilResponse?> RequestForecastAsync()
        {
            HttpClient httpClient = httpClientFactory.CreateClient();

            return await httpClient.GetFromJsonAsync<HGBrasilResponse>(URL_HGBrasil);
        }

        protected IEnumerable<ForecastInfoDay> MapForecasts(HGBrasilResponse response)
        {
            DateTime currentDateTime = DateTime.Now.Date;
            
            int currentDayCode = int.Parse(currentDateTime.ToString("yyyyMMdd"));

            string city = response.results.city_name;
            
            foreach (HGBrasilForecasts item in response.results.forecast.Take(7))
            {
                DateTime dateFormat = DateTime.ParseExact(item.full_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                
                int itemDayCode = int.Parse(dateFormat.ToString("yyyyMMdd"));

                int dayX = itemDayCode - currentDayCode;

                ForecastInfoDay newForecast = new()
                {
                    City = city,
                    ForecastDate = currentDateTime,
                    Source = _source,
                    DayX = dayX,
                    DayCode = itemDayCode,
                    Description = item.description,
                };

                if(dayX == 0)
                {
                    newForecast.RealTemperatureMin = item.min;
                    newForecast.RealTemperatureMax = item.max;

                }
                else
                {
                    newForecast.MinTemperatureForecastMadeMinusX = item.min;
                    newForecast.MaxTemperatureForecastMadeMinusX = item.max;
                }

                yield return newForecast;
            }
            
        }
    }
}
