using Xunit;
using Alabo.App.Core.UserType.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.UserType.Domain.Services
{
    public class ICountyServiceTests : CoreTest
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
        [TestMethod("AddOrUpdate_CountyView")]
        [TestIgnore]
        public void AddOrUpdate_CountyView_test()
        {
            //CountyView CountyView = null;
            //var result = Service<ICountyService>().AddOrUpdate( CountyView);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Delete_Int64")]
        public void Delete_Int64_test()
        {
            var id = 0;
            var result = Resolve<ICountyService>().Delete(id);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPageList_Object")]
        public void GetPageList_Object_test()
        {
            object query = null;
            var result = Resolve<ICountyService>().GetPageList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetView_Int64")]
        public void GetView_Int64_test()
        {
            var id = 0;
            var result = Resolve<ICountyService>().GetView(id);
            Assert.NotNull(result);
        }
    }
}