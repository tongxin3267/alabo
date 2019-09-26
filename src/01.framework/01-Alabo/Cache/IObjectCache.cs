using Alabo.Dependency;
using System;

namespace Alabo.Cache
{
    /// <summary>
    ///     Interface IObjectCache
    /// </summary>
    public interface IObjectCache : IScopeDependency
    {
        /// <summary>
        ///     Gets the context.
        /// </summary>
        ICacheContext Context { get; }

        /// <summary>
        ///     读取或设置缓存
        ///     如果缓存中不存在，则将值插入缓存中
        /// </summary>
        /// <param name="dataRetriever">The data retriever.</param>
        /// <param name="cacheKey">The cache key.</param>
        CacheValue<T> GetOrSet<T>(Func<T> dataRetriever, string cacheKey) where T : class;

        /// <summary>
        ///     Get the specified cacheKey, dataRetriever and expiration.
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="cacheKey">Cache key.</param>
        /// <param name="dataRetriever">Data retriever.</param>
        /// <param name="expiration">Expiration.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        CacheValue<T> GetOrSet<T>(Func<T> dataRetriever, string cacheKey, TimeSpan expiration) where T : class;

        /// <summary>
        ///     Get the specified cacheKey, dataRetriever and expiration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRetriever"></param>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        CacheValue<T> GetOrSetPublic<T>(Func<T> dataRetriever, string cacheKey) where T : class;

        /// <summary>
        ///     Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void Set(string key, object value);

        /// <summary>
        ///     Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void Set<T>(string key, T value);

        /// <summary>
        ///     设置所有租户公用缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetPublic<T>(string key, T value);

        /// <summary>
        ///     Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expire">The expire.</param>
        void Set<T>(string key, T value, TimeSpan expire);

        /// <summary>
        ///     Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        T Get<T>(string key);

        /// <summary>
        ///     Tries the get.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        bool TryGet<T>(string key, out T value);

        /// <summary>
        ///     获取所有租户通用缓存,比如内，应用程序池等
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        bool TryGetPublic<T>(string key, out T value);

        /// <summary>
        ///     Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        void Remove(string key);

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        void Clear();

        /// <summary>
        ///     Gets the keys.
        /// </summary>
        string[] GetKeys();
    }
}