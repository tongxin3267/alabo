using Alabo.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Alabo.Domains.Services.Delete {

    /// <summary>
    ///     删除操作
    /// </summary>
    public interface IDelete<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="ids">用逗号分隔的Id列表，范例："1,2"</param>
        void DeleteMany(string ids);

        /// <summary>
        ///     删除 单个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        bool Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     移除实体单个实体
        /// </summary>
        /// <param name="id">实体标识</param>
        bool Delete(TKey id);

        /// <summary>
        ///     移除实体单个实体
        /// </summary>
        /// <param name="id">实体标识</param>
        bool Delete(object id);

        /// <summary>
        ///     移除实体单个实体
        /// </summary>
        /// <param name="entity">实体</param>
        bool Delete(TEntity entity);

        /// <summary>
        ///     移除实体集合
        /// </summary>
        /// <param name="ids">实体编号集合</param>
        void DeleteMany(IEnumerable<TKey> ids);

        /// <summary>
        ///     移除实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void DeleteMany(IEnumerable<TEntity> entities);

        /// <summary>
        ///     移除实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void DeleteMany(Expression<Func<TEntity, bool>> predicates);

        /// <summary>
        ///     删除所有的数据
        /// </summary>
        void DeleteAll();
    }
}