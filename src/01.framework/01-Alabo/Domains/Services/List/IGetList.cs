using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Alabo.Domains.Services.List
{
    /// <summary>
    ///     获取全部数据
    /// </summary>
    public interface IGetList<TEntity, TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     获取全部
        /// </summary>
        IList<TDto> GetListToDto<TDto>() where TDto : IResponse, new();

        /// <summary>
        ///     获取数据的数据列表
        /// </summary>
        IList<TEntity> GetList();

        /// <summary>
        ///     根据条件查询数据列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     根据条件查询数据列表，同时获取扩展属性
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IList<TEntity> GetListExtension(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     根据条件查询数据列表
        /// </summary>
        /// <param name="dictionary">字典数据</param>
        IList<TEntity> GetList(Dictionary<string, string> dictionary);

        /// <summary>
        ///     根据条件查询数据列表
        /// </summary>
        /// <param name="query"></param>
        IList<TEntity> GetList(IExpressionQuery<TEntity> query);

        /// <summary>
        ///     根据Id列表查询
        /// </summary>
        /// <param name="ids">Id标识列表</param>
        IList<TEntity> GetList(IEnumerable<TKey> ids);

        /// <summary>
        ///     根据Id列表查询
        /// </summary>
        /// <param name="ids">Id标识列表</param>
        IList<TEntity> GetList(string ids);

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="predicate">条件</param>
        IList<TEntity> GetListNoTracking(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        ///     获取所有的Id列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        IList<TKey> GetIdList(Expression<Func<TEntity, bool>> predicate = null);
    }
}