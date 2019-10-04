using Alabo.UI.Design.AutoReports.Enums;
using System.Collections.Generic;

namespace Alabo.UI.Design.AutoReports {

    /// <summary>
    ///     图表数据格式
    ///     数据格式参考：https://v-charts.js.org/#/radar
    /// </summary>
    public class AutoReportChart {

        /// <summary>
        ///     图表类型
        /// </summary>
        public ReportChartType Type { get; set; }

        /// <summary>
        ///     图表列
        ///     前台示列格式：['日期', '访问用户', '下单用户', '下单率']
        /// </summary>
        public List<string> Columns { get; set; }

        /// <summary>
        ///     行
        ///     前台示列格式：
        ///     { '日期': '1/1', '访问用户': 1393, '下单用户': 1093, '下单率': 0.32 },
        ///     { '日期': '1/2', '访问用户': 3530, '下单用户': 3230, '下单率': 0.26 },
        ///     { '日期': '1/3', '访问用户': 2923, '下单用户': 2623, '下单率': 0.76 },
        /// </summary>
        public List<object> Rows { get; set; }
    }
}