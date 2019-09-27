using Alabo.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Dictionary
{
    public interface IDictionaryServiceAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        Task<Dictionary<string, string>> GetDictionaryAsync(object id);

        /// <summary>
        ///     获取实体的字典集合
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        Task<TEntity> GetDictionaryAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     根据条件查询数据列表
        /// </summary>
        /// <param name="dictionary">查询字符串，如:id=1</param>
        Task<TEntity> FindAsync(Dictionary<string, string> dictionary);
    }
}