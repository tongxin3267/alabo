namespace Alabo.Domains.Services.Report.Dtos
{
    /// <summary>
    ///     CountReportTable
    /// </summary>
    public class CountReportTable
    {
        /// <summary>
        ///     表格名称
        /// </summary>
        public string Name { get; set; }

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

        /// <summary>
        ///     表格
        /// </summary>
        public CountReportItem AutoReportChart { get; set; }
    }
}