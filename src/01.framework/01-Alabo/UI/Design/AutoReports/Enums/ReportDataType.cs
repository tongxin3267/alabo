using Alabo.Web.Mvc.Attributes;

namespace Alabo.UI.Design.AutoReports.Enums
{
    /// <summary>
    ///     数据表
    /// </summary>
    [ClassProperty(Name = "数据表")]
    public enum ReportDataType
    {
        /// <summary>
        ///     对应前台 zk-data-number
        /// </summary>
        Number = 1,

        /// <summary>
        ///     对应前台zk-data-tabs
        /// </summary>
        Tabs = 2,

        /// <summary>
        ///     对应前台 zk-data-amount
        /// </summary>
        Amount = 3,

        /// <summary>
        ///     对应前台 zk-data-day-mount
        /// </summary>
        DayAmount = 4,

        /// <summary>
        ///     对应前台 zk-data-general
        /// </summary>
        General = 5,

        /// <summary>
        ///     对应前台 zk-data-growing
        /// </summary>
        Growing = 6,

        /// <summary>
        ///     对应前台 zk-data-introduce
        /// </summary>
        Introduce = 7,

        /// <summary>
        ///     对应前台 zk-data-member
        /// </summary>
        Member = 8,

        /// <summary>
        ///     对应前台 zk-data-ratio
        /// </summary>
        Ratio = 9,

        /// <summary>
        ///     对应前台 zk-data-states
        /// </summary>
        States = 10,

        /// <summary>
        ///     对应前台 zk-data-total
        /// </summary>
        Total = 11,

        /// <summary>
        ///     对应前台 zk-data-type
        /// </summary>
        Type = 12
    }
}