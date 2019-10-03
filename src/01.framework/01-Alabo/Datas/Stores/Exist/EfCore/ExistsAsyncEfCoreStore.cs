using Alabo.Datas.Stores.Distinct.EfCore;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Exist.EfCore
{
    public abstract class ExistsAsyncEfCoreStore<TEntity, TKey> : DistinctEfCoreStore<TEntity, TKey>,
        IExistsAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected ExistsAsyncEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        ///     判断是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) {
                return false;
            }

            return await Set.AnyAsync(predicate);
        }
    }
}