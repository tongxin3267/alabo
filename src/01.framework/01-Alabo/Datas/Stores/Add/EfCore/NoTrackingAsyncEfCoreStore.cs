using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Alabo.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Add.EfCore
{
    public abstract class NoTrackingAsyncEfCoreStore<TEntity, TKey> : EfCoreStoreBase<TEntity, TKey>,
        INoTrackingAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected NoTrackingAsyncEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        ///     查找未跟踪单个实体
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="cancellationToken">取消令牌</param>
        public async Task<TEntity> FindByIdNoTrackingAsync(TKey id,
            CancellationToken cancellationToken = default)
        {
            var entities = await FindByIdsNoTrackingAsync(id);
            if (entities == null || entities.Count == 0) {
                return null;
            }

            return entities[0];
        }

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        public async Task<List<TEntity>> FindByIdsNoTrackingAsync(params TKey[] ids)
        {
            return await FindByIdsNoTrackingAsync((IEnumerable<TKey>)ids);
        }

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        /// <param name="cancellationToken">取消令牌</param>
        public async Task<List<TEntity>> FindByIdsNoTrackingAsync(IEnumerable<TKey> ids,
            CancellationToken cancellationToken = default)
        {
            if (ids == null) {
                return null;
            }

            return await FindAsNoTracking().Where(t => ids.Contains(t.Id)).ToListAsync(cancellationToken);
        }

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">逗号分隔的标识列表，范例："1,2"</param>
        public async Task<List<TEntity>> FindByIdsNoTrackingAsync(string ids)
        {
            var idList = Convert.ToList<TKey>(ids);
            return await FindByIdsNoTrackingAsync(idList);
        }
    }
}