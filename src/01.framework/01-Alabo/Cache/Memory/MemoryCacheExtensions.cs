using Alabo.Tenants;
using Castle.Core.Internal;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Alabo.Cache.Memory {

    public static class MemoryCacheExtensions {

        public static MemoryCacheContext OfMemory(this ICacheContext context) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            if (context is MemoryCacheContext) {
                return context as MemoryCacheContext;
            }

            throw new InvalidCastException("context is not a MemoryCacheContext instance.");
        }

        /// <summary>
        ///     租户的缓冲Key
        /// </summary>
        /// <param name="cacheKey">Cache Key</param>
        public static string TenantCacheKey(string cacheKey) {
            if (cacheKey.IsNullOrEmpty()) {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (TenantContext.IsTenant && !TenantContext.CurrentTenant.IsNullOrEmpty()) {
                if (!cacheKey.Contains(TenantContext.CurrentTenant)) {
                    cacheKey = $"{TenantContext.CurrentTenant}_{cacheKey}";
                }
            }

            if (cacheKey.Length >= 250) {
                using (var sha1 = SHA1.Create()) {
                    var data = sha1.ComputeHash(Encoding.UTF8.GetBytes(cacheKey));
                    return Convert.ToBase64String(data, Base64FormattingOptions.None);
                }
            }

            return cacheKey;
        }

        /// <summary>
        ///     公共缓冲Key
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static string PublicCacheKey(string cacheKey) {
            if (cacheKey.IsNullOrEmpty()) {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            cacheKey = $"Global_{cacheKey}";
            if (cacheKey.Length >= 250) {
                using (var sha1 = SHA1.Create()) {
                    var data = sha1.ComputeHash(Encoding.UTF8.GetBytes(cacheKey));
                    return Convert.ToBase64String(data, Base64FormattingOptions.None);
                }
            }

            return cacheKey;
        }
    }
}