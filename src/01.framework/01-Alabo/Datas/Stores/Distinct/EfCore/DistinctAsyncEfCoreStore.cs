using Alabo.Datas.Stores.Delete.EfCore;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using System;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Distinct.EfCore
{
    public abstract class DistinctAsyncEfCoreStore<TEntity, TKey> : DeleteEfCoreStore<TEntity, TKey>,
        IDistinctAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected DistinctAsyncEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<bool> DistinctAsync(string filedName)
        {
            throw new NotImplementedException();
        }
    }
}