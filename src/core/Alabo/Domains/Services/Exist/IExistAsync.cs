using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alabo.Domains.Entities;

namespace Alabo.Domains.Services.Exist
{
    public interface IExistAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     判断实体是否存在
        /// </summary>
        /// <param name="ids">实体标识集合</param>
        Task<bool> ExistsAsync(params TKey[] ids);

        /// <summary>
        ///     判断是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     判断实体是否存在
        /// </summary>
        /// <param name="id">实体标识集合</param>
        Task<bool> ExistsAsync(TKey id);
    }
}