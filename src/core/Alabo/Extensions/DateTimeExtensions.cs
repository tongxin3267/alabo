using System;
using System.Text;

namespace Alabo.Extensions
{
    /// <summary>
    ///     系统扩展 - 日期
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        ///     获取Unix时间戳
        /// </summary>
        /// <param name="time">时间</param>
        public static long GetUnixTimestamp(DateTime time)
        {
            var start = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            var ticks = (time - start.Add(new TimeSpan(8, 0, 0))).Ticks;
            return (ticks / TimeSpan.TicksPerSecond).ConvertToLong();
        }

        /// <summary>
        ///     从Unix时间戳获取时间
        /// </summary>
        /// <param name="timestamp">Unix时间戳</param>
        public static DateTime GetTimeFromUnixTimestamp(long timestamp)
        {
            var start = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            var span = new TimeSpan(long.Parse(timestamp + "0000000"));
            return start.Add(span).Add(new TimeSpan(8, 0, 0));
        }

        /// <summary>
        ///     获取格式化字符串，带时分秒，格式："yyyy-MM-dd HH:mm:ss"
        ///     小于1970之前的日期不显示
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="removeSecond">是否移除秒</param>
        public static string ToTimeString(this DateTime dateTime, bool removeSecond = false)
        {
            return dateTime.Year < 1970
                ? string.Empty
                : dateTime.ToString(removeSecond ? "yy-MM-dd HH:mm" : "yy-MM-dd HH:mm:ss");
        }

        /// <summary>
        ///     获取格式化字符串，带时分秒，格式："yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="removeSecond">是否移除秒</param>
        public static string ToDateTimeString(this DateTime? dateTime, bool removeSecond = false)
        {
            if (dateTime == null) {
                return string.Empty;
            }

            return ToTimeString(dateTime.Value, removeSecond);
        }

        /// <summary>
        ///     获取格式化字符串，不带时分秒，格式："yyyy-MM-dd"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        ///     获取格式化字符串，不带时分秒，格式："yyyy-MM-dd"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToDateString(this DateTime? dateTime)
        {
            if (dateTime == null) {
                return string.Empty;
            }

            return ToDateString(dateTime.Value);
        }

