using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores.Add.Mongo
{
    public class GetListAsyncMongoStore<TEntity, TKey> : GetListMongoStore<TEntity, TKey>,
        IGetListAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected GetListAsyncMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<TEntity>> GetListAsync()
        {
            return await Collection.AsQueryable().ToListAsync(CancellationToken.None);
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) {
                return await Collection.AsQueryable().ToListAsync(CancellationToken.None);
            }

            var list = Collection.AsQueryable().Where(predicate).ToListAsync(CancellationToken.None);
            return await list;
        }
    }
}