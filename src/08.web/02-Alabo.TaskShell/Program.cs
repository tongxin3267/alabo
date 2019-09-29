using Alabo.Schedules.Job;
using Alabo.Web.TaskShell.Job;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace Alabo.Web.TaskShell
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
        ///     Run jobs
        /// </summary>
        private static async Task RunJobs(IWebHost host)
        {
            var scheduler = new Scheduler();
            await scheduler.AddJobAsync<BookImportJob>();
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