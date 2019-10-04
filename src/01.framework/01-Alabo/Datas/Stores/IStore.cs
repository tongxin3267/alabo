using Alabo.Datas.Stores.Add;
using Alabo.Datas.Stores.Column;
using Alabo.Datas.Stores.Count;
using Alabo.Datas.Stores.Delete;
using Alabo.Datas.Stores.Distinct;
using Alabo.Datas.Stores.Exist;
using Alabo.Datas.Stores.Max;
using Alabo.Datas.Stores.Page;
using Alabo.Datas.Stores.Random;
using Alabo.Datas.Stores.Report;
using Alabo.Datas.Stores.Update;
using Alabo.Dependency;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Repositories.EFCore;

namespace Alabo.Datas.Stores {

    /// <summary>
    ///     存储器
    /// </summary>
    public interface IStore : IScopeDependency {
    }

    /// <summary>
    ///     存储器
    /// </summary>
    /// <typeparam name="TEntity">对象类型</typeparam>
    /// <typeparam name="TKey">对象标识类型</typeparam>
    public interface IStore<TEntity, TKey> : IStore,
        IUpdateAsyncStore<TEntity, TKey>, IUpdateStore<TEntity, TKey>, IReportStore<TEntity, TKey>,
        IRandomStore<TEntity, TKey>, IRandomAsyncStore<TEntity, TKey>,
        IGetPageStore<TEntity, TKey>, IGetPageAsyncStore<TEntity, TKey>,
        IMaxStore<TEntity, TKey>, IMaxAsyncStore<TEntity, TKey>,
        IExistsStore<TEntity, TKey>, IExistsAsyncStore<TEntity, TKey>,
        IDistinctStore<TEntity, TKey>, IDistinctAsyncStore<TEntity, TKey>,
        IDeleteStore<TEntity, TKey>, IDeleteAsyncStore<TEntity, TKey>,
        ICountStore<TEntity, TKey>, ICountAsyncStore<TEntity, TKey>,
        IColumnStore<TEntity, TKey>, IColumnAsyncStore<TEntity, TKey>,
        INoTrackingStore<TEntity, TKey>, INoTrackingAsyncStore<TEntity, TKey>,
        IGetListStore<TEntity, TKey>, IGetListAsyncStore<TEntity, TKey>,
        ISingleStore<TEntity, TKey>, ISingleAsyncStore<TEntity, TKey>,
        IAddStore<TEntity, TKey>, IAddAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity {

        /// <summary>
        ///     Sql操作
        /// </summary>
        IRepositoryContext RepositoryContext { get; }
    }
}