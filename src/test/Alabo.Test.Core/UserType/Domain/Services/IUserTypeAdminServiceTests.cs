using Xunit;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.UserType.Domain.Services
{
    public class IUserTypeAdminServiceTests : CoreTest
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
        [TestMethod("AddOrUpdate_ViewUserTypeEdit_HttpRequest")]
        [TestIgnore]
        public void AddOrUpdate_ViewUserTypeEdit_HttpRequest_test()
        {
            //ViewUserTypeEdit viewUserType = null;
            //HttpRequest httpRequest = null;
            //var result = Service<IUserTypeAdminService>().AddOrUpdate( viewUserType, httpRequest);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserTypeEdit_Guid_Int64")]
        [TestIgnore]
        public void GetUserTypeEdit_Guid_Int64_test()
        {
            //var userTypeId =Guid.Empty ;
            //var id = 0;
            //var result = Service<IUserTypeAdminService>().GetUserTypeEdit( userTypeId, id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetViewUserTypePageList_UserTypeInput")]
        [TestIgnore]
        public void GetViewUserTypePageList_UserTypeInput_test()
        {
            //UserTypeInput userTypeInput = null;
            //var result = Service<IUserTypeAdminService>().GetViewUserTypePageList( userTypeInput);
            //Assert.NotNull(result);
        }
    }
}