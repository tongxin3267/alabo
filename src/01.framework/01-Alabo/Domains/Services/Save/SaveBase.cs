using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories.Model;
using Alabo.Validations.Aspects;
using System;
using System.Linq.Expressions;

namespace Alabo.Domains.Services.Save {

    public abstract class SaveBase<TEntity, TKey> : SaveAsyncBase<TEntity, TKey>, ISave<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected SaveBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store) {
        }

        public bool AddOrUpdate(TEntity model, Expression<Func<TEntity, bool>> predicate) {
            var result = false;
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            var find = GetByIdNoTracking(predicate);
            if (find == null) {
                result = Add(model);
            } else {
                if (TableType == TableType.Mongodb) {
                    result = Store.UpdateSingle(model);
                } else {
                    var proeprties = model.GetType().GetProperties();
                    var dest = find.GetType().GetProperties();
                    foreach (var item in proeprties) {
                        foreach (var d in dest) {
                            if (!d.CanWrite) {
                                continue;
                            }

                            if (d.Name == item.Name) {
                                d.SetValue(find, item.GetValue(model));
                                break;
                            }
                        }
                    }

                    result = Store.UpdateSingle(model);
                }
            }

            return result;
        }

        public bool AddOrUpdate(TEntity model) {
            return AddOrUpdate(model, IdPredicate(model.Id));
        }

        public bool AddOrUpdate(TEntity model, bool predicate) {
            var result = false;
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            if (!predicate) {
                result = Add(model);
            }

            result = Store.UpdateSingle(model);
            return result;
        }

        public void Save<TRequest>([Valid] TRequest request) where TRequest : IRequest, IKey, new() {
            throw new NotImplementedException();
            // Store.Save(request);
        }
    }
}