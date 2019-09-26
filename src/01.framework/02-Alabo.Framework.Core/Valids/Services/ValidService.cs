using System;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Users.Services;

namespace Alabo.Framework.Core.Valids.Services {

    public class ValidService : ServiceBase, IValidService {

        public ServiceResult VerifyUser(string userName) {
            if (userName.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("用户名不能为空");
            }
            var user = Resolve<IAlaboUserService>().GetSingle(userName);
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
            var user = Resolve<IAlaboUserService>().GetSingle(userId);
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

            //var grade = Resolve<IGradeService>().GetGrade(gradeId);
            //if (grade == null) {
            //    return ServiceResult.FailedWithMessage("等级不存在");
            //}
            return ServiceResult.Success;
        }

        public ServiceResult VerifySafePassword() {
            if (HttpWeb.HttpContext.Request.Form.ContainsKey("PayPassword")) {
                var safePassword = HttpWeb.HttpContext.Request.Form["PayPassword"].ToString();
                if (safePassword.IsNullOrEmpty()) {
                    return ServiceResult.FailedWithMessage("请输入支付密码");
                }
                var loginUser = Resolve<IAlaboUserDetailService>().GetSingle(r => r.UserId == HttpWeb.UserId);
                if (safePassword.ToMd5HashString() != loginUser.PayPassword) {
                    return ServiceResult.FailedWithMessage("操作失败：支付密码不正确");
                }
            }

            return ServiceResult.Success;
        }

        public ValidService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}