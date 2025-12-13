using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAccuracyMonitor.Domain.Enums;

namespace WeatherAccuracyMonitor.Application.ValueModels.ResponsesBFF
{
    public class InfoCurrentWeek
    {
        public IEnumerable<string> ColumnsName { get; set; }
        public IEnumerable<InfoDataLine> LinesData { get; set; }
    }

    public class InfoDataLine
    {
        public int DayX { get; set; }
        public string ForecastWasMade { get; set; }        
        public List<ForecastDayInfoCard> ForecastsDayInfoCards { get; set; }
        
    }

    public class ForecastDayInfoCard
    {
        public int DayCode { get; set; }
        public IEnumerable<ForecastDayInfoItem> Items { get; set; }
    }

    public class ForecastDayInfoItem
    {
        public Sources Sources { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public string Description { get; set; }
    }
}
