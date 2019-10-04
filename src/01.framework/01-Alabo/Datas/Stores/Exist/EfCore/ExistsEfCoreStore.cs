using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Alabo.Datas.Stores.Exist.EfCore {

    public abstract class ExistsEfCoreStore<TEntity, TKey> : ExistsAsyncEfCoreStore<TEntity, TKey>,
        IExistsStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity {

        protected ExistsEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// <summary>
        ///     判断是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        public bool Exists(Expression<Func<TEntity, bool>> predicate) {
            if (predicate == null) {
                return false;
            }

            return Set.Any(predicate);
        }

        public bool Exists() {
            return Set.Any();
        }
    }
}