using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Alabo.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Add.EfCore
{
    public class SingleAsyncEfCoreStore<TEntity, TKey> : SingleEfCoreStore<TEntity, TKey>,
        ISingleAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected SingleAsyncEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<TEntity> FirstOrDefaultAsync()
        {
            return await Set.FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetSingleAsync(object id)
        {
            if (id.SafeString().IsEmpty()) return null;

            return await Set.FindAsync(new[] { id }, default);
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Set.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> LastOrDefaultAsync()
        {
            return await Set.LastOrDefaultAsync();
        }
    }
}