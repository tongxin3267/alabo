using Xunit;
using Alabo.App.Core.Themes.Domain.Services;
using Alabo.App.Core.Themes.Dtos;
using Alabo.Framework.Core.Enums;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Open.Themes.Domain.Services
{
    public class IThemeServiceTests : CoreTest
    {
        [Fact]
        [TestMethod("GetAllThemeAsync_TemplateCenterInput")]
        public void GetAllThemeAsync_TemplateCenterInput_test()
        {
            //TemplateCenterInput templateCenter = null;
            //var result = Resolve<IThemeService>().GetAllThemeAsync(templateCenter);
            //Assert.NotNull(result);
        }

        /*end*/

        [Fact]
        [TestMethod("GetDefaultTheme_ClientType_HttpContext")]
        [TestIgnore]
        public void GetDefaultTheme_ClientType_HttpContext_test()
        {
            //			var clientType = (Alabo.Framework.Core.Enums.ClientType)0;
            //			HttpContext context = null;
            //			var result = Service<IThemeService>().GetDefaultTheme( clientType, context);
            //			Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetDefaultTheme_ClientType")]
        public void GetDefaultTheme_ClientType_test()
        {
            //var clientType = (ClientType) 0;
            //var result = Service<IThemeService>().GetDefaultTheme( clientType);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("InitSite_HttpContext")]
        public void InitSite_HttpContext_test()
        {
            //HttpContext context = null;
            //var result = Service<IThemeService>().InitSite(context);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("SetDefaultTheme_HttpContext")]
        public void SetDefaultTheme_HttpContext_test()
        {
            //HttpContext context = null;
            //Service<IThemeService>().SetDefaultTheme(context);
        }

        [Fact]
        [TestMethod("SetDefaultTheme")]
        public void SetDefaultTheme_test()
        {
            //Service<IThemeService>().SetDefaultTheme();
        }
    }
}