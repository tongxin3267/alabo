using Alabo.Industry.Shop.Products.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Shop.Product.Domain.Services
{
    public class IProductServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IProductService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IProductService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IProductService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IProductService>().GetList();
            var countList = Resolve<IProductService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}