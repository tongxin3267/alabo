using System;
using System.Linq.Expressions;
using Alabo.Domains.Entities;

namespace Alabo.Domains.Services.Count
{
    public interface ICount<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     统计数量
        /// </summary>
        /// <param name="predicate">查询条件</param>
        long Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     统计数量
        /// </summary>
        long Count();
    }
}