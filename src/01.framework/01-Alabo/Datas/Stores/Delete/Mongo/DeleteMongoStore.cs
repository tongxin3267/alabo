using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.Datas.Stores.Delete.Mongo {

    public abstract class DeleteMongoStore<TEntity, TKey> : DeleteAsyncMongoStore<TEntity, TKey>,
        IDeleteStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity {

        protected DeleteMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public bool Delete(TEntity entity) {
            if (entity == null) {
                return false;
            }

            Collection.DeleteOne(ToFilter(entity.Id));
            var find = GetSingle(entity.Id);
            if (find == null) {
                return true;
            }

            return false;
        }

        public void Delete(IEnumerable<TEntity> entities) {
            if (entities.Any()) {
                var ids = entities.Select(r => r.Id);
                Collection.DeleteMany(r => ids.Contains(r.Id));
            }
        }
    }
}