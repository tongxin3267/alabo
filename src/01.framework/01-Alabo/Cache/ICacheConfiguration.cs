namespace Alabo.Cache {

    /// <summary>
    ///     Interface ICacheConfiguration
    /// </summary>
    public interface ICacheConfiguration {

        /// <summary>
        ///     Gets the cache configuration string.
        /// </summary>
        string CacheConfigurationString { get; set; }
    }

    /// <summary>
    /// </summary>
    public class CacheConfiguration : ICacheConfiguration {

        /// <summary>
        ///     Initializes a new instance of the <see cref="CacheConfiguration" /> class.
        /// </summary>
        public CacheConfiguration(string cacheConfigurationString) {
            CacheConfigurationString = cacheConfigurationString;
        }

        /// <summary>
        /// </summary>
        public string CacheConfigurationString { get; set; }
    }
}