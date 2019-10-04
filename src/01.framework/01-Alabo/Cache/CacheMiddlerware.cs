using Microsoft.Extensions.Caching.Memory;

namespace Alabo.Cache {

    public class CacheMiddlerware {
        private readonly IMemoryCache _memoryCache;

        public CacheMiddlerware(IMemoryCache cache) {
            _memoryCache = cache;
        }

        public void SetValue(string key, object value) {
            _memoryCache.TryGetValue(key, out value);
        }
    }
}