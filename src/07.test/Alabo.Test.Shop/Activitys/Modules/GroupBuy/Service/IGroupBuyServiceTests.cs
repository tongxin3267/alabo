using System.Linq;
using Alabo.Framework.Core.WebApis.Dtos;
using Alabo.Industry.Shop.Activitys.Domain.Services;
using Alabo.Industry.Shop.Activitys.Modules.GroupBuy.Model;
using Alabo.Industry.Shop.Activitys.Modules.GroupBuy.Service;
using Alabo.Industry.Shop.Orders.Domain.Services;
using Alabo.Industry.Shop.Products.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Shop.Activitys.Modules.GroupBuy.Service
{
    public class IGroupBuyServiceTests : CoreTest
    {
        //[Theory]
        [InlineData(1)]
        [InlineData(20)]
        [InlineData(-1)]
        [TestMethod("GetGroupBuyProductRecords_Int64")]
        public void GetGroupBuyProductRecords_Int64_test1(long entitId)
        {
            var product = Resolve<IProductService>().GetRandom(entitId);
            if (product != null)
            {
                var result = Resolve<GroupBuyService>().GetGroupBuyProductRecords(product.Id);
                Assert.NotNull(result);
            }
        }

        //[Theory]
        [InlineData(1)]
        [InlineData(20)]
        [InlineData(-1)]
        [TestMethod("GetGrouyBuyUserByOrderId_Int64")]
        public void GetGrouyBuyUserByOrderId_Int64_test1(long entitId)
        {
            var order = Resolve<IOrderService>().GetRandom(entitId);
            if (order != null)
            {
                var result = Resolve<GroupBuyService>().GetGrouyBuyUserByOrderId(order.Id);
                Assert.NotNull(result);
            }
        }

        [Fact]
        [TestMethod("GetAllProductIds")]
        public void GetAllProductIds_test()
        {
            var result = Resolve<IGroupBuyService>().GetAllProductIds();
            Assert.NotNull(result);
            var activitys = Resolve<IActivityService>().GetList(r => r.Key == typeof(GroupBuyActivity).FullName);

            Assert.Equal(result.Count, activitys.Count());
        }

        [Fact]
        [TestMethod("GetGroupBuyProductRecords_Int64")]
        public void GetGroupBuyProductRecords_Int64_test()
        {
            var productId = 0;
            var result = Resolve<IGroupBuyService>().GetGroupBuyProductRecords(productId);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetGrouyBuyUserByOrderId_Int64")]
        public void GetGrouyBuyUserByOrderId_Int64_test()
        {
            var orderId = 0;
            var result = Resolve<IGroupBuyService>().GetGrouyBuyUserByOrderId(orderId);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetProductItems_ApiBaseInput")]
        public void GetProductItems_ApiBaseInput_test()
        {
            var parameter = new ApiBaseInput();
            var result = Resolve<IGroupBuyService>().GetProductItems(parameter);
            Assert.NotNull(result);
            var activitys = Resolve<IActivityService>().GetList(r => r.Key == typeof(GroupBuyActivity).FullName);

            if (activitys.Count() > 0) Assert.True(result.ProductItems.Count() > 0);
        }
    }
}