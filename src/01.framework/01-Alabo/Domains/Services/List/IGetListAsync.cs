using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.List
{
    /// <summary>
    ///     获取全部数据
    /// </summary>
    public interface IGetListAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        Task<List<TDto>> GetAllAsync<TDto>() where TDto : IResponse, new();

        /// <summary>
        ///     获取数据的数据列表
        /// </summary>
        Task<IEnumerable<TEntity>> GetListAsync();

        /// <summary>
        ///     根据条件查询数据列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     根据条件查询数据列表
        /// </summary>
        /// <param name="dictionary">字典数据</param>
        Task<IEnumerable<TEntity>> GetListAsync(Dictionary<string, string> dictionary);

        /// <summary>
        ///     根据条件查询数据列表
        /// </summary>
        /// <param name="query"></param>
        Task<IEnumerable<TEntity>> GetListAsync(IExpressionQuery<TEntity> query);

        /// <summary>
        ///     根据Id列表查询
        /// </summary>
        /// <param name="ids">Id标识列表</param>
        Task<IEnumerable<TEntity>> GetListAsync(IEnumerable<TKey> ids);

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="predicate">条件</param>
        Task<List<TEntity>> GetListNoTrackingAsync(Expression<Func<TEntity, bool>> predicate = null);
    }
}