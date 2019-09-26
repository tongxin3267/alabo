using System.Collections.Generic;
using Xunit;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Core.User.ViewModels;
using Alabo.Extensions;
using Alabo.Randoms;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Alabo.Users.Entities;

namespace Alabo.Test.Core.User.Domain.Services {

    public class IUserServiceTests : CoreTest {

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        public void GetSingle_UserId(long userId) {
            var user = Resolve<IUserService>().GetRandom(userId);
            var result = Resolve<IUserService>().GetSingle(user.Id);
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        public void GetSingleTest_UserName(long userId) {
            var user = Resolve<IUserService>().GetRandom(userId);
            var parameter = user.UserName;
            var result = Resolve<IUserService>().GetSingle(parameter);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetSingle_Int64")]
        public void GetSingle_Int64_test(long userId) {
            var user = Resolve<IUserService>().GetRandom(userId);
            var result = Resolve<IUserService>().GetSingle(user.Id);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("UserTeam_Int64")]
        public void UserTeam_Int64_test(long userId) {
            var user = Resolve<IUserService>().GetRandom(userId);
            var result = Resolve<IUserService>().UserTeam(user.Id);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetNomarlUser_Int64")]
        public void GetNomarlUser_Int64_test(long userId) {
            var user = Resolve<IUserService>().GetRandom(userId);
            //var userId = 0;
            var result = Resolve<IUserService>().GetNomarlUser(user.Id);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetSingle_String")]
        public void GetSingle_String_test(long userId) {
            var user = Resolve<IUserService>().GetRandom(userId);
            var userName = user.UserName;
            var result = Resolve<IUserService>().GetSingle(userName);
            Assert.NotNull(result);
            Assert.Equal(result.UserName, user.UserName);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetSingleByMobile_String")]
        public void GetSingleByMobile_String_test(long userId) {
            var user = Resolve<IUserService>().GetRandom(userId);
            var mobile = user.Mobile;
            var result = Resolve<IUserService>().GetSingleByMobile(mobile);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetSingleByMail_String")]
        public void GetSingleByMail_String_test(long userId) {
            var user = Resolve<IUserService>().GetRandom(userId);
            var mail = user.Email;
            var result = Resolve<IUserService>().GetSingleByMail(mail);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetUserDetail_Int64")]
        public void GetUserDetail_Int64_test(long userId) {
            var user = Resolve<IUserService>().GetRandom(userId);
            var result = Resolve<IUserService>().GetUserDetail(user.Id);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetUserDetail_String")]
        public void GetUserDetail_String_test(long userId) {
            var user = Resolve<IUserService>().GetRandom(userId);
            var UserName = user.UserName;
            var result = Resolve<IUserService>().GetUserDetail(UserName);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("UpdateUser_User")]
        public void UpdateUser_User_test(long userId) {
            var user = Resolve<IUserService>().GetRandom(userId);
            var model = user;
            var result = Resolve<IUserService>().UpdateUser(model);
            Assert.True(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("ExistsUserName_String")]
        public void ExistsUserName_String_test(long userId) {
            var user = Resolve<IUserService>().GetRandom(userId);
            var name = user.UserName;
            var result = Resolve<IUserService>().ExistsUserName(name);
            Assert.True(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("ExistsMobile_String")]
        public void ExistsMobile_String_test(long userId) {
            //var user = Service<IUserService>().GetRandom(userId);
            //var mobile = user.Mobile;
            //var result = Service<IUserService>().ExistsMobile(mobile);
            //Assert.True(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetTeamCenterService_Int64_Int64")]
        public void GetTeamCenterService_Int64_Int64_test(long userId) {
            userId = Resolve<IUserService>().GetRandom(userId).Id;
            var grade = 0;
            var result = Resolve<IUserService>().GetTeamCenterService(userId, grade);
            Assert.True(result > 0);
        }

        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId) {
            var model = Resolve<IUserService>().GetRandom(entityId);
            if (model != null) {
                var newModel = Resolve<IUserService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        } /*end*/

        [Theory]
        [InlineData(-1)]
        [TestMethod("ExistsMail_String")]
        public void ExistsMail_String_test(long entityId) {
            var model = Resolve<IUserService>().GetRandom(entityId);
            if (model != null) {
                var mail = model.Email;
                var result = Resolve<IUserService>().ExistsMail(mail);
                Assert.True(result);
            }
        }

        [Fact]
        [TestMethod("AddUser_User")]
        public void AddUser_User_test() {
            var user = Resolve<IUserService>().GetRandom(10);
            if (user.UserName.Length > 10) {
                user.UserName = user.UserName.Substring(2, 5);
            }

            user.UserName = user.UserName + RandomHelper.Number(10, 90);
            user.Detail = new UserDetail();
            user.Detail.Password = "123456".ToMd5HashString();
            user.Detail.PayPassword = "123456".ToMd5HashString();
            var result = Resolve<IUserService>().AddUser(user);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("DeleteUserCache_Int64_String")]
        public void DeleteUserCache_Int64_String_test() {
            var userId = 0;
            var UserName = "";
            Resolve<IUserService>().DeleteUserCache(userId, UserName);
        }

        [Fact]
        [TestMethod("DeleteUserCache_Int64")]
        public void DeleteUserCache_Int64_test() {
            var userId = 0;
            Resolve<IUserService>().DeleteUserCache(userId);
        }

        [Fact]
        [TestMethod("GetHomeUserStyle_User")]
        public void GetHomeUserStyle_User_test() {
            Users.Entities.User user = null;
            var result = Resolve<IUserService>().GetHomeUserStyle(user);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetList_IList1")]
        public void GetList_IList1_test() {
            var result = Resolve<IUserService>().GetList(new List<long>());
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetRecommondUserPage_Object")]
        public void GetRecommondUserPage_Object_test() {
            //Object query = null;
            //var result = Service<IUserService>().GetRecommondUserPage(query);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserDetailByOpenId_String")]
        [TestIgnore]
        public void GetUserDetailByOpenId_String_test() {
            //            var openId = "";
            //            var result = Service<IUserService>().GetUserDetailByOpenId(openId);
            //            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserStyle_User")]
        public void GetUserStyle_User_test() {
            Users.Entities.User user = null;
            var result = Resolve<IUserService>().GetUserStyle(user);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetViewUserPageList_Object")]
        public void GetViewUserPageList_Object_test() {
            object query = null;
            var result = Resolve<IUserService>().GetViewUserPageList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetViewUserPageList_UserInput")]
        [TestIgnore]
        public void GetViewUserPageList_UserInput_test() {
            //            UserInput userInput = null;
            //            var result = Service<IUserService>().GetViewUserPageList(userInput);
            //            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetViewUserPageList_ViewUser")]
        public void GetViewUserPageList_ViewUser_test() {
            ViewUser viewUser = null;
            var result = Resolve<IUserService>().GetViewUserPageList(viewUser);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("IsAdmin_Int64")]
        public void IsAdmin_Int64_test() {
            var userId = 0;
            var result = Resolve<IUserService>().IsAdmin(userId);
            Assert.False(result);
        }

        [Fact]
        [TestMethod("MaxUserId")]
        public void MaxUserId_test() {
            var result = Resolve<IUserService>().MaxUserId();
            Assert.True(result > 0);
        }

        [Fact]
        [TestMethod("PlanformUser")]
        public void PlanformUser_test() {
            var result = Resolve<IUserService>().PlanformUser();
            Assert.NotNull(result);
        }

        [Fact]
        public void UpdateUserTest() {
            //var parameter = new Alabo.App.Core.User.Domain.Entities.User();
            //var result = Service<IUserService>().GetSingle(parameter);
            //Assert.NotNull(result);
        }
    }
}