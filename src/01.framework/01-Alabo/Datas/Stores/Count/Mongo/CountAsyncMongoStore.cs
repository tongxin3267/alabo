using Alabo.Datas.Stores.Column.Mongo;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Count.Mongo {

    public abstract class CountAsyncMongoStore<TEntity, TKey> : ColumnMongoStore<TEntity, TKey>,
        ICountAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity {

        protected CountAsyncMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate) {
            if (predicate == null) {
                return await Collection.AsQueryable().LongCountAsync();
            }

            return await Collection.AsQueryable().LongCountAsync(predicate);
        }

        public async Task<long> CountAsync() {
            return await Collection.AsQueryable().LongCountAsync();
        }
    }
}