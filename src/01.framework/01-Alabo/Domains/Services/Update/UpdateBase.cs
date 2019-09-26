using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Tree;
using Alabo.Extensions;
using Alabo.Validations.Aspects;
using System;
using System.Linq.Expressions;

namespace Alabo.Domains.Services.Update
{
    public abstract class UpdateBase<TEntity, TKey> : TreeBaseAsync<TEntity, TKey>, IUpdate<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        protected UpdateBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public bool Update([Valid] TEntity model)
        {
            var cacheKey = $"{typeof(TEntity).Name}_ReadCache_{model.Id.ToStr().Trim()}";
            ObjectCache.Remove(cacheKey);
            return Store.UpdateSingle(model);
        }

        public bool UpdateNoTracking(TEntity model)
        {
            var cacheKey = $"{typeof(TEntity).Name}_ReadCache_{model.Id.ToStr().Trim()}";
            ObjectCache.Remove(cacheKey);
            return Store.UpdateNoTracking(model);
        }

        public bool Update(Action<TEntity> updateAction, Expression<Func<TEntity, bool>> predicate = null)
        {
            return Store.Update(updateAction, predicate);
        }

        public bool Update<TUpdateRequest>([Valid] TUpdateRequest request) where TUpdateRequest : IRequest, new()
        {
            throw new NotImplementedException();
            //return Store.Update(request);
        }
    }
}