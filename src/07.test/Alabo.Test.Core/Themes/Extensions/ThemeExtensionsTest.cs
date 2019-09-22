using Xunit;
using Alabo.App.Core.Themes.Extensions;

namespace Alabo.Test.Core.Themes.Extensions
{
    public class ThemeExtensionsTest
    {
        [Theory]
        [InlineData("/pages/user/reg", "/user/reg")]
        [InlineData("pages/user/reg//", "/user/reg")]
        [InlineData("pages/user/reg///", "/user/reg")]
        [InlineData("pages/user/reg/", "/user/reg")]
        [InlineData("//pages/user/reg", "/user/reg")]
        [InlineData("/pages//user/reg", "/user/reg")]
        [InlineData("/pages////user/reg", "/user/reg")]
        [InlineData("/pages///user/reg", "/user/reg")]
        public void ToSafeUrlTest(string url, string safeUrl)
        {
            var result = url.ToSafeApiUrl();
            Assert.Equal(result, safeUrl);
        }

        [Theory]
        [InlineData("/api/user/reg", "/api/user/reg")]
        [InlineData("api/user/reg//", "/api/user/reg")]
        [InlineData("api/user/reg///", "/api/user/reg")]
        [InlineData("Api/user/reg/", "/api/user/reg")]
        [InlineData("//Api/user/reg", "/api/user/reg")]
        [InlineData("/api//user/reg", "/api/user/reg")]
        [InlineData("/api///user//reg", "/api/user/reg")]
        [InlineData("//api//user/reg", "/api/user/reg")]
        public void ToSafeApiUrlTest(string url, string safeUrl)
        {
            var result = url.ToSafeApiUrl();
            Assert.Equal(result, safeUrl);
        }

        [Theory]
        [InlineData("core/zk-list/", "/core/zk-list")]
        [InlineData("core/zk-List/", "/core/zk-list")]
        [InlineData("/core//zk-list/", "/core/zk-list")]
        [InlineData("Core/zk-list/", "/core/zk-list")]
        [InlineData("core/zk-list//", "/core/zk-list")]
        [InlineData("/core/zk-list/", "/core/zk-list")]
        [InlineData("core//zk-list/", "/core/zk-list")]
        public void ToSafeComponentPathTest(string url, string safeUrl)
        {
            var result = url.ToSafeComponentPath();
            Assert.Equal(result, safeUrl);
        }

        [Theory]
        [InlineData("core/zk-list/", "CoreList")]
        [InlineData("core/zk-List/", "CoreList")]
        [InlineData("/core//zk-list/", "CoreList")]
        [InlineData("Core/zk-list/", "CoreList")]
        [InlineData("core/zk-list//", "CoreList")]
        [InlineData("/core/zk-list/", "CoreList")]
        [InlineData("core//zk-list/", "CoreList")]
        [InlineData("core//zk-list-product-class", "CoreListProductClass")]
        public void GetVariableName(string url, string name)
        {
            var result = url.ToVariableName();
            Assert.Equal(result, name);
        }

        [Fact]
        public void ToSafeUrlTest2()
        {
            var url = "/User/Reg";

            url = url.ToSafeUrl();
            Assert.Equal("/user/reg", url);

            url = "pages/User/Reg";

            url = url.ToSafeUrl();
            Assert.Equal("/user/reg", url);

            url = "/pages/User/Reg";

            url = url.ToSafeUrl();
            Assert.Equal("/user/reg", url);

            url = "/pages/User/Reg/";

            url = url.ToSafeUrl();
            Assert.Equal("/user/reg", url);
        } /*end*/
    }
}