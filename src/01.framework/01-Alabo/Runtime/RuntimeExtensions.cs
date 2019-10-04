using Alabo.Cache;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Alabo.Runtime {

    /// <summary>
    ///     Class RuntimeExtensions.
    /// </summary>
    public static class RuntimeExtensions {

        /// <summary>
        ///     添加s the runtime configuration.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static IServiceCollection AddRuntimeConfiguration(this IServiceCollection services,
            IConfiguration configuration) {
            if (services == null) {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null) {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddSingleton(configuration);
            services.AddSingleton<RuntimeContext>();
            services.AddSingleton<ICacheConfiguration, RuntimeContext>();
            return services;
        }

        /// <summary>
        ///     Uses the runtime context.
        /// </summary>
        /// <param name="app">The application.</param>
        public static IApplicationBuilder UseRuntimeContext(this IApplicationBuilder app) {
            async Task Middleware(HttpContext context, Func<Task> next) {
                var threadId = Thread.CurrentThread.ManagedThreadId;
                RuntimeContext.SetRequestServices(threadId, context.RequestServices);
                await next();
            }

            return app.Use(Middleware);
        }

        /// <summary>
        ///     Sets the runtime request services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void SetRuntimeRequestServices(IServiceProvider services) {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            RuntimeContext.SetRequestServices(threadId, services);
        }

        /// <summary>
        ///     Configurations the runtime path.
        /// </summary>
        /// <param name="env">The env.</param>
        /// <param name="configuration"></param>
        public static void ConfigurationRuntimePath(this IHostingEnvironment env, IConfiguration configuration) {
            RuntimeContext.Current.Path = new RuntimePath(env);
            RuntimeContext.Current.Configuration = configuration;
        }

        /// <summary>
        ///     Configurations the runtime path.
        /// </summary>
        /// <param name="path">The env.</param>
        /// <param name="configuration"></param>
        public static void ConfigurationRuntimePath(this IConfiguration configuration, string path) {
            RuntimeContext.Current.Path = new RuntimePath(path);
            RuntimeContext.Current.Configuration = configuration;
        }
    }
}