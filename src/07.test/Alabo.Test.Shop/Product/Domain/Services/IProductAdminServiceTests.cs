using Alabo.Industry.Shop.Products.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Shop.Product.Domain.Services
{
    public class IProductAdminServiceTests : CoreTest
    {
        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetViewProductEdit_Int64_Int64")]
        public void GetViewProductEdit_Int64_Int64_test(long productId)
        {
            var product = Resolve<IProductService>().GetRandom(productId);
            var result = Resolve<IProductAdminService>().GetViewProductEdit(productId, product.StoreId);
            Assert.NotNull(result);
            Assert.NotNull(result.Product);
        }

        /*end*/

        [Fact]
        [TestMethod("AddOrUpdate_ViewProductEdit_HttpRequest")]
        [TestIgnore]
        public void AddOrUpdate_ViewProductEdit_HttpRequest_test()
        {
            //ViewProductEdit viewProduct = null;
            //HttpRequest httpRequest = null;
            //var result = Service<IProductAdminService>().AddOrUpdate(viewProduct, httpRequest);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("CheckCategoryHasProduct_Guid")]
        [TestIgnore]
        public void CheckCategoryHasProduct_Guid_test()
        {
            //var categoryId = Guid.Empty;
            //var result = Service<IProductAdminService>().CheckCategoryHasProduct(categoryId);
            //Assert.True(result);
        }

        [Fact]
        [TestMethod("Delete_Int64")]
        public void Delete_Int64_test()
        {
            var id = 0;
            Resolve<IProductAdminService>().Delete(id);
        }

        [Fact]
        [TestMethod("GetImg_Int64")]
        public void GetImg_Int64_test()
        {
            //////var productId = 0;
            //////Service<IProductAdminService>().GetImg( productId);
        }

        [Fact]
        [TestMethod("GetPageView_ViewProductEdit")]
        [TestIgnore]
        public void GetPageView_ViewProductEdit_test()
        {
            //ViewProductEdit view = null;
            //var result = Service<IProductAdminService>().GetPageView(view);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_Int64")]
        public void GetSingle_Int64_test()
        {
            //var id = 0;
            //var result = Service<IProductAdminService>().GetSingle( id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetViewProductList_Object")]
        public void GetViewProductList_Object_test()
        {
            object query = null;
            var result = Resolve<IProductAdminService>().GetViewProductList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetWhiteFoot")]
        public void GetWhiteFoot_test()
        {
            //Service<IProductAdminService>().GetWhiteFoot();
        }
    }
}