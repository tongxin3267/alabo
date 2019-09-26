using Alabo.Domains.Base.Entities;
using Alabo.Domains.Base.Services;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Tenant
{
    public class MongodbEntity : CoreTest
    {
        [Fact]
        public void Log_Test()
        {
            var logs = new Logs
            {
                Content = "测试"
            };
            Resolve<ILogsService>().Add(logs);
        }
    }
}