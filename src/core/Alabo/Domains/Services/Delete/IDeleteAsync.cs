using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alabo.Domains.Entities;

namespace Alabo.Domains.Services.Delete
{
    /// <summary>
    ///     删除操作
    /// </summary>
    public interface IDeleteAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        Task DeleteManyAsync(string ids);

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="predicate">查询条件</param>
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     移除实体
        /// </summary>
        /// <param name="id">实体标识</param>
        Task<bool> DeleteAsync(TKey id);

        /// <summary>
        ///     移除实体
        /// </summary>
        /// <param name="entity">实体</param>
        Task<bool> DeleteAsync(TEntity entity);

        /// <summary>
        ///     移除实体集合
        /// </summary>
        /// <param name="ids">实体编号集合</param>
        Task DeleteManyAsync(IEnumerable<TKey> ids);

        /// <summary>
        ///     移除实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        Task DeleteManyAsync(IEnumerable<TEntity> entities);

        /// <summary>
        ///     删除所有的数据
        /// </summary>
        Task DeleteAllAsync();

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="predicate">查询条件</param>
        Task<bool> DeleteManyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}