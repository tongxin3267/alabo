using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Alabo.Domains.Services.ById
{
    /// <summary>
    ///     获取指定标识实体
    /// </summary>
    public interface IGetById<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     通过编号获取
        /// </summary>
        /// <param name="id">实体编号</param>
        TDto GetById<TDto>(object id) where TDto : IResponse, new();

        /// <summary>
        ///     通过编号列表获取
        /// </summary>
        /// <param name="ids">用逗号分隔的Id列表，范例："1,2"</param>
        IEnumerable<TDto> GetByIds<TDto>(string ids) where TDto : IResponse, new();

        /// <summary>
        ///     查找未跟踪单个实体
        /// </summary>
        /// <param name="id">标识</param>
        TEntity GetByIdNoTracking(TKey id);

        /// <summary>
        ///     查找未跟踪单个实体
        /// </summary>
        /// <param name="id">标识</param>
        TEntity GetByIdNoTracking(object id);

        /// <summary>
        ///     查找未跟踪单个实体
        /// </summary>
        /// <param name="predicate">标识</param>
        TEntity GetByIdNoTracking(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        IEnumerable<TEntity> GetByIdsNoTracking(params TKey[] ids);

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        IEnumerable<TEntity> GetByIdsNoTracking(IEnumerable<TKey> ids);

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">逗号分隔的标识列表，范例："1,2"</param>
        IEnumerable<TEntity> GetByIdsNoTracking(string ids);
    }
}