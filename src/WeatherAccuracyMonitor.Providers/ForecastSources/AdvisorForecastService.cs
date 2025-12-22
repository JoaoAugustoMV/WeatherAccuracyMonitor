using System.Collections.Generic;
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
    public class AdvisorForecastService : WeatherForecastServiceBase
    {
        private readonly string URL_ADVISOR;

        protected override Sources _source => Sources.ADVISOR;

        public AdvisorForecastService(ILogger<AdvisorForecastService> logger, IHttpClientFactory httpClientFactory,
            IDbContextFactory<AppDbContext> dbConnFactory,
            IConfiguration configuration) : base(logger, httpClientFactory, dbConnFactory)
        {
            URL_ADVISOR = configuration["ADVISOR_URL"]!;
        }

        protected override async Task<IEnumerable<ForecastInfoDay>> RetrieveForecasts()
        {
            try
            {
                AdvisorResponse? advisorResponse = await RequestForecastAsync();

                if (advisorResponse != null)
                {
                    return MapForecasts(advisorResponse);
                }

                logger.LogError("Advisor Error Request");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error on Request");
            }

            return [];

        }

        private async Task<AdvisorResponse?> RequestForecastAsync()
        {
            HttpClient httpClient = httpClientFactory.CreateClient();

            return await httpClient.GetFromJsonAsync<AdvisorResponse>(URL_ADVISOR);
        }

        protected IEnumerable<ForecastInfoDay> MapForecasts(AdvisorResponse advisorResponse)
        {
            DateTime currentDateTime = DateTime.Now.Date;

            int currentDayCode = int.Parse(currentDateTime.ToString("yyyyMMdd"));

            string city = advisorResponse.name;

            foreach (DataAdvisorResponse item in advisorResponse.data)
            {
                int itemDayCode = int.Parse(item.date.Replace("-", ""));

                int dayX = itemDayCode - currentDayCode;

                ForecastInfoDay newForecast = new()
                {
                    City = city,
                    ForecastDate = currentDateTime,
                    Source = _source,
                    DayX = dayX,
                    DayCode = itemDayCode,
                    Description = item.text_icon.text.pt
                };

                if (dayX == 0)
                {
                    newForecast.RealTemperatureMin = item.temperature.min;
                    newForecast.RealTemperatureMax = item.temperature.max;

                }
                else
                {
                    newForecast.MinTemperatureForecastMadeMinusX = item.temperature.min;
                    newForecast.MaxTemperatureForecastMadeMinusX = item.temperature.max;
                }

                yield return newForecast;
            }

        }
    }
}
