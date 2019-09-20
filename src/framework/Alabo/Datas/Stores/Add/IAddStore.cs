using System.Collections.Generic;
using Alabo.Domains.Entities.Core;
using Alabo.Validations.Aspects;

namespace Alabo.Datas.Stores.Add
{
    public interface IAddStore<in TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        /// <summary>
        ///     添加单个实体
        /// </summary>
        /// <param name="entity"></param>
        bool AddSingle([Valid] TEntity entity);

        /// <summary>
        ///     批量添加多个实体
        /// </summary>
        /// <param name="soucre"></param>
        void AddMany(IEnumerable<TEntity> soucre);
    }
}