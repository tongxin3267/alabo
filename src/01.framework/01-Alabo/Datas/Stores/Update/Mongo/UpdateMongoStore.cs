using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Alabo.Validations.Aspects;
using MongoDB.Driver;

namespace Alabo.Datas.Stores.Update.Mongo
{
    public abstract class UpdateMongoStore<TEntity, TKey> : UpdateAsyncMongoStore<TEntity, TKey>,
        IUpdateStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected UpdateMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void AddUpdateOrDelete(IEnumerable<TEntity> entities, Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update(Action<TEntity> updateAction, Expression<Func<TEntity, bool>> predicate = null)
        {
            //TODO: mongodb批量操作数据库
            throw new NotImplementedException();
            //var expression = IdPredicate(entity.Id);
            //var filter = ToFilter(entity.Id);
            ////var updateModel = Collection.FindOneAndReplace(filter, entity);
            ////if (updateModel != null) {
            ////    return true;
            ////}

            //if (entity == null)
            //    throw new NullReferenceException();

            //var type = entity.GetType();
            //// 修改的属性集合
            //var updateDefinition = Builders<TEntity>.Update.Set(propert.Name, replaceValue);

            //foreach (var propert in type.GetProperties()) {
            //    if (propert.Name.ToLower() != "id" || propert.Name != "CreateTime") {
            //        var replaceValue = propert.GetValue(entity);

            //        list.Add();
            //    }
            //}

            //#region 有可修改的属性

            //if (list.Count > 0) {
            //    // 合并多个修改//new List<UpdateDefinition<T>>() { Builders<T>.Update.Set("Name", "111") }
            //    var builders = Builders<TEntity>.Update.Combine(updateDefinition);
            //    // 执行提交修改
            //    var result = Collection.UpdateMany(filter, updateDefinition);
            //}

            //#endregion 有可修改的属性

            //return true;
        }

        public void UpdateMany(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void UpdateMany(Action<TEntity> updateAction, Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public bool UpdateNoTracking([Valid] TEntity model)
        {
            throw new NotImplementedException();
        }

        public bool UpdateSingle([Valid] TEntity entity)
        {
            var expression = IdPredicate(entity.Id);
            var filter = ToFilter(entity.Id);
            var updateModel = Collection.FindOneAndReplace(filter, entity);
            if (updateModel != null) return true;

            return true;
        }
    }
}