using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace WeatherAccuracyMonitor.Infra.Cache
{
    public class CacheService(IMemoryCache memoryCache) : ICacheService
    {
        public T? Get<T>(string key)
        {
            memoryCache.TryGetValue(key, out T? value);
            
            return value;
        }

        public void Set<T>(string key, T value, uint ttlSeconds = 10)
        {
            memoryCache.Set(key, value, TimeSpan.FromSeconds(ttlSeconds));
        }
    }
}
