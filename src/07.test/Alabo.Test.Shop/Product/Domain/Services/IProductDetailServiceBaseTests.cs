using Alabo.Industry.Shop.Products.Domain.Services;
using Xunit;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Shop.Product.Domain.Services
{
    public class IProductDetailServiceBaseTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IProductDetailService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IProductDetailService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Fact]
        [TestMethod("Count_Expected_Test")]
        public void Count_ExpectedBehavior()
        {
            var count = Resolve<IProductDetailService>().Count();
            Assert.True(count >= 0);

            var list = Resolve<IProductDetailService>().GetList();
            var countList = Resolve<IProductDetailService>().Count();
            Assert.Equal(count, countList);
        } /*end*/
    }
}