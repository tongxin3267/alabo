using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Query;
using Alabo.Extensions;
using MongoDB.Driver;
using System;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Alabo.Datas.Stores.Add.Mongo
{
    public abstract class SingleMongoStore<TEntity, TKey> : NoTrackingMongoStore<TEntity, TKey>,
        ISingleStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected SingleMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public TEntity FirstOrDefault()
        {
            return Collection.AsQueryable().FirstOrDefault();
        }

        public TEntity GetSingle(object id)
        {
            if (id.IsNullOrEmpty()) {
                return null;
            }

            return GetSingle(IdPredicate(id));
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.AsQueryable().FirstOrDefault(predicate);
        }

        public TEntity GetSingle(IExpressionQuery<TEntity> queryCriteria)
        {
            if (queryCriteria != null)
            {
                var resultList = queryCriteria.Execute(Collection.AsQueryable()).FirstOrDefault();
                return resultList;
            }

            return FirstOrDefault();
        }

        public TEntity GetSingleOrderBy(Expression<Func<TEntity, TKey>> keySelector)
        {
            return GetSingleOrderBy(keySelector, null);
        }

        public TEntity GetSingleOrderBy(Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<TEntity, bool>> predicate)
        {
            var queryCriteria = new ExpressionQuery<TEntity>();
            queryCriteria.OrderBy(keySelector);
            if (predicate != null) {
                queryCriteria.And(predicate);
            }

            var find = queryCriteria.Execute(Collection.AsQueryable());
            return find.FirstOrDefault();
        }

        public TEntity GetSingleOrderByDescending(Expression<Func<TEntity, TKey>> keySelector)
        {
            return GetSingleOrderByDescending(keySelector, null);
        }

        public TEntity GetSingleOrderByDescending(Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<TEntity, bool>> predicate)
        {
            var queryCriteria = new ExpressionQuery<TEntity>();
            queryCriteria.OrderByDescending(keySelector);
            if (predicate != null) {
                queryCriteria.And(predicate);
            }

            var find = queryCriteria.Execute(Collection.AsQueryable());
            return find.FirstOrDefault();
        }

        public TEntity LastOrDefault()
        {
            var find = GetSingleOrderByDescending(r => r.Id);
            return find;
        }
    }
}