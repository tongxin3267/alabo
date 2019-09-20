using Alabo.Web.Mvc.Attributes;

namespace Alabo.UI.AutoReports.Enums
{
    /// <summary>
    ///     图表, 类型参考自 https://v-charts.js.org/#/line
    /// </summary>
    [ClassProperty(Name = "图表类型")]
    public enum ReportChartType
    {
        /// <summary>
        ///     折线图
        /// </summary>
        Line = 0,

        /// <summary>
        ///     柱状图
        /// </summary>
        Histogram = 1,

        /// <summary>
        ///     条形图
        /// </summary>
        Bar = 2,

        /// <summary>
        ///     饼图
        /// </summary>
        Pie = 3,

        /// <summary>
        ///     环图
        /// </summary>
        Ring = 4,

        /// <summary>
        ///     瀑布图
        /// </summary>
        Waterfall = 5,

        /// <summary>
        ///     漏斗图
        /// </summary>
        Funnel = 6,

        /// <summary>
        ///     雷达图
        /// </summary>
        Radar = 7,

        /// <summary>
        ///     地图
        /// </summary>
        Map = 8,

        /// <summary>
        ///     桑基图
        /// </summary>
        Sankey = 9,

        /// <summary>
        ///     热力图
        /// </summary>
        Heatmap = 10,

        /// <summary>
        ///     散点图
        /// </summary>
        Scatter = 11,

        /// <summary>
        ///     K线图
        /// </summary>
        Candle = 12,

        /// <summary>
        ///     仪表盘
        /// </summary>
        Gauge = 13,

        /// <summary>
        ///     树图
        /// </summary>
        Tree = 14,

        /// <summary>
        ///     水球图
        /// </summary>
        Liquidfill = 15,

        /// <summary>
        ///     词云图
        /// </summary>
        Wordcloud = 16
    }
}