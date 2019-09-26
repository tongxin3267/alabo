using System;
using System.Collections.Generic;
using Alabo.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Alabo.Cache.Memory
{
    /// <summary>
    ///     Class MemoryObjectCache.
    /// </summary>
    public class MemoryObjectCache : IObjectCache
    {
        /// <summary>
        ///     The 所有 memory object cache key
        /// </summary>
        private const string AllMemoryObjectCacheKey = "_allMemoryObjectCacheKey";

        /// <summary>
        ///     Initializes a new instance of the <see cref="MemoryObjectCache" /> class.
        /// </summary>
        /// <param name="context">上下文</param>
        public MemoryObjectCache(ICacheContext context)
        {
            Context = context;
        }

        /// <summary>
        ///     Gets the context.
        /// </summary>
        public ICacheContext Context { get; set; }

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        public void Clear()
        {
            if (GetKeys() != null)
                foreach (var item in GetKeys())
                    if (!item.IsNullOrEmpty())
                        Context?.OfMemory()?.MemoryCache?.Remove(item);
        }

        /// <summary>
        ///     Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public T Get<T>(string key)
        {
            key = MemoryCacheExtensions.TenantCacheKey(key);
            return Context.OfMemory().MemoryCache.Get<T>(key);
        }

        /// <summary>
        ///     Gets the keys.
        /// </summary>
        public string[] GetKeys()
        {
            TryGet(AllMemoryObjectCacheKey, out List<string> cacheList);
            if (cacheList == null) return null;

            return cacheList.ToArray();
        }

        /// <summary>
        ///     Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            key = MemoryCacheExtensions.TenantCacheKey(key);
            if (!key.IsNullOrEmpty())
            {
                Context.OfMemory().MemoryCache.Remove(key);
                RemoveCacheKey(key);
            }
        }

        /// <summary>
        ///     Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Set(string key, object value)
        {
            key = MemoryCacheExtensions.TenantCacheKey(key);
            if (!key.IsNullOrEmpty())
            {
                Context.OfMemory().MemoryCache.Set(key, value);
                AddCacheKey(key);
            }
        }

        /// <summary>
        ///     Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Set<T>(string key, T value)
        {
            key = MemoryCacheExtensions.TenantCacheKey(key);
            if (!key.IsNullOrEmpty())
            {
                Context.OfMemory().MemoryCache.Set(key, value);
                AddCacheKey(key);
            }
        }

        /// <summary>
        ///     Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expire">The expire.</param>
        public void Set<T>(string key, T value, TimeSpan expire)
        {
            key = MemoryCacheExtensions.TenantCacheKey(key);
            if (!key.IsNullOrEmpty())
            {
                Context.OfMemory().MemoryCache.Set(key, value, expire);
                AddCacheKey(key);
            }
        }

        /// <summary>
        ///     Tries the get.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public bool TryGet<T>(string key, out T value)
        {
            key = MemoryCacheExtensions.TenantCacheKey(key);
            return Context.OfMemory().MemoryCache.TryGetValue(key, out value);
        }

        /// <summary>
        ///     Get the specified cacheKey, dataRetriever and expiration.
        ///     获取并设置缓存
        /// </summary>
        /// <param name="cacheKey">Cache key.</param>
        /// <param name="dataRetriever">Data retriever.</param>
        /// <param name="expiration">Expiration.</param>
        public CacheValue<T> GetOrSet<T>(Func<T> dataRetriever, string cacheKey, TimeSpan expiration) where T : class
        {
            cacheKey = MemoryCacheExtensions.TenantCacheKey(cacheKey);
            TryGet(cacheKey, out T result);
            if (result != null) return new CacheValue<T>(result, true);

            var item = dataRetriever?.Invoke();
            if (item != null)
            {
                Set(cacheKey, item, expiration);
                return new CacheValue<T>(item, true);
            }

            return CacheValue<T>.NoValue;
        }

        /// <summary>
        ///     Get the specified cacheKey, dataRetriever and expiration.
        ///     30分钟缓存一次
        /// </summary>
        /// <param name="dataRetriever">Data retriever.</param>
        /// <param name="cacheKey">Cache key.</param>
        public CacheValue<T> GetOrSet<T>(Func<T> dataRetriever, string cacheKey) where T : class
        {
            return GetOrSet(dataRetriever, cacheKey, TimeSpan.FromDays(30));
        }

        /// <summary>
        ///     Adds the cache key.
        /// </summary>
        /// <param name="key">The key.</param>
        private void AddCacheKey(string key)
        {
            ;
            key = MemoryCacheExtensions.TenantCacheKey(key);
            TryGet(AllMemoryObjectCacheKey, out List<string> cacheList);
            if (cacheList == null) cacheList = new List<string>();

            if (!cacheList.Contains(key))
            {
                cacheList.Add(key);
                Set(AllMemoryObjectCacheKey, cacheList);
            }
        }

        /// <summary>
        ///     Removes the cache key.
        /// </summary>
        /// <param name="key">The key.</param>
        private void RemoveCacheKey(string key)
        {
            key = MemoryCacheExtensions.TenantCacheKey(key);
            TryGet(AllMemoryObjectCacheKey, out List<string> cacheList);
            if (cacheList == null) cacheList = new List<string>();

            if (cacheList.Contains(key))
            {
                cacheList.Remove(key);
                Set(AllMemoryObjectCacheKey, cacheList);
            }
        }

        #region global cache

        /// <summary>
        ///     Get or set of global cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRetriever"></param>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public CacheValue<T> GetOrSetPublic<T>(Func<T> dataRetriever, string cacheKey) where T : class
        {
            cacheKey = MemoryCacheExtensions.PublicCacheKey(cacheKey);
            TryGetPublic(cacheKey, out T result);
            if (result != null) return new CacheValue<T>(result, true);

            var item = dataRetriever?.Invoke();
            if (item != null)
            {
                SetPublic(cacheKey, item);
                return new CacheValue<T>(item, true);
            }

            return CacheValue<T>.NoValue;
        }

        /// <summary>
        ///     Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SetPublic<T>(string key, T value)
        {
            key = MemoryCacheExtensions.PublicCacheKey(key);
            if (!key.IsNullOrEmpty()) Context.OfMemory().MemoryCache.Set(key, value);
        }

        /// <summary>
        ///     Try get of global cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetPublic<T>(string key, out T value)
        {
            key = MemoryCacheExtensions.PublicCacheKey(key);
            return Context.OfMemory().MemoryCache.TryGetValue(key, out value);
        }

        #endregion global cache
    }
}