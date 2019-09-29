using Alabo.Industry.Shop.Products.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Shop.Product.Domain.Services
{
    public class IProductDetailServiceTests : CoreTest
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
        } /*end*/
    }
}