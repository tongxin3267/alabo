using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Add.Mongo {

    public abstract class SingleAsyncMongoStore<TEntity, TKey> : SingleMongoStore<TEntity, TKey>,
        ISingleAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity {

        protected SingleAsyncMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public async Task<TEntity> FirstOrDefaultAsync() {
            return await Collection.AsQueryable().FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetSingleAsync(object id) {
            return await GetSingleAsync(IdPredicate(id));
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate) {
            return await Collection.AsQueryable().FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> LastOrDefaultAsync() {
            return await Collection.AsQueryable().LastOrDefaultAsync();
        }
    }
}