using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAcurracyMonitorETL.Services
{
    public class ForecastServiceExecutor
    {
        private readonly IEnumerable<WeatherForecastServiceBase> _services;

        public ForecastServiceExecutor(IEnumerable<WeatherForecastServiceBase> services)
        {
            _services = services;
        }

        public async Task RunAll()
        {
            await Task.WhenAll(_services.Select(s => s.ExecuteTemperaturesPredictions()));            
        }
    }
}
