using Alabo.Framework.Basic.Address.Domain.Services;
using Alabo.Framework.Basic.Regions.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.Common.Domain.Services
{
    public class IRegionServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IRegionService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IRegionService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IRegionService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IRegionService>().GetList();
            var countList = Resolve<IRegionService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}