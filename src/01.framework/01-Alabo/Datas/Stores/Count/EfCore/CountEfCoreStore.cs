using System;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores.Count.EfCore
{
    public abstract class CountEfCoreStore<TEntity, TKey> : CountAsyncEfCoreStore<TEntity, TKey>,
        ICountStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected CountEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        ///     查找数量
        /// </summary>
        /// <param name="predicate">条件</param>
        public long Count(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) return Set.Count();

            return Set.Count(predicate);
        }

        /// <summary>
        ///     查找数量
        /// </summary>
        public long Count()
        {
            return Set.Count();
        }
    }
}