using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Trees;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Tree {

    public interface ITreeAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     通过标识查找列表
        /// </summary>
        /// <param name="ids">标识列表</param>
        Task<List<TDto>> FindByIdsAsync<TDto>(string ids) where TDto : class, IResponse, ITreeNode, new();

        /// <summary>
        ///     启用
        /// </summary>
        /// <param name="ids">标识列表</param>
        Task EnableAsync(string ids);

        /// <summary>
        ///     冻结
        /// </summary>
        /// <param name="ids">标识列表</param>
        Task DisableAsync(string ids);

        /// <summary>
        ///     交换排序
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="swapId">目标标识</param>
        Task SwapSortAsync(Guid id, Guid swapId);

        /// <summary>
        ///     修正排序
        /// </summary>
        /// <param name="parameter">查询参数</param>
        Task FixSortIdAsync<TQueryParameter>(TQueryParameter parameter);

        /// <summary>
        ///     生成排序号
        /// </summary>
        /// <param name="parentId">父标识</param>
        Task<int> GenerateSortIdAsync<TParentId>(TParentId parentId);

        /// <summary>
        ///     获取全部下级实体
        /// </summary>
        /// <param name="parent">父实体</param>
        Task<List<TEntity>> GetAllChildrenAsync(TEntity parent);

        /// <summary>
        ///     更新实体及所有下级节点路径
        /// </summary>
        /// <param name="entity">实体</param>
        Task UpdatePathAsync(TEntity entity);
    }
}