using Xunit;
using Alabo.App.Shop.Store.Domain.Entities.Extensions;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Shop.Store.Domain.Services
{
    public class IStoreServiceTests : CoreTest
    {
        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetUserStore_Int64")]
        [TestIgnore]
        public void GetUserStore_Int64_test(long userId)
        {
            //         var user = Service<IUserService>().GetRandom(userId);
            //         var result = Service<IStoreService>().GetUserStore( user.Id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("AddOrUpdate_ViewStore")]
        [TestIgnore]
        public void AddOrUpdate_ViewStore_test()
        {
            //ViewStore store = null;
            //var result = Service<IStoreService>().AddOrUpdate( store);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPageList_Object")]
        public void GetPageList_Object_test()
        {
            object query = null;
            var result = Resolve<IStoreService>().GetPageList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetStoreExtension_Int64")]
        [TestIgnore]
        public void GetStoreExtension_Int64_test()
        {
            //var storeId = 0;
            //var result = Service<IStoreService>().GetStoreExtension( storeId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetStoreItemListFromCache")]
        public void GetStoreItemListFromCache_test()
        {
            var result = Resolve<IStoreService>().GetStoreItemListFromCache();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetStoreProductSku_StoreProductSkuDtos")]
        [TestIgnore]
        public void GetStoreProductSku_StoreProductSkuDtos_test()
        {
            //StoreProductSkuDtos storeProductSkuDtos = null;
            //var result = Service<IStoreService>().GetStoreProductSku( storeProductSkuDtos);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetView_Int64")]
        public void GetView_Int64_test()
        {
            var id = 0;
            var result = Resolve<IStoreService>().GetView(id);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetViewStore_Int64")]
        public void GetViewStore_Int64_test()
        {
            var Id = 0;
            var result = Resolve<IStoreService>().GetViewStore(Id);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetViewStorePageList_PagedInputDto")]
        [TestIgnore]
        public void GetViewStorePageList_PagedInputDto_test()
        {
            //PagedInputDto dto = null;
            //var result = Service<IStoreService>().GetViewStorePageList( dto);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("PlanformStore")]
        [TestIgnore]
        public void PlanformStore_test()
        {
            //var result = Service<IStoreService>().PlanformStore();
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("UpdateExtensions_Int64_StoreExtension")]
        public void UpdateExtensions_Int64_StoreExtension_test()
        {
            var stroeId = 0;
            StoreExtension storeExtension = null;
            var result = Resolve<IStoreService>().UpdateExtensions(stroeId, storeExtension);
            Assert.NotNull(result);
        }

        /*end*/
    }
}