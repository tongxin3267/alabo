using Alabo.Industry.Shop.Orders.Domain.Enums;
using Alabo.Industry.Shop.Orders.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Shop.Order.Domain.Service
{
    public class IOrderServiceTests : CoreTest
    {
        [Fact]
        [TestMethod("Confirm_ConfirmInput")]
        [TestIgnore]
        public void Confirm_ConfirmInput_test()
        {
            //ConfirmInput parameter = null;
            //var result = Service<IOrderService>().Confirm( parameter);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Delete_Int64")]
        public void Delete_Int64_test()
        {
            var id = 0;
            var result = Resolve<IOrderService>().Delete(id);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetMethodByStatus_OrderStatus")]
        public void GetMethodByStatus_OrderStatus_test()
        {
            var orderStatus = (OrderStatus) 0;
            var result = Resolve<IOrderService>().GetMethodByStatus(orderStatus);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPageList_OrderListInput")]
        [TestIgnore]
        public void GetPageList_OrderListInput_test()
        {
            //OrderListInput orderInput = null;
            //var result = Service<IOrderService>().GetPageList( orderInput);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_Int64_Int64")]
        [TestIgnore]
        public void GetSingle_Int64_Int64_test()
        {
            //var id = 0;
            //var UserdId = 0;
            //var result = Service<IOrderService>().GetSingle( id, UserdId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_Int64")]
        [TestIgnore]
        public void GetSingle_Int64_test()
        {
            //var orderId = 0;
            //var result = Service<IOrderService>().GetSingle( orderId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingleWithProducts_Int64")]
        [TestIgnore]
        public void GetSingleWithProducts_Int64_test()
        {
            //var orderId = 0;
            //var result = Service<IOrderService>().GetSingleWithProducts( orderId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Rate_RateInput")]
        [TestIgnore]
        public void Rate_RateInput_test()
        {
            //RateInput parameter = null;
            //var result = Service<IOrderService>().Rate( parameter);
            //Assert.NotNull(result);
        }

        /*end*/
    }
}