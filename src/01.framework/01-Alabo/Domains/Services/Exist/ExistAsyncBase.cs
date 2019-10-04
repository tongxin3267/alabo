using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Distinct;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Exist {

    public abstract class ExistAsyncBase<TEntity, TKey> : DistinctBase<TEntity, TKey>, IExistAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected ExistAsyncBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store) {
        }

        public async Task<bool> ExistsAsync(params TKey[] ids) {
            return await Store.ExistsAsync(r => ids.Contains(r.Id));
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate) {
            return await Store.ExistsAsync(predicate);
        }

        public async Task<bool> ExistsAsync(TKey id) {
            var predicate = IdPredicate(id);
            return await Store.ExistsAsync(predicate);
        }
    }
}