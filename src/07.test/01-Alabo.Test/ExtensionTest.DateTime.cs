using System;
using Xunit;
using ZKCloud.Extensions;
using Convert = ZKCloud.Helpers.Convert;

namespace ZKCloud.Test
{
    /// <summary>
    ///     系统扩展测试 - 日期格式扩展
    /// </summary>
    public partial class ExtensionTest
    {
        /// <summary>
        ///     测试获取时间间隔描述
        /// </summary>
        [Fact]
        public void TestDescription_Span()
        {
            var span = new DateTime(2000, 1, 1, 1, 0, 1) - new DateTime(2000, 1, 1, 1, 0, 0);
            Assert.Equal("1秒", ZKCloud.Extensions.Extensions.Description(span));
            span = new DateTime(2000, 1, 1, 1, 1, 0) - new DateTime(2000, 1, 1, 1, 0, 0);
            Assert.Equal("1分", ZKCloud.Extensions.Extensions.Description(span));
            span = new DateTime(2000, 1, 1, 1, 0, 0) - new DateTime(2000, 1, 1, 0, 0, 0);
            Assert.Equal("1小时", ZKCloud.Extensions.Extensions.Description(span));
            span = new DateTime(2000, 1, 2, 0, 0, 0) - new DateTime(2000, 1, 1, 0, 0, 0);
            Assert.Equal("1天", ZKCloud.Extensions.Extensions.Description(span));
            span = new DateTime(2000, 1, 2, 0, 2, 0) - new DateTime(2000, 1, 1, 0, 0, 0);
            Assert.Equal("1天2分", ZKCloud.Extensions.Extensions.Description(span));
            span = "2000-1-1 06:10:10.123".ToDate() - "2000-1-1 06:10:10.122".ToDate();
            Assert.Equal("1毫秒", ZKCloud.Extensions.Extensions.Description(span));
            span = "2000-1-1 06:10:10.1000001".ToDate() - "2000-1-1 06:10:10.1000000".ToDate();
            Assert.Equal("0.0001毫秒", ZKCloud.Extensions.Extensions.Description(span));
        }

        /// <summary>
        ///     获取格式化中文日期字符串
        /// </summary>
        [Fact]
        public void TestToChineseDateString()
        {
            var date = "2012-01-02";
            Assert.Equal("2012年1月2日", ZKCloud.Extensions.Extensions.ToChineseDateString(Convert.ToDate(date)));
            Assert.Equal("2012年12月12日",
                ZKCloud.Extensions.Extensions.ToChineseDateString(Convert.ToDate("2012-12-12")));
            Assert.Equal("", ZKCloud.Extensions.Extensions.ToChineseDateString(Convert.ToDateOrNull("")));
            Assert.Equal("2012年1月2日", ZKCloud.Extensions.Extensions.ToChineseDateString(Convert.ToDateOrNull(date)));
        }

        /// <summary>
        ///     获取格式化中文日期时间字符串
        /// </summary>
        [Fact]
        public void TestToChineseDateTimeString()
        {
            var date = "2012-01-02 11:11:11";
            Assert.Equal("2012年1月2日 11时11分11秒",
                ZKCloud.Extensions.Extensions.ToChineseDateTimeString(Convert.ToDate(date)));
            Assert.Equal("2012年12月12日 11时11分11秒",
                ZKCloud.Extensions.Extensions.ToChineseDateTimeString(Convert.ToDate("2012-12-12 11:11:11")));
            Assert.Equal("2012年1月2日 11时11分",
                ZKCloud.Extensions.Extensions.ToChineseDateTimeString(Convert.ToDate(date), true));
            Assert.Equal("", ZKCloud.Extensions.Extensions.ToChineseDateTimeString(Convert.ToDateOrNull("")));
            Assert.Equal("2012年1月2日 11时11分11秒",
                ZKCloud.Extensions.Extensions.ToChineseDateTimeString(Convert.ToDateOrNull(date)));
            Assert.Equal("2012年1月2日 11时11分",
                ZKCloud.Extensions.Extensions.ToChineseDateTimeString(Convert.ToDateOrNull(date), true));
        }

        /// <summary>
        ///     获取格式化日期字符串
        /// </summary>
        [Fact]
        public void TestToDateString()
        {
            var date = "2012-01-02";
            Assert.Equal(date, ZKCloud.Extensions.Extensions.ToDateString(Convert.ToDate(date)));
            Assert.Equal("", ZKCloud.Extensions.Extensions.ToDateString(Convert.ToDateOrNull("")));
            Assert.Equal(date, ZKCloud.Extensions.Extensions.ToDateString(Convert.ToDateOrNull(date)));
        }

        /// <summary>
        ///     测试获取格式化日期时间字符串
        /// </summary>
        [Fact]
        public void TestToDateTimeString()
        {
            var date = "2012-01-02 11:11:11";
            Assert.Equal(date, Convert.ToDate(date).ToDateTimeString());
            Assert.Equal("2012-01-02 11:11", Convert.ToDate(date).ToDateTimeString(true));
            Assert.Equal("", ZKCloud.Extensions.Extensions.ToDateTimeString(Convert.ToDateOrNull("")));
            Assert.Equal(date, ZKCloud.Extensions.Extensions.ToDateTimeString(Convert.ToDateOrNull(date)));
        }

        /// <summary>
        ///     获取格式化毫秒字符串
        /// </summary>
        [Fact]
        public void TestToMillisecondString()
        {
            var date = "2012-01-02 11:11:11.123";
            Assert.Equal(date, ZKCloud.Extensions.Extensions.ToMillisecondString(Convert.ToDate(date)));
            Assert.Equal("", ZKCloud.Extensions.Extensions.ToMillisecondString(Convert.ToDateOrNull("")));
            Assert.Equal(date, ZKCloud.Extensions.Extensions.ToMillisecondString(Convert.ToDateOrNull(date)));
        }

        /// <summary>
        ///     获取格式化时间字符串
        /// </summary>
        [Fact]
        public void TestToTimeString()
        {
            var date = "2012-01-02 11:11:11";
            Assert.Equal("11:11:11", ZKCloud.Extensions.Extensions.ToTimeString(Convert.ToDate(date)));
            Assert.Equal("", ZKCloud.Extensions.Extensions.ToTimeString(Convert.ToDateOrNull("")));
            Assert.Equal("11:11:11", ZKCloud.Extensions.Extensions.ToTimeString(Convert.ToDateOrNull(date)));
        }
    }
}