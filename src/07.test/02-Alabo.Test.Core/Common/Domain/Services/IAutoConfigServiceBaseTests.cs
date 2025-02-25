using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.Common.Domain.Services
{
    public class IAutoConfigServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IAutoConfigService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IAutoConfigService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IAutoConfigService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IAutoConfigService>().GetList();
            var countList = Resolve<IAutoConfigService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}