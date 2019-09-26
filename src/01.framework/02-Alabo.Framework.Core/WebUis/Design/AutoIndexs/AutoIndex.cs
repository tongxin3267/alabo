using Alabo.Core.WebUis.Design.AutoTables;
using Alabo.Domains.Services.Report;

namespace Alabo.Core.WebUis.Design.AutoIndexs
{
    public class AutoIndex
    {
        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     颜色
        /// </summary>
        public string Color { get; set; }

        public string Icon { get; set; } = UIHelper.Icon;
    }

    public class AutoIndexItem
    {
        /// <summary>
        ///     表格
        /// </summary>
        public AutoTable Table { get; set; }

        /// <summary>
        ///     数据表
        /// </summary>
        public AutoReprotData AutoReprotData { get; set; }

        /// <summary>
        ///     图表
        /// </summary>
        public AutoReportChart AutoReportChart { get; set; }
    }
}