using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Validations.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Bulk {

    public abstract class BulkBase<TEntity, TKey> : BulkAsyncBase<TEntity, TKey>, IBulk<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        protected BulkBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store) {
        }

        public void AddMany(IEnumerable<TEntity> soucre) {
            if (soucre == null || !soucre.Any()) {
                return;
            }

            var addList = new List<TEntity>();
            soucre.Foreach(r => {
                if (r != null) {
                    r.CreateTime = DateTime.Now;
                    addList.Add(r);
                }
            });
            if (!addList.Any()) {
                return;
            }

            Store.AddMany(addList);
        }

        public void AddUpdateOrDelete(IEnumerable<TEntity> entities, Expression<Func<TEntity, bool>> predicate) {
            Store.AddUpdateOrDelete(entities, predicate);
        }

        public List<TDto> SaveMany<TDto, TRequest>(List<TRequest> addList, List<TRequest> updateList,
            List<TRequest> deleteList)
            where TDto : IResponse, new()
            where TRequest : IRequest, IKey, new() {
            throw new NotImplementedException();
            //            return Store.Save(addList, updateList, deleteList);
        }

        public void UpdateMany([Valid] IEnumerable<TEntity> entities) {
            throw new NotImplementedException();
            //Store.Update(entities);
        }

        public void UpdateMany(Action<TEntity> updateAction, Expression<Func<TEntity, bool>> predicate = null) {
            Store.UpdateMany(updateAction, predicate);
        }

        public Task UpdateAsync([Valid] IEnumerable<TEntity> entities) {
            throw new NotImplementedException();
            //            return Store.UpdateAsync(entities);
        }
    }
}