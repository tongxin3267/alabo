using Alabo.Framework.Themes.Domain.Services;
using Xunit;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Themes.Domain.Services
{
    public class IThemeServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IThemeService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IThemeService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IThemeService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IThemeService>().GetList();
            var countList = Resolve<IThemeService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}