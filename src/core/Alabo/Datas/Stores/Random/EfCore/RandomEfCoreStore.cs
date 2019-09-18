using System;
using System.Linq;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores.Random.EfCore
{
    public abstract class RandomEfCoreStore<TEntity, TKey> : RandomAsyncEfCoreStore<TEntity, TKey>,
        IRandomStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected RandomEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        ///     随机查询
        /// </summary>
        public TEntity GetRandom()
        {
            return ToQueryable().Where(x => true).OrderBy(a => Guid.NewGuid()).Take(1).FirstOrDefault();
        }
    }
}