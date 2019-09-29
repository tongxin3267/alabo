using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.User.Domain.Services
{
    public class IUserAdminServiceTests : CoreTest
    {
        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("Delete_Int64")]
        [TestIgnore]
        public void Delete_Int64_test(long userId)
        {
            //         var user = Service<IUserService>().GetRandom(userId);
            //         var result = Service<IUserAdminService>().Delete( user.Id);
            //Assert.True(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("DeleteUser_Int64")]
        [TestIgnore]
        public void DeleteUser_Int64_test(long userId)
        {
            //         var user = Service<IUserService>().GetRandom(userId);
            //         var result = Service<IUserAdminService>().DeleteUser( user.Id);
            //Assert.True(result);
        }

        /*end*/

        [Fact]
        [TestMethod("UpdateUser_User")]
        [TestIgnore]
        public void UpdateUser_User_test()
        {
            //User user = null;
            //var result = Service<IUserAdminService>().UpdateUser( user);
            //Assert.True(result);
        }

        [Fact]
        [TestMethod("UpdateUserDetail_UserDetail")]
        [TestIgnore]
        public void UpdateUserDetail_UserDetail_test()
        {
            //UserDetail userDetail = null;
            //var result = Service<IUserAdminService>().UpdateUserDetail( userDetail);
            //Assert.True(result);
        }
    }
}