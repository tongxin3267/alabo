using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Linq;
using System;

namespace Alabo.Domains.Services.Cache {

    public abstract class CacheBase<TEntity, TKey> : CacheAsyncBase<TEntity, TKey>, ICache<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected CacheBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store) {
        }

        public TEntity GetSingleFromCache(TKey id) {
            var cacheKey = $"{typeof(TEntity).Name}_ReadCache_{id.ToStr().Trim()}";
            return ObjectCache.GetOrSet(() => {
                if (typeof(TKey) == typeof(Guid)) {
                    var expression =
                        LinqHelper.GetExpression<TEntity, bool>($"entity.Id == Guid.Parse(\"{id}\")", "entity");
                    var find = GetSingle(expression);
                    return find;
                } else {
                    var expression = Lambda.GreaterEqual<TEntity>("Id", id);
                    var find = GetSingle(expression);
                    return find;
                }
            }, cacheKey).Value;
        }
    }
}