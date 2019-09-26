using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Tasks.Schedules.Domain.Enums {

    /// <summary>
    ///     分润到账方式
    ///     分为：一次性到账，与周期性到账两种方式
    /// </summary>
    [ClassProperty(Name = "分润到帐方式")]
    public enum ToAccountTimeType {

        /// <summary>
        ///     秒到
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "一次性秒到（出于性能的考虑，一次性到账会有所延迟。根据数据量以及服务器配置不同，到账的时间有所差别，一般在5分钟内会到账）")]
        SecondsToOnlyOne = 1,

        ///// <summary>
        ///// 指定时间一次性到账
        ///// </summary>
        ////[LabelCssClass(BadgeColorCalss.Success)]
        ////[Display(Name = "指定时间一次性到账（在指定的时间一次性到账）")]
        ////SpecifyTimeOnlyOne = 2,

        ///// <summary>
        ///// 按天一次性到账
        ///// </summary>
        ////[LabelCssClass(BadgeColorCalss.Success)]
        ////[Display(Name = "按天一次性到账")]
        ////DayToOnlyOne = 3,

        ///// <summary>
        ///// 按周一次性到账
        ///// </summary>
        ////[LabelCssClass(BadgeColorCalss.Success)]
        ////[Display(Name = "按周一次性到账")]
        ////WeekToOnlyOne = 4,

        ///// <summary>
        ///// 按月一次性到账
        ///// </summary>
        ////[LabelCssClass(BadgeColorCalss.Success)]
        ////[Display(Name = " 按月一次性到账")]
        ////MonthToOnlyOne = 5,

        ///// <summary>
        ///// 按季度一次性到账
        ///// </summary>
        ////[LabelCssClass(BadgeColorCalss.Success)]
        ////[Display(Name = "按季度一次性到账")]
        ////QuarterOnlyTo = 6,

        ///// <summary>
        ///// 按年一次性到账
        ///// </summary>
        ////[LabelCssClass(BadgeColorCalss.Success)]
        ////[Display(Name = "按年一次性到账")]
        ////YearOnlyTo = 7,

        ///// <summary>
        ///// 按天周期性到
        ///// </summary>
        ////[LabelCssClass(BadgeColorCalss.Success)]
        ////[Display(Name = "按小时循环性到账（每小时指定的时间到账一次，直到所有到账结束）")]
        ////HoursPeriodicTo = 11,

        ///// <summary>
        ///// 按天周期性到
        ///// </summary>
        ////[LabelCssClass(BadgeColorCalss.Success)]
        ////[Display(Name = "按天循环性到账（每天指定的时间到账一次，直到所有到账结束）")]
        ////DayPeriodicTo = 12,

        ///// <summary>
        ///// 按星期周期性到账
        ///// </summary>
        ////[LabelCssClass(BadgeColorCalss.Success)]
        ////[Display(Name = "按星期循环性到账（表示每星期几执行一次，每星期指定的时间到账一次，直到所有到账结束）")]
        ////WeekPeriodicTo = 13,

        ///// <summary>
        ///// 按星期周期性到账
        ///// </summary>
        ////[LabelCssClass(BadgeColorCalss.Success)]
        ////[Display(Name = "按月循环性到账（表示每月几号执行一次，每星期指定的时间到账一次，直到所有到账结束）")]
        ////MonthPeriodicTo = 14,

        ///// <summary>
        ///// 按季度周期性到账
        ///// </summary>
        ////[LabelCssClass(BadgeColorCalss.Success)]
        ////[Display(Name = "按季度循环性到账（每季度指定的时间到账一次，直到所有到账结束）")]
        ////QuarterPeriodicTo = 15,

        ///// <summary>
        ///// 按年周期性到账
        ///// </summary>
        ////[LabelCssClass(BadgeColorCalss.Success)]
        ////[Display(Name = "按年循环性到账（每年指定的时间到账一次，直到所有到账结束）")]
        ////YearPeriodicTo = 16,

        /// <summary>
        ///     分期到账
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = " 分期到账（分期到账，需单独设置循环周期，循环周期单位为天。比如循环周期为2天时，表示两天执行一次）")]
        SpecifyTimePeriodicTo = 100
    }
}