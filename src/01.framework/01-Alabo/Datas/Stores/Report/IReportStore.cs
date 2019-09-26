using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Services.Report;
using Alabo.Domains.Services.Report.Dtos;
using System.Collections.Generic;

namespace Alabo.Datas.Stores.Report
{
    public interface IReportStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        List<AutoReport> GetCountReport(CountReportInput inputParas);

        PagedList<CountReportTable> GetCountTable(CountReportInput inputParas);

        List<AutoReport> GetSumReport(SumTableInput inputParas);

        PagedList<SumReportTable> GetSumReportTable(SumTableInput inputParas);
    }
}