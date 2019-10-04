using Alabo.Domains.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Alabo.Datas.Stores.Add {

    public interface INoTrackingStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity {

        /// <summary>
        ///     查找未跟踪单个实体
        /// </summary>
        /// <param name="id">标识</param>
        TEntity FindByIdNoTracking(TKey id);

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        List<TEntity> FindByIdsNoTracking(params TKey[] ids);

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        List<TEntity> FindByIdsNoTracking(IEnumerable<TKey> ids);

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">逗号分隔的标识列表，范例："1,2"</param>
        List<TEntity> FindByIdsNoTracking(string ids);

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="predicate">逗号分隔的标识列表，范例："1,2"</param>
        List<TEntity> FindByIdsNoTracking(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     查找未跟踪单个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        TEntity FindByIdNoTracking(Expression<Func<TEntity, bool>> predicate);
    }
}