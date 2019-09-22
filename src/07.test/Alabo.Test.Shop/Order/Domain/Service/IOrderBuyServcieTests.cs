using Xunit;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Shop.Order.Domain.Service
{
    public class IOrderBuyServcieTests : CoreTest
    {
        [Fact]
        [TestMethod("Buy_BuyInput")]
        [TestIgnore]
        public void Buy_BuyInput_test()
        {
            //BuyInput orderBuyInput = null;
            //var result = Service<IOrderBuyServcie>().Buy(orderBuyInput);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("BuyInfo_BuyInfoInput")]
        public void BuyInfo_BuyInfoInput_test()
        {
            BuyInfoInput buyInfoInput = null;
            var result = Resolve<IOrderBuyServcie>().BuyInfo(buyInfoInput);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("CountPrice_StoreProductSku_UserOrderInput_IEnumerable1_IList1")]
        [TestIgnore]
        public void CountPrice_StoreProductSku_UserOrderInput_IEnumerable1_IList1_test()
        {
            //StoreProductSku storeProductSku = null;
            //UserOrderInput userOrderInput = null;
            //var result = Service<IOrderBuyServcie>().CountPrice(ref storeProductSku, userOrderInput, null, null);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPrice_UserOrderInput")]
        [TestIgnore]
        public void GetPrice_UserOrderInput_test()
        {
            //UserOrderInput userOrderInput = null;
            //var result = Service<IOrderBuyServcie>().GetPrice(userOrderInput);
            //Assert.NotNull(result);
        }
    }
}