using Alabo.Domains.Entities.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Delete
{
    public interface IDeleteAsyncStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        /// <summary>
        ///     移除实体集合
        /// </summary>
        Task<bool> DeleteAsync(TEntity entity);

        /// <summary>
        ///     移除实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        Task<bool> DeleteAsync(IEnumerable<TEntity> entities);
    }
}