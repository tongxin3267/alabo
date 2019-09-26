using System;
using System.Linq;
using System.Linq.Expressions;
using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;

namespace Alabo.Domains.Services.Exist
{
    public abstract class ExistBase<TEntity, TKey> : ExistAsyncBase<TEntity, TKey>, IExist<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected ExistBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public bool Exists(params TKey[] ids)
        {
            return Exists(r => ids.Contains(r.Id));
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return Store.Exists(predicate);
        }

        /// <summary>
        ///     判断除Id以外的记录，是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(Expression<Func<TEntity, bool>> predicate, TKey id)
        {
            var find = GetSingle(predicate);
            if (find == null) return false;

            if (find.Id.Equals(id)) return false;

            return true;
        }

        public bool Exists(TKey id)
        {
            var predicate = IdPredicate(id);
            return Exists(predicate);
        }

        public bool Exists()
        {
            return Store.Exists();
        }
    }
}