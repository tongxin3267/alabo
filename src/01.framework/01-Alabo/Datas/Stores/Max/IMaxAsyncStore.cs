using Alabo.Domains.Entities.Core;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Max
{
    public interface IMaxAsyncStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
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