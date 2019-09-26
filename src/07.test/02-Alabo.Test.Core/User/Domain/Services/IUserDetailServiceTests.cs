using Xunit;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.User.Domain.Services {

    public class IUserDetailServiceTests : CoreTest {

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetExtensions_Int64")]
        [TestIgnore]
        public void GetExtensions_Int64_test(long userId) {
            //         var user = Service<IUserService>().GetRandom(userId);
            //         var result = Service<IUserDetailService>().GetExtensions( user.Id);
            //Assert.NotNull(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("QrCore_Int64")]
        [TestIgnore]
        public void QrCore_Int64_test(long userId) {
            //var user = Service<IUserService>().GetRandom(userId);
            //         var result = Service<IUserDetailService>().QrCore(user.Id);
            //         Assert.NotNull(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("CreateCode_User")]
        [TestIgnore]
        public void CreateCode_User_test(long userId) {
            //var user = Service<IUserService>().GetRandom(userId);
            //Service<IUserDetailService>().CreateCode(user);
            //var qrcodePath = FileHelper.QrcodePath+ $"/wwwroot/qrcode/{user.Id}.jpeg";
            //Assert.True(File.Exists(qrcodePath));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetUserOutput_Int64")]
        [TestIgnore]
        public void GetUserOutput_Int64_test(long userId) {
            //         var user = Service<IUserService>().GetRandom(userId);
            //         var result = Service<IUserDetailService>().GetUserOutput( user.Id);
            //Assert.NotNull(result);
        }

        /*end*/

        [Fact]
        [TestMethod("ChangeMobile_ViewChangMobile")]
        [TestIgnore]
        public void ChangeMobile_ViewChangMobile_test() {
            //ViewChangMobile view = null;
            //var result = Service<IUserDetailService>().ChangeMobile( view);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ChangePassword_PasswordInput_Boolean")]
        [TestIgnore]
        public void ChangePassword_PasswordInput_Boolean_test() {
            //PasswordInput passwordInput = null;
            //var checkLastPassword = false;
            //var result = Service<IUserDetailService>().ChangePassword( passwordInput, checkLastPassword);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("FindPassword_FindPasswordInput")]
        [TestIgnore]
        public void FindPassword_FindPasswordInput_test() {
            //FindPasswordInput findPassword = null;
            //var result = Service<IUserDetailService>().FindPassword( findPassword);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("UpdateExtensions_Int64_UserExtensions")]
        public void UpdateExtensions_Int64_UserExtensions_test() {
            //var userId = 0;
            //UserExtensions userExtensions = null;
            //var result = Resolve<IUserDetailService>().UpdateExtensions(userId, userExtensions);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("UpdateOpenId_String_Int64")]
        public void UpdateOpenId_String_Int64_test() {
            var openId = "";
            var userId = 0;
            Resolve<IUserDetailService>().UpdateOpenId(openId, userId);
        }

        [Fact]
        [TestMethod("UpdateSingle_UserDetail")]
        [TestIgnore]
        public void UpdateSingle_UserDetail_test() {
            //UserDetail userDetail = null;
            //var result = Service<IUserDetailService>().UpdateSingle( userDetail);
            //Assert.True(result);
        }
    }
}