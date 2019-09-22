using Xunit;
using Alabo.App.Shop.Product.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Shop.Product.Domain.Services
{
    public class IProductLineServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IProductLineService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IProductLineService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IProductLineService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IProductLineService>().GetList();
            var countList = Resolve<IProductLineService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}