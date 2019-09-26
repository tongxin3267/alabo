using Xunit;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.Core.Admins.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Admin.Domain.Services
{
    public class IAdminServiceTests : CoreTest
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
        [TestMethod("AddAdminFromExistUser_String_Int32")]
        [TestIgnore]
        public void AddAdminFromExistUser_String_Int32_test()
        {
            //var UserName = "";
            //var roleId = 0;
            //var result = Service<IAdminService>().AddAdminFromExistUser( UserName, roleId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("AddAdminFromNewUser_String_String_String")]
        [TestIgnore]
        public void AddAdminFromNewUser_String_String_String_test()
        {
            //var name = "";
            //var password = "";
            //var roleId = "";
            //var result = Service<IAdminService>().AddAdminFromNewUser( name, password, roleId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Authentication_HttpContext_AuthenticationViewModel")]
        public void Authentication_HttpContext_AuthenticationViewModel_test()
        {
            //HttpContext httpContext = null;
            //AuthenticationViewModel authenticationViewModel = null;
            //var result = Service<IAdminService>().Authentication( httpContext, authenticationViewModel);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("CheckAuthority_HttpContext")]
        [TestIgnore]
        public void CheckAuthority_HttpContext_test()
        {
            //HttpContext httpContext = null;
            //var result = Service<IAdminService>().CheckAuthority( httpContext);
            //Assert.True(result);
        }

        [Fact]
        [TestMethod("ClearCache")]
        public void ClearCache_test()
        {
            //	Service<IAdminService>().ClearCache();
        }

        [Fact]
        [TestMethod("GetCssOrJs_String")]
        public void GetCssOrJs_String_test()
        {
            var viewName = "";
            var result = Resolve<IAdminService>().GetCssOrJs(viewName);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("TruncateTable")]
        public void TruncateTable_test()
        {
            // �������иò��ԣ�����ᵼ�����ݶ�ʧ
            //Service<IAdminService>().TruncateTable();
        }
    }
}