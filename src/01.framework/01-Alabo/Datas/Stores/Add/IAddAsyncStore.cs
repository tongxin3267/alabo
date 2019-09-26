using Alabo.Domains.Entities.Core;
using Alabo.Validations.Aspects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Add
{
    public interface IAddAsyncStore<in TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        /// <summary>
        ///     添加实体，异步
        /// </summary>
        /// <param name="entity">实体</param>
        Task<bool> AddSingleAsync([Valid] TEntity entity);

        /// <summary>
        ///     添加实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        Task AddManyAsync([Valid] IEnumerable<TEntity> entities);
    }
}