using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAccuracyMonitor.Providers.SourcesResponses
{
    public class AdvisorResponse
    {
        public string name {  get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public IEnumerable<DataAdvisorResponse> data { get; set; }
        
    }

    public class DataAdvisorResponse
    {
        public string date { get; set; }
        public TemperatureAdvisorResponse temperature { get; set; }
        public AdvisorTextIcon text_icon { get; set; }  
    }

    public class TemperatureAdvisorResponse
    {
        public double min { get; set; }
        public double max { get; set; }
    }

    public class AdvisorTextIcon
    {
        public AdvisorText text { get; set; }
    }

    public class AdvisorText
    {
        public string pt { get; set; }
        public string en { get; set; }
        public string es { get; set; }
    }
}
