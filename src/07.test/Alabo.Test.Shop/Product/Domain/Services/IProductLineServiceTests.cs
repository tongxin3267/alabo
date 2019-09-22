using Xunit;
using Alabo.App.Shop.Product.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Shop.Product.Domain.Services
{
    public class IProductLineServiceTests : CoreTest
    {
        /*end*/

        [Fact]
        [TestMethod("Delete_Int64")]
        public void Delete_Int64_test()
        {
            //var id = 0;
            //var result = Service<IProductLineService>().Delete( id);
            //Assert.True(result);
        }

        [Fact]
        [TestMethod("GetLineModels_Int64_Nullable_System_Int32_Nullable_System_Int32")]
        [TestIgnore]
        public void GetLineModels_Int64_Nullable_System_Int32_Nullable_System_Int32_test()
        {
            //var id = 0;
            //var pageSize = 0;
            //var pageIndex = 0;
            //var result = Service<IProductLineService>().GetLineModels(id, pageSize, pageIndex);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetLineModels_String_Nullable_System_Int32_Nullable_System_Int32")]
        public void GetLineModels_String_Nullable_System_Int32_Nullable_System_Int32_test()
        {
            var pname = "";
            var pageSize = 0;
            var pageIndex = 0;
            var result = Resolve<IProductLineService>().GetLineModels(pname, pageSize, pageIndex);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPageList_Object")]
        public void GetPageList_Object_test()
        {
            object query = null;
            var result = Resolve<IProductLineService>().GetPageList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetProductIds_List1")]
        [TestIgnore]
        public void GetProductIds_List1_test()
        {
            //var result = Service<IProductLineService>().GetProductIds(null);
            //Assert.NotNull(result);
        }
    }
}