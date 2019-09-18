using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Domains.Services.Single;
using Alabo.Linq;

namespace Alabo.Domains.Services.List
{
    public abstract class GetListBaseAsync<TEntity, TKey> : SingleAsyncBase<TEntity, TKey>, IGetListAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected GetListBaseAsync(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public async Task<List<TDto>> GetAllAsync<TDto>() where TDto : IResponse, new()
        {
            var list = await GetListAsync();
            return ToDto<TDto>(list).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync()
        {
            return await Store.GetListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Store.GetListAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Dictionary<string, string> dictionary)
        {
            var predicate = LinqHelper.DictionaryToLinq<TEntity>(dictionary);
            return await GetListAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(IExpressionQuery<TEntity> query)
        {
            // return await Store.GetList(query).ToList();
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(IEnumerable<TKey> ids)
        {
            return await GetListAsync(r => ids.Contains(r.Id));
        }

        public async Task<List<TEntity>> GetListNoTrackingAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }
    }
}