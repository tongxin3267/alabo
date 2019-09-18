using System.Collections.Generic;
using Xunit;
using ZKCloud.Extensions;
using ZKCloud.Test.Samples;

namespace ZKCloud.Test
{
    /// <summary>
    ///     扩展测试 - 公共扩展
    /// </summary>
    public partial class ExtensionTest
    {
        /// <summary>
        ///     测试获取枚举描述
        /// </summary>
        [Fact]
        public void TestDescription()
        {
            Assert.Equal("B2", EnumSample.B.Description());
        }

        /// <summary>
        ///     转换为用分隔符连接的字符串
        /// </summary>
        [Fact]
        public void TestJoin()
        {
            Assert.Equal("1,2,3", new List<int> {1, 2, 3}.Join());
            Assert.Equal("'1','2','3'", new List<int> {1, 2, 3}.Join("'"));
        }

        /// <summary>
        ///     测试安全获取值
        /// </summary>
        [Fact]
        public void TestSafeValue()
        {
            int? value = null;
            Assert.Equal(0, ZKCloud.Extensions.Extensions.SafeValue(value));
            value = 1;
            Assert.Equal(1, ZKCloud.Extensions.Extensions.SafeValue(value));
        }

        /// <summary>
        ///     测试获取枚举值
        /// </summary>
        [Fact]
        public void TestValue()
        {
            Assert.Equal(2, EnumSample.B.Value());
            Assert.Equal("2", EnumSample.B.Value<string>());
        }
    }
}