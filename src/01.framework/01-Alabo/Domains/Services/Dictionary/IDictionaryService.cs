using Alabo.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Alabo.Domains.Services.Dictionary
{
    public interface IDictionaryService<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     获取实体的字典集合
        /// </summary>
        /// <param name="id">Id标识</param>
        Dictionary<string, string> GetDictionary(object id);

        /// <summary>
        ///     获取实体的字典集合
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        Dictionary<string, string> GetDictionary(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     根据条件查询数据列表
        /// </summary>
        /// <param name="dictionary">查询字符串，如:id=1</param>
        TEntity Find(Dictionary<string, string> dictionary);

        /// <summary>
        ///     获取
        /// </summary>
        /// <returns></returns>
        IList<KeyValue> GetKeyValue(Expression<Func<TEntity, bool>> predicate);
    }
}