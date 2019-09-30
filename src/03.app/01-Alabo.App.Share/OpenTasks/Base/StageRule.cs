using Alabo.Domains.Enums;
using System;

namespace Alabo.App.Share.OpenTasks.Base
{
    /// <summary>
    ///     分期方式
    /// </summary>
    public class StageRule
    {
        /// <summary>
        ///     分期期数
        /// </summary>
        //[Display(Name = "分期期数")]
        public int StagePeriods { get; set; }

        /// <summary>
        ///     分期模式
        /// </summary>
        //[Display(Name = "分期模式", Description = "1:天 2：星期 3：月")]
        public TimeType timeType { get; set; }

        /// <summary>
        ///     分期间隔
        ///     到账方式为SpecifyTimePeriodicTo时有用
        /// </summary>
        //[Display(Name = "分期间隔", Description = "当分期方式为天时，则该数值代表几天执行一次。当分期方式为星期时，则该数值代表每星期几执行一次。当分期方式为月时，则为每月几号")]
        public int StageInterval { get; set; }

        /// <summary>
        ///     非秒到的到账时间
        ///     建议设置每天凌晨3-5点
        ///     到账方式为SecondsToOnlyOne没有用
        ///     到账方式为SpecifyTimeOnlyOne 是指定的时间（比如2017年3月25日20:41:01）
        /// </summary>

        public DateTime ToAccountTime { get; set; }
    }
}