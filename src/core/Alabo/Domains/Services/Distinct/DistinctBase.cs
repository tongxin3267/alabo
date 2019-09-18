using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;

namespace Alabo.Domains.Services.Distinct
{
    public abstract class DistinctBase<TEntity, TKey> : DistinctAsyncBase<TEntity, TKey>, IDistinct<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected DistinctBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public bool Distinct(string filedName)
        {
            return Store.Distinct(filedName);
        }
    }
}