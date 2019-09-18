namespace Alabo.UI.AutoReports.Dtos
{
    /// <summary>
    ///     报表表格
    /// </summary>
    public class SumReportTable
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
        ///     表格
        /// </summary>
        public SumReportTableItems SumReportTableItem { get; set; }
    }
}