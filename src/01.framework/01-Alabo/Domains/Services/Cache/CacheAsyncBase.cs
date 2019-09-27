using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.ById;
using Alabo.Extensions;
using Alabo.Linq;
using System;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Cache
{
    public abstract class CacheAsyncBase<TEntity, TKey> : GetByIdBase<TEntity, TKey>, ICacheAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        protected CacheAsyncBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public async Task<TEntity> GetSingleFromCacheAsync(TKey id)
        {
            var cacheKey = $"{typeof(TEntity).Name}_ReadCache_{id.ToStr().Trim()}";
            TEntity find = null;
            if (typeof(TKey) == typeof(Guid))
            {
                var expression =
                    LinqHelper.GetExpression<TEntity, bool>($"entity.Id == Guid.Parse(\"{id}\")", "entity");
                find = await GetSingleAsync(expression);
            }
            else
            {
                var expression = Lambda.GreaterEqual<TEntity>("Id", id);
                find = await GetSingleAsync(expression);
            }

            return find;
        }
    }
}