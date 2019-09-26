using System;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.Framework.Core.Valids.Services
{
    public interface IValidService : IService
    {
        /// <summary>
        ///     根据用户名，验证用户状态是否正常
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        ServiceResult VerifyUser(string userName);

        /// <summary>
        ///     根据用户Id，验证用户状态是否正常
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ServiceResult VerifyUser(long userId);

        /// <summary>
        ///     验证用户等级
        /// </summary>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        ServiceResult VerifyUserGrade(Guid gradeId);

        /// <summary>
        ///     验证支付密码
        /// </summary>
        /// <returns></returns>
        ServiceResult VerifySafePassword();
    }
}