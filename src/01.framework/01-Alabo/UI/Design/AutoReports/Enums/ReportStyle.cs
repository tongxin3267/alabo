using System.ComponentModel.DataAnnotations;

namespace Alabo.UI.Design.AutoReports.Enums {

    /// <summary>
    ///     统计方式
    /// </summary>
    public enum ReportStyle {

        /// <summary>
        ///     数量统计，比如会员量
        ///     如果是数量统计的时候，直接统计Id
        /// </summary>
        [Display(Name = "数量统计")] Count = 1,

        /// <summary>
        ///     求和统计
        ///     本如本月订单量，本日订单量
        /// </summary>
        [Display(Name = "求和统计")] Sum = 2,

        /// <summary>
        ///     平均值，比如客单价
        /// </summary>
        [Display(Name = "平均值")] Avg = 3,

        /// <summary>
        ///     最小值
        /// </summary>
        [Display(Name = "最小值")] Min = 4,

        /// <summary>
        ///     最大值
        /// </summary>
        [Display(Name = "最大值")] Max = 5,

        /// <summary>
        ///     昨日环比
        /// </summary>
        [Display(Name = "较前一日(昨日环比)")] ChainRatioYesterday = 11,

        /// <summary>
        ///     较上周同期
        /// </summary>
        [Display(Name = "较上周同期(上周同期环比)")] ChainRatioLastWeek = 12,

        /// <summary>
        ///     较上月同期(上月同期环比)
        /// </summary>
        [Display(Name = "较上月同期(上月同期环比)")] ChainRatioLastMonth = 13,

        /// <summary>
        ///     较上季度同期(上季度同期环比)
        /// </summary>
        [Display(Name = "较上季度同期(上季度同期环比)")] ChainRatioLastQuarter = 14,

        /// <summary>
        ///     较上一年同期(上一年同期环比
        /// </summary>
        [Display(Name = "较上一年同期(上一年同期环比")] ChainRatioLastYear = 15
    }
}