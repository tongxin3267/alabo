using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.UserType.Domain.Services
{
    public class IServiceCenterServiceTests : CoreTest
    {
        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("UserUpgradServiceCenter_Int64_Boolean")]
        [TestIgnore]
        public void UserUpgradServiceCenter_Int64_Boolean_test(long userId)
        {
            //         var user = Service<IUserService>().GetRandom(userId);
            //         var isSelf = false;
            //Service<IServiceCenterService>().UserUpgradServiceCenter( userId, isSelf);
        }

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
        [TestMethod("AddOrUpdate_ServiceCenterView")]
        [TestIgnore]
        public void AddOrUpdate_ServiceCenterView_test()
        {
            //ServiceCenterView serviceCenterView = null;
            //var result = Service<IServiceCenterService>().AddOrUpdate( serviceCenterView);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Delete_Int64")]
        public void Delete_Int64_test()
        {
            var id = 0;
            var result = Resolve<IServiceCenterService>().Delete(id);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPageList_Object")]
        public void GetPageList_Object_test()
        {
            object query = null;
            var result = Resolve<IServiceCenterService>().GetPageList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetServiceParentUser_Int64")]
        public void GetServiceParentUser_Int64_test()
        {
            var parentUserId = 0;
            var result = Resolve<IServiceCenterService>().GetServiceParentUser(parentUserId);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetServiceUser_Int64")]
        public void GetServiceUser_Int64_test()
        {
            var userId = 0;
            var result = Resolve<IServiceCenterService>().GetServiceUser(userId);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetView_Int64")]
        public void GetView_Int64_test()
        {
            var id = 0;
            var result = Resolve<IServiceCenterService>().GetView(id);
            Assert.NotNull(result);
        }
    }
}