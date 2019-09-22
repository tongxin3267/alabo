using System.Collections.Generic;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores.Delete.EfCore
{
    public abstract class DeleteEfCoreStore<TEntity, TKey> : DeleteAsyncEfCoreStore<TEntity, TKey>,
        IDeleteStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected DeleteEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public bool Delete(TEntity entity)
        {
            if (entity == null) {
                return false;
            }

            UnitOfWork.Set<TEntity>().Remove(entity);
            var count = UnitOfWork.SaveChanges();
            if (count >= 0) {
                return true;
            }

            return false;
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null) {
                return;
            }

            UnitOfWork.Set<TEntity>().RemoveRange(entities);
            UnitOfWork.SaveChanges();
        }
    }
}