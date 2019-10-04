using Alabo.Datas.Stores.Report.Mongo;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Alabo.Validations.Aspects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Update.Mongo {

    public abstract class UpdateAsyncMongoStore<TEntity, TKey> : ReportMongoStore<TEntity, TKey>,
        IUpdateAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity {

        protected UpdateAsyncMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public Task AddUpdateOrDeleteAsync(IEnumerable<TEntity> entities, Expression<Func<TEntity, bool>> predicate) {
            throw new NotImplementedException();
        }

        public Task UpdateAsync([Valid] TEntity entity) {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Action<TEntity> updateAction, Expression<Func<TEntity, bool>> predicate = null) {
            throw new NotImplementedException();
        }

        public Task UpdateManyAsync(Action<TEntity> updateAction, Expression<Func<TEntity, bool>> predicate = null) {
            throw new NotImplementedException();
        }

        public Task UpdateManyAsync(IEnumerable<TEntity> entities) {
            throw new NotImplementedException();
        }
    }
}