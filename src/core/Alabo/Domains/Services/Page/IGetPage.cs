using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;

namespace Alabo.Domains.Services.Page
{
    /// <summary>
    ///     读取实体分页数据
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IGetPage<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     通过object参数
        ///     获取分页
        /// </summary>
        /// <param name="paramater"></param>
        /// <param name="dictionary">字典类型</param>
        PagedList<TEntity> GetPagedList(object paramater, Dictionary<string, string> dictionary);


        /// <summary>
        ///     通过object获取分页数据，转换成前台Api接口绑定对象
        ///     与前台x-table先对应
        /// </summary>
        /// <param name="paramater"></param>
        /// <returns></returns>
        PageResult<TEntity> GetApiPagedList(object paramater);


        /// <summary>
        ///     获取分页，通过AutoMapping转换成对象
        /// </summary>
        /// <param name="paramater">The paramater.</param>
        /// <param name="predicate">查询条件</param>
        PageResult<TOutput> GetApiPagedList<TOutput>(object paramater,
            Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        ///     通过object参数
        ///     获取分页
        /// </summary>
        /// <param name="paramater"></param>
        PagedList<TEntity> GetPagedList(object paramater);

        /// <summary>
        ///     获取分页，通过AutoMapping转换成对象
        /// </summary>
        /// <param name="paramater">The paramater.</param>
        /// <param name="predicate">查询条件</param>
        PagedList<TOutput> GetPagedList<TOutput>(object paramater, Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        ///     Gets the paged list.
        /// </summary>
        /// <param name="paramater">The paramater.</param>
        /// <param name="searchView">The search view.</param>
        /// <param name="predicate">查询条件</param>
        PagedList<TEntity> GetPagedList(object paramater, Type searchView,
            Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// </summary>
        /// <param name="query"></param>
        PagedList<TEntity> GetPagedList(IPageQuery<TEntity> query);

        /// <summary>
        ///     分页查询
        /// </summary>
        /// <param name="parmater">Url参数</param>
        /// <param name="predicate">后台lamada表达式.</param>
        PagedList<TEntity> GetPagedList(object parmater, Expression<Func<TEntity, bool>> predicate);

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
        IEnumerable<TEntity> GetListByPage(int pageSize, int pageIndex,
            Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        ///     通过分页方式获取数据，降序方式
        /// </summary>
        IEnumerable<TEntity> GetListByPageDesc(int pageSize, int pageIndex,
            Expression<Func<TEntity, bool>> predicate = null);
    }
}