using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherAccuracyMonitor.Domain.Entities;
using WeatherAccuracyMonitor.Domain.Enums;
using WeatherAccuracyMonitorBackend.Domain.Repositories;
using System.Globalization;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WeatherAccuracyMonitor.Application;
using WeatherAccuracyMonitor.Data;
using Microsoft.EntityFrameworkCore;
using WeatherAccuracyMonitor.Data.Repositories;

namespace WeatherAccuracyMonitor.Application
{
    public abstract class WeatherForecastServiceBase
    {
        protected abstract Sources _source { get; }

        protected readonly ILogger logger;
        protected readonly IHttpClientFactory httpClientFactory;
       
        private readonly IDbContextFactory<AppDbContext> dbConnFactory;

        public WeatherForecastServiceBase(ILogger<WeatherForecastServiceBase> logger,
            IHttpClientFactory httpClientFactory,
            IDbContextFactory<AppDbContext> dbConnFactory
            )
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
            this.dbConnFactory = dbConnFactory;

            logger.LogInformation($"WeatherForecastServiceBase - {_source.ToString()} - Initialized");
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
                using var dbContext = dbConnFactory.CreateDbContext();

                ForecastInfoDayRepository forecastInfoDayRepository = new (dbContext);
                await forecastInfoDayRepository.InsertAsync(forecastInfoDays);
            }
            catch (Exception ex) 
            {
                logger.LogError(ex, "SaveTemperaturesPredictions - Error");
            }
        }

        protected abstract Task<IEnumerable<ForecastInfoDay>> RetrieveForecasts();        

        
    }
}
