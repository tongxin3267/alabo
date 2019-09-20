using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;

namespace Alabo.Domains.Services.Single
{
    public abstract class SingleAsyncBase<TEntity, TKey> : SingleBase<TEntity, TKey>, ISingleAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected SingleAsyncBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public Task<TEntity> FirstOrDefaultAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetSingleAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetSingleAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> LastOrDefaultAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> NextAsync(TEntity model)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> PrexAsync(TEntity model)
        {
            throw new NotImplementedException();
        }
    }
}