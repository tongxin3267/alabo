using Alabo.Schedules.Job;
using Alabo.Web.CodeGeneration.EntityCode;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace Alabo.Web.CodeGeneration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            RunJobs(host).GetAwaiter().GetResult();
            host.Run();
        }

        /// <summary>
        ///     通过调度任务启动，不同的生成，请手动修改生成方法
        /// </summary>
        private static async Task RunJobs(IWebHost host)
        {
            var scheduler = new Scheduler();
            //实体服务和方法生成
            await scheduler.AddJobAsync<EntityCodeGenerationJob>();
            //start
            await scheduler.StartAsync();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }
    }
}