using Alabo.Datas.Stores;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;

namespace Alabo.Domains.Repositories
{
    /// <summary>
    ///     Interface IRepository
    /// </summary>
    public interface IRepository
    {
    }

    /// <summary>
    ///     仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public interface IRepository<TEntity, TKey> : IRepository, IStore<TEntity, TKey>
        where TEntity : class, IAggregateRoot, IKey<TKey>
    {
    }
}