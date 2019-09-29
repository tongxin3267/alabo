using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Page;
using System;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Random
{
    public abstract class RandomAsyncBase<TEntity, TKey> : GetPageBase<TEntity, TKey>, IRandomAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        protected RandomAsyncBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public Task<TEntity> GetRandomAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}