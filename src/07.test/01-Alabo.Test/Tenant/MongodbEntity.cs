using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Alabo.Domains.Base.Entities;
using Alabo.Domains.Base.Services;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Tenant {

    public class MongodbEntity : CoreTest {

        [Fact]
        public void Log_Test() {
            Logs logs = new Logs {
                Content = "测试"
            };
            Resolve<ILogsService>().Add(logs);
        }
    }
}