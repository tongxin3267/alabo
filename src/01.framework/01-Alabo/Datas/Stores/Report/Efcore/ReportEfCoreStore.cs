using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alabo.Datas.Stores.Random.EfCore;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Services.Report;
using Alabo.Domains.Services.Report.Dtos;
using Alabo.Extensions;

namespace Alabo.Datas.Stores.Report.Efcore
{
    public abstract class ReportEfCoreStore<TEntity, TKey> : RandomEfCoreStore<TEntity, TKey>,
        IReportStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected ReportEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<AutoReport> GetCountReport(CountReportInput inputParas)
        {
            //标记待更新
            var StartTime = DateTime.Now;
            var EndTime = DateTime.Now;

            var queryList = QueryList(x => x.CreateTime >= StartTime && x.CreateTime <= EndTime);
            var rsList = ReportStoreCommons<TEntity, TKey>.GetCountReport(queryList, inputParas);

            return rsList;
        }

        public PagedList<CountReportTable> GetCountTable(CountReportInput inputParas)
        {
            //标记待更新
            var StartTime = DateTime.Now;
            var EndTime = DateTime.Now;

            var queryList = QueryList(x => x.CreateTime >= StartTime && x.CreateTime <= EndTime);
            var rsList = ReportStoreCommons<TEntity, TKey>.GetCountTable(queryList, inputParas);

            return rsList;
        }

        public List<AutoReport> GetSumReport(SumTableInput inputParas)
        {
            var queryList = QueryList(x => x.CreateTime >= inputParas.StartTime && x.CreateTime <= inputParas.EndTime);
            var rsList = ReportStoreCommons<TEntity, TKey>.GetSumReport(queryList, inputParas);

            return rsList;
        }

        public PagedList<SumReportTable> GetSumReportTable(SumTableInput inputParas)
        {
            Expression<Func<TEntity, bool>> predicate = x => x.CreateTime > DateTime.MinValue;
            if (inputParas.StartTime != null && inputParas.EndTime != null)
                predicate = predicate.And(x => x.CreateTime >= inputParas.StartTime);

            var queryList = QueryList(predicate);
            var rsList = ReportStoreCommons<TEntity, TKey>.GetSumReportTable(queryList, inputParas);

            return rsList;
        }

        public static IEnumerable<TEntity> WhereQuery(IEnumerable<TEntity> queryList, string columnName,
            string propertyValue)
        {
            return queryList.Where(m =>
            {
                return m.GetType().GetProperty(columnName).GetValue(m, null).ToString().StartsWith(propertyValue);
            });
        }

        public IEnumerable<TEntity> QueryList(Expression<Func<TEntity, bool>> predicate)
        {
            var rs = Set.AsQueryable().Where(predicate).ToList();

            return rs;
        }
    }
}