using Xunit;

namespace ZKCloud.Test.Helpers
{
    /// <summary>
    ///     Web测试
    /// </summary>
    public class WebTest
    {
        /// <summary>
        ///     Url编码
        /// </summary>
        [Fact]
        public void TestUrlEncode()
        {
            Assert.Equal("http%3a%2f%2fwww.a.com", ZKCloud.Helpers.HttpWeb.UrlEncode(@"http://www.a.com"));
        }

        /// <summary>
        ///     Url编码,转大写
        /// </summary>
        [Fact]
        public void TestUrlEncode_Upper()
        {
            Assert.Equal("http%3A%2F%2Fwww.a.com", ZKCloud.Helpers.HttpWeb.UrlEncode(@"http://www.a.com", true));
        }
    }
}