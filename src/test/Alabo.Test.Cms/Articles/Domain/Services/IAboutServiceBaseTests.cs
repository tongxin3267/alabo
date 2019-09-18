using Xunit;
using Alabo.App.Cms.Articles.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Cms.Articles.Domain.Services
{
    public class IAboutServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IAboutService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IAboutService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IAboutService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IAboutService>().GetList();
            var countList = Resolve<IAboutService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}