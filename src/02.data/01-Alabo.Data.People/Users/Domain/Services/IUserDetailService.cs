using System.Collections.Generic;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Users.Entities;

namespace Alabo.App.Core.User.Domain.Services {

    /// <summary>
    ///     Interface IUserDetailService
    /// </summary>
    public interface IUserDetailService : IService<UserDetail, long> {

        /// <summary>
        /// 检查当前登录用户的支付密码
        /// </summary>
        /// <param name="payPassword"></param>
        /// <param name="loginUserId"></param>
        /// <returns></returns>
        bool CheckPayPassword(string payPassword, long loginUserId);

        /// <summary>
        ///     更新数据
        /// </summary>
        /// <param name="userDetail">The 会员 detail.</param>
        bool UpdateSingle(UserDetail userDetail);

        /// <summary>
        /// 确认支付密码 传入明文
        /// </summary>
        ServiceResult ConfirmPayPassword(string payPassWord, long loginUserId);

        /// <summary>
        ///     修改支付密码和登录密码
        /// </summary>
        /// <param name="passwordInput">密码传入明文</param>
        /// <param name="checkLastPassword">是否检查老密码</param>
        ServiceResult ChangePassword(PasswordInput passwordInput, bool checkLastPassword = true);

        /// <summary>
        ///     找回密码
        /// </summary>
        /// <param name="findPassword">The find password.</param>
        ServiceResult FindPassword(FindPasswordInput findPassword);

        /// <summary>
        ///     找回密码
        /// </summary>
        /// <param name="findPassword">The find password.</param>
        ServiceResult FindPayPassword(FindPasswordInput findPassword);

        /// <summary>
        ///     Changes the mobile.
        ///     修改手机号码
        /// </summary>
        /// <param name="view">The 视图.</param>
        ServiceResult ChangeMobile(ViewChangMobile view);

        /// <summary>
        ///     获取s the 会员 output.
        /// </summary>
        /// <param name="userId">会员Id</param>
        UserOutput GetUserOutput(long userId);

        /// <summary>
        ///     更新s the open identifier.
        /// </summary>
        /// <param name="openId">The open identifier.</param>
        /// <param name="userId">会员Id</param>
        void UpdateOpenId(string openId, long userId);

        /// <summary>
        /// 是否实名认证
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool IsIdentity(long userId);
    }
}