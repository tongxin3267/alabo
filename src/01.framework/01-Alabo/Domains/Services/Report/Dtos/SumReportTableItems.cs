using System.Collections.Generic;

namespace Alabo.Domains.Services.Report.Dtos
{
    /// <summary>
    ///     表格
    /// </summary>
    public class SumReportTableItems
    {
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
        public List<SumColumns> Columns { get; set; }

        /// <summary>
        ///     表格行
        /// </summary>
        public List<object> Rows { get; set; }
    }

    /// <summary>
    ///     列
    /// </summary>
    public class SumColumns
    {
        public string name { get; set; }
        public string type { get; set; }
    }
}