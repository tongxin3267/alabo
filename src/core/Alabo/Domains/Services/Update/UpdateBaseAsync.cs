using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Sql;
using Alabo.Validations.Aspects;

namespace Alabo.Domains.Services.Update
{
    public abstract class UpdateBaseAsync<TEntity, TKey> : NativeBaseAsync<TEntity, TKey>, IUpdateAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        protected UpdateBaseAsync(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public async Task<bool> UpdateAsync<TUpdateRequest>([Valid] TUpdateRequest request)
            where TUpdateRequest : IRequest, new()
        {
            throw new NotImplementedException();
            //return await Store.UpdateAsync(request);
        }

        public async Task<bool> UpdateAsync([Valid] TEntity model)
        {
            //return await Store.UpdateAsync(model);
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Action<TEntity> updateAction,
            Expression<Func<TEntity, bool>> predicate = null)
        {
            return await Store.UpdateAsync(updateAction, predicate);
        }
    }
}