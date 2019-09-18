using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Query;

namespace Alabo.Datas.Stores.Page
{
    public interface IGetPageAsyncStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        /// <summary>
        ///     分页查询
        /// </summary>
        /// <param name="query">分页查询</param>
        Task<PagedList<TEntity>> GetPagedListAsync(IPageQuery<TEntity> query);

        /// <summary>
        ///     分页查询
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        Task<PagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate, int pageSize,
            int pageIndex);
    }
}