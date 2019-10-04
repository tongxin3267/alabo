using Alabo.Domains.Entities.Core;
using System;
using System.Linq.Expressions;

namespace Alabo.Datas.Stores.Exist {

    /// <summary>
    ///     通过标识判断是否存在
    /// </summary>
    /// <typeparam name="TEntity">对象类型</typeparam>
    /// <typeparam name="TKey">对象标识类型</typeparam>
    public interface IExistsStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity {

        /// <summary>
        ///     判断是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        bool Exists(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     是否存在数据
        /// </summary>
        bool Exists();
    }
}