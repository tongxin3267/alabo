using Alabo.Domains.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Single {

    public interface ISingleAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <param name="id">实体标识</param>
        Task<TEntity> GetSingleAsync(object id);

        /// <param name="id">实体标识</param>
        Task<TEntity> GetSingleAsync(TKey id);

        /// <summary>
        ///     获取默认的一条数据
        /// </summary>
        Task<TEntity> FirstOrDefaultAsync();

        /// <summary>
        ///     获取最后默认的一条数据
        /// </summary>
        Task<TEntity> LastOrDefaultAsync();

        /// <summary>
        ///     查询单条记录
        /// </summary>
        /// <param name="predicate">查询条件</param>
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     下一条记录
        ///     如果不存在返回为null
        ///     Nexts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        Task<TEntity> NextAsync(TEntity model);

        /// <summary>
        ///     Prexes the specified model.
        ///     上一条记录，如果不存在返回为空
        /// </summary>
        /// <param name="model">The model.</param>
        Task<TEntity> PrexAsync(TEntity model);
    }
}