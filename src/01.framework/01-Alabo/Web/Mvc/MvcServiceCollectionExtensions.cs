using Alabo.Runtime;
using Alabo.Web.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.WebEncoders;
using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Alabo.Web.Mvc
{
    /// <summary>
    ///     Class MvcServiceCollectionExtensions.
    /// </summary>
    public static class MvcServiceCollectionExtensions
    {
        /// <summary>
        ///     项目MVC服务
        /// </summary>
        /// <param name="services">The services.</param>
        public static IServiceCollection AddMvcServiceProvider(this IServiceCollection services)
        {
            services.AddResponseCompression(); //页面压缩
            services.AddResponseCaching(); // 页面缓存

            // 源码编码问题  （效果一样）
            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });

            var builder = services.AddMvc(option =>
            {
                option.CacheProfiles.Add("responseCache", new CacheProfile
                {
                    Duration = 30
                });
                option.CacheProfiles.Add("test2", new CacheProfile
                {
                    Location = ResponseCacheLocation.None,
                    NoStore = true
                });
                if (RuntimeContext.Current.WebsiteConfig.IsDevelopment == false) {
                    option.Filters.Add<ExceptionHandlerAttribute>();
                }
                //  option.Filters.Add<WebExceptionFilterAttribute>();
            }).AddJsonOptions(options =>
            {
                // 设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }).AddControllersAsServices();

            // Cookie 与权限
            services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(60); });

            return services;
        }

        /// <summary>
        ///     项目MVC配置
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public static IApplicationBuilder AddMvcApplication(this IApplicationBuilder app, IHostingEnvironment env)
        {
            //客户端Api跨域请求
            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
                options.AllowCredentials();
            });
            app.UseResponseCaching();
            app.UseResponseCompression(); // 页面压缩

            app.UseStaticFiles();

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(env.ContentRootPath),
                RequestPath = new PathString(""),
                EnableDefaultFiles = true, //启用默认文件
                EnableDirectoryBrowsing = false //关闭目录浏览
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthentication();
            //使用session
            app.UseSession();
            // 添加路由

            //此处不放出错误，目前版本很难调试
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
                app.UseStatusCodePages();
            }
            else
            {
                // 正式环境
                app.UseExceptionHandler("/Home/Error");
            }

            app.AddMvcRouts();

            return app;
        }

        #region 添加路由

        /// <summary>
        ///     添加s the MVC routs.
        /// </summary>
        /// <param name="app">The application.</param>
        private static IApplicationBuilder AddMvcRouts(this IApplicationBuilder app)
        {
            app.UseMvc(routes => { routes.MapRoute("default", "{controller=Api}/{action=Index}/{id?}"); });
            return app;
        }

        #endregion 添加路由
    }
}