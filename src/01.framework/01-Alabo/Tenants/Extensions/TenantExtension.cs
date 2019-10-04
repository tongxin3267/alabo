using Microsoft.AspNetCore.Builder;

namespace Alabo.Tenants.Extensions {

    /// <summary>
    ///     tenant extension
    /// </summary>
    public static class Extensions {

        /// <summary>
        ///     tenant
        /// </summary>
        /// <param name="builder">IApplicationBuilder</param>
        public static IApplicationBuilder UseTenant(this IApplicationBuilder builder) {
            return builder.UseMiddleware<TenantMiddleware>();
        }
    }
}