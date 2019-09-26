using Alabo.App.Core.ApiStore;
using Alabo.Apps;
using Alabo.Datas.Ef;
using Alabo.Datas.UnitOfWorks;
using Alabo.Datas.UnitOfWorks.SqlServer;
using Alabo.Events.Default;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Logging.Extensions;
using Alabo.Runtime;
using Alabo.Test.Base.Core.Test;
using Alabo.Web.Extensions;
using Alabo.Web.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Alabo.Framework.Core.WebApis;

namespace Alabo.Test.Base.Core.Model {

    public abstract class CoreTest : IBaseTest {
        private static readonly IServiceCollection services = new ServiceCollection();

        private readonly IServiceScope _serviceScope;

        static CoreTest() {
            var testHostingEnvironment = new TestHostingEnvironment();
            var builder = new ConfigurationBuilder()
                .SetBasePath(testHostingEnvironment.WebRootPath).AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Configuration.ConfigurationRuntimePath(testHostingEnvironment.WebRootPath);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(services =>
                new HttpContextAccessor {
                    HttpContext = new DefaultHttpContext {
                        RequestServices = services
                    }
                });

            //   services.AddTasks();
            services.AddMvc();
            // 项目底层配置服务
            services.AddAppServcie(Configuration);
            //添加Mvc服务
            services.AddMvc(options => {
                //options.Filters.Add( new AutoValidateAntiforgeryTokenAttribute() );
                options.Filters.Add(new ExceptionHandlerAttribute());
            }
            ).AddControllersAsServices();

            //添加NLog日志操作
            services.AddNLog();

            //添加事件总线服务
            services.AddEventBus();

            //注册XSRF令牌服务
            services.AddXsrfToken();

            //添加工作单元
            var config = Configuration.GetConnectionString("ConnectionString");
            var database = RuntimeContext.GetTenantDataBase();
            config = config.Replace(RuntimeContext.Current.WebsiteConfig.MongoDbConnection.Database, database);
            services.AddUnitOfWork<IUnitOfWork, SqlServerUnitOfWork>(config);

            services.AddTestService<ITest, ITestBase>();
            // Api 接口相关服务
            services.AddApiService();
            // 添加支付方式等,第三方注入
            services.AddApiStoreService();

            services.AddUtil();
        }

        public CoreTest() {
            Services = services.BuildServiceProvider();
            _serviceScope = Services.CreateScope();

            //RuntimeExtensions.SetRuntimeRequestServices(Services);
        }

        protected IServiceProvider Services { get; }

        private static IConfigurationRoot Configuration { get; }

        public void Dispose() {
            _serviceScope.Dispose();
            // Ioc.Dispose();
            //  ContainerManager.Default.DisposeTestPerHttpRequestContainer();
        }

        /// <summary>
        ///     获取依赖
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected T Resolve<T>() {
            return Ioc.Resolve<T>();
        }
    }
}