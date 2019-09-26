using Alabo.Cache;
using Alabo.Dependency;
using Alabo.Domains.Repositories;

namespace Alabo.Framework.Core.WebUis
{
    public interface IUI : IScopeDependency
    {
        /// <summary>
        ///     缓存
        /// </summary>
        IObjectCache ObjectCache { get; }

        /// <summary>
        ///     数据仓储
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        TRepository Repository<TRepository>() where TRepository : IRepository;

        /// <summary>
        ///     获取服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        TService Resolve<TService>();
    }
}