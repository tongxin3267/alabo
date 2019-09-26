//using System;
//using Xunit;
//using Alabo.App.Core.Themes.Domain.Services;
//using Alabo.App.Core.Themes.Dtos;
//using Alabo.Framework.Core.Enums;
//using Alabo.Test.Base.Core;
//using Alabo.Test.Base.Core.Model;

//namespace Alabo.Test.Open.Themes.Domain.Services {
//    public class IThemePageServiceTests : CoreTest {
//        [Theory]
//        [InlineData(-1)]
//        [TestMethod("GetThemePageInfo_ThemePageInput")]
//        public void GetThemePageInfo_ThemePageInput_test(long entityId) {
//            var model = Resolve<IThemePageService>().GetRandom(entityId);
//            if (model != null) {
//                var themePageInput = new ThemePageInput();
//                themePageInput.ClientType = ClientType.WapH5;
//                themePageInput.Url = model.Path;
//                var result = Resolve<IThemePageService>().GetPageInfo(themePageInput);
//                Assert.NotNull(result);
//            }
//        }

//        [Fact]
//        [TestMethod("GetAllPageInfo_ClientType")]
//        public void GetAllPageInfo_ClientType_test() {
//            foreach (ClientType item in Enum.GetValues(typeof(ClientType))) {
//                var result = Resolve<IThemePageService>().GetAllPageInfo(item);
//                Assert.NotNull(result);
//            }
//        }

//        /*end*/

//        [Fact]
//        [TestMethod("GetThemePageFromService_Theme_ThemePageInput_HttpContext")]
//        public void GetThemePageFromService_Theme_ThemePageInput_HttpContext_test() {
//            Theme theme = null;
//            ThemePageInput themePageInput = null;
//            HttpContext context = null;
//            var result = Service<IThemePageService>().GetThemePageFromService(theme, themePageInput, context);
//            Assert.NotNull(result);
//        }

//        [Fact]
//        [TestMethod("GetThemePageInfo_ThemePageInput_HttpContext")]
//        public void GetThemePageInfo_ThemePageInput_HttpContext_test() {
//            ThemePageInput themePageInput = null;
//            HttpContext context = null;
//            var result = Service<IThemePageService>().GetThemePageInfo(themePageInput, context);
//            Assert.NotNull(result);
//        }

//        [Fact]
//        [TestMethod("InitThemePage_HttpContext")]
//        public void InitThemePage_HttpContext_test() {
//            HttpContext httpContext = null;
//            var result = Service<IThemePageService>().InitThemePage(httpContext);
//            Assert.NotNull(result);
//        }

//        [Fact]
//        [TestMethod("Save_ThemePage_HttpContext")]
//        public void Save_ThemePage_HttpContext_test() {
//            ThemePage themePage = null;
//            HttpContext context = null;
//            var result = Service<IThemePageService>().Save(themePage, context);
//            Assert.NotNull(result);
//        }
//    }
//}