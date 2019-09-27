using Alabo.Domains.Entities;
using Alabo.Domains.Services.Report.Dtos;
using System.Collections.Generic;

namespace Alabo.Domains.Services.Report
{
    public interface IReportStore<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     查询按日期 count 统计
        /// </summary>
        List<AutoReport> GetCountReport(CountReportInput inputParas);

        PagedList<CountReportTable> GetCountTable(CountReportInput inputParas);

        List<AutoReport> GetSumReport(SumTableInput inputParas);

        PagedList<SumReportTable> GetSumReportTable(SumTableInput inputParas);

        /// <summary>
        ///     获取单条数据
        /// </summary>
        /// <returns></returns>
        decimal GetSingleReportData(SingleReportInput singleReportInput);
    }
}