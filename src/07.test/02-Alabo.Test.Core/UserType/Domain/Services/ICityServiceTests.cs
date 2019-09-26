using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.UserType.Domain.Services
{
    public class ICityServiceTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            //var model = Service<IAdminService>().GetRandom(entityId);
            //if (model != null)
            //{
            //    var newModel = Service<IAdminService>().GetSingleFromCache(model.Id);
            //    Assert.NotNull(newModel);
            //    Assert.Equal(newModel.Id, model.Id);
            //}
        } /*end*/

        [Fact]
        [TestMethod("AddOrUpdate_CityView")]
        [TestIgnore]
        public void AddOrUpdate_CityView_test()
        {
            //CityView cityView = null;
            //var result = Service<ICityService>().AddOrUpdate( cityView);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Delete_Int64")]
        public void Delete_Int64_test()
        {
            var id = 0;
            var result = Resolve<ICityService>().Delete(id);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetCityUserType_Int64")]
        [TestIgnore]
        public void GetCityUserType_Int64_test()
        {
            //var regionId = 0;
            //var result = Service<ICityService>().GetCityUserType( regionId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPageList_Object")]
        public void GetPageList_Object_test()
        {
            object query = null;
            var result = Resolve<ICityService>().GetPageList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetView_Int64")]
        public void GetView_Int64_test()
        {
            var id = 0;
            var result = Resolve<ICityService>().GetView(id);
            Assert.NotNull(result);
        }
    }
}