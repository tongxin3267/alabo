using System.Collections.Generic;

namespace Alabo.UI.Design.AutoReports.Dtos {

    /// <summary>
    ///     表格
    /// </summary>
    public class CountReportItem {

        /// <summary>
        ///     当前页记录数
        /// </summary>
        public long CurrentSize { get; set; }

        /// <summary>
        ///     总记录数 private
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        ///     当前页 private
        /// </summary>
        public long PageIndex { get; set; }

        /// <summary>
        ///     每页记录数 private
        /// </summary>

        public long PageSize { get; set; }

        /// <summary>
        ///     总页数
        /// </summary>
        public long PageCount { get; set; }

        /// <summary>
        ///     表格列
        /// </summary>
        public List<Columns> Columns { get; set; }

        /// <summary>
        ///     表格行
        /// </summary>
        public List<object> Rows { get; set; }
    }

    public class Columns {
        public string name { get; set; }
        public string type { get; set; }
    }
}