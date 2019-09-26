using System;
using System.Linq;
using System.Linq.Expressions;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using MongoDB.Driver;

namespace Alabo.Datas.Stores.Max.Mongo
{
    public abstract class MaxMongoStore<TEntity, TKey> : MaxAsyncMongoStore<TEntity, TKey>, IMaxStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected MaxMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public TEntity Max()
        {
            return Collection.AsQueryable().Max();
        }

        public TEntity Max(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}