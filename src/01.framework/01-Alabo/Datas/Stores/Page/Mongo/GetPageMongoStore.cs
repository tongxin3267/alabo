using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Query;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;

namespace Alabo.Datas.Stores.Page.Mongo
{
    public abstract class GetPageMongoStore<TEntity, TKey> : GetPageAsyncMongoStore<TEntity, TKey>,
        IGetPageStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected GetPageMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public PagedList<TEntity> GetPagedList(IPageQuery<TEntity> queryCriteria)
        {
            var predicate = queryCriteria.BuildExpression();
            return GetPagedList(predicate, queryCriteria.PageSize, queryCriteria.PageIndex);
        }

        public PagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate, int pageSize, int pageIndex)
        {
            if (pageSize < 1) pageSize = 1;

            if (pageIndex < 1) pageIndex = 1;

            long totalCount;
            List<TEntity> resultList;
            if (predicate != null)
            {
                resultList = Collection.AsQueryable().Where(predicate)
                    .OrderByDescending(r => r.Id)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(CancellationToken.None);
                totalCount = Count(predicate);
            }
            else
            {
                resultList = Collection.AsQueryable()
                    .OrderByDescending(r => r.Id)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize).ToList(CancellationToken.None);
                totalCount = Count();
            }

            return PagedList<TEntity>.Create(resultList, totalCount, pageSize, pageIndex);
        }

        public long PageCount(int pageSize, Expression<Func<TEntity, bool>> predicate)
        {
            var count = Count(predicate);
            var pageCount = pageSize / count + 1;
            return pageCount;
        }

        public long PageCount(int pageSize)
        {
            var count = Count();
            var pageCount = pageSize / count + 1;
            return pageCount;
        }

        public IEnumerable<TEntity> GetListByPage(Expression<Func<TEntity, bool>> predicate, int pageSize,
            int pageIndex)
        {
            if (pageSize < 1) pageSize = 1;

            if (pageIndex < 1) pageIndex = 1;

            var query = Collection.AsQueryable();
            if (predicate != null) query = query.Where(predicate);

            var source = query
                .OrderBy(r => r.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).OrderByDescending(r => r.Id)
                .ToList();
            return source;
        }

        public IEnumerable<TEntity> GetListByPageDesc(Expression<Func<TEntity, bool>> predicate, int pageSize,
            int pageIndex)
        {
            if (pageSize < 1) pageSize = 1;

            if (pageIndex < 1) pageIndex = 1;

            var query = Collection.AsQueryable();
            if (predicate != null) query = query.Where(predicate);

            var source = query
                .OrderBy(r => r.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).OrderByDescending(r => r.Id)
                .ToList();
            return source;
        }
    }
}