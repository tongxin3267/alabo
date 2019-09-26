using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Extensions;

namespace Alabo.Domains.Services.Delete
{
    public abstract class DeleteBase<TEntity, TKey> : DeleteBaseAsync<TEntity, TKey>, IDelete<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected DeleteBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public bool Delete(TEntity entity)
        {
            var result = Store.Delete(entity);
            if (result) Log($"删除{{Table}}记录一条,删除记录Id:{entity.Id}");

            return result;
        }

        public void DeleteMany(IEnumerable<TEntity> entities)
        {
            Store.Delete(entities);
            Log($"批量删除{{Table}}记录{entities.Count()}条,删除记录Id:{entities.Select(r => r.Id).SplitString()}");
        }

        public bool Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var find = Store.GetSingle(predicate);
            var result = Delete(find);
            return result;
        }

        public void DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            var list = Store.GetList(predicate);
            DeleteMany(list);
        }

        public bool Delete(TKey id)
        {
            var find = Store.GetSingle(id);
            return Delete(find);
        }

        public void DeleteMany(IEnumerable<TKey> ids)
        {
            var list = GetList(ids);
            DeleteMany(list);
        }

        public void DeleteAll()
        {
            var list = GetList();
            DeleteMany(list);
        }

        public void DeleteMany(string ids)
        {
            var list = GetList(ids);
            DeleteMany(list);
        }

        public bool Delete(object id)
        {
            var find = Store.GetSingle(id);
            return Delete(find);
        }
    }
}