using Alabo.Datas.Stores.Update.EfCore;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores {

    public abstract class EfCoreStore<TEntity, TKey> : UpdateEfCoreStore<TEntity, TKey>, IStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity {

        /// <summary>
        ///     初始化查询存储器
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        protected EfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}