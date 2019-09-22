using System.Collections.Generic;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.UI.AutoReports;
using Alabo.UI.AutoReports.Dtos;

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