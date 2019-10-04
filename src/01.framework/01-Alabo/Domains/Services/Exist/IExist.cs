using Alabo.Domains.Entities;
using System;
using System.Linq.Expressions;

namespace Alabo.Domains.Services.Exist {

    public interface IExist<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     判断实体是否存在
        /// </summary>
        /// <param name="ids">实体标识集合</param>
        bool Exists(params TKey[] ids);

        /// <summary>
        ///     判断是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        bool Exists(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     判断除Id以外的记录，是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="id">排除Id</param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> predicate, TKey id);

        /// <summary>
        ///     判断实体是否存在
        /// </summary>
        /// <param name="id">实体标识集合</param>
        bool Exists(TKey id);

        /// <summary>
        ///     是否存在数据
        /// </summary>
        bool Exists();
    }
}