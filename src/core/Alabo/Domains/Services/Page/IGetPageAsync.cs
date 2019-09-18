using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;

namespace Alabo.Domains.Services.Page
{
    public interface IGetPageAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        Task<PagedList<TEntity>> GetPagedListAsync(object paramater, Dictionary<string, string> dictionary);

        /// <summary>
        ///     通过object参数
        ///     获取分页
        /// </summary>
        /// <param name="paramater"></param>
        Task<PagedList<TEntity>> GetPagedListAsync(object paramater);

        /// <summary>
        ///     获取分页，通过AutoMapping转换成对象
        /// </summary>
        /// <param name="paramater">The paramater.</param>
        /// <param name="predicate">查询条件</param>
        Task<PagedList<TOutput>> GetPagedListAsync<TOutput>(object paramater,
            Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        ///     Gets the paged list.
        /// </summary>
        /// <param name="paramater">The paramater.</param>
        /// <param name="searchView">The search view.</param>
        /// <param name="predicate">查询条件</param>
        Task<PagedList<TEntity>> GetPagedListAsync(object paramater, Type searchView,
            Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// </summary>
        /// <param name="query"></param>
        Task<PagedList<TEntity>> GetPagedListAsync(IPageQuery<TEntity> query);

        /// <summary>
        ///     分页查询
        /// </summary>
        /// <param name="parmater">Url参数</param>
        /// <param name="predicate">后台lamada表达式.</param>
        Task<PagedList<TEntity>> GetPagedListAsync(object parmater, Expression<Func<TEntity, bool>> predicate);
    }
}