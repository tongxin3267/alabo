using System;
using Xunit;
using ZKCloud.Test.Base.Core;
using DateTimeExtensions = ZKCloud.Extensions.DateTimeExtensions;

namespace ZKCloud.Test.Extensions
{
    public class DateTimeExtensionsTests
    {
        /// <summary>
        ///     �����ַ���,"2012-01-02"
        /// </summary>
        public const string DateString1 = "2012-01-02";

        /// <summary>
        ///     �����ַ���,"2012-11-12"
        /// </summary>
        public const string DateString2 = "2012-11-12";

        /// <summary>
        ///     ����ʱ���ַ���,"2012-01-02 01:02:03"
        /// </summary>
        public const string DatetimeString1 = "2012-01-02 01:02:03";

        [Fact]
        [TestMethod("ApiFormat_DateTime")]
        public void ApiFormat_DateTime_test()
        {
            var dateTime = DateTime.Now;
            var result = DateTimeExtensions.ApiFormat(dateTime);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ConvertDateTimeFileString_DateTime")]
        public void ConvertDateTimeFileString_DateTime_test()
        {
            var time = DateTime.Now;
            var result = DateTimeExtensions.ConvertDateTimeFileString(time);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ConvertDateTimeInt_DateTime")]
        public void ConvertDateTimeInt_DateTime_test()
        {
            var time = DateTime.Now;
            var result = DateTimeExtensions.ConvertDateTimeInt(time);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ConvertDateTimeToDateString_DateTime")]
        public void ConvertDateTimeToDateString_DateTime_test()
        {
            var time = DateTime.Now;
            var result = DateTimeExtensions.ConvertDateTimeToDateString(time);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ConvertDateTimeToTimeString_DateTime")]
        public void ConvertDateTimeToTimeString_DateTime_test()
        {
            var time = DateTime.Now;
            var result = DateTimeExtensions.ConvertDateTimeToTimeString(time);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("DateDiff_DateTime_DateTime")]
        public void DateDiff_DateTime_DateTime_test()
        {
            var dateTime1 = DateTime.Now;
            var dateTime2 = DateTime.Now;
            var result = DateTimeExtensions.DateDiff(dateTime1, dateTime2);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Description_TimeSpan")]
        public void Description_TimeSpan_test()
        {
            var span = TimeSpan.FromHours(1);
            var result = DateTimeExtensions.Description(span);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("DiffDays_DateTime_DateTime")]
        public void DiffDays_DateTime_DateTime_test()
        {
            var src = DateTime.Now.AddDays(1);
            var dst = DateTime.Now;
            var result = DateTimeExtensions.DiffDays(src, dst);
            Assert.True(result > 0);
            Assert.True(result == 1);
        }

        [Fact]
        [TestMethod("DiffMonth_DateTime_DateTime")]
        public void DiffMonth_DateTime_DateTime_test()
        {
            var src = DateTime.Now.AddMonths(1);
            var dst = DateTime.Now;
            var result = DateTimeExtensions.DiffMonth(src, dst);
            Assert.True(result > 0);
            Assert.True(result == 1);
        }

        [Fact]
        [TestMethod("DiffQuarter_DateTime_DateTime")]
        public void DiffQuarter_DateTime_DateTime_test()
        {
            var src = DateTime.Now.AddMonths(3);
            var dst = DateTime.Now;
            var result = DateTimeExtensions.DiffQuarter(src, dst);
            Assert.True(result > 0);
            Assert.True(result == 1);
        }

        [Fact]
        [TestMethod("DiffWeeks_DateTime_DateTime")]
        public void DiffWeeks_DateTime_DateTime_test()
        {
            var src = DateTime.Now.AddDays(7);
            var dst = DateTime.Now;
            var result = DateTimeExtensions.DiffWeeks(src, dst);
            Assert.True(result > 0);
            Assert.True(result == 1);
        }

        [Fact]
        [TestMethod("DiffYear_DateTime_DateTime")]
        public void DiffYear_DateTime_DateTime_test()
        {
            var src = DateTime.Now.AddYears(1);
            var dst = DateTime.Now;
            var result = DateTimeExtensions.DiffYear(src, dst);
            Assert.True(result > 0);
            Assert.True(result == 1);
        }

        [Fact]
        [TestMethod("GetDayBegin_DateTime")]
        public void GetDayBegin_DateTime_test()
        {
            var time = DateTime.Now;
            var result = DateTimeExtensions.GetDayBegin(time);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetDayFinish_DateTime")]
        public void GetDayFinish_DateTime_test()
        {
            var time = DateTime.Now;
            var result = DateTimeExtensions.GetDayFinish(time);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetMonthBegin_DateTime")]
        public void GetMonthBegin_DateTime_test()
        {
            var time = DateTime.Now;
            var result = DateTimeExtensions.GetMonthBegin(time);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetMonthFinish_DateTime")]
        public void GetMonthFinish_DateTime_test()
        {
            var time = DateTime.Now;
            var result = DateTimeExtensions.GetMonthFinish(time);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetQuarterBegin_DateTime")]
        public void GetQuarterBegin_DateTime_test()
        {
            var time = DateTime.Now;
            var result = DateTimeExtensions.GetQuarterBegin(time);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetQuarterFinish_DateTime")]
        public void GetQuarterFinish_DateTime_test()
        {
            var time = DateTime.Now;
            var result = DateTimeExtensions.GetQuarterFinish(time);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetTimeFromUnixTimestamp_Int64")]
        public void GetTimeFromUnixTimestamp_Int64_test()
        {
            var timestamp = 0;
            var result = DateTimeExtensions.GetTimeFromUnixTimestamp(timestamp);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUnixTimestamp_DateTime")]
        public void GetUnixTimestamp_DateTime_test()
        {
            var time = DateTime.Now;
            var result = DateTimeExtensions.GetUnixTimestamp(time);
            Assert.True(result > 0);
        }

        [Fact]
        [TestMethod("GetWeekBegin_DateTime")]
        public void GetWeekBegin_DateTime_test()
        {
            var time = DateTime.Now;
            var result = DateTimeExtensions.GetWeekBegin(time);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetWeekFinish_DateTime")]
        public void GetWeekFinish_DateTime_test()
        {
            var time = DateTime.Now;
            var result = DateTimeExtensions.GetWeekFinish(time);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetYearBegin_DateTime")]
        public void GetYearBegin_DateTime_test()
        {
            var time = DateTime.Now;
            var result = DateTimeExtensions.GetYearBegin(time);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetYearFinish_DateTime")]
        public void GetYearFinish_DateTime_test()
        {
            var time = DateTime.Now;
            var result = DateTimeExtensions.GetYearFinish(time);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Performance_Action")]
        public void Performance_Action_test()
        {
            //Action action  = new Action();
            //var result = DateTimeExtensions.Performance(action);
            //Assert.NotNull(result);
        }

        /// <summary>
        ///     ���Դ�Unixʱ�����ȡʱ��
        /// </summary>
        [Fact]
        public void TestGetTimeFromUnixTimestamp()
        {
            Assert.Equal(new DateTime(1970, 01, 01, 12, 12, 12), DateTimeExtensions.GetTimeFromUnixTimestamp(15132));
            Assert.Equal(new DateTime(2000, 12, 12, 12, 12, 12),
                DateTimeExtensions.GetTimeFromUnixTimestamp(976594332));
            Assert.Equal(new DateTime(2014, 02, 18, 04, 24, 59),
                DateTimeExtensions.GetTimeFromUnixTimestamp(1392668699));
        }

        /// <summary>
        ///     ���Ի�ȡUnixʱ���
        /// </summary>
        [Fact]
        public void TestGetUnixTimestamp()
        {
            Assert.Equal(15132, DateTimeExtensions.GetUnixTimestamp(new DateTime(1970, 01, 01, 12, 12, 12)));
            Assert.Equal(976594332, DateTimeExtensions.GetUnixTimestamp(new DateTime(2000, 12, 12, 12, 12, 12)));
            Assert.Equal(1392668699, DateTimeExtensions.GetUnixTimestamp(new DateTime(2014, 02, 18, 04, 24, 59)));
        }

        [Fact]
        [TestMethod("ToChineseDateString_DateTime")]
        public void ToChineseDateString_DateTime_test()
        {
            var dateTime = DateTime.Now;
            var result = DateTimeExtensions.ToChineseDateString(dateTime);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToChineseDateString_Nullable_System_DateTime")]
        public void ToChineseDateString_Nullable_System_DateTime_test()
        {
            var dateTime = DateTime.Now;
            var result = DateTimeExtensions.ToChineseDateString(dateTime);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToChineseDateTimeString_DateTime_Boolean")]
        public void ToChineseDateTimeString_DateTime_Boolean_test()
        {
            var dateTime = DateTime.Now;
            var removeSecond = false;
            var result = DateTimeExtensions.ToChineseDateTimeString(dateTime, removeSecond);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToChineseDateTimeString_Nullable_System_DateTime_Boolean")]
        public void ToChineseDateTimeString_Nullable_System_DateTime_Boolean_test()
        {
            var dateTime = DateTime.Now;
            var removeSecond = false;
            var result = DateTimeExtensions.ToChineseDateTimeString(dateTime, removeSecond);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToDateString_DateTime")]
        public void ToDateString_DateTime_test()
        {
            var dateTime = DateTime.Now;
            var result = DateTimeExtensions.ToDateString(dateTime);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToDateString_Nullable_System_DateTime")]
        public void ToDateString_Nullable_System_DateTime_test()
        {
            var dateTime = DateTime.Now;
            var result = DateTimeExtensions.ToDateString(dateTime);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToDateTimeString_Nullable_System_DateTime_Boolean")]
        public void ToDateTimeString_Nullable_System_DateTime_Boolean_test()
        {
            var dateTime = DateTime.Now;
            var removeSecond = false;
            var result = DateTimeExtensions.ToDateTimeString(dateTime, removeSecond);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToMillisecondString_DateTime")]
        public void ToMillisecondString_DateTime_test()
        {
            var dateTime = DateTime.Now;
            var result = DateTimeExtensions.ToMillisecondString(dateTime);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToMillisecondString_Nullable_System_DateTime")]
        public void ToMillisecondString_Nullable_System_DateTime_test()
        {
            var dateTime = DateTime.Now;
            var result = DateTimeExtensions.ToMillisecondString(dateTime);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToTimeString_DateTime_Boolean")]
        public void ToTimeString_DateTime_Boolean_test()
        {
            var dateTime = DateTime.Now;
            var removeSecond = false;
            var result = DateTimeExtensions.ToTimeString(dateTime, removeSecond);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToTimeString_DateTime")]
        public void ToTimeString_DateTime_test()
        {
            var dateTime = DateTime.Now;
            var result = DateTimeExtensions.ToTimeString(dateTime);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToTimeString_Nullable_System_DateTime")]
        public void ToTimeString_Nullable_System_DateTime_test()
        {
            var dateTime = DateTime.Now;
            var result = DateTimeExtensions.ToTimeString(dateTime);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Truncate_DateTime")]
        public void Truncate_DateTime_test()
        {
            var src = DateTime.Now;
            var result = DateTimeExtensions.Truncate(src);
            Assert.NotNull(result);
        }

        /*end*/
    }
}