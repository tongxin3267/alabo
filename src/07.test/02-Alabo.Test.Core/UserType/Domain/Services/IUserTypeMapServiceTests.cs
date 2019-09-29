using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.UserType.Domain.Services
{
    public class IUserTypeMapServiceTests : CoreTest
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
        }

        [Fact]
        [TestMethod("GetNearestMap_Int64_Guid")]
        public void GetNearestMap_Int64_Guid_test()
        {
            //var userId = 0;
            //var userTypeId = Guid.Empty;
            //var result = Service<IUserTypeMapService>().GetNearestMap(userId, userTypeId);
            //Assert.NotNull(result);
        }

        /*end*/

        [Fact]
        [TestMethod("GetParentMap_UserType")]
        public void GetParentMap_UserType_test()
        {
            //App.Core.UserType.Domain.Entities.UserType userType = null;
            //var result = Service<IUserTypeMapService>().GetParentMap(userType);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserType_Int64_Guid")]
        public void GetUserType_Int64_Guid_test()
        {
            //var userId = 0;
            //var userTypeId = Guid.Empty;
            //var result = Service<IUserTypeMapService>().GetUserType(userId, userTypeId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("SetTypeUser_Int64_Guid")]
        public void SetTypeUser_Int64_Guid_test()
        {
            //var userId = 0;
            //var userTypeId = Guid.Empty;
            //Service<IUserTypeMapService>().SetTypeUser(userId, userTypeId);
        }

        [Fact]
        [TestMethod("UpdateAfterChangeMap_UserType")]
        public void UpdateAfterChangeMap_UserType_test()
        {
            //App.Core.UserType.Domain.Entities.UserType userType = null;
            //var result = Service<IUserTypeMapService>().UpdateAfterChangeMap(userType);
            //Assert.NotNull(result);
        }
    }
}