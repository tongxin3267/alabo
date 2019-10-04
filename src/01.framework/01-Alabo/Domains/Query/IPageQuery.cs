using Alabo.Domains.Entities.Core;
using System.Linq;

namespace Alabo.Domains.Query {

    /// <summary>
    ///     分页查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPageQuery<T> : IOrderQuery<T> where T : class, IEntity {

        /// <summary>
        ///     Gets or sets a value indicating whether [enable paging].
        /// </summary>
        bool EnablePaging { get; set; }

        int PageSize { get; set; }

        int PageIndex { get; set; }

        int ExecuteCountQuery(IQueryable<T> query);
    }
}