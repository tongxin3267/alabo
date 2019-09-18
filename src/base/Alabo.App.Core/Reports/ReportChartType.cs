using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Reports {

    [ClassProperty(Name = "统计图类型")]
    public enum ReportChartType {

        /// <summary>
        ///     柱状图
        ///     http://echarts.baidu.com/demo.html#bar-stack
        /// </summary>
        Bar = 1,

        /// <summary>
        ///     饼状图
        ///     参考样式http://echarts.baidu.com/demo.html#pie-simple
        /// </summary>
        Pie = 2,

        Line = 3
    }
}