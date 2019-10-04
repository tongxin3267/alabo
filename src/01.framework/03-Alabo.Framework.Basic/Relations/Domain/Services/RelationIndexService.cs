using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.Framework.Basic.Relations.Domain.Repositories;
using Alabo.Framework.Core.Reflections.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.Framework.Basic.Relations.Domain.Services {

    public class RelationIndexService : ServiceBase<RelationIndex, long>, IRelationIndexService {

        public RelationIndexService(IUnitOfWork unitOfWork, IRepository<RelationIndex, long> repository) : base(
            unitOfWork, repository) {
        }

        public ServiceResult AddUpdateOrDelete(string fullName, long entityId, string relationIds) {
            var result = ServiceResult.Success;
            var list = GetList(r => r.Type == fullName && r.EntityId == entityId).Select(r => r.RelationId).ToList();
            var addList = new List<RelationIndex>();
            var temp = relationIds.ToLongList();
            var deleteList = list.Except(relationIds.ToLongList()).ToList();
            temp.Except(list).Foreach(o => {
                addList.Add(new RelationIndex {
                    Type = fullName,
                    EntityId = entityId,
                    RelationId = o
                });
            });
            //foreach (var item in relationIds.ToSplitList()) {
            //    var relationId = item.ToInt16();
            //    if (relationId > 0) {
            //        if (!list.Contains(relationId)) {
            //            RelationIndex index = new RelationIndex();
            //            index.Type = type;
            //            index.EntityId = entityId;
            //            index.RelationId = item.ToInt16();
            //            addList.Add(index);
            //        }
            //        //else {
            //        //    deleteList.Add(relationId);
            //        //}
            //    }
            //}
            //删除级联数据
            Delete(r => deleteList.Contains(r.RelationId) && r.EntityId == entityId && r.Type == fullName);
            //添加级联数据
            AddMany(addList);
            return result;
        }

        /// <summary>
        ///     批量更新RealtionIndex
        ///     批量更新分类、标签等，包括删除、添加、更新操作
        /// </summary>
        /// <typeparam name="T">级联类型</typeparam>
        /// <param name="entityId">实体Id</param>
        /// <param name="relationIds">级联ID字符串，多个Id 用逗号隔开,格式：12,13,14,15</param>
        public ServiceResult AddUpdateOrDelete<T>(long entityId, string relationIds) where T : class, IRelation {
            return AddUpdateOrDelete(typeof(T).FullName, entityId, relationIds);
        }

        /// <summary>
        ///     获取Id字符串列表，返回格式逗号隔开
        ///     示列数据：12,13,14,15
        /// </summary>
        /// <typeparam name="T">级联类型</typeparam>
        /// <param name="entityId">实体Id</param>
        public string GetRelationIds<T>(long entityId) where T : class, IRelation {
            var type = typeof(T).FullName;
            var list = GetList(r => r.Type == type && r.EntityId == entityId).Select(r => r.RelationId).ToList();
            var ids = list.JoinToString(",");
            return ids;
        }

        public string GetRelationIds(string type, long entityId) {
            var list = GetList(r => r.Type == type && r.EntityId == entityId).Select(r => r.RelationId).ToList();
            var ids = list.JoinToString(",");
            return ids;
        }

        public List<RelationIndex> GetEntityIds(string ids) {
            var query = Repository<IRelationIndexRepository>().GetList();
            var tags = ids.ToIntList();
            var list = (from t in query join i in tags on t.RelationId equals i select t).ToList();
            return list;
        }

        public ServiceResult AddUpdateOrDelete(long entityId, string relationIds, string type) {
            var result = ServiceResult.Success;
            var list = GetList(r => r.Type == type && r.EntityId == entityId).Select(r => r.RelationId).ToList();
            var addList = new List<RelationIndex>();
            var temp = relationIds.ToLongList();
            var deleteList = list.Except(relationIds.ToLongList()).ToList();
            temp.Except(list).Foreach(o => {
                addList.Add(new RelationIndex {
                    Type = type,
                    EntityId = entityId,
                    RelationId = o
                });
            });
            //删除级联数据
            Delete(r => deleteList.Contains(r.RelationId) && r.EntityId == entityId && r.Type == type);
            //添加级联数据
            AddMany(addList);
            return result;
        }
    }
}