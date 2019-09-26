using Alabo.Domains.Entities.Core;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Count
{
    /// <summary>
    ///     查找数量
    /// </summary>
    /// <typeparam name="TEntity">对象类型</typeparam>
    /// <typeparam name="TKey">对象标识类型</typeparam>
    public interface ICountAsyncStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        /// <summary>
        ///     查找数量
        /// </summary>
        /// <param name="predicate">查询条件</param>
        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     查找数量
        /// </summary>
        Task<long> CountAsync();
    }
}