using Alabo.Industry.Shop.Products.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Shop.Product.Domain.Services
{
    public class IProductSkuServiceTests : CoreTest
    {
        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetStoreMoneyBuySkus_IEnumerable1_Int64")]
        [TestIgnore]
        public void GetStoreMoneyBuySkus_IEnumerable1_Int64_test(long userId)
        {
            //         var user = Service<IUserService>().GetRandom(userId);
            //         var result = Service<IProductSkuService>().GetStoreMoneyBuySkus( null, user.Id);
            //Assert.NotNull(result);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(43)]
        [InlineData(54)]
        [InlineData(51)]
        [TestMethod("AutoUpdateSkuPrice_Int64")]
        public void AutoUpdateSkuPrice_Int64_test(long productId)
        {
            var product = Resolve<IProductService>().GetRandom(productId);
            if (product != null) Resolve<IProductSkuService>().AutoUpdateSkuPrice(product.Id);
        }

        [Fact]
        [TestMethod("AddUpdateOrDelete_Product_String")]
        [TestIgnore]
        public void AddUpdateOrDelete_Product_String_test()
        {
            //Product product = null;
            //var skuJson = "";
            //var result = Service<IProductSkuService>().AddUpdateOrDelete( product, skuJson);
            //Assert.NotNull(result);
        }

        /*end*/
    }
}