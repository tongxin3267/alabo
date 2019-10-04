using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Validations.Aspects;
using System;

namespace Alabo.Domains.Services.Add {

    public abstract class AddBase<TEntity, TKey> : AddAsyncBase<TEntity, TKey>, IAdd<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        protected AddBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store) {
        }

        public bool Add<TRequest>([Valid] TRequest request) where TRequest : IRequest, new() {
            //            if (request == null)
            //                throw new ArgumentNullException(nameof(request));
            //            var entity = TEntity(request);
            //            if (entity == null)
            //                throw new ArgumentNullException(nameof(entity));
            //            return Add(entity);
            throw new NotImplementedException();
        }

        public bool Add([Valid] TEntity model) {
            var result = Store.AddSingle(model);
            //if (result) Log($"成功新增[{typeof(TEntity).Name}]记录一条,新增记录Id:{model.Id}");
            //失败再打印日志,避免资源浪费
            if (!result) {
                Log($"新增[{typeof(TEntity).Name}]失败,新增记录:{model.ToJson()}");
            }

            var cacheKey = $"{typeof(TEntity).Name}_ReadCache_{model.Id.ToStr().Trim()}";
            ObjectCache.Remove(cacheKey);
            return result;
        }
    }
}