using System.Collections.Generic;
using System.Threading.Tasks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;

namespace Alabo.Domains.Services.ById
{
    /// <summary>
    ///     获取指定标识实体
    /// </summary>
    public interface IGetByIdAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        Task<TDto> GetByIdAsync<TDto>(object id) where TDto : IResponse, new();

        /// <summary>
        ///     通过编号列表获取
        /// </summary>
        /// <param name="ids">用逗号分隔的Id列表，范例："1,2"</param>
        Task<List<TDto>> GetByIdsAsync<TDto>(string ids) where TDto : IResponse, new();

        /// <summary>
        ///     查找未跟踪单个实体
        /// </summary>
        /// <param name="id">标识</param>
        Task<TEntity> GetByIdNoTrackingAsync(TKey id);

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        Task<List<TEntity>> GetByIdsNoTrackingAsync(params TKey[] ids);

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        Task<List<TEntity>> GetByIdsNoTrackingAsync(IEnumerable<TKey> ids);

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">逗号分隔的标识列表，范例："1,2"</param>
        Task<List<TEntity>> GetByIdsNoTrackingAsync(string ids);
    }
}