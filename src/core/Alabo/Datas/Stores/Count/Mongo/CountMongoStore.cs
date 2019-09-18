using System;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores.Count.Mongo
{
    public abstract class CountMongoStore<TEntity, TKey> : CountAsyncMongoStore<TEntity, TKey>,
        ICountStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected CountMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public long Count(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) {
                return Collection.AsQueryable().LongCount();
            }

            return Collection.AsQueryable().LongCount(predicate);
        }

        public long Count()
        {
            return Collection.Count(FilterDefinition<TEntity>.Empty);
        }
    }
}