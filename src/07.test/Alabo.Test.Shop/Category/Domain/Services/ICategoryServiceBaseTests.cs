using Xunit;
using Alabo.App.Shop.Category.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Shop.Category.Domain.Services
{
    public class ICategoryServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<ICategoryService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<ICategoryService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<ICategoryService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<ICategoryService>().GetList();
            var countList = Resolve<ICategoryService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}