using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Convert = Alabo.Helpers.Convert;

namespace Alabo.Datas.Stores.Add.Mongo {

    public abstract class NoTrackingAsyncMongoStore<TEntity, TKey> : MongoCoreStoreBase<TEntity, TKey>,
        INoTrackingAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity {

        protected NoTrackingAsyncMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public async Task<TEntity> FindByIdNoTrackingAsync(TKey id,
            CancellationToken cancellationToken = default) {
            var entities = await FindByIdsNoTrackingAsync(id);
            if (entities == null || entities.Count == 0) {
                return null;
            }

            return entities[0];
        }

        public async Task<List<TEntity>> FindByIdsNoTrackingAsync(params TKey[] ids) {
            return await FindByIdsNoTrackingAsync((IEnumerable<TKey>)ids);
        }

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">逗号分隔的标识列表，范例："1,2"</param>
        public async Task<List<TEntity>> FindByIdsNoTrackingAsync(string ids) {
            var idList = Convert.ToList<TKey>(ids);
            return await FindByIdsNoTrackingAsync(idList);
        }

        public Task<List<TEntity>> FindByIdsNoTrackingAsync(IEnumerable<TKey> ids,
            CancellationToken cancellationToken = default) {
            throw new NotImplementedException();
        }
    }
}