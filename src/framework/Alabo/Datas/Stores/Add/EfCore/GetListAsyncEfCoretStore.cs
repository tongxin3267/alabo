using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores.Add.EfCore
{
    public abstract class GetListAsyncEfCoretStore<TEntity, TKey> : GetListEfCoreStore<TEntity, TKey>,
        IGetListAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        /// <summary>
        ///     初始化查询存储器
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        protected GetListAsyncEfCoretStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<TEntity>> GetListAsync()
        {
            return await ToQueryable().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) {
                return await Set.ToListAsync();
            }

            return await ToQueryable(predicate).ToListAsync();
        }
    }
}