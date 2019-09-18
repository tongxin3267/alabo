using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;

namespace Alabo.Domains.Services.Sql
{
    public abstract class NativeBaseAsync<TEntity, TKey> : NativeBase<TEntity, TKey>, INativeAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected NativeBaseAsync(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public Task<TEntity> GetLastSingleAsync(TimeType timeType, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> SqlGingleAsync<TTEntity>(string sql, object param = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> SqlListAsync<IEntity>(string sql, object param = null)
        {
            throw new NotImplementedException();
        }
    }
}