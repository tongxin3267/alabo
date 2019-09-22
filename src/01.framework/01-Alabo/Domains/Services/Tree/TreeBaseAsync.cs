using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Trees;

namespace Alabo.Domains.Services.Tree
{
    public abstract class TreeBaseAsync<TEntity, TKey> : TreeBase<TEntity, TKey>, ITreeAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        protected TreeBaseAsync(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public async Task<List<TDto>> FindByIdsAsync<TDto>(string ids) where TDto : class, IResponse, ITreeNode, new()
        {
            throw new NotImplementedException();
            //return await Store.FindByIdsAsync(ids);
        }

        public async Task EnableAsync(string ids)
        {
            throw new NotImplementedException();
            //return await Store.EnableAsync(ids);
        }

        public async Task DisableAsync(string ids)
        {
            throw new NotImplementedException();
            //return await Store.DisableAsync(ids);
        }

        public async Task SwapSortAsync(Guid id, Guid swapId)
        {
            throw new NotImplementedException();
            //return await Store.SwapSortAsync(id, swapId);
        }

        public async Task FixSortIdAsync<TQueryParameter>(TQueryParameter parameter)
        {
            throw new NotImplementedException();
            //return await Store.FixSortIdAsync(parameter);
        }

        public async Task<int> GenerateSortIdAsync<TParentId>(TParentId parentId)
        {
            throw new NotImplementedException();
            //return await Store.GenerateSortIdAsync(parentId);
        }

        public async Task<List<TEntity>> GetAllChildrenAsync(TEntity parent)
        {
            throw new NotImplementedException();
            //return await Store.GetAllChildrenAsync(parent);
        }

        public async Task UpdatePathAsync(TEntity entity)
        {
            throw new NotImplementedException();
            //return await Store.UpdatePathAsync(entity);
        }
    }
}