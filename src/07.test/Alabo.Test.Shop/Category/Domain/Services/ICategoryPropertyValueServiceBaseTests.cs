using Alabo.Industry.Shop.Categories.Domain.Services;
using Xunit;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Shop.Category.Domain.Services
{
    public class ICategoryPropertyValueServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<ICategoryPropertyValueService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<ICategoryPropertyValueService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<ICategoryPropertyValueService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<ICategoryPropertyValueService>().GetList();
            var countList = Resolve<ICategoryPropertyValueService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}