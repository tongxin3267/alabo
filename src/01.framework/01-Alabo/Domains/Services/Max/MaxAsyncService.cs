using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Exist;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Max {

    public abstract class MaxAsyncService<TEntity, TKey> : ExistBase<TEntity, TKey>, IMaxAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        protected MaxAsyncService(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store) {
        }

        public Task<TEntity> MaxAsync() {
            throw new NotImplementedException();
        }

        public Task<TEntity> MaxAsync(Expression<Func<TEntity, bool>> predicate) {
            throw new NotImplementedException();
        }

        public Task<long> MaxIdAsync() {
            throw new NotImplementedException();
        }
    }
}