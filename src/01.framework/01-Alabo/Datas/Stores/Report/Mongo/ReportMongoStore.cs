using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using Alabo.Datas.Stores.Random.Mongo;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Services.Report;
using Alabo.Domains.Services.Report.Dtos;

namespace Alabo.Datas.Stores.Report.Mongo
{
    public abstract class ReportMongoStore<TEntity, TKey> : RandomMongoStore<TEntity, TKey>,
        IReportStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected ReportMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
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
            var queryList = QueryList(x => x.CreateTime >= inputParas.StartTime && x.CreateTime <= inputParas.EndTime);
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
            var rs = Collection.AsQueryable().Where(predicate).ToList();

            return rs;
        }
    }
}