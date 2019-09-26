using Alabo.Framework.Themes.Domain.Services;
using Xunit;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Themes.Domain.Services
{
    public class IThemePageServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IThemePageService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IThemePageService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IThemePageService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IThemePageService>().GetList();
            var countList = Resolve<IThemePageService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}