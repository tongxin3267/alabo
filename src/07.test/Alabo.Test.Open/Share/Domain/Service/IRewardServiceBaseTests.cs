using Xunit;
using Alabo.App.Open.Share.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Open.Share.Domain.Service
{
    public class IRewardServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IRewardService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IRewardService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IRewardService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IRewardService>().GetList();
            var countList = Resolve<IRewardService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}