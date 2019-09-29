using Alabo.Cache.Memory;
using Alabo.Cache.Redis;
using Alabo.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace Alabo.Cache
{
    /// <summary>
    ///     Class CacheExtensions.
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        ///     添加缓存服务
        /// </summary>
        /// <param name="services">The services.</param>
        public static IServiceCollection AddCacheService(this IServiceCollection services)
        {
            services.AddMemoryCache();
            var cacheScheme = RuntimeContext.Current.WebsiteConfig.CacheScheme;
            // 使用内存作为缓存时
            ICacheContext cacheContext = new MemoryCacheContext();
            IObjectCache objectCache = new MemoryObjectCache(cacheContext);
            if (cacheScheme == "redis")
            {
                // 使用redis做为缓存时
                ICacheConfiguration cacheConfiguration =
                    new CacheConfiguration(RuntimeContext.Current.CacheConfigurationString);
                cacheContext = new RedisCacheContext(cacheConfiguration);
                objectCache = new RedisObjectCache(cacheContext);
            }

            services.AddSingleton(cacheContext);
            services.AddSingleton(objectCache);
            return services;
        }
    }
}