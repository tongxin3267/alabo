using Xunit;
using Alabo.App.Cms.Articles.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Cms.Articles.Domain.Services
{
    public class ISinglePageServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<ISinglePageService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<ISinglePageService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<ISinglePageService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<ISinglePageService>().GetList();
            var countList = Resolve<ISinglePageService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}