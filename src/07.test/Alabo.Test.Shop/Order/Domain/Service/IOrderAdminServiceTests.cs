using Xunit;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Shop.Order.Domain.Service
{
    public class IOrderAdminServiceTests : CoreTest
    {
        [Fact]
        [TestMethod("AddSinglePay_SinglePayInput")]
        [TestIgnore]
        public void AddSinglePay_SinglePayInput_test()
        {
            //SinglePayInput singlePayInput = null;
            //var result = Service<IOrderAdminService>().AddSinglePay( singlePayInput);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAdminPageList_Object")]
        public void GetAdminPageList_Object_test()
        {
            object query = null;
            var result = Resolve<IOrderAdminService>().GetAdminPageList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetShopSalePagedList_Object")]
        public void GetShopSalePagedList_Object_test()
        {
            object query = null;
            var result = Resolve<IOrderAdminService>().GetShopSalePagedList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetViewAdminOrder_Int64_Int64")]
        public void GetViewAdminOrder_Int64_Int64_test()
        {
            var id = 0;
            var userId = 0;
            var result = Resolve<IOrderAdminService>().GetViewAdminOrder(id, userId);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetViewPriceStyleSalePagedList_Object")]
        public void GetViewPriceStyleSalePagedList_Object_test()
        {
            object query = null;
            var result = Resolve<IOrderAdminService>().GetViewPriceStyleSalePagedList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ProductStockUpdate")]
        [TestIgnore]
        public void ProductStockUpdate_test()
        {
            //Service<IOrderAdminService>().ProductStockUpdate();
        }

        [Fact]
        [TestMethod("UpdateUserSaleInfo_Int64")]
        [TestIgnore]
        public void UpdateUserSaleInfo_Int64_test()
        {
            //var orderId = 0;
            //Service<IOrderAdminService>().UpdateUserSaleInfo( orderId);
        }
    }
}