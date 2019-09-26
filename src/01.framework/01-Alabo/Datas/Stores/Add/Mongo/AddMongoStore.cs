using System;
using System.Collections.Generic;
using System.Data;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Alabo.Extensions;
using Alabo.Validations.Aspects;

namespace Alabo.Datas.Stores.Add.Mongo
{
    /// <summary>
    ///     Mongodb 查询器
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class AddMongoStore<TEntity, TKey> : GetListAsyncMongoStore<TEntity, TKey>, IAddStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected AddMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void AddMany(IEnumerable<TEntity> soucre)
        {
            Collection.InsertMany(soucre);
        }

        public bool AddSingle([Valid] TEntity entity)
        {
            if (entity.Id.IsNullOrEmpty()) throw new InvalidExpressionException("Id不能为空,添加时");

            entity.CreateTime = DateTime.Now;
            Collection.InsertOne(entity);
            return true;
        }
    }
}