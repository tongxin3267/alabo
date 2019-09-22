//using System;
//using Xunit;
//using Alabo.App.Core.Admin.Domain.CallBacks;
//using Alabo.App.Core.Admin.Domain.Services;
//using Alabo.Test.Base.Attribute;
//using Alabo.Test.Base.Core;
//using Alabo.Test.Base.Core.Model;

//namespace Alabo.Test.Core.Admin.Domain.Services
//{
//    public class IPostRoleOldServiceTests : CoreTest
//    {
//        [Theory]
//        [InlineData(2)]
//        [InlineData(1)]
//        [InlineData(-1)]
//        [TestMethod("IsSuperAdmin_Int64")]
//        [TestIgnore]
//        public void IsSuperAdmin_Int64_test(long userId)
//        {
//            //         var user = Service<IUserService>().GetRandom(userId);
//            //         var result = Service<IPostRoleOldService>().IsSuperAdmin(user.Id);
//            //Assert.True(result);
//        }

//        [Theory]
//        [InlineData(-1)]
//        [TestMethod("GetSingleFromCache_Test")]
//        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
//        {
//            //var model = Service<IAdminService>().GetRandom(entityId);
//            //if (model != null)
//            //{
//            //    var newModel = Service<IAdminService>().GetSingleFromCache(model.Id);
//            //    Assert.NotNull(newModel);
//            //    Assert.Equal(newModel.Id, model.Id);
//            //}
//        } /*end*/

//        [Fact]
//        [TestMethod("AddOrUpdate_PostRoleConfig")]
//        [TestIgnore]
//        public void AddOrUpdate_PostRoleConfig_test()
//        {
//            PostRoleConfig role = null;
//            //var result = Service<IPostRoleOldService>().AddOrUpdate(role);
//            //Assert.NotNull(result);
//        }

//        [Fact]
//        [TestMethod("Delete_Guid")]
//        public void Delete_Guid_test()
//        {
//            var id = Guid.Empty;
//            var result = Resolve<IPostRoleOldService>().Delete(id);
//            Assert.NotNull(result);
//        }

//        [Fact]
//        [TestMethod("GetList_Func2")]
//        public void GetList_Func2_test()
//        {
//            var result = Resolve<IPostRoleOldService>().GetList(o => false);
//            Assert.NotNull(result);
//        }

//        [Fact]
//        [TestMethod("GetRoleForbidDictionary_Int64")]
//        public void GetRoleForbidDictionary_Int64_test()
//        {
//            var userId = 0;
//            var result = Resolve<IPostRoleOldService>().GetRoleForbidDictionary(userId);
//            Assert.NotNull(result);
//        }

//        [Fact]
//        [TestMethod("GetSingle_Func2")]
//        [TestIgnore]
//        public void GetSingle_Func2_test()
//        {
//            //var result = Service<IPostRoleOldService>().GetSingle(o => false);
//            //Assert.NotNull(result);
//        }

//        [Fact]
//        [TestMethod("GetUserRole_Int64")]
//        public void GetUserRole_Int64_test()
//        {
//            var userId = 0;
//            var result = Resolve<IPostRoleOldService>().GetUserRole(userId);
//            Assert.NotNull(result);
//        }
//    }
//}