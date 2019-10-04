using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Domains.Services.Max;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Page {

    public abstract class GetPageAsyncBase<TEntity, TKey> : MaxService<TEntity, TKey>, IGetPageAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        protected GetPageAsyncBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store) {
        }

        public Task<PagedList<TEntity>> GetPagedListAsync(object paramater, Dictionary<string, string> dictionary) {
            throw new NotImplementedException();
        }

        public Task<PagedList<TEntity>> GetPagedListAsync(object paramater) {
            throw new NotImplementedException();
        }

        public Task<PagedList<TOutput>> GetPagedListAsync<TOutput>(object paramater,
            Expression<Func<TEntity, bool>> predicate = null) {
            throw new NotImplementedException();
        }

        public Task<PagedList<TEntity>> GetPagedListAsync(object paramater, Type searchView,
            Expression<Func<TEntity, bool>> predicate = null) {
            throw new NotImplementedException();
        }

        public Task<PagedList<TEntity>> GetPagedListAsync(IPageQuery<TEntity> query) {
            throw new NotImplementedException();
        }

        public Task<PagedList<TEntity>> GetPagedListAsync(object parmater, Expression<Func<TEntity, bool>> predicate) {
            throw new NotImplementedException();
        }
    }
}