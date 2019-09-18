using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Market.Relationship.Domain.CallBacks;
using Alabo.App.Market.Relationship.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Extensions;

namespace Alabo.App.Market.Relationship.Domain.Services {

    public class RelationshipIndexService : ServiceBase<RelationshipIndex, ObjectId>, IRelationshipIndexService {

        public RelationshipIndexService(IUnitOfWork unitOfWork, IRepository<RelationshipIndex, ObjectId> repository) :
            base(unitOfWork, repository) {
        }

        /// <summary>
        ///     �û�ע����¼�
        /// </summary>
        /// <param name="user"></param>
        public void UserRegAfter(User user) {
            //TODO:�û���ʼ������(zhang.zl)
            if (user.ParentId <= 0) {
                return;
            }

            if (user.Map.ParentMap.IsNullOrEmpty()) {
                throw new ValidException("�û�Map����Ϊ��");
            }

            var maps = user.Map.ParentMap.DeserializeJson<List<ParentMap>>();
            if (maps == null) {
                throw new ValidException("δ�ҵ�������Ա��Parent Map.");
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
                // �жϴ����û��ȼ��Ƿ��ڸ÷�Χ������������˳�
                if (!lowGrades.Contains(user.GradeId)) {
                    continue;
                }

                // ���и����û�
                var parentUsers = Resolve<IUserService>().GetList(maps.Select(r => r.UserId).ToList());
                foreach (var itemMap in maps) {
                    // �ҵ������û��ȼ�����������
                    var parentUser =
                        parentUsers.FirstOrDefault(r => r.Id == itemMap.UserId && hightGrades.Contains(r.GradeId));
                    if (parentUser != null) {
                        var relationshipIndex = new RelationshipIndex {
                            ConfigId = item.Id,
                            UserId = user.Id,
                            ParentId = parentUser.Id
                        };
                        // �����Ƽ���ϵ
                        Add(relationshipIndex);
                        break;
                    }
                }
            }
        }
    }
}