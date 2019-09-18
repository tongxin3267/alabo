using Alabo.App.Core.Reports;

namespace Alabo.App.Core.User.Reports {

    public class UserGradeReport {
        ///// <summary>
        ///// 等级Id
        ///// </summary>
        //public Guid GradeId { get; set; }

        ///// <summary>
        ///// 等级名称
        ///// </summary>
        //[ReportColumn("GradeName", Text = "等级名称")]
        //public string GradeName { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        [ReportColumn("Total", Text = "累计商家")]
        public decimal TotalCount { get; set; }

        /// <summary>
        /// 未启用
        /// </summary>
        [ReportColumn("UnActivated", Text = "未启用")]
        public decimal UnActivatedCount { get; set; }

        /// <summary>
        /// 未启用
        /// </summary>
        [ReportColumn("Today", Text = "今日商家")]
        public decimal TodayCount { get; set; }

        /// <summary>
        /// 未启用
        /// </summary>
        [ReportColumn("ThisMonth", Text = "本月商家")]
        public decimal ThisMonthCount { get; set; }
    }
}