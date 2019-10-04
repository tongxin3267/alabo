using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using System;

namespace Alabo.Datas.Stores.Distinct.EfCore {

    public abstract class DistinctEfCoreStore<TEntity, TKey> : DistinctAsyncEfCoreStore<TEntity, TKey>,
        IDistinctStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity {

        protected DistinctEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public bool Distinct(string filedName) {
            throw new NotImplementedException();
        }
    }
}