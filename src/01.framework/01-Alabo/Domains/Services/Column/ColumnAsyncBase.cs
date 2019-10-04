using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Cache;
using System;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Column {

    public abstract class ColumnAsyncBase<TEntity, TKey> : CacheBase<TEntity, TKey>, IColumnAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        protected ColumnAsyncBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store) {
        }

        public Task<object> GetFieldValueAsync(object id, string field) {
            throw new NotImplementedException();
        }
    }
}