using Xunit;
using Alabo.Domains.Base.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Common.Domain.Services
{
    public class ILogsServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<ILogsService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<ILogsService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<ILogsService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<ILogsService>().GetList();
            var countList = Resolve<ILogsService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}