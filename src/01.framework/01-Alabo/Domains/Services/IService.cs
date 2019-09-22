using Alabo.Cache;
using Alabo.Dependency;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services.Add;
using Alabo.Domains.Services.Attach;
using Alabo.Domains.Services.Bulk;
using Alabo.Domains.Services.ById;
using Alabo.Domains.Services.Cache;
using Alabo.Domains.Services.Column;
using Alabo.Domains.Services.Count;
using Alabo.Domains.Services.Delete;
using Alabo.Domains.Services.Dictionary;
using Alabo.Domains.Services.Distinct;
using Alabo.Domains.Services.Exist;
using Alabo.Domains.Services.List;
using Alabo.Domains.Services.Max;
using Alabo.Domains.Services.Page;
using Alabo.Domains.Services.Random;
using Alabo.Domains.Services.Report;
using Alabo.Domains.Services.Save;
using Alabo.Domains.Services.Single;
using Alabo.Domains.Services.Sql;
using Alabo.Domains.Services.Time;
using Alabo.Domains.Services.Tree;
using Alabo.Domains.Services.Update;
using Alabo.Domains.Services.View;

namespace Alabo.Domains.Services
{
    /// <summary>
    ///     应用服务
    /// </summary>
    public interface IService : IScopeDependency
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
        TService Resolve<TService>() where TService : IService;
    }

    /// <summary>
    ///     实体服务接口
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IService<TEntity, TKey> : IService, IUpdate<TEntity, TKey>, IUpdateAsync<TEntity, TKey>,
        IViewBase<TEntity, TKey>,
        ITreeAsync<TEntity, TKey>, ITree<TEntity, TKey>,
        ITime<TEntity, TKey>, IReportStore<TEntity, TKey>,
        INativeAsync<TEntity, TKey>, INative<TEntity, TKey>, ISingle<TEntity, TKey>, ISingleAsync<TEntity, TKey>,
        ISaveAsync<TEntity, TKey>, ISave<TEntity, TKey>,
        IRandomAsync<TEntity, TKey>, IRandom<TEntity, TKey>, IGetPage<TEntity, TKey>,
        IGetPageAsync<TEntity, TKey>, IMax<TEntity, TKey>, IMaxAsync<TEntity, TKey>,
        IGetList<TEntity, TKey>, IGetListAsync<TEntity, TKey>,
        IExistAsync<TEntity, TKey>, IExist<TEntity, TKey>,
        IDistinct<TEntity, TKey>, IDistinctAsync<TEntity, TKey>,
        IDictionaryService<TEntity, TKey>, IDictionaryServiceAsync<TEntity, TKey>,
        IDeleteAsync<TEntity, TKey>, IDelete<TEntity, TKey>,
        ICountAsync<TEntity, TKey>, ICount<TEntity, TKey>,
        IColumn<TEntity, TKey>, IColumnAsync<TEntity, TKey>,
        ICache<TEntity, TKey>, ICacheAsync<TEntity, TKey>,
        IGetByIdAsync<TEntity, TKey>, IGetById<TEntity, TKey>,
        IBulkAsync<TEntity, TKey>, IBulk<TEntity, TKey>,
        IAddAsync<TEntity, TKey>, IAdd<TEntity, TKey>,
        IAttach<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     日志记录
        /// </summary>
        /// <param name="content">日志内容，内容不能为空</param>
        /// <param name="level">日志级别</param>
        void Log(string content, LogsLevel level = LogsLevel.Information);
    }
}