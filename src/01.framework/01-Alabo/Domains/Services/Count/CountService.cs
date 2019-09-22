using System;
using System.Linq.Expressions;
using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;

namespace Alabo.Domains.Services.Count
{
    public abstract class CountService<TEntity, TKey> : CountAsyncService<TEntity, TKey>, ICount<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected CountService(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public long Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Store.Count(predicate);
        }

        public long Count()
        {
            return Store.Count();
        }
    }
}