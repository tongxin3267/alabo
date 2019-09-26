using System;
using Alabo.Industry.Shop.Products.Domain.Services;
using Xunit;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Shop.Product.Domain.Services
{
    public class IProductServiceTests : CoreTest
    {
        /*end*/

        [Fact]
        [TestMethod("GetDisplayPrice_Decimal_Guid_Decimal")]
        public void GetDisplayPrice_Decimal_Guid_Decimal_test()
        {
            var price = 0;
            var priceStyleId = Guid.Empty;
            var productMinCashRate = 0;
            var result = Resolve<IProductService>().GetDisplayPrice(price, priceStyleId, productMinCashRate);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetProductClassList")]
        public void GetProductClassList_test()
        {
            var result = Resolve<IProductService>().GetProductClassList();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetProductItems_ProductApiInput")]
        [TestIgnore]
        public void GetProductItems_ProductApiInput_test()
        {
            //ProductApiInput productApiInput = null;
            //var result = Service<IProductService>().GetProductItems( productApiInput);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetShow_Int64")]
        [TestIgnore]
        public void GetShow_Int64_test()
        {
            //var id = 0;
            //var result = Service<IProductService>().GetShow( id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Show_Int64")]
        public void Show_Int64_test()
        {
            //var id = 0;
            //var result = Resolve<IProductService>().Show(id);
            //Assert.NotNull(result);
        }
    }
}