using Xunit;
using Alabo.App.Shop.Activitys.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Shop.Activitys.Domain.Services
{
    public class IActivityServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IActivityService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IActivityService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IActivityService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IActivityService>().GetList();
            var countList = Resolve<IActivityService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}