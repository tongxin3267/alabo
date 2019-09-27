using Alabo.Datas.Stores.Count.EfCore;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Delete.EfCore
{
    public abstract class DeleteAsyncEfCoreStore<TEntity, TKey> : CountEfCoreStore<TEntity, TKey>,
        IDeleteAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected DeleteAsyncEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<bool> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}