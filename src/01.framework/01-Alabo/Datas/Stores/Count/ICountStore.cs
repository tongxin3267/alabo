using Alabo.Domains.Entities.Core;
using System;
using System.Linq.Expressions;

namespace Alabo.Datas.Stores.Count {

    /// <summary>
    ///     查找数量
    /// </summary>
    /// <typeparam name="TEntity">对象类型</typeparam>
    /// <typeparam name="TKey">对象标识类型</typeparam>
    public interface ICountStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity {

        /// <summary>
        ///     查找数量
        /// </summary>
        /// <param name="predicate">条件</param>
        long Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     统计数量
        /// </summary>
        long Count();
    }
}