﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alabo.Domains.Entities.Core;
using Alabo.Validations.Aspects;

namespace Alabo.Datas.Stores.Update
{
    /// <summary>
    ///     修改实体
    /// </summary>
    /// <typeparam name="TEntity">对象类型</typeparam>
    /// <typeparam name="TKey">对象标识类型</typeparam>
    public interface IUpdateAsyncStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        /// <summary>
        ///     修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        Task UpdateAsync([Valid] TEntity entity);

        /// <summary>
        ///     更新单个实体
        /// </summary>
        /// <param name="updateAction"></param>
        /// <param name="predicate">查询条件</param>
        Task<bool> UpdateAsync(Action<TEntity> updateAction, Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        ///     批量更新多个实体
        /// </summary>
        /// <param name="updateAction"></param>
        /// <param name="predicate">查询条件</param>
        Task UpdateManyAsync(Action<TEntity> updateAction, Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        ///     添加，更新，或删除
        ///     添加：主键Id为0或者为空的商品
        ///     编辑：主键Id，在数据库中存在的商品
        ///     删除：根据输入条件查找出所有的列表，如果列表不在传入的数据中，则删除
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="predicate">判断条件</param>
        Task AddUpdateOrDeleteAsync(IEnumerable<TEntity> entities, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     修改实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        Task UpdateManyAsync(IEnumerable<TEntity> entities);
    }
}