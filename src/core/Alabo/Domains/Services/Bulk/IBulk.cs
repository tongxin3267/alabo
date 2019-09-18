﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Validations.Aspects;

namespace Alabo.Domains.Services.Bulk
{
    /// <summary>
    ///     批量保存操作
    /// </summary>
    public interface IBulk<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     批量保存AddMany
        /// </summary>
        /// <param name="addList">新增列表</param>
        /// <param name="updateList">修改列表</param>
        /// <param name="deleteList">删除列表</param>
        List<TDto> SaveMany<TDto, TRequest>(List<TRequest> addList, List<TRequest> updateList,
            List<TRequest> deleteList)
            where TRequest : IRequest, IKey, new()
            where TDto : IResponse, new();

        /// <summary>
        ///     批量添加多个实体
        /// </summary>
        /// <param name="soucre"></param>
        void AddMany(IEnumerable<TEntity> soucre);

        /// <summary>
        ///     批量更新多个实体
        /// </summary>
        /// <param name="updateAction"></param>
        /// <param name="predicate">查询条件</param>
        void UpdateMany(Action<TEntity> updateAction, Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        ///     添加，更新，或删除
        ///     添加：主键Id为0或者为空的商品
        ///     编辑：主键Id，在数据库中存在的商品
        ///     删除：根据输入条件查找出所有的列表，如果列表不在传入的数据中，则删除
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="predicate">判断条件</param>
        void AddUpdateOrDelete(IEnumerable<TEntity> entities, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     修改实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void UpdateMany([Valid] IEnumerable<TEntity> entities);
    }
}