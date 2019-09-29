using Alabo.Industry.Shop.Activitys.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Shop.Activitys.Domain.Services
{
    public class IActivityAdminServiceTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IActivityService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IActivityService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        } /*end*/

        [Fact]
        [TestMethod("AddOrUpdate_ViewActivityModel")]
        [TestIgnore]
        public void AddOrUpdate_ViewActivityModel_test()
        {
            //ViewActivityModel model = null;
            //var result = Service<IActivityAdminService>().AddOrUpdate( model);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Delete_Int64")]
        [TestIgnore]
        public void Delete_Int64_test()
        {
            //var id = 0;
            //var result = Service<IActivityAdminService>().Delete( id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetActivityModuleAttribute_String")]
        [TestIgnore]
        public void GetActivityModuleAttribute_String_test()
        {
            //var key = "";
            //var result = Service<IActivityAdminService>().GetActivityModuleAttribute( key);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAllActivityAttributes")]
        public void GetAllActivityAttributes_test()
        {
            var result = Resolve<IActivityAdminService>().GetAllActivityAttributes();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAllTypes")]
        public void GetAllTypes_test()
        {
            var result = Resolve<IActivityAdminService>().GetAllTypes();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetOrderPageList_Object")]
        [TestIgnore]
        public void GetOrderPageList_Object_test()
        {
            //Object query = null;
            //var result = Service<IActivityAdminService>().GetOrderPageList( query);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPageList_Object")]
        [TestIgnore]
        public void GetPageList_Object_test()
        {
            //Object query = null;
            //var result = Service<IActivityAdminService>().GetPageList( query);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPlatformAuthorityConfig_String")]
        [TestIgnore]
        public void GetPlatformAuthorityConfig_String_test()
        {
            //var key = "";
            //var result = Service<IActivityAdminService>().GetPlatformAuthorityConfig( key);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetProductPageList_Object")]
        [TestIgnore]
        public void GetProductPageList_Object_test()
        {
            //Object query = null;
            //var result = Service<IActivityAdminService>().GetProductPageList( query);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetRecordPageList_Object")]
        [TestIgnore]
        public void GetRecordPageList_Object_test()
        {
            //Object query = null;
            //var result = Service<IActivityAdminService>().GetRecordPageList( query);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetViewActivityModel_ActivityEditInput")]
        [TestIgnore]
        public void GetViewActivityModel_ActivityEditInput_test()
        {
            //ActivityEditInput editInput = null;
            //var result = Service<IActivityAdminService>().GetViewActivityModel( editInput);
            //Assert.NotNull(result);
        }
    }
}