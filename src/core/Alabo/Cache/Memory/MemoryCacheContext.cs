using System;
using Microsoft.Extensions.Caching.Memory;

namespace Alabo.Cache.Memory
{
    public class MemoryCacheContext : ICacheContext
    {
        public MemoryCacheContext()
        {
            Instance = new MemoryCache(new MemoryCacheOptions());
        }

        public IMemoryCache MemoryCache => Instance as IMemoryCache;
        public object Instance { get; }

        public void Dispose()
        {
            if (Instance is IDisposable) {
                (Instance as IDisposable).Dispose();
            }
        }
    }
}