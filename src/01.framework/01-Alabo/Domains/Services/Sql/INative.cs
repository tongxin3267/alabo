using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using System;
using System.Collections.Generic;

namespace Alabo.Domains.Services.Sql {

    /// <summary>
    ///     原生SQL语句查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface INative<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     获取最近一条记录
        /// </summary>
        /// <param name="timeType"></param>
        /// <param name="dateTime"></param>
        TEntity GetLastSingle(TimeType timeType, DateTime dateTime);

        /// <summary>
        ///     通过Sql获取单个实体
        /// </summary>
        /// <typeparam name="TTEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        TEntity SqlGingle<TTEntity>(string sql, object param = null);

        /// <summary>
        ///     通过Sql获取实体列表
        /// </summary>
        /// <typeparam name="TTEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        IEnumerable<TEntity> SqlList<TTEntity>(string sql, object param = null);
    }
}