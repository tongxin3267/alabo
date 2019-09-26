using Alabo.Datas.Stores.Distinct.Mongo;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Exist.Mongo
{
    public abstract class ExistsAsyncMongoStore<TEntity, TKey> : DistinctMongoStore<TEntity, TKey>,
        IExistsAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected ExistsAsyncMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var find = await GetSingleAsync(predicate);
            if (find == null) return false;

            return true;
        }
    }
}