using System.Collections.Generic;
using System.Linq;
using Alabo.Cloud.People.Relationship.Domain.CallBacks;
using Alabo.Cloud.People.Relationship.Domain.Entities;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Users.Dtos;
using Alabo.Users.Entities;
using MongoDB.Bson;

namespace Alabo.Cloud.People.Relationship.Domain.Services {

    public class RelationshipIndexService : ServiceBase<RelationshipIndex, ObjectId>, IRelationshipIndexService {

        public RelationshipIndexService(IUnitOfWork unitOfWork, IRepository<RelationshipIndex, ObjectId> repository) :
            base(unitOfWork, repository) {
        }

        /// <summary>
        ///     用户注册后事件
        /// </summary>
        /// <param name="user"></param>
        public void UserRegAfter(User user) {
            //TODO:用户初始化问题(zhang.zl)
            if (user.ParentId <= 0) {
                return;
            }

            if (user.Map.ParentMap.IsNullOrEmpty()) {
                throw new ValidException("用户Map不能为空");
            }

            var maps = user.Map.ParentMap.DeserializeJson<List<ParentMap>>();
            if (maps == null) {
                throw new ValidException("未找到触发会员的Parent Map.");
            }

            var userRelationshipConfigs = Resolve<IAutoConfigService>().GetList<UserRelationshipIndexConfig>();
            foreach (var item in userRelationshipConfigs.Where(r => r.IsEnable)) {
                if (item.HighGrades.IsNullOrEmpty() || item.LowGrades.IsNullOrEmpty()) {
                    continue;
                }

                var hightGrades = item.HighGrades.SliptToGuidList();
                if (hightGrades.Count == 0) {
                    continue;
                }

                var lowGrades = item.LowGrades.SliptToGuidList();
                // 判断触发用户等级是否在该范围，如果不在则退出
                if (!lowGrades.Contains(user.GradeId)) {
                    continue;
                }

                // 所有父级用户
                var parentUsers = Resolve<IUserService>().GetList(maps.Select(r => r.UserId).ToList());
                foreach (var itemMap in maps) {
                    // 找到父级用户等级符合条件的
                    var parentUser =
                        parentUsers.FirstOrDefault(r => r.Id == itemMap.UserId && hightGrades.Contains(r.GradeId));
                    if (parentUser != null) {
                        var relationshipIndex = new RelationshipIndex {
                            ConfigId = item.Id,
                            UserId = user.Id,
                            ParentId = parentUser.Id
                        };
                        // 添加推荐关系
                        Add(relationshipIndex);
                        break;
                    }
                }
            }
        }
    }
}