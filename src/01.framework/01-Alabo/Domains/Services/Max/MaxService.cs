using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using System;
using System.Linq.Expressions;

namespace Alabo.Domains.Services.Max {

    public abstract class MaxService<TEntity, TKey> : MaxAsyncService<TEntity, TKey>, IMax<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected MaxService(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store) {
        }

        public TEntity Max() {
            return Store.Max();
        }

        public TEntity Max(Expression<Func<TEntity, bool>> predicate) {
            return Store.Max(predicate);
        }

        public long MaxId() {
            if (typeof(TKey) == typeof(long)) {
                if (Max() == null) {
                    return 0;
                }

                return Max().Id.ConvertToLong();
            }

            throw new NotImplementedException("非long字段不支持MaxId服务");
        }
    }
}