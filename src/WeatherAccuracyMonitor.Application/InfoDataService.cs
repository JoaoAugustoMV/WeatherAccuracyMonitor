using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAccuracyMonitor.Application.Interfaces;
using WeatherAccuracyMonitor.Application.ValueModels.ResponsesBFF;
using WeatherAccuracyMonitor.Crosscutting.Utils;
using WeatherAccuracyMonitor.Infra.Cache;
using WeatherAccuracyMonitorBackend.Domain.Repositories;

namespace WeatherAccuracyMonitor.Application
{
    public class InfoDataService(IForecastInfoDayRepository forecastInfoDayRepository, ICacheService cacheService) : IInfoDataService
    {
        private const string CacheKey = "InfoCurrentWeekCacheKey";
        private const uint CacheTtl = 3600;

        private static IEnumerable<string> GetColumsName()
        {            
            DateTime todayDate = DateTime.Now;
            
            List<string> dayNames = [];
            for (int i = 0; i < 7; i++) 
            {
                DateTime currentDate = todayDate.AddDays(i);
                StringBuilder currentName = new();

                currentName.Append($"DIA {currentDate.ToString("dd/MM")} {currentDate.GetStringDayOfWeek()}");
                if(i == 0)
                {
                    currentName.Append("(Hoje)");
                }
                else if(i == 1)
                {
                    currentName.Append("(Amanha)");
                }

                dayNames.Add(currentName.ToString());
            }


            return dayNames;
        }        

        public async Task<IEnumerable<InfoDataLine>> GetInfo(DateTime minDate, DateTime maxDate)
        {
            Dictionary<int, string> nomeLinha = new Dictionary<int, string>()
            {
            };
            
            for (int i = 0; i < 7; i++)
            {
                string currentTitle = string.Empty;
                if (i == 0)
                {
                    currentTitle = "Hoje";
                }
                else if (i == 1)
                {
                    currentTitle = "Ontem";
                }
                else
                {
                    currentTitle = $"{i + 1} dias Atras";
                }

                nomeLinha.Add(i, currentTitle);
            }

            var minCode = int.Parse(minDate.ToString("yyyyMMdd"));
            var maxCode = int.Parse(maxDate.ToString("yyyyMMdd"));

            var allData = forecastInfoDayRepository.GetAll()
                .Where(f => minCode <= f.DayCode && f.DayCode < maxCode)
                .ToList()
                .OrderByDescending(o => o.ForecastDate)
                .GroupBy(f => f.ForecastDate).Select(s => s.OrderBy(r=> r.Source).GroupBy(g => g.DayCode))
            .ToList();

            List<InfoDataLine> infoDataLines = [];

            int count = 0;
            foreach (var groups in allData)
            {
                InfoDataLine dataLine = new ()
                {
                    DayX = count,
                    ForecastsDayInfoCards = [],
                    ForecastWasMade = nomeLinha[count]
                };
                count++;

                foreach (var forecastInfoDay in groups)
                {
                    int dayX = forecastInfoDay.First().DayX;
                    ForecastDayInfoCard card = new ()
                    {
                        DayCode = forecastInfoDay.First().DayCode,
                        Items = forecastInfoDay.Select(f => new ForecastDayInfoItem
                        {
                            Sources = f.Source,
                            MinTemperature = dayX == 0 ? f.RealTemperatureMin : f.MinTemperatureForecastMadeMinusX,
                            MaxTemperature = dayX == 0 ? f.RealTemperatureMax : f.MaxTemperatureForecastMadeMinusX,
                            Description = f.Description
                        })
                    };

                    dataLine.ForecastsDayInfoCards.Add(card);
                }

                infoDataLines.Add(dataLine);
            }
            return infoDataLines.OrderBy(f => f.DayX);
        }

        public async Task<InfoCurrentWeek> GetInfoCurrentWeek()
        {
            InfoCurrentWeek? infoCurrentWeek = cacheService.Get<InfoCurrentWeek>(CacheKey);
            if (infoCurrentWeek != null)
            {
                return infoCurrentWeek;
            }

            DateTime now = DateTime.Now;
            DateTime max = now.AddDays(7).Date;

            infoCurrentWeek = new ()
            {
                ColumnsName = GetColumsName(),
                LinesData = await GetInfo(now, max)
            };

            cacheService.Set(CacheKey, infoCurrentWeek, CacheTtl);

            return infoCurrentWeek;
        }
    }
}
