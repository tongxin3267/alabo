using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alabo.Datas.Stores.Exist.Mongo;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Alabo.Datas.Stores.Max.Mongo
{
    public abstract class MaxAsyncMongoStore<TEntity, TKey> : ExistsMongoStore<TEntity, TKey>,
        IMaxAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected MaxAsyncMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<TEntity> MaxAsync()
        {
            return await Collection.AsQueryable().MaxAsync();
        }

        public Task<TEntity> MaxAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}