using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alabo.Datas.Stores.Exist.EfCore;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Microsoft.EntityFrameworkCore;

namespace Alabo.Datas.Stores.Max.EfCore
{
    public abstract class MaxAsyncEfCoreStore<TEntity, TKey> : ExistsEfCoreStore<TEntity, TKey>,
        IMaxAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected MaxAsyncEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<TEntity> MaxAsync()
        {
            var query = RepositoryContext.Query<TEntity>();
            return await query.MaxAsync();
        }

        public Task<TEntity> MaxAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}