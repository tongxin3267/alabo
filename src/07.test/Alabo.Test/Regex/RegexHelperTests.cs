using Xunit;
using ZKCloud.Core.Regex;

namespace ZKCloud.Test.Regex
{
    public class RegexHelperTests
    {
        /// <summary>
        ///     ���Ի�ȡֵ
        /// </summary>
        /// <param name="input">�����ַ���</param>
        /// <param name="pattern">ģʽ�ַ���</param>
        /// <param name="resultPattern">���ģʽ�ַ���</param>
        /// <param name="result">���</param>
        [Theory]
        [InlineData("", "", "", "")]
        [InlineData("123", "a", "", "")]
        [InlineData("123", @"\d", "", "1")]
        [InlineData("123abc456", @"\d+([a-z]+\d+)", "$1", "abc456")]
        [InlineData("123abc456", @"\d+([a-z]\d+)", "$1", "")]
        public void TestGetValue(string input, string pattern, string resultPattern, string result)
        {
            Assert.Equal(result, RegexHelper.GetValue(input, pattern, resultPattern));
        } /*end*/

        /// <summary>
        ///     ���Ի�ȡֵ����
        /// </summary>
        [Fact]
        public void TestGetValues()
        {
            Assert.Empty(RegexHelper.GetValues("", "", null));
            Assert.Empty(RegexHelper.GetValues("123abc456", @"\d{5}", new[] {"$1"}));
            Assert.Equal("123", RegexHelper.GetValues("123abc456", @"(\d*)", new[] {"$1"})["$1"]);
            Assert.Equal("abc", RegexHelper.GetValues("123abc456", @"\d*([a-z]*)\d*", new[] {"$1"})["$1"]);
            Assert.Equal("123",
                RegexHelper.GetValues("123abc456", @"(\d*)([a-z]*)(\d*)", new[] {"$1", "$2", "$3"})["$1"]);
            Assert.Equal("abc",
                RegexHelper.GetValues("123abc456", @"(\d*)([a-z]*)(\d*)", new[] {"$1", "$2", "$3"})["$2"]);
            Assert.Equal("456",
                RegexHelper.GetValues("123abc456", @"(\d*)([a-z]*)(\d*)", new[] {"$1", "$2", "$3"})["$3"]);
        }
    }
}