using System;
using System.Linq.Expressions;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using MongoDB.Driver;

namespace Alabo.Datas.Stores.Exist.Mongo
{
    public abstract class ExistsMongoStore<TEntity, TKey> : ExistsAsyncMongoStore<TEntity, TKey>,
        IExistsStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected ExistsMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            var find = GetSingle(predicate);
            if (find == null) return false;

            return true;
        }

        public bool Exists()
        {
            return Collection.AsQueryable().Any();
        }
    }
}