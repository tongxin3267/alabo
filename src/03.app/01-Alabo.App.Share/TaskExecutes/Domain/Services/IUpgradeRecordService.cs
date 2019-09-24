using MongoDB.Bson;
using System;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Tasks.Domain.Services {

    public interface IUpgradeRecordService : IService<UpgradeRecord, ObjectId> {

        /// <summary>
        /// 修改用户等级，同时增加记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="afterGradeId"></param>
        /// <returns></returns>
        ServiceResult ChangeUserGrade(long userId, Guid afterGradeId, UpgradeType upgradeType);
    }
}