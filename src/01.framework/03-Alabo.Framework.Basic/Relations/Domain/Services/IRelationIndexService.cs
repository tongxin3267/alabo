using System.Collections.Generic;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.Framework.Core.Reflections.Interfaces;

namespace Alabo.Framework.Basic.Relations.Domain.Services {

    public interface IRelationIndexService : IService<RelationIndex, long> {

        /// <summary>
        ///     批量更新RealtionIndex
        ///     批量更新分类、标签等，包括删除、添加、更新操作
        /// </summary>
        /// <typeparam name="T">级联类型</typeparam>
        /// <param name="entityId">实体Id</param>
        /// <param name="relationIds">级联ID字符串，多个Id 用逗号隔开,格式：12,13,14,15</param>
        ServiceResult AddUpdateOrDelete<T>(long entityId, string relationIds) where T : class, IRelation;

        ServiceResult AddUpdateOrDelete(string fullName, long entityId, string relationIds);

        /// <summary>
        ///     获取Id字符串列表，返回格式逗号隔开
        ///     示列数据：12,13,14,15
        /// </summary>
        /// <typeparam name="T">级联类型</typeparam>
        /// <param name="entityId">实体Id</param>
        string GetRelationIds<T>(long entityId) where T : class, IRelation;

        /// <summary>
        ///     获取Id字符串列表，返回格式逗号隔开
        ///     示列数据：12,13,14,15
        /// </summary>
        /// <typeparam name="T">级联类型</typeparam>
        /// <param name="ids">Id标识列表</param>
        List<RelationIndex> GetEntityIds(string ids);

        /// <summary>
        ///     获取Id字符串列表，返回格式逗号隔开(营销活动)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="entityId"></param>
        string GetRelationIds(string type, long entityId);

        /// <summary>
        ///     批量更新RealtionIndex
        ///     批量更新分类、标签等，包括删除、添加、更新操作(营销活动)
        /// </summary>
        /// <typeparam name="T">级联类型</typeparam>
        /// <param name="entityId">实体Id</param>
        /// <param name="relationIds">级联ID字符串，多个Id 用逗号隔开,格式：12,13,14,15</param>
        /// <param name="type"></param>
        ServiceResult AddUpdateOrDelete(long entityId, string relationIds, string type);
    }
}