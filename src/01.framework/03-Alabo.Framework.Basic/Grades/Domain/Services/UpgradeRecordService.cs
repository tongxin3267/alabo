using MongoDB.Bson;
using System;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Users.Services;

namespace Alabo.App.Core.Tasks.Domain.Services {

    public class UpgradeRecordService : ServiceBase<UpgradeRecord, ObjectId>, IUpgradeRecordService {

        public UpgradeRecordService(IUnitOfWork unitOfWork, IRepository<UpgradeRecord, ObjectId> repository) : base(unitOfWork, repository) {
        }

        public ServiceResult ChangeUserGrade(long userId, Guid afterGradeId, UpgradeType upgradeType) {
            var afterGrade = Resolve<IGradeService>().GetGrade(afterGradeId);
            if (afterGrade == null) {
                return ServiceResult.FailedWithMessage("��Ҫ�����ĵȼ�������");
            }
            var user = Resolve<IAlaboUserService>().GetSingle(r => r.Id == userId);
            if (user == null) {
                return ServiceResult.FailedWithMessage("�û�������");
            }
            var beforeGrade = Resolve<IGradeService>().GetGrade(user.GradeId);
            if (beforeGrade == null) {
                return ServiceResult.FailedWithMessage("��ʼ�ȼ�������");
            }

            user.GradeId = afterGradeId;
            if (Resolve<IAlaboUserService>().Update(user)) {
                UpgradeRecord upgradeRecord = new UpgradeRecord {
                    AfterGradeId = afterGradeId,
                    BeforeGradeId = beforeGrade.Id,
                    UserId = user.Id,
                    Type = upgradeType,
                };
                Add(upgradeRecord);
                //TODO 2019��9��23�� �ع�ɾ������ ɾ������
                //  Resolve<IAlaboUserService>().DeleteUserCache(user.Id, user.UserName);
                return ServiceResult.Success;
            } else {
                return ServiceResult.FailedWithMessage("�ȼ��޸�ʧ��");
            }
        }
    }
}