using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using System;
using System.Linq.Expressions;

namespace Alabo.Datas.Stores.Max.EfCore
{
    public abstract class MaxEfCoreStore<TEntity, TKey> : MaxAsyncEfCoreStore<TEntity, TKey>, IMaxStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected MaxEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public TEntity Max()
        {
            var maxIntance = GetSingleOrderByDescending(r => r.Id); // 按Id降序排列
            return maxIntance;
        }

        public TEntity Max(Expression<Func<TEntity, bool>> predicate)
        {
            var maxIntance = GetSingleOrderByDescending(r => r.Id, predicate); // 按Id降序排列
            return maxIntance;
        }
    }
}