using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Domains.Entities;
using Alabo.RestfulApi;

namespace Alabo.Framework.Core.WebApis.Controller
{

    /// <summary>
    ///     通用Api接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class ApiBaseController<TEntity, TKey> : ApiBaseUserController<TEntity, TKey> where TEntity : class, IAggregateRoot<TEntity, TKey> {

        protected ApiBaseController() : base() {
        }
    }
}