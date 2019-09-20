using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores.Add
{
    public interface IGetListAsyncStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        /// <summary>
        ///     获取数据的数据列表
        /// </summary>
        Task<IEnumerable<TEntity>> GetListAsync();

        /// <summary>
        ///     根据条件查询数据列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
    }
}