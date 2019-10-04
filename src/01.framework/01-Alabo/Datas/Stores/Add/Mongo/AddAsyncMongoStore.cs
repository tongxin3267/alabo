using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Alabo.Validations.Aspects;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Add.Mongo {

    public abstract class AddAsyncMongoStore<TEntity, TKey> : AddMongoStore<TEntity, TKey>,
        IAddAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity {

        protected AddAsyncMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public async Task AddManyAsync([Valid] IEnumerable<TEntity> entities) {
            await Collection.InsertManyAsync(entities);
        }

        public async Task<bool> AddSingleAsync([Valid] TEntity entity) {
            var model = (IMongoEntity)entity;
            if (model.IsObjectIdEmpty()) {
                model.Id = ObjectId.GenerateNewId();
                model.CreateTime = DateTime.Now;
            }

            await Collection.InsertOneAsync(entity);
            return true;
        }
    }
}