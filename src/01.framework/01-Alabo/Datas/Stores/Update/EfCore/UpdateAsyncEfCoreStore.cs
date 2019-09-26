using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alabo.Datas.Stores.Report.Efcore;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores.Update.EfCore
{
    public class UpdateAsyncEfCoreStore<TEntity, TKey> : ReportEfCoreStore<TEntity, TKey>,
        IUpdateAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        public UpdateAsyncEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Action<TEntity> updateAction, Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateManyAsync(Action<TEntity> updateAction, Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public Task AddUpdateOrDeleteAsync(IEnumerable<TEntity> entities, Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     修改实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        public virtual async Task UpdateManyAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            var newEntities = entities.ToList();
            var oldEntities = await FindByIdsNoTrackingAsync(newEntities.Select(t => t.Id));
            ValidateVersion(newEntities, oldEntities);
            UnitOfWork.UpdateRange(newEntities);
        }
    }
}