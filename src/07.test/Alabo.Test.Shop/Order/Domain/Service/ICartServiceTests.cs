using Alabo.Industry.Shop.Carts.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Shop.Order.Domain.Service
{
    public class ICartServiceTests : CoreTest
    {
        [Fact]
        [TestMethod("AddCart_OrderProductInput")]
        [TestIgnore]
        public void AddCart_OrderProductInput_test()
        {
            //OrderProductInput orderProductInput = null;
            //var result = Service<ICartService>().AddCart( orderProductInput);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetCart_Int64")]
        public void GetCart_Int64_test()
        {
            var userId = 0;
            var result = Resolve<ICartService>().GetCart(userId);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("RemoveCart_OrderProductInput")]
        [TestIgnore]
        public void RemoveCart_OrderProductInput_test()
        {
            //OrderProductInput orderProductInput = null;
            //var result = Service<ICartService>().RemoveCart( orderProductInput);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("UpdateCart_OrderProductInput")]
        [TestIgnore]
        public void UpdateCart_OrderProductInput_test()
        {
            //OrderProductInput orderProductInput = null;
            //var result = Service<ICartService>().UpdateCart( orderProductInput);
            //Assert.NotNull(result);
        }
    }
}