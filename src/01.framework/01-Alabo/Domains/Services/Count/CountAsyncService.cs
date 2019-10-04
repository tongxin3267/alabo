using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Column;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Count {

    public abstract class CountAsyncService<TEntity, TKey> : ColumnBase<TEntity, TKey>, ICountAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected CountAsyncService(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store) {
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate) {
            return await Store.CountAsync(predicate);
        }

        public async Task<long> CountAsync() {
            return await Store.CountAsync();
        }
    }
}