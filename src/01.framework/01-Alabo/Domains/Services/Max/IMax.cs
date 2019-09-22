using System;
using System.Linq.Expressions;
using Alabo.Domains.Entities;

namespace Alabo.Domains.Services.Max
{
    public interface IMax<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     最大Id
        /// </summary>
        long MaxId();

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