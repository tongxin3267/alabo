using Alabo.UI.Design.AutoReports.Enums;
using System.Collections.Generic;

namespace Alabo.UI.Design.AutoReports
{
    /// <summary>
    ///     数据表数据格式
    /// </summary>
    public class AutoReprotData
    {
        /// <summary>
        ///     数据表类型
        /// </summary>
        public ReportDataType Type { get; set; }

        /// <summary>
        ///     数据列表
        /// </summary>
        public List<AutoReprotDataItem> List { get; set; }
    }

    public class AutoReprotDataItem
    {
        /// <summary>
        ///     数据名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     主数值：重点突出数字
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        ///     辅助数值：比如百分比
        /// </summary>
        public decimal SubValue { get; set; }

        /// <summary>
        ///     数据简介
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     主题颜色,比如背景颜色，或文字内容颜色等
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        ///     字体颜色,和主题颜色形成高对比，让展示更漂亮
        /// </summary>
        public string FontColor { get; set; }
    }
}