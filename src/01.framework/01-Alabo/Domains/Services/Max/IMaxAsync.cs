using Alabo.Domains.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Max
{
    public interface IMaxAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        Task<long> MaxIdAsync();

        /// <summary>
        ///     获取最大值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        Task<TEntity> MaxAsync();

        /// <summary>
        ///     查询单条记录
        /// </summary>
        /// <param name="predicate">查询条件</param>
        Task<TEntity> MaxAsync(Expression<Func<TEntity, bool>> predicate);
    }
}