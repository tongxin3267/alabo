using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Bulk;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.ById {

    public abstract class GetByIdAsyncBase<TEntity, TKey> : BulkBase<TEntity, TKey>, IGetByIdAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected GetByIdAsyncBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store) {
        }

        public Task<TDto> GetByIdAsync<TDto>(object id) where TDto : IResponse, new() {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdNoTrackingAsync(TKey id) {
            throw new NotImplementedException();
        }

        public Task<List<TDto>> GetByIdsAsync<TDto>(string ids) where TDto : IResponse, new() {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetByIdsNoTrackingAsync(params TKey[] ids) {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetByIdsNoTrackingAsync(IEnumerable<TKey> ids) {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetByIdsNoTrackingAsync(string ids) {
            throw new NotImplementedException();
        }
    }
}