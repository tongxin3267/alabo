using Alabo.Apps;
using Alabo.Datas.Ef;
using Alabo.Datas.UnitOfWorks;
using Alabo.Datas.UnitOfWorks.SqlServer;
using Alabo.Events.Default;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis;
using Alabo.Logging.Extensions;
using Alabo.Runtime;
using Alabo.Tenants.Extensions;
using Alabo.Tool.Payment;
using Alabo.Web.Extensions;
using Alabo.Web.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Alabo.Web.CodeGeneration
{
    /// <summary>
    ///     startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     初始化启动配置
        /// </summary>
        /// <param name="configuration">配置</param>
        /// <param name="env">主机111111123</param>
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            env.ConfigurationRuntimePath(Configuration);
            Configuration = configuration;
        }

        /// <summary>
        ///     配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     配置服务
        /// </summary>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // 项目底层配置服务
            services.AddAppServcie(Configuration);

            services.AddMvcServiceProvider();
            //添加NLog日志操作
            services.AddNLog();
            //添加事件总线服务
            services.AddEventBus();
            //注册XSRF令牌服务
            //   services.AddXsrfToken();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //添加工作单元
            services.AddUnitOfWork<IUnitOfWork, SqlServerUnitOfWork>(Configuration
                .GetConnectionString("ConnectionString").GetConnectionStringForMaster());

            // mvc 相关服务

            // Api 接口相关服务
            services.AddApiService();
            // 添加支付方式等,第三方注入
            services.AddApiStoreService();
            // 添加任务调度服务
            //  services.AddTasks();

            return services.AddUtil();
        }

        /// <summary>
        ///     公共配置
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // 项目底层启动服务
            app.UseAppApplication();
            app.UseErrorLog();
            //  app.UseXsrfToken();
            // api相关配置，需放在mvc之前
            app.AddApiConfig(env);

            //tenant
            app.UseTenant();

            // mvc 相关配置
            app.AddMvcApplication(env);
        }
    }
}