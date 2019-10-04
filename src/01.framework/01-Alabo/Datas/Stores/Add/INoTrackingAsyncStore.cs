using Alabo.Domains.Entities.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Add {

    public interface INoTrackingAsyncStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity {

        /// <summary>
        ///     查找未跟踪单个实体
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="cancellationToken">取消令牌</param>
        Task<TEntity> FindByIdNoTrackingAsync(TKey id,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        Task<List<TEntity>> FindByIdsNoTrackingAsync(params TKey[] ids);

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        /// <param name="cancellationToken">取消令牌</param>
        Task<List<TEntity>> FindByIdsNoTrackingAsync(IEnumerable<TKey> ids,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">逗号分隔的标识列表，范例："1,2"</param>
        Task<List<TEntity>> FindByIdsNoTrackingAsync(string ids);
    }
}