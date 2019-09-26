using Alabo.Industry.Shop.OrderDeliveries.Domain.Services;
using Xunit;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Shop.Order.Domain.Service
{
    public class IOrderDeliveryServiceTests : CoreTest
    {
        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetDeliverUser_Int64")]
        [TestIgnore]
        public void GetDeliverUser_Int64_test(long userId)
        {
            //var user = Service<IUserService>().GetRandom(userId);
            //var result = Service<IOrderDeliveryService>().GetDeliverUser(user.Id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetOrderDeliveries_Int64")]
        public void GetOrderDeliveries_Int64_test()
        {
            var orderId = 0;
            var result = Resolve<IOrderDeliveryService>().GetOrderDeliveries(orderId);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPageList_Object")]
        public void GetPageList_Object_test()
        {
            object query = null;
            var result = Resolve<IOrderDeliveryService>().GetPageList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetViewOrderDeliveryEdit_Int64")]
        public void GetViewOrderDeliveryEdit_Int64_test()
        {
            var id = 0;
            var result = Resolve<IOrderDeliveryService>().GetViewOrderDeliveryEdit(id);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("OfflineDeliver_OfflineDeliverDtos")]
        public void OfflineDeliver_OfflineDeliverDtos_test()
        {
            //OfflineDeliverDtos offlineDeliverDtos = null;
            //var result = Service<IOrderDeliveryService>().OfflineDeliver(offlineDeliverDtos);
            //Assert.NotNull(result);
        }
    }
}