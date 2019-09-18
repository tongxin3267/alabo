using System;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Extensions;

namespace Alabo.App.Core.Admin.Domain.Services {

    public class VerifyService : ServiceBase, IVerifyService {

        public ServiceResult VerifyUser(string userName) {
            if (userName.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("用户名不能为空");
            }
            var user = Resolve<IUserService>().GetSingle(userName);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户不存在");
            }
            if (user.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("用户状态不正常");
            }
            return ServiceResult.Success;
        }

        public ServiceResult VerifyUser(long userId) {
            if (userId <= 0) {
                return ServiceResult.FailedWithMessage("用户Id不能小于0");
            }
            var user = Resolve<IUserService>().GetSingle(userId);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户不存在");
            }
            if (user.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("用户状态不正常");
            }
            return ServiceResult.Success;
        }

        public ServiceResult VerifyUserGrade(Guid gradeId) {
            if (gradeId.IsGuidNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("等级ID不能为空");
            }

            var grade = Resolve<IGradeService>().GetGrade(gradeId);
            if (grade == null) {
                return ServiceResult.FailedWithMessage("等级不存在");
            }
            return ServiceResult.Success;
        }

        public VerifyService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}