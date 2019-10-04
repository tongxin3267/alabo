using System;

namespace Alabo.Cache.Redis {

    internal static class RedisCacheContextExtensions {

        public static RedisCacheContext OfRedis(this ICacheContext context) {
            if (context is RedisCacheContext) {
                return context as RedisCacheContext;
            }

            throw new InvalidCastException("context is not a RedisCacheContext instance.");
        }
    }
}