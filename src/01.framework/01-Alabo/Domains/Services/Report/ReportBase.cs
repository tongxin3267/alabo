using System.Collections.Generic;
using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Random;
using Alabo.Domains.Services.Report.Dtos;

namespace Alabo.Domains.Services.Report
{
    public abstract class ReportBase<TEntity, TKey> : RandomBase<TEntity, TKey>, IReportStore<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        protected ReportBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public List<AutoReport> GetCountReport(CountReportInput inputParas)
        {
            return Store.GetCountReport(inputParas);
        }

        public PagedList<CountReportTable> GetCountTable(CountReportInput inputParas)
        {
            return Store.GetCountTable(inputParas);
        }

        public decimal GetSingleReportData(SingleReportInput singleReportInput)
        {
            return 0m;
        }

        public List<AutoReport> GetSumReport(SumTableInput inputParas)
        {
            return Store.GetSumReport(inputParas);
        }

        public PagedList<SumReportTable> GetSumReportTable(SumTableInput inputParas)
        {
            return Store.GetSumReportTable(inputParas);
        }
    }
}