using System;
using Xunit;
using Alabo.App.Shop.Category.Domain.Entities;
using Alabo.App.Shop.Category.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Shop.Category.Domain.Services
{
    public class ICategoryPropertyServiceTests : CoreTest
    {
        [Fact]
        [TestMethod("AddOrUpdateOrDelete_CategoryProperty_String")]
        [TestIgnore]
        public void AddOrUpdateOrDelete_CategoryProperty_String_test()
        {
            CategoryProperty model = null;
            //var fieldJson = "";
            //var result = Service<ICategoryPropertyService>().AddOrUpdateOrDelete( model, fieldJson);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetCategoryPropertyList_List1")]
        [TestIgnore]
        public void GetCategoryPropertyList_List1_test()
        {
            //var result = Service<ICategoryPropertyService>().GetCategoryPropertyList( null);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetList_Guid_Boolean")]
        public void GetList_Guid_Boolean_test()
        {
            var categoryId = Guid.Empty;
            var isSale = false;
            var result = Resolve<ICategoryPropertyService>().GetList(categoryId, isSale);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_Guid_Boolean")]
        [TestIgnore]
        public void GetSingle_Guid_Boolean_test()
        {
            //var id =Guid.Empty ;
            //var isSale = false;
            //var result = Service<ICategoryPropertyService>().GetSingle( id, isSale);
            //Assert.NotNull(result);
        }

        /*end*/
    }
}