using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace WeatherAcurracyMonitorETL
{
    public class FunctionForecastETL
    {
        private readonly ILogger _logger;

        public FunctionForecastETL(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FunctionForecastETL>();
        }

        [Function("FunctionForecastETL")]
        public void Run([TimerTrigger("* 5 * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }


    }
}
