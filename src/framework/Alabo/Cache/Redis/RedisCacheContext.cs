using StackExchange.Redis;

namespace Alabo.Cache.Redis
{
    /// <summary>
    ///     Class RedisCacheContext.
    /// </summary>
    public class RedisCacheContext : ICacheContext
    {
        /// <summary>
        ///     The object cache index
        /// </summary>
        private static readonly int ObjectCacheIndex = 1;

        /// <summary>
        ///     The list cache index
        /// </summary>
        private static readonly int ListCacheIndex = 2;

        /// <summary>
        ///     The hash cache index
        /// </summary>
        private static readonly int HashCacheIndex = 3;

        /// <summary>
        ///     The file cache index
        /// </summary>
        private static readonly int FileCacheIndex = 4;

        /// <summary>
        ///     The file cache database
        /// </summary>
        private IDatabase _fileCacheDatabase;

        /// <summary>
        ///     The hash cache database
        /// </summary>
        private IDatabase _hashCacheDatabase;

        /// <summary>
        ///     The list cache database
        /// </summary>
        private IDatabase _listCacheDatabase;

        /// <summary>
        ///     The object cache database
        /// </summary>
        private IDatabase _objectCacheDatabase;

        /// <summary>
        ///     The redis connection
        /// </summary>
        private IConnectionMultiplexer _redisConnection;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RedisCacheContext" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public RedisCacheContext(ICacheConfiguration configuration)
        {
            //_redisConnection = ConnectionMultiplexer.Connect(configuration.CacheConfigurationString);
            var configurationOptions = ConfigurationOptions.Parse(configuration.CacheConfigurationString);
            configurationOptions.ConnectTimeout = 60000;
            configurationOptions.ResponseTimeout = 30000;
            configurationOptions.SyncTimeout = 30000;
            _redisConnection = ConnectionMultiplexer.Connect(configurationOptions);
        }

        /// <summary>
        ///     Gets the object cache database.
        /// </summary>
        public IDatabase ObjectCacheDatabase
        {
            get
            {
                if (_objectCacheDatabase == null && _redisConnection != null && _redisConnection.IsConnected) {
                    _objectCacheDatabase = _redisConnection.GetDatabase(ObjectCacheIndex);
                }

                return _objectCacheDatabase;
            }
        }

        /// <summary>
        ///     Gets the list cache database.
        /// </summary>
        public IDatabase ListCacheDatabase
        {
            get
            {
                if (_listCacheDatabase == null && _redisConnection != null && _redisConnection.IsConnected) {
                    _listCacheDatabase = _redisConnection.GetDatabase(ListCacheIndex);
                }

                return _listCacheDatabase;
            }
        }

        /// <summary>
        ///     Gets the hash cache database.
        /// </summary>
        public IDatabase HashCacheDatabase
        {
            get
            {
                if (_hashCacheDatabase == null && _redisConnection != null && _redisConnection.IsConnected) {
                    _hashCacheDatabase = _redisConnection.GetDatabase(HashCacheIndex);
                }

                return _hashCacheDatabase;
            }
        }

        /// <summary>
        ///     Gets the file cache database.
        /// </summary>
        public IDatabase FileCacheDatabase
        {
            get
            {
                if (_fileCacheDatabase == null && _redisConnection != null && _redisConnection.IsConnected) {
                    _fileCacheDatabase = _redisConnection.GetDatabase(FileCacheIndex);
                }

                return _fileCacheDatabase;
            }
        }

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        public object Instance => _redisConnection;

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_redisConnection != null)
            {
                _redisConnection.Close();
                _redisConnection.Dispose();
                _redisConnection = null;
                _objectCacheDatabase = null;
                _listCacheDatabase = null;
                _hashCacheDatabase = null;
                _fileCacheDatabase = null;
            }
        }
    }
}