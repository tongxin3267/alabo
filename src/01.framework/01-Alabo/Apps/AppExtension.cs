using Alabo.Cache;
using Alabo.RestfulApi;
using Alabo.Runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alabo.Apps
{
    /// <summary>
    ///     项目扩展
    /// </summary>
    public static class AppExtension
    {
        /// <summary>
        ///     底层项目启动服务
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static IServiceCollection AddAppServcie(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationInsightsTelemetry(configuration);
            // 项目Runtime 配置
            services.AddRuntimeConfiguration(configuration);

            //缓存
            services.AddCacheService();
            services.AddSingleton<RestClientConfig>();

            return services;
        }

        /// <summary>
        ///     项目底层启动服务
        /// </summary>
        /// <param name="app">The application.</param>
        public static IApplicationBuilder UseAppApplication(this IApplicationBuilder app)
        {
            // 项目Runtime配置
            app.UseRuntimeContext();
            return app;
        }
    }
}