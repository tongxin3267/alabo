using Microsoft.EntityFrameworkCore.Internal;
using Xunit;
using ZKCloud.App.Core.Finance.Domain.Repositories;
using ZKCloud.App.Core.Finance.Domain.Services;
using ZKCloud.Cache;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Domains.Services
{
    public class DomainServicesTest : CoreTest
    {
        [Fact]
        public void GetLst_Test()
        {
            var billList = Resolve<IBillService>().GetList();
            Assert.NotNull(billList);
            Assert.True(billList.Any());
        }

        [Fact]
        public void ObjectCache_Test()
        {
            var cache = Resolve<IObjectCache>();
            Assert.NotNull(cache);
        }

        [Fact]
        public void TestRepositoryTest()
        {
            var userRepository = Resolve<IBillRepository>();
            Assert.NotNull(userRepository);
        }

        [Fact]
        public void TestUserService()
        {
            var userService = Resolve<IBillService>();
            Assert.NotNull(userService);
        }
    }
}