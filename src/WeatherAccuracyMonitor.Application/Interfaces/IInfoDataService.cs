using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAccuracyMonitor.Application.ValueModels.ResponsesBFF;

namespace WeatherAccuracyMonitor.Application.Interfaces
{
    public interface IInfoDataService
    {
        Task<IEnumerable<InfoDataLine>> GetInfo(DateTime minDate, DateTime maxDate);
        Task<InfoCurrentWeek> GetInfoCurrentWeek();
    }
}
