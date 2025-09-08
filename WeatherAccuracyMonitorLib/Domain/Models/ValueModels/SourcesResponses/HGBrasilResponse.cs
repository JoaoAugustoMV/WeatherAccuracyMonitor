using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAccuracyMonitorLib.Domain.Models.ValueModels.SourcesResponses
{
    public class HGBrasilResponse
    {
        public HGBrasilResults results {  get; set; }
    }

    public class HGBrasilResults
    {
        public string city_name { get; set; }
        public IEnumerable<HGBrasilForecasts> forecast {  get; set; }
    }

    public class HGBrasilForecasts
    {
        public string full_date { get; set; }
        public double max {  get; set; }
        public double min { get; set; }
        public string description { get; set; }
    }
}
