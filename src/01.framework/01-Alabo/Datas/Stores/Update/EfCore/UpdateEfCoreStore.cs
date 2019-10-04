using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Alabo.Validations.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Alabo.Datas.Stores.Update.EfCore {

    public class UpdateEfCoreStore<TEntity, TKey> : UpdateAsyncEfCoreStore<TEntity, TKey>, IUpdateStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity {

        protected UpdateEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public bool Update(Action<TEntity> updateAction, Expression<Func<TEntity, bool>> predicate = null) {
            var query = UnitOfWork.Set<TEntity>().AsQueryable();
            if (predicate != null) {
                query = query.Where(predicate);
            }

            var source = query.ToList();
            foreach (var item in source) {
                updateAction(item);
            }

            var count = UnitOfWork.SaveChanges();
            if (count >= 0) {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        public bool UpdateSingle(TEntity entity) {
            UnitOfWork.Update(entity);
            var count = UnitOfWork.SaveChanges();
            if (count > 0) {
                return true;
            }

            return false;
        }

        public bool UpdateNoTracking([Valid] TEntity entity) {
            if (entity == null) {
                throw new ArgumentNullException(nameof(entity));
            }

            var oldEntity = FindByIdNoTracking(entity.Id);
            Update(entity, oldEntity);
            return true;
        }

        /// <summary>
        ///     修改实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        public virtual void UpdateMany(IEnumerable<TEntity> entities) {
            if (entities == null) {
                throw new ArgumentNullException(nameof(entities));
            }

            var newEntities = entities.ToList();
            var oldEntities = FindByIdsNoTracking(newEntities.Select(t => t.Id));
            ValidateVersion(newEntities, oldEntities);
            UnitOfWork.UpdateRange(newEntities);
            var count = UnitOfWork.SaveChanges();
        }

        public void UpdateMany(Action<TEntity> updateAction, Expression<Func<TEntity, bool>> predicate = null) {
            var query = UnitOfWork.Set<TEntity>().AsQueryable();
            if (predicate != null) {
                query = query.Where(predicate);
            }

            var source = query.ToList();
            foreach (var item in source) {
                updateAction(item);
            }

            UnitOfWork.SaveChanges();
        }

        public void AddUpdateOrDelete(IEnumerable<TEntity> entities, Expression<Func<TEntity, bool>> predicate) {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     修改实体
        /// </summary>
        /// <param name="newEntity">新实体</param>
        /// <param name="oldEntity">旧实体</param>
        protected void Update(TEntity newEntity, TEntity oldEntity) {
            if (newEntity == null) {
                throw new ArgumentNullException(nameof(newEntity));
            }

            if (oldEntity == null) {
                throw new ArgumentNullException(nameof(oldEntity));
            }

            ValidateVersion(newEntity, oldEntity);
            var entity = Find(newEntity.Id);
            UnitOfWork.Entry(entity).CurrentValues.SetValues(newEntity);
        }
    }
}