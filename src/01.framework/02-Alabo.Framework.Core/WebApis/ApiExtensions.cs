using System;
using Alabo.Core.WebApis.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;

namespace Alabo.Core.WebApis {

    /// <summary>
    ///     Class ApiExtensions.
    /// </summary>
    public static class ApiExtensions {

        /// <summary>
        ///     Api接口配置，包括跨域，安全配置等
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">env</param>
        public static IApplicationBuilder AddApiConfig(this IApplicationBuilder app, IHostingEnvironment env) {
            app.AddSwaggerConfig(env); // Api接口文档
            //客户端Api跨域请求
            app.UseCors(options => {
                options.AllowAnyHeader().SetPreflightMaxAge(TimeSpan.FromHours(2400));
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
                options.AllowCredentials();
                ;
            });
            app.UseForwardedHeaders(new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            return app;
        }

        /// <summary>
        ///     添加  API 服务.
        /// </summary>
        /// <param name="services">The services.</param>
        public static IServiceCollection AddApiService(this IServiceCollection services) {
            services.AddSwaggerService();
            return services;
        }
    }
}