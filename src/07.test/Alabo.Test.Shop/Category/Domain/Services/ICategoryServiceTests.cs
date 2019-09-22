using System;
using Xunit;
using Alabo.App.Shop.Category.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Shop.Category.Domain.Services
{
    public class ICategoryServiceTests : CoreTest
    {
        [Fact]
        [TestMethod("AddOrUpdateOrDeleteProductCategoryData_Product_HttpRequest")]
        [TestIgnore]
        public void AddOrUpdateOrDeleteProductCategoryData_Product_HttpRequest_test()
        {
            //Product product = null;
            //HttpRequest request = null;
            //var result = Service<ICategoryService>().AddOrUpdateOrDeleteProductCategoryData( product, request);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Delete_Guid")]
        public void Delete_Guid_test()
        {
            var id = Guid.Empty;
            var result = Resolve<ICategoryService>().Delete(id);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_Guid")]
        [TestIgnore]
        public void GetSingle_Guid_test()
        {
            //var guid =Guid.Empty ;
            //var result = Service<ICategoryService>().GetSingle( guid);
            //Assert.NotNull(result);
        }

        /*end*/
    }
}