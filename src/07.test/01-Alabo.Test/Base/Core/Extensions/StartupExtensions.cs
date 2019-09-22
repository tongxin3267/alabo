using Alabo.Cache;
using Alabo.Runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Alabo.Test.Base.Core.Extensions {

    public static class StartupExtensions {

        public static IServiceCollection AddTestServices(this IServiceCollection services) {
            // Add ZKCloud Services.
            // services.AddRuntimeService();
            services.AddCacheService();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }

        public static void AddTestConfigurations(this IConfigurationRoot configuration, IApplicationBuilder app,
            IHostingEnvironment env, ILoggerFactory loggerFactory) {
            loggerFactory.AddConsole(configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            //此处不放出错误，目前版本很难调试
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            } else {
                app.UseStatusCodePagesWithRedirects("/{0}");
                app.UseExceptionHandler("/error");
            }

            app.UseRuntimeContext();
        }
    }
}