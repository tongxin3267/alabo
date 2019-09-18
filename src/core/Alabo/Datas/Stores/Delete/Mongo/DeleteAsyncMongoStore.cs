using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alabo.Datas.Stores.Count.Mongo;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores.Delete.Mongo
{
    public abstract class DeleteAsyncMongoStore<TEntity, TKey> : CountMongoStore<TEntity, TKey>,
        IDeleteAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected DeleteAsyncMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            var filter = ToFilter(IdPredicate(entity.Id));
            var result = await Collection.DeleteOneAsync(filter);
            if (result.DeletedCount > 0) {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(IEnumerable<TEntity> entities)
        {
            var ids = entities.Select(r => r.Id);
            var filter = ToFilter(r => ids.Contains(r.Id));
            var result = await Collection.DeleteManyAsync(filter);
            if (result.DeletedCount > 0) {
                return true;
            }

            return false;
        }
    }
}