using System;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Framework.Basic.Grades.Domain.Entities;
using Alabo.Framework.Basic.Grades.Domain.Enums;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Grades.Domain.Services
{

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