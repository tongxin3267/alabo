using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Count;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Delete
{
    public abstract class DeleteBaseAsync<TEntity, TKey> : CountService<TEntity, TKey>, IDeleteAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected DeleteBaseAsync(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public async Task DeleteAllAsync()
        {
            var list = await GetListAsync();
            await DeleteManyAsync(list);
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var find = await GetSingleAsync(predicate);
            return await Store.DeleteAsync(find);
        }

        public async Task<bool> DeleteAsync(TKey id)
        {
            var find = await GetSingleAsync(id);
            return await DeleteAsync(find);
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            return await DeleteAsync(entity);
        }

        public async Task DeleteManyAsync(string ids)
        {
            var find = await GetSingleAsync(ids);
            await DeleteAsync(find);
        }

        public async Task DeleteManyAsync(IEnumerable<TKey> ids)
        {
            var find = await GetListAsync(ids);
            await DeleteManyAsync(find);
        }

        public async Task DeleteManyAsync(IEnumerable<TEntity> entities)
        {
            await Store.DeleteAsync(entities);
        }

        public async Task<bool> DeleteManyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var list = await GetListAsync(predicate);
            return await Store.DeleteAsync(list);
        }
    }
}