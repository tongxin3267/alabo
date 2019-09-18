using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Query;

namespace Alabo.Datas.Stores.Add.Mongo
{
    public abstract class GetListMongoStore<TEntity, TKey> : SingleAsyncMongoStore<TEntity, TKey>,
        IGetListStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected GetListMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<TEntity> GetList()
        {
            return ToQueryable().ToList();
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return ToQueryable(predicate)
                .OrderByDescending(r => r.Id)
                .ToList();
        }

        public IEnumerable<TEntity> GetList(IExpressionQuery<TEntity> queryCriteria)
        {
            if (queryCriteria != null)
            {
                var predicate = queryCriteria.BuildExpression();
                return GetList(predicate);
            }

            return GetList();
        }

        public IEnumerable<TKey> GetIdList(Expression<Func<TEntity, bool>> predicate = null)
        {
            var result = ToQueryable(predicate)
                .OrderByDescending(r => r.Id)
                .Select(r => r.Id).ToList();
            return result;
        }
    }
}