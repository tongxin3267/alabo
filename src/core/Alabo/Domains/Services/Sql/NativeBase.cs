using System;
using System.Collections.Generic;
using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Services.Save;

namespace Alabo.Domains.Services.Sql
{
    public abstract class NativeBase<TEntity, TKey> : SaveBase<TEntity, TKey>, INative<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected NativeBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public TEntity GetLastSingle(TimeType timeType, DateTime dateTime)
        {
            throw new NotImplementedException();
            //return Store.GetLastSingle(timeType, dateTime);
        }

        public TEntity SqlGingle<TTEntity>(string sql, object param = null)
        {
            throw new NotImplementedException();
            //return Store.SqlGingle(sql, param);
        }

        public IEnumerable<TEntity> SqlList<TTEntity>(string sql, object param = null)
        {
            throw new NotImplementedException();
            //return Store.SqlList(sql, param);
        }
    }
}