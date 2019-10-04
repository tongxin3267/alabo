using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.Linq;
using Alabo.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Alabo.Domains.Services.List {

    public abstract class GetListBase<TEntity, TKey> : GetListBaseAsync<TEntity, TKey>, IGetList<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected GetListBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store) {
        }

        /// <summary>
        ///     获取全部
        /// </summary>
        public IList<TDto> GetListToDto<TDto>() where TDto : IResponse, new() {
            var list = GetList();
            return ToDto<TDto>(list).ToList();
        }

        public IList<TEntity> GetList() {
            var result = Store.GetList().ToList();
            // 扩展属性处理
            result.Foreach(r => { r = JsonMapping.ConvertToExtension(r); });
            return result;
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate) {
            var result = Store.GetList(predicate).ToList();
            // 扩展属性处理
            // result.Foreach(r => { r = JsonMapping.ConvertToExtension(r); });
            return result;
        }

        public IList<TEntity> GetListExtension(Expression<Func<TEntity, bool>> predicate) {
            var result = Store.GetList(predicate).ToList();
            // 扩展属性处理
            result.Foreach(r => { r = JsonMapping.ConvertToExtension(r); });
            return result;
        }

        public IList<TEntity> GetList(Dictionary<string, string> dictionary) {
            var predicate = LinqHelper.DictionaryToLinq<TEntity>(dictionary);
            return GetList(predicate);
        }

        public IList<TEntity> GetList(IExpressionQuery<TEntity> queryCriteria) {
            return Store.GetList(queryCriteria).ToList();
        }

        public IList<TEntity> GetList(IEnumerable<TKey> ids) {
            return GetList(r => ids.Contains(r.Id));
        }

        public IList<TEntity> GetList(string ids) {
            return GetList(IdsToList(ids));
        }

        public IList<TEntity> GetListNoTracking(Expression<Func<TEntity, bool>> predicate = null) {
            var result = Store.FindByIdsNoTracking(predicate).ToList();
            // 扩展属性处理
            result.Foreach(r => { r = JsonMapping.ConvertToExtension(r); });
            return result;
        }

        public IList<TKey> GetIdList(Expression<Func<TEntity, bool>> predicate = null) {
            return Store.GetIdList(predicate).ToList();
        }
    }
}