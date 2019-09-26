using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Alabo.Datas.Stores.Page
{
    public interface IGetPageStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        /// <summary>
        ///     分页查询
        /// </summary>
        /// <param name="query">分页查询</param>
        PagedList<TEntity> GetPagedList(IPageQuery<TEntity> query);

        /// <summary>
        ///     分页查询
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        PagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate, int pageSize, int pageIndex);

        /// <summary>
        ///     根据每页大小，获取总页数量
        /// </summary>
        /// <param name="pageSize">每页大小：示范值30</param>
        /// <param name="predicate">查询条件</param>
        long PageCount(int pageSize, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     根据每页大小，获取总页数量
        /// </summary>
        /// <param name="pageSize">每页大小：示范值30</param>
        long PageCount(int pageSize);

        /// <summary>
        ///     通过分页方式获取数据，升序方式
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        IEnumerable<TEntity> GetListByPage(Expression<Func<TEntity, bool>> predicate, int pageSize, int pageIndex);

        /// <summary>
        ///     通过分页方式获取数据，降序方式
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        IEnumerable<TEntity> GetListByPageDesc(Expression<Func<TEntity, bool>> predicate, int pageSize, int pageIndex);
    }
}