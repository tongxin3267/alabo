using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Convert = Alabo.Helpers.Convert;

namespace Alabo.Datas.Stores.Add.EfCore
{
    public abstract class NoTrackingEfCoreStore<TEntity, TKey> : NoTrackingAsyncEfCoreStore<TEntity, TKey>,
        INoTrackingStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected NoTrackingEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        ///     查找未跟踪单个实体
        /// </summary>
        /// <param name="id">标识</param>
        public TEntity FindByIdNoTracking(TKey id)
        {
            var entities = FindByIdsNoTracking(id);
            if (entities == null || entities.Count == 0) return null;

            return entities[0];
        }

        /// <summary>
        ///     查找未跟踪单个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        public TEntity FindByIdNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return ToQueryableAsNoTracking().FirstOrDefault(predicate);
        }

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        public List<TEntity> FindByIdsNoTracking(params TKey[] ids)
        {
            return FindByIdsNoTracking((IEnumerable<TKey>)ids);
        }

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        public List<TEntity> FindByIdsNoTracking(IEnumerable<TKey> ids)
        {
            if (ids == null) return null;

            var result = FindAsNoTracking()
                .OrderByDescending(r => r.Id)
                .Where(t => ids.Contains(t.Id)).ToList();
            return result;
        }

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">逗号分隔的标识列表，范例："1,2"</param>
        public List<TEntity> FindByIdsNoTracking(string ids)
        {
            var idList = Convert.ToList<TKey>(ids);
            return FindByIdsNoTracking(idList);
        }

        public List<TEntity> FindByIdsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate != null)
                return FindAsNoTracking().Where(predicate)
                    .OrderByDescending(r => r.Id)
                    .ToList();

            return FindAsNoTracking().ToList();
        }
    }
}