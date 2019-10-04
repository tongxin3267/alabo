using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Delete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Dictionary {

    public abstract class DictionaryAsyncBaseService<TEntity, TKey> : DeleteBase<TEntity, TKey>,
        IDictionaryServiceAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        protected DictionaryAsyncBaseService(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork,
            store) {
        }

        public Task<TEntity> FindAsync(Dictionary<string, string> dictionary) {
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, string>> GetDictionaryAsync(object id) {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetDictionaryAsync(Expression<Func<TEntity, bool>> predicate) {
            throw new NotImplementedException();
        }
    }
}