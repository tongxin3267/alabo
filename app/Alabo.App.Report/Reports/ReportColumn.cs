using System;

namespace Alabo.App.Core.Reports {

    public class ReportColumn {

        /// <summary>
        ///     列对于数据名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     数据类型
        /// </summary>
        public Type DataType { get; set; }

        /// <summary>
        ///     格式化规则
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        ///     列显示名称
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     列描述文字信息
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        ///     列默认排序
        /// </summary>
        public int Order { get; set; }
    }
}