using Alabo.Domains.Entities.Core;
using System;
using System.Linq.Expressions;

namespace Alabo.Datas.Stores.Max {

    public interface IMaxStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity {

        /// <summary>
        ///     获取最大值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        TEntity Max();

        /// <summary>
        ///     查询单条记录
        /// </summary>
        /// <param name="predicate">查询条件</param>
        TEntity Max(Expression<Func<TEntity, bool>> predicate);
    }
}