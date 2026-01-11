using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAccuracyMonitor.Infra.Cache
{
    public interface ICacheService
    {
        T? Get<T>(string key);
        void Set<T> (string key, T value, uint ttlSeconds = 10);
    }
}
