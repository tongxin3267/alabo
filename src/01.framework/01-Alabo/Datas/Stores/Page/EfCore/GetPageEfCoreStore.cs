using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Query;
using Alabo.Mapping;

namespace Alabo.Datas.Stores.Page.EfCore
{
    public abstract class GetPageEfCoreStore<TEntity, TKey> : GetPageAsyncEfCoreStore<TEntity, TKey>,
        IGetPageStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected GetPageEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public PagedList<TEntity> GetPagedList(IPageQuery<TEntity> queryCriteria)
        {
            var query = UnitOfWork.Set<TEntity>();
            if (queryCriteria == null)
                return PagedList<TEntity>.Create(query.ToList(), query.Count(), query.Count(), 1);

            var count = queryCriteria.ExecuteCountQuery(query);
            var queryResult = queryCriteria.Execute(query);
            var resultList = queryResult.ToList();
            resultList.ForEach(r => { r = JsonMapping.ConvertToExtension(r); });
            return PagedList<TEntity>.Create(resultList, count, queryCriteria.PageSize, queryCriteria.PageIndex);
        }

        public PagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate, int pageSize, int pageIndex)
        {
            if (pageSize < 1) pageSize = 1;

            if (pageIndex < 1) pageIndex = 1;

            var source = ToQueryable(predicate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return PagedList<TEntity>.Create(source, Count(predicate), pageSize, pageIndex);
        }

        public long PageCount(int pageSize, Expression<Func<TEntity, bool>> predicate)
        {
            var count = Count(predicate);
            var pageCount = count / pageSize + 1;
            return pageCount;
        }

        public long PageCount(int pageSize)
        {
            var count = Count();
            var pageCount = count / pageSize + 1;
            return pageCount;
        }

        public IEnumerable<TEntity> GetListByPage(Expression<Func<TEntity, bool>> predicate, int pageSize,
            int pageIndex)
        {
            if (pageSize < 1) pageSize = 1;

            if (pageIndex < 1) pageIndex = 1;

            var source = ToQueryable(predicate)
                .OrderBy(r => r.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return source;
        }

        public IEnumerable<TEntity> GetListByPageDesc(Expression<Func<TEntity, bool>> predicate, int pageSize,
            int pageIndex)
        {
            if (pageSize < 1) pageSize = 1;

            if (pageIndex < 1) pageIndex = 1;

            var source = ToQueryable(predicate)
                .OrderByDescending(r => r.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return source;
        }
    }
}