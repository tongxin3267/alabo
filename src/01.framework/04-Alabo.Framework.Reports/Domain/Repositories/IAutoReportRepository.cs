using System.Collections.Generic;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services.Report;
using Alabo.Framework.Reports.Domain.Entities;
using Alabo.UI.Design.AutoReports;
using Alabo.UI.Design.AutoReports.Dtos;
using MongoDB.Bson;

namespace Alabo.Framework.Reports.Domain.Repositories
{
    /// <summary>
    ///     数据统计
    /// </summary>
    public interface IAutoReportRepository : IRepository<Report, ObjectId>
    {
        /// <summary>
        ///     按天统计数据，只支持Sql数据库
        /// </summary>
        /// <param name="countReportInput"></param>
        /// <returns></returns>
        List<object> GetDayCountReport(CountReportInput countReportInput);

        /// <summary>
        ///     按天数根据状态 统计数量 扩展
        /// </summary>
        /// <param name="countReport"></param>
        List<AutoReport> GetDayCountReportByFiled(CountReportInput countReport);

        /// <summary>
        ///     根据字段 日期 统计报表
        /// </summary>
        /// <param name="countReport"></param>
        /// <param name="EnumName"></param>
        /// <returns></returns>
        List<AutoReport> GetReportFormWithField(CountReportInput countReport, string EnumName);

        /// <summary>
        ///     根据日期 状态 统计报表表格
        /// </summary>
        /// <param name="countReport"></param>
        /// <returns></returns>
        List<object> GetDayCountReportTableByFiled(CountReportInput countReport);

        /// <summary>
        ///     根据日期 状态 统计报表表格 升级
        /// </summary>
        /// <param name="countReport"></param>
        /// <param name="EnumName"></param>
        /// <returns></returns>
        List<CountReportTable> GetCountReportTableWithField(CountReportInput countReport, string EnumName);

        /// <summary>
        ///     根据字段求和 统计报表
        /// </summary>
        /// <param name="sumReportInput"></param>
        /// <returns></returns>
        List<AutoReport> GetSumReport(SumReportInput sumReportInput);

        /// <summary>
        ///     按天统计实体增加数据,输出表格形式
        /// </summary>
        /// <param name="sumReportInpu"></param>
        /// <returns></returns>
        List<SumReportTable> GetSumReportTable(SumReportInput sumReportInpu, string EnumName);

        /// <summary>
        ///     根据配置统计单一数字
        /// </summary>
        /// <param name="singleReportInput"></param>
        /// <returns></returns>
        decimal GetSingleData(SingleReportInput singleReportInput);
    }
}