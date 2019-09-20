using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Alabo.Datas.Stores.Column.EfCore;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores.Count.EfCore
{
    public abstract class CountAsyncEfCoreStore<TEntity, TKey> : ColumnEfCoreStore<TEntity, TKey>,
        ICountAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected CountAsyncEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        ///     查找数量
        /// </summary>
        /// <param name="predicate">条件</param>
        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) {
                return await Set.CountAsync();
            }

            return await Set.CountAsync(predicate);
        }

        /// <summary>
        ///     查找数量
        /// </summary>
        public async Task<long> CountAsync()
        {
            return await Set.CountAsync();
        }
    }
}