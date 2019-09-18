using System.Collections.Generic;

namespace Alabo.UI.AutoReports
{
    /// <summary>
    ///     前台报表展示格式
    ///     三种展示方式
    ///     1. 只有数据表
    ///     2. 只有图表
    ///     3. 图表和数据表
    /// </summary>
    public class AutoReport
    {
        /// <summary>
        ///     日期格式
        ///     格式：2019-6-26
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        ///     数量
        /// </summary>
        public long Count { get; set; }

        /// <summary>
        ///     比例
        /// </summary>
        public string Rate { get; set; }

        public List<AutoReportItem> AutoReportItem { get; set; }


        /// <summary>
        ///     报表名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string Icon { get; set; }


        /// <summary>
        ///     数据表
        /// </summary>
        public AutoReprotData AutoReprotData { get; set; }

        /// <summary>
        ///     图表
        /// </summary>
        public AutoReportChart AutoReportChart { get; set; }
    }

    public class AutoReportItem
    {
        public string Name { get; set; }
        public string TempName { get; set; }
        public string Value { get; set; }
    }
}