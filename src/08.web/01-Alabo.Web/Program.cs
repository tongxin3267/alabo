using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Alabo.App.Core.Common.Job;
using Alabo.App.Core.Tasks.Job;
using Alabo.App.Shop.Order.Job;
using Alabo.Core.Admins.Job;
using Alabo.Runtime;
using Alabo.Schedules;
using Alabo.Schedules.Job;
using Alabo.Tenants.Domain.Entities;

namespace Alabo.Web {

    /// <summary>
    /// Program
    /// </summary>
    public class Program {

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args) {
            var host = BuildWebHost(args);
            //   RunJobs(host).GetAwaiter().GetResult();
            host.Run();
        }

        /// <summary>
        /// Run jobs
        /// </summary>
        private static async Task RunJobs(IWebHost host) {
            // 授权调度,30分钟一次
            //This scheduler is only in master teant.
            var scheduler = new Scheduler();
            //   await scheduler.AddJobAsync<ServerAuthenticationJob>();

            if (RuntimeContext.Current.WebsiteConfig.IsDevelopment == false) {
                // 发送,2秒一次
                await scheduler.AddJobAsync<MessageQueueJob>();
                // 分润调度,3分钟一次
                await scheduler.AddJobAsync<ShareJob>();
                // 后台队列任务,1分钟一次
                await scheduler.AddJobAsync<BackJob>();
                // 后台队列任务,1分钟一次
                await scheduler.AddJobAsync<QueueTaskJob>();
                // 升级队列,1分钟一次
                //await scheduler.AddJobAsync<UpgradeJob>();
                // 所有的统计任务，30分钟一次
                //await scheduler.AddReportAsync();
                // 定时访问后台访问接口，解决第一次访问速度问题，3分钟一次
                await scheduler.AddJobAsync<AccessHostJob>();
                //start
                await scheduler.StartAsync();

                //定时关闭订单
                await scheduler.AddJobAsync<OrderStatusJob>();

                ////tenant jobs
                //await TenantContext.StartTenantJobs(TenantJobs);
            }
        }

        /// <summary>
        /// 租户调度任务
        /// </summary>
        /// <param name="tenant"></param>
        private static async void TenantJobs(Tenant tenant) {
            var scheduler = new Scheduler(tenant);
            // product sync job (Default 10 Minutes)
            //await scheduler.AddJobAsync<ProductSyncJob>();
            //// order sync job (Default 3 Minutes)
            //await scheduler.AddJobAsync<OrderSyncJob>();
            //// Theme sync job (Default 10 Minutes)
            //await scheduler.AddJobAsync<ThemeSyncJob>();
            // Start
            await scheduler.StartAsync();
        }

        /// <summary>
        /// Builds the web host.
        /// </summary>
        /// <param name="args">The arguments.</param>

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}