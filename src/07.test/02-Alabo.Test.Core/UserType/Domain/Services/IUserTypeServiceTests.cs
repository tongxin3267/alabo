using System;
using Xunit;
using Alabo.App.Core.UserType.Domain.Services;
using Alabo.Extensions;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.UserType.Domain.Services
{
    public class IUserTypeServiceTests : CoreTest
    {
        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("UserAllGradeId_Int64")]
        [TestIgnore]
        public void UserAllGradeId_Int64_test(long userId)
        {
            //var user = Service<IUserService>().GetRandom(userId);
            //var result = Service<IUserTypeService>().UserAllGradeId(user.Id);
            //Assert.NotNull(result);
        }

        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IUserTypeService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IUserTypeService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        } /*end*/

        [Fact]
        [TestMethod("GetAllTypes")]
        public void GetAllTypes_test()
        {
            var result = Resolve<IUserTypeService>().GetAllTypes();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAllUserType_Int64")]
        public void GetAllUserType_Int64_test()
        {
            var userId = 0;
            var result = Resolve<IUserTypeService>().GetAllUserType(userId);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSideBarByKey_Guid")]
        public void GetSideBarByKey_Guid_test()
        {
            var userTypeId = Guid.Empty;
            var result = Resolve<IUserTypeService>().GetSideBarByKey(userTypeId);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_Guid_Int64")]
        [TestIgnore]
        public void GetSingle_Guid_Int64_test()
        {
            //var userTypeId = Guid.Empty;
            //var entityId = 0;
            //var result = Service<IUserTypeService>().GetSingle(userTypeId, entityId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_Int64_Guid")]
        [TestIgnore]
        public void GetSingle_Int64_Guid_test()
        {
            //var userId = 0;
            //var userTypeId = Guid.Empty;
            //var result = Service<IUserTypeService>().GetSingle(userId, userTypeId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_Int64")]
        [TestIgnore]
        public void GetSingle_Int64_test()
        {
            //var id = 0;
            //var result = Service<IUserTypeService>().GetSingle(id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_Int64_UserTypeEnum")]
        [TestIgnore]
        public void GetSingle_Int64_UserTypeEnum_test()
        {
            //var userId = 0;
            //var userTypeEnum = (Alabo.Framework.Core.Enums.Enum.UserTypeEnum)0;
            //var result = Service<IUserTypeService>().GetSingle(userId, userTypeEnum);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingleDetail_Int64")]
        [TestIgnore]
        public void GetSingleDetail_Int64_test()
        {
            //var id = 0;
            //var result = Service<IUserTypeService>().GetSingleDetail(id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserTypeIdByGradeKey_String")]
        public void GetUserTypeIdByGradeKey_String_test()
        {
            var gradeKey = "";
            var result = Resolve<IUserTypeService>().GetUserTypeIdByGradeKey(gradeKey);
            Assert.True(result.IsGuidNullOrEmpty());
        }

        [Fact]
        [TestMethod("GetUserTypeKey_Guid")]
        [TestIgnore]
        public void GetUserTypeKey_Guid_test()
        {
            //var userTypeId = Guid.Empty;
            //var result = Service<IUserTypeService>().GetUserTypeKey(userTypeId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserTypeUser_Guid_Int64")]
        [TestIgnore]
        public void GetUserTypeUser_Guid_Int64_test()
        {
            //var userTypeId = Guid.Empty;
            //var eneityId = 0;
            //var result = Service<IUserTypeService>().GetUserTypeUser(userTypeId, eneityId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("HasUserType_Int64_List1")]
        [TestIgnore]
        public void HasUserType_Int64_List1_test()
        {
            //var userId = 0;
            //var result = Service<IUserTypeService>().HasUserType(userId, new List<Guid>());
            //Assert.True(result);
        }

        [Fact]
        [TestMethod("HasUserType_Int64_List1")]
        [TestIgnore]
        public void HasUserType_Int64_List1_test0()
        {
            //var userId = 0;
            //var result = Service<IUserTypeService>().HasUserType(userId, new List<string>());
            //Assert.True(result);
        }

        [Fact]
        [TestMethod("Update_UserType")]
        [TestIgnore]
        public void Update_UserType_test()
        {
            //UserType userType = null;
            //Service<IUserTypeService>().Update(userType);
        }
    }
}