using System.Collections.Generic;

namespace Alabo.Framework.Reports.Dtos {

    /// <summary>
    /// 输入报表表格格式
    /// </summary>
    public class CountTableOutput {

        /// <summary>
        /// 日期格式
        ///格式：2019-6-26
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public long Count { get; set; }

        /// <summary>
        /// 比例
        /// </summary>
        public string Rate { get; set; }

        public List<CountTableOutputItems> CountTableOutputItem { get; set; }
    }

    public class CountTableOutputItems {
        public string Name { get; set; }
        public string TempName { get; set; }
        public string Value { get; set; }
    }
}