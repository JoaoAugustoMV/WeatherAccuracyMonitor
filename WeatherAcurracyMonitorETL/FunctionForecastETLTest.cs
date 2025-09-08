using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using WeatherAccuracyMonitorLib.Domain.Services.ForecastServices;
using WeatherAcurracyMonitorETL.Services;

namespace WeatherAcurracyMonitorETL
{
    public class FunctionForecastETLTest
    {
        private readonly ILogger _logger;
        private readonly ForecastServiceExecutor _forecastServiceExecutor;

        public FunctionForecastETLTest(ILoggerFactory loggerFactory, ForecastServiceExecutor forecastServiceExecutor)
        {
            _logger = loggerFactory.CreateLogger<FunctionForecastETLTest>();
            _forecastServiceExecutor = forecastServiceExecutor;
        }

        [Function("FunctionForecastETLTest")]
        public async Task Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {

            _logger.LogInformation($"C# Http function executed at: {DateTime.Now}");
            try
            {
                await _forecastServiceExecutor.RunAll();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error Run");
            }
            
        }


    }
}
