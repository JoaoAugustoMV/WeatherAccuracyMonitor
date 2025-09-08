using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAccuracyMonitorLib.Utils
{
    public static class DateTimeUtils
    {
        public static string GetStringDayOfWeek(this DateTime dateTime, string culture = "pt-BR")
        {
            CultureInfo cultura = new CultureInfo(culture);
            return cultura.TextInfo.ToTitleCase(dateTime.ToString("dddd", cultura));

        }
    }
}
