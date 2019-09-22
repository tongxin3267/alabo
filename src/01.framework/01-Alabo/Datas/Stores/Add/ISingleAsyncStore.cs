using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores.Add
{
    /// <summary>
    ///     查找单个实体
    /// </summary>
    /// <typeparam name="TEntity">对象类型</typeparam>
    /// <typeparam name="TKey">对象标识类型</typeparam>
    public interface ISingleAsyncStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        /// <param name="id">实体标识</param>
        Task<TEntity> GetSingleAsync(object id);

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
    }
}