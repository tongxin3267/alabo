using Alabo.Domains.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Count {

    public interface ICountAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     统计数量
        /// </summary>
        /// <param name="predicate">查询条件</param>
        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     查找数量
        /// </summary>
        Task<long> CountAsync();
    }
}