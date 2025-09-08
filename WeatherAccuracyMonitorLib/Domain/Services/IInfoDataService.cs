using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAccuracyMonitorLib.Domain.Models.ValueModels.ResponsesBFF;

namespace WeatherAccuracyMonitorLib.Domain.Services
{
    public interface IInfoDataService
    {
        Task<IEnumerable<InfoDataLine>> GetInfo(DateTime minDate, DateTime maxDate);
        Task<InfoCurrentWeek> GetInfoCurrentWeek();
    }
}
