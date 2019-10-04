using Alabo.Datas.Stores.Page.EfCore;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Random.EfCore {

    public abstract class RandomAsyncEfCoreStore<TEntity, TKey> : GetPageEfCoreStore<TEntity, TKey>,
        IRandomAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity {

        protected RandomAsyncEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// <summary>
        ///     异步随机查询
        /// </summary>
        public async Task<TEntity> GetRandomAsync() {
            return await Set.Where(x => true).OrderBy(a => Guid.NewGuid()).Take(1).FirstOrDefaultAsync();
        }
    }
}