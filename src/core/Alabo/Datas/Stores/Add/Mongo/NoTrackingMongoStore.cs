using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Convert = Alabo.Helpers.Convert;

namespace Alabo.Datas.Stores.Add.Mongo
{
    public abstract class NoTrackingMongoStore<TEntity, TKey> : NoTrackingAsyncMongoStore<TEntity, TKey>,
        INoTrackingStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected NoTrackingMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        ///     查找未跟踪单个实体
        /// </summary>
        /// <param name="id">标识</param>
        public TEntity FindByIdNoTracking(TKey id)
        {
            var entities = FindByIdsNoTracking(id);
            if (entities == null || entities.Count == 0) {
                return null;
            }

            return entities[0];
        }

        /// <summary>
        ///     查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        public List<TEntity> FindByIdsNoTracking(params TKey[] ids)
        {
            return FindByIdsNoTracking((IEnumerable<TKey>) ids);
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
            return Collection.AsQueryable().Where(predicate).ToList();
        }

        public TEntity FindByIdNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.AsQueryable().FirstOrDefault(predicate);
        }

        public List<TEntity> FindByIdsNoTracking(IEnumerable<TKey> ids)
        {
            return Collection.AsQueryable().Where(r => ids.Contains(r.Id)).ToList();
        }
    }
}