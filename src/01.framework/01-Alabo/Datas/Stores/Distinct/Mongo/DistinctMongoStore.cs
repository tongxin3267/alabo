using System;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores.Distinct.Mongo
{
    public abstract class DistinctMongoStore<TEntity, TKey> : DistinctAsyncMongoStore<TEntity, TKey>,
        IDistinctStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected DistinctMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public bool Distinct(string filedName)
        {
            throw new NotImplementedException();
        }
    }
}