        /// <summary>
        ///     获取格式化字符串，不带年月日，格式："HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("yy-MM-dd HH:mm");
        }

        /// <summary>
        ///     获取格式化字符串，不带年月日，格式："HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToTimeString(this DateTime? dateTime)
        {
            if (dateTime == null) {
                return string.Empty;
            }

            return dateTime.Value.ToString("yy-MM-dd HH:mm");
        }

        /// <summary>
        ///     获取格式化字符串，带毫秒，格式："yyyy-MM-dd HH:mm:ss.fff"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToMillisecondString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        /// <summary>
        ///     获取格式化字符串，带毫秒，格式："yyyy-MM-dd HH:mm:ss.fff"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToMillisecondString(this DateTime? dateTime)
        {
            if (dateTime == null) {
                return string.Empty;
            }

            return ToMillisecondString(dateTime.Value);
        }

        /// <summary>
        ///     获取格式化字符串，不带时分秒，格式："yyyy年MM月dd日"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToChineseDateString(this DateTime dateTime)
        {
            return string.Format("{0}年{1}月{2}日", dateTime.Year, dateTime.Month, dateTime.Day);
        }

        /// <summary>
        ///     获取格式化字符串，不带时分秒，格式："yyyy年MM月dd日"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToChineseDateString(this DateTime? dateTime)
        {
            if (dateTime == null) {
                return string.Empty;
            }

            return ToChineseDateString(dateTime.Value);
        }

        /// <summary>
        ///     获取格式化字符串，带时分秒，格式："yyyy年MM月dd日 HH时mm分"
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="removeSecond">是否移除秒</param>
        public static string ToChineseDateTimeString(this DateTime dateTime, bool removeSecond = false)
        {
            var result = new StringBuilder();
            result.AppendFormat("{0}年{1}月{2}日", dateTime.Year, dateTime.Month, dateTime.Day);
            result.AppendFormat(" {0}时{1}分", dateTime.Hour, dateTime.Minute);
            if (removeSecond == false) {
                result.AppendFormat("{0}秒", dateTime.Second);
            }

            return result.ToString();
        }

        /// <summary>
        ///     获取格式化字符串，带时分秒，格式："yyyy年MM月dd日 HH时mm分"
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="removeSecond">是否移除秒</param>
        public static string ToChineseDateTimeString(this DateTime? dateTime, bool removeSecond = false)
        {
            if (dateTime == null) {
                return string.Empty;
            }

            return ToChineseDateTimeString(dateTime.Value, removeSecond);
        }

        /// <summary>
        ///     获取描述
        /// </summary>
        /// <param name="span">时间间隔</param>
        public static string Description(this TimeSpan span)
        {
            var result = new StringBuilder();
            if (span.Days > 0) {
                result.AppendFormat("{0}天", span.Days);
            }

            if (span.Hours > 0) {
                result.AppendFormat("{0}小时", span.Hours);
            }

            if (span.Minutes > 0) {
                result.AppendFormat("{0}分", span.Minutes);
            }

            if (span.Seconds > 0) {
                result.AppendFormat("{0}秒", span.Seconds);
            }

            if (span.Milliseconds > 0) {
                result.AppendFormat("{0}毫秒", span.Milliseconds);
            }

            return result.Length > 0 ? result.ToString() : $"{span.TotalSeconds * 1000}毫秒";
        }

        /// <summary>
        ///     获取时间差
        /// </summary>
        /// <param name="dateTime1"></param>
        /// <param name="dateTime2"></param>
        public static string DateDiff(DateTime dateTime1, DateTime dateTime2)
        {
            string dateDiff = null;
            var ts1 = new TimeSpan(dateTime1.Ticks);
            var ts2 = new TimeSpan(dateTime2.Ticks);
            var ts = ts1.Subtract(ts2).Duration();
            dateDiff = ts.Days + "天" + ts.Hours + "小时" + ts.Minutes + "分钟" + ts.Seconds + "秒";
            return dateDiff;
        }

        /// <summary>
        ///     将时间转换成 刚刚，1小时前，10天前等格式输出
        /// </summary>
        /// <param name="dateTime"></param>
        public static string ApiFormat(this DateTime dateTime)
        {
            var ts1 = new TimeSpan(dateTime.Ticks);
            var ts2 = new TimeSpan(DateTime.Now.Ticks);
            var ts = ts1.Subtract(ts2).Duration();

            string hours = ts.Hours.ToString(),
                minutes = ts.Minutes.ToString(),
                seconds = ts.Seconds.ToString();
            if (ts.Seconds <= 60) {
                return "刚刚";
            }

            if (ts.Minutes <= 60) {
                return $"{ts.Minutes}分钟前";
            }

            if (ts.Hours <= 24) {
                return $"{ts.Hours}小时前";
            }

            if (ts.Days <= 30) {
                return $"{ts.Days}天前";
            }

            return dateTime.ToString("yyyy-MM-dd HH:ss");
        }

        ///// <summary>
        ///// 将Unix时间戳转换为DateTime类型时间
        ///// </summary>
        ///// <param name="d">double 型数字</param>
        ///// <returns>DateTime</returns>
        //public static System.DateTime ConvertIntDateTime(double d)
        //{
        //    System.DateTime time = System.DateTime.MinValue;
        //    System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        //    time = startTime.AddSeconds(d);
        //    return time;
        //}

        /// <summary>
        ///     将时间类型转换成日期字符串 2011-06-09
        /// </summary>
        /// <param name="time"></param>
        public static string ConvertDateTimeToDateString(this DateTime time)
        {
            string str;
            var month = time.Month.ToString();
            if (month.Length == 1) {
                month = "0" + month;
            }

            var day = time.Day.ToString();
            if (day.Length == 1) {
                day = "0" + month;
            }

            str = string.Format("{0}-{1}-{2}", time.Year, month, day);
            return str;
        }

        /// <summary>
        ///     将时间类型转换成日期字符串 20110609
        /// </summary>
        /// <param name="time"></param>
        public static string ConvertDateTimeFileString(this DateTime time)
        {
            var str = string.Empty;
            var month = time.Month.ToString();
            if (month.Length == 1) {
                month = "0" + month;
            }

            var day = time.Day.ToString();
            if (day.Length == 1) {
                day = "0" + month;
            }

            str = $"{time.Year}{month}{day}{time.Hour}{time.Minute}{time.Second}";
            return str;
        }

        /// <summary>
        ///     将时间类型转换成时间字符串 12-14
        /// </summary>
        /// <param name="time"></param>
        public static string ConvertDateTimeToTimeString(this DateTime time)
        {
            var str = string.Empty;
            var month = time.Month.ToString();
            if (month.Length == 1) {
                month = "0" + month;
            }

            var day = time.Day.ToString();
            if (day.Length == 1) {
                day = "0" + month;
            }

            str = string.Format("{0}-{1}-{2}", time.Year, month, day);
            return str;
        }

        /// <summary>
        ///     当天的开始时间
        /// </summary>
        public static DateTime GetDayBegin(this DateTime time)
        {
            return time.Date;
        }

        /// <summary>
        ///     当天的结束时间
        /// </summary>
        public static DateTime GetDayFinish(this DateTime time)
        {
            return time.Date.AddDays(1).AddTicks(-1);
        }

        /// <summary>
        ///     当周的开始时间
        /// </summary>
        public static DateTime GetWeekBegin(this DateTime time)
        {
            return time.Date.AddDays(-(int) time.DayOfWeek);
        }

        /// <summary>
        ///     当周的结束时间
        /// </summary>
        public static DateTime GetWeekFinish(this DateTime time)
        {
            return time.GetWeekBegin().AddDays(7).AddTicks(-1);
        }

        /// <summary>
        ///     当月的开始时间
        /// </summary>
        public static DateTime GetMonthBegin(this DateTime time)
        {
            return time.Date.AddDays(1 - time.Day);
        }

        /// <summary>
        ///     当月的结束时间
        /// </summary>
        public static DateTime GetMonthFinish(this DateTime time)
        {
            return time.GetMonthBegin().AddMonths(1).AddTicks(-1);
        }

        /// <summary>
        ///     当季的开始时间
        /// </summary>
        public static DateTime GetQuarterBegin(this DateTime time)
        {
            var monthBegin = time.GetMonthBegin();
            return monthBegin.AddMonths(-((monthBegin.Month - 1) % 3));
        }

        /// <summary>
        ///     当季的结束时间
        /// </summary>
        public static DateTime GetQuarterFinish(this DateTime time)
        {
            return time.GetQuarterBegin().AddMonths(3).AddTicks(-1);
        }

        /// <summary>
        ///     当年的开始时间
        /// </summary>
        public static DateTime GetYearBegin(this DateTime time)
        {
            return time.Date.AddDays(1 - time.DayOfYear);
        }

        /// <summary>
        ///     当年的结束时间
        /// </summary>
        public static DateTime GetYearFinish(this DateTime time)
        {
            return time.GetYearBegin().AddYears(1).AddTicks(-1);
        }

        /// <summary>
        ///     返回相差天数
        /// </summary>
        public static int DiffDays(this DateTime src, DateTime dst)
        {
            return (src.Date - dst.Date).Days;
        }

        /// <summary>
        ///     返回相差周数
        /// </summary>
        public static int DiffWeeks(this DateTime src, DateTime dst)
        {
            return (src.GetWeekBegin() - dst.GetWeekBegin()).Days / 7;
        }

        /// <summary>
        ///     返回相差月数
        /// </summary>
        public static int DiffMonth(this DateTime src, DateTime dst)
        {
            return (src.Year - dst.Year) * 12 + (src.Month - dst.Month);
        }

        /// <summary>
        ///     返回相差季数
        /// </summary>
        public static int DiffQuarter(this DateTime src, DateTime dst)
        {
            return DiffMonth(src.GetQuarterBegin(), dst.GetQuarterBegin()) / 3;
        }

        /// <summary>
        ///     返回相差年数
        /// </summary>
        public static int DiffYear(this DateTime src, DateTime dst)
        {
            return src.Year - dst.Year;
        }

        /// <summary>
        ///     去除时间中的毫秒部分
        /// </summary>
        public static DateTime Truncate(this DateTime src)
        {
            return new DateTime(src.Ticks - src.Ticks % TimeSpan.TicksPerSecond, src.Kind);
        }

        /// <summary>
        ///     将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>double</returns>
        public static double ConvertDateTimeInt(this DateTime time)
        {
            double intResult = 0;
            var startTime = TimeZoneInfo.ConvertTimeToUtc(new DateTime(1970, 1, 1)).AddHours(16); //根据时区加16个小时
            intResult = Convert.ToInt64((time - startTime).TotalSeconds);
            return intResult;
        }

        /// <summary>
        ///     性能测试函数
        /// </summary>
        /// <param name="action">The action.</param>
        public static TimeSpan Performance(Action action)
        {
            var dtBegin = DateTime.Now;
            Console.WriteLine("开始时间" + dtBegin);

            action();

            var dtEnnd = DateTime.Now;
            var dtTime = dtEnnd - dtBegin;
            Console.WriteLine($"结束时间{dtEnnd},耗时{dtTime.TotalSeconds}秒");
            return dtTime;
        }
    }
}