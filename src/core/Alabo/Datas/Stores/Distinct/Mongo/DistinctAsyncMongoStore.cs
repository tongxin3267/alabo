using System;
using System.Threading.Tasks;
using Alabo.Datas.Stores.Delete.Mongo;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores.Distinct.Mongo
{
    public abstract class DistinctAsyncMongoStore<TEntity, TKey> : DeleteMongoStore<TEntity, TKey>,
        IDistinctAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected DistinctAsyncMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<bool> DistinctAsync(string filedName)
        {
            throw new NotImplementedException();
        }
    }
}