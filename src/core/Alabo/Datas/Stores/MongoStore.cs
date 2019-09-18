using Alabo.Datas.Stores.Update.Mongo;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores
{
    /// <summary>
    ///     Mongodb数据库查询器
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class MongoStore<TEntity, TKey> : UpdateMongoStore<TEntity, TKey>, IStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        /// <summary>
        ///     初始化查询存储器
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        protected MongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}