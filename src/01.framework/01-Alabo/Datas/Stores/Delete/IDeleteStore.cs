using Alabo.Domains.Entities.Core;
using System.Collections.Generic;

namespace Alabo.Datas.Stores.Delete
{
    public interface IDeleteStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        bool Delete(TEntity entity);

        /// <summary>
        ///     移除实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void Delete(IEnumerable<TEntity> entities);
    }
}