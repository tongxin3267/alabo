using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;

namespace Alabo.Domains.Repositories
{
    /// <summary>
    ///     仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public abstract class RepositoryMongo<TEntity, TKey> : MongoStore<TEntity, TKey>, IRepository<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     初始化仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        protected RepositoryMongo(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}