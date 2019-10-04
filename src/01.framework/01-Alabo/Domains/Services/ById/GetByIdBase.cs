using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Alabo.Domains.Services.ById {

    public abstract class GetByIdBase<TEntity, TKey> : GetByIdAsyncBase<TEntity, TKey>, IGetById<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected GetByIdBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store) {
        }

        public TDto GetById<TDto>(object id) where TDto : IResponse, new() {
            var find = Store.GetSingle(id);
            return ToDto<TDto>(find);
        }

        public TEntity GetByIdNoTracking(TKey id) {
            return Store.FindByIdNoTracking(id);
        }

        public TEntity GetByIdNoTracking(object id) {
            return Store.FindByIdNoTracking((TKey)id);
        }

        public TEntity GetByIdNoTracking(Expression<Func<TEntity, bool>> predicate) {
            return Store.FindByIdNoTracking(predicate);
        }

        public IEnumerable<TDto> GetByIds<TDto>(string ids) where TDto : IResponse, new() {
            var finds = GetList(ids);
            return ToDto<TDto>(finds).ToList();
        }

        public IEnumerable<TEntity> GetByIdsNoTracking(params TKey[] ids) {
            return Store.FindByIdsNoTracking(ids);
        }

        public IEnumerable<TEntity> GetByIdsNoTracking(IEnumerable<TKey> ids) {
            return Store.FindByIdsNoTracking(ids);
        }

        public IEnumerable<TEntity> GetByIdsNoTracking(string ids) {
            return Store.FindByIdsNoTracking(ids);
        }
    }
}