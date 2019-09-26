using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Alabo.Dependency;
using Alabo.Extensions;
using Alabo.Runtime;
using Alabo.Schedules.Job;
using Quartz;

namespace Alabo.Framework.Core.Admins.Job
{
    /// <summary>
    ///     3分钟定时访问网站
    ///     暂时解决网站打开速度问题
    /// </summary>
    public class AccessHostJob : JobBase
    {
        public override TimeSpan? GetInterval()
        {
            return TimeSpan.FromMinutes(3);
        }

        protected override async Task Execute(IJobExecutionContext context, IScope scope)
        {
            var clientHost = RuntimeContext.Current.WebsiteConfig.ClientHost;
            if (!clientHost.IsNullOrEmpty())
            {
                var apiUrl = clientHost + "/Api/Theme/GetAllClientPages?clientType=WapH5&path=index";
                await HttpGetAsync(apiUrl);
            }
        }

        public static async Task<string> HttpGetAsync(string url)
        {
            var httpClient = new HttpClient();
            var data = await httpClient.GetByteArrayAsync(url);
            var ret = Encoding.UTF8.GetString(data);
            return ret;
        }
    }
}