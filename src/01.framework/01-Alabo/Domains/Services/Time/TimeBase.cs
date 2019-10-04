using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Services.Update;
using Alabo.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Alabo.Domains.Services.Time {

    public class TimeBase<TEntity, TKey> : UpdateBaseAsync<TEntity, TKey>, ITime<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        public TimeBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store) {
        }

        public IEnumerable<TEntity> GetLastList(TimeType timeType, Expression<Func<TEntity, bool>> predicate) {
            return GetLastList(timeType, DateTime.Now, predicate);
        }

        public IEnumerable<TEntity> GetLastList(TimeType timeType, DateTime dateTime,
            Expression<Func<TEntity, bool>> predicate) {
            predicate = timeType.GetPredicate<TEntity, TKey>(dateTime, predicate);
            return GetList(predicate);
        }

        public TEntity GetLastSingle(TimeType timeType, DateTime dateTime, Expression<Func<TEntity, bool>> predicate) {
            predicate = timeType.GetPredicate<TEntity, TKey>(dateTime, predicate);
            return GetSingle(predicate);
        }

        public TEntity GetLastSingle(TimeType timeType, Expression<Func<TEntity, bool>> predicate) {
            return GetLastSingle(timeType, DateTime.Now, predicate);
        }
    }
}