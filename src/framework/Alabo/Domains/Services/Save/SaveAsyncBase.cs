using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Report;

namespace Alabo.Domains.Services.Save
{
    public abstract class SaveAsyncBase<TEntity, TKey> : ReportBase<TEntity, TKey>, ISaveAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected SaveAsyncBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public async Task<bool> AddOrUpdateAsync(TEntity model, Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
            //return await Store.AddOrUpdateAsync(model, predicate);
        }

        public async Task<bool> AddOrUpdateAsync(TEntity model)
        {
            throw new NotImplementedException();
            //return await Store.AddOrUpdateAsync(model);
        }

        public async Task<bool> AddOrUpdateAsync(TEntity model, bool predicate)
        {
            throw new NotImplementedException();
            //return await Store.AddOrUpdateAsync(model, predicate);
        }

        public async Task SaveAsync<TRequest>(TRequest request) where TRequest : IRequest, IKey, new()
        {
            throw new NotImplementedException();
            //await Store.SaveAsync(request);
        }
    }
}