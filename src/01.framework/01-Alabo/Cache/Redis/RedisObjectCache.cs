using Alabo.Extensions;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Alabo.Cache.Redis
{
    /// <summary>
    ///     Class RedisObjectCache.
    /// </summary>
    internal class RedisObjectCache : IObjectCache
    {
        /// <summary>
        ///     The redis database
        /// </summary>
        private readonly IDatabase _redisDatabase;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RedisObjectCache" /> class.
        /// </summary>
        /// <param name="context">上下文</param>
        public RedisObjectCache(ICacheContext context)
        {
            Context = context;
            _redisDatabase = Context.OfRedis().ObjectCacheDatabase;
            HashKeyCache<byte>.KeyConvertor = key => key;
            HashKeyCache<short>.KeyConvertor = key => key;
            HashKeyCache<int>.KeyConvertor = key => key;
            HashKeyCache<long>.KeyConvertor = key => key;
            HashKeyCache<string>.KeyConvertor = key => key;
            HashKeyCache<bool>.KeyConvertor = key => key;
            HashKeyCache<char>.KeyConvertor = key => key;
            HashKeyCache<double>.KeyConvertor = key => key;
            HashKeyCache<float>.KeyConvertor = key => key;
        }

        /// <summary>
        ///     Gets the context.
        /// </summary>
        public ICacheContext Context { get; }

        /// <summary>
        ///     Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public T Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) {
                throw new ArgumentNullException(nameof(key));
            }

            var stringValue = _redisDatabase.StringGet(key);
            if (!stringValue.HasValue) {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(stringValue);
        }

        /// <summary>
        ///     Tries the get.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public bool TryGet<T>(string key, out T value)
        {
            if (string.IsNullOrWhiteSpace(key)) {
                throw new ArgumentNullException(nameof(key));
            }

            var stringValue = _redisDatabase.StringGet(key);
            if (!stringValue.HasValue)
            {
                value = default;
                return false;
            }

            try
            {
                value = JsonConvert.DeserializeObject<T>(stringValue);
                return true;
            }
            catch
            {
                value = default;
                return false;
            }
        }

        /// <summary>
        ///     Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) {
                throw new ArgumentNullException(nameof(key));
            }

            _redisDatabase.KeyDelete(key);
        }

        /// <summary>
        ///     Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Set(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key)) {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            _redisDatabase.StringSet(key, value.ToJson());
        }

        /// <summary>
        ///     Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Set<T>(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key)) {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            _redisDatabase.StringSet(key, value.ToJson(typeof(T)));
        }

        /// <summary>
        ///     Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expire">The expire.</param>
        public void Set<T>(string key, T value, TimeSpan expire)
        {
            if (string.IsNullOrWhiteSpace(key)) {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            _redisDatabase.StringSet(key, value.ToJson(typeof(T)), expire);
        }

        /// <summary>
        ///     Gets the keys.
        /// </summary>
        public string[] GetKeys()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public CacheValue<T> GetOrSet<T>(Func<T> dataRetriever, string cacheKey) where T : class
        {
            return GetOrSet(dataRetriever, cacheKey, TimeSpan.FromHours(30));
        }

        public CacheValue<T> GetOrSet<T>(Func<T> dataRetriever, string cacheKey, TimeSpan expiration) where T : class
        {
            //ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));
            //ArgumentCheck.NotNegativeOrZero(expiration, nameof(expiration));

            TryGet(HandleCacheKey(cacheKey), out T result);
            if (result != null) {
                return new CacheValue<T>(result, true);
            }

            var item = dataRetriever?.Invoke();
            if (item != null)
            {
                Set(cacheKey, item, expiration);
                return new CacheValue<T>(item, true);
            }

            return CacheValue<T>.NoValue;
        }

        /// <summary>
        ///     Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public object Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) {
                throw new ArgumentNullException(nameof(key));
            }

            string result = _redisDatabase.StringGet(key);
            return result;
        }

        /// <summary>
        ///     Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expire">The expire.</param>
        public void Set(string key, object value, TimeSpan expire)
        {
            if (string.IsNullOrWhiteSpace(key)) {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            _redisDatabase.StringSet(key, JsonConvert.SerializeObject(value), expire);
        }

        /// <summary>
        ///     Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="hashKey">The hash key.</param>
        /// <param name="hashValue">The hash value.</param>
        public void Set<TKey, TValue>(string key, TKey hashKey, TValue hashValue)
        {
            if (hashKey == null) {
                throw new ArgumentNullException(nameof(hashKey));
            }

            if (hashValue == null) {
                throw new ArgumentNullException(nameof(hashValue));
            }

            var redisHashKey = GetHashKey(hashKey);
            _redisDatabase.HashSet(key, redisHashKey, JsonConvert.SerializeObject(hashValue));
        }

        /// <summary>
        ///     Tries the get.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="hashKey">The hash key.</param>
        /// <param name="value">The value.</param>
        public bool TryGet<T, TKey>(string key, TKey hashKey, out T value)
        {
            if (hashKey == null) {
                throw new ArgumentNullException(nameof(hashKey));
            }

            var redisHashKey = GetHashKey(hashKey);
            var result = _redisDatabase.HashGet(key, redisHashKey);
            if (!result.HasValue)
            {
                value = default;
                return false;
            }

            value = JsonConvert.DeserializeObject<T>(result);
            return true;
        }

        /// <summary>
        ///     Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="hashKey">The hash key.</param>
        public void Remove<TKey>(string key, TKey hashKey)
        {
            Remove(key, hashKey);
        }

        public async Task<CacheValue<T>> GetOrSetAsync<T>(Func<Task<T>> dataRetriever, string cacheKey,
            TimeSpan expiration) where T : class
        {
            // ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));
            TryGet(HandleCacheKey(cacheKey), out T result);
            if (result != null) {
                return new CacheValue<T>(result, true);
            }

            var item = await dataRetriever?.Invoke();
            if (item != null)
            {
                Set(cacheKey, item, expiration);
                return new CacheValue<T>(item, true);
            }

            return CacheValue<T>.NoValue;
        }

        /// <summary>
        ///     获取s the hash key.
        /// </summary>
        /// <param name="hashKey">The hash key.</param>
        private RedisValue GetHashKey<TKey>(TKey hashKey)
        {
            RedisValue redisHashKey;
            var keyConvertor = HashKeyCache<TKey>.KeyConvertor;
            if (keyConvertor == null) {
                redisHashKey = hashKey.GetHashCode();
            } else {
                redisHashKey = keyConvertor(hashKey);
            }

            return redisHashKey;
        }

        private string HandleCacheKey(string cacheKey)
        {
            // Memcached has a 250 character limit
            // Following memcached.h in https://github.com/memcached/memcached/
            if (cacheKey.Length >= 250) {
                using (var sha1 = SHA1.Create())
                {
                    var data = sha1.ComputeHash(Encoding.UTF8.GetBytes(cacheKey));
                    return Convert.ToBase64String(data, Base64FormattingOptions.None);
                }
            }

            return cacheKey;
        }

        /// <summary>
        ///     Class HashKeyCache.
        /// </summary>
        private static class HashKeyCache<T>
        {
            /// <summary>
            ///     Gets or sets the key convertor.
            /// </summary>
            public static Func<T, RedisValue> KeyConvertor { get; set; }
        }

        #region global

        public CacheValue<T> GetOrSetPublic<T>(Func<T> dataRetriever, string cacheKey) where T : class
        {
            throw new NotImplementedException();
        }

        public void SetPublic<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public bool TryGetPublic<T>(string key, out T value)
        {
            throw new NotImplementedException();
        }

        #endregion global
    }
}