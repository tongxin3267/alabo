using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Alabo.App.Core.Common.Job;
using Alabo.App.Core.Tasks.Job;
using Alabo.App.Market.BookDonae.Domain.Services;
using Alabo.App.Open.Operate.Job;
using Alabo.App.Shop.Order.Job;
using Alabo.Helpers;
using Alabo.Runtime;
using Alabo.Schedules.Job;
using Alabo.Web.TaskShell;
using Alabo.Web.TaskShell.Job;

namespace Alabo.Web.TaskShell {

    public class Program {

        public static void Main(string[] args) {
            var host = BuildWebHost(args);
            RunJobs(host).GetAwaiter().GetResult();
            host.Run();
        }

        /// <summary>
        /// Run jobs
        /// </summary>
        private static async Task RunJobs(IWebHost host) {
            var scheduler = new Scheduler();
            await scheduler.AddJobAsync<BookImportJob>();
            //start
            await scheduler.StartAsync();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}