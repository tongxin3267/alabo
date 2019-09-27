using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Sql
{
    public interface INativeAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        Task<TEntity> GetLastSingleAsync(TimeType timeType, DateTime dateTime);

        /// <summary>
        ///     通过Sql获取单个实体
        /// </summary>
        /// <typeparam name="TTEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        Task<TEntity> SqlGingleAsync<TTEntity>(string sql, object param = null);

        /// <summary>
        ///     通过Sql获取实体列表
        /// </summary>
        /// <typeparam name="TTEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        Task<IEnumerable<TEntity>> SqlListAsync<IEntity>(string sql, object param = null);
    }
}