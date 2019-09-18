using System.Collections.Generic;
using MongoDB.Bson;
using Xunit;
using ZKCloud.Domains.Base.Entities;
using ZKCloud.Domains.Base.Services;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Domains.Services
{
    public class LogsTest : CoreTest
    {
        [Fact]
        public void Add_Test()
        {
            var Logs = new Logs
            {
                UserId = 1,
                Content = "测试日志"
            };
            var result = Resolve<ILogsService>().Add(Logs);
        }

        [Fact]
        public void AddMany_Test()
        {
            var Logs = new Logs
            {
                UserId = 1,
                Content = "测试日志"
            };
            var list = new List<Logs>();
            list.Add(Logs);
            list.Add(Logs);
            Resolve<ILogsService>().AddMany(list);
        }

        [Fact]
        public void FirstOrDefault_Test()
        {
            var result = Resolve<ILogsService>().FirstOrDefault();
            Assert.NotNull(result);
        }

        [Fact]
        public void GetByIdNoTracking_Test()
        {
            var Logs = Resolve<ILogsService>().FirstOrDefault();
            var result = Resolve<ILogsService>().GetByIdNoTracking(Logs.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetList_Test()
        {
            var result = Resolve<ILogsService>().GetList(r => r.UserId == 0);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetSingle_test()
        {
            var result = Resolve<ILogsService>().GetSingle(r => r.Id != ObjectId.Empty);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetSingleOrderBy_Test()
        {
            var result = Resolve<ILogsService>().GetSingleOrderBy(r => r.Id);
            Assert.NotNull(result);
            var last = Resolve<ILogsService>().FirstOrDefault();
            Assert.Equal(result.Id, last.Id);
            Assert.Equal(result, last);
        }

        [Fact]
        public void GetSingleOrderByDescending_Test()
        {
            var result = Resolve<ILogsService>().GetSingleOrderByDescending(r => r.Id);
            Assert.NotNull(result);
            var last = Resolve<ILogsService>().LastOrDefault();
            Assert.Equal(result.Id, last.Id);
            Assert.Equal(result, last);
        }

        [Fact]
        public void LastOrDefault_Test()
        {
            var result = Resolve<ILogsService>().LastOrDefault();
            Assert.NotNull(result);
        }

        [Fact]
        public void Max_Test()
        {
            var result = Resolve<ILogsService>().Max();
            Assert.NotNull(result);
            var last = Resolve<ILogsService>().LastOrDefault();
            Assert.Equal(result.Id, last.Id);
            Assert.Equal(result, last);
        }

        [Fact]
        public void Update_Test()
        {
            var result = Resolve<ILogsService>().FirstOrDefault();
            if (result != null)
            {
                result.Content = "测试更新";
            }

            Resolve<ILogsService>().Update(result);
        }
    }
}