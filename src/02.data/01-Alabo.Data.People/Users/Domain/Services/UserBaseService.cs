using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Data.People.Users.Domain.Configs;
using Alabo.Data.People.Users.Dtos;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Framework.Basic.Grades.Domain.Services;
using Alabo.Framework.Core.Admins.Configs;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Helpers;
using Alabo.Tables.Domain.Services;
using Alabo.Users.Dtos;
using Alabo.Users.Entities;

namespace Alabo.Data.People.Users.Domain.Services
{
    public class UserBaseService : ServiceBase, IUserBaseService
    {
        public UserBaseService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        ///     包含
        ///     1.用户名和密码登录，
        ///     2.手机号和密码登录
        ///     3.微信OpenId登录
        ///     4.手机号和手机验证码登录
        ///     5.后台管理员登录
        ///     注意：全局登录行数只维护这个，满足所有的登录场景
        /// </summary>
        /// <param name="loginInput"></param>
        /// <returns></returns>
        public Tuple<ServiceResult, UserOutput> Login(LoginInput loginInput)
        {
            var config = Resolve<IAutoConfigService>().GetValue<AdminCenterConfig>();
            if (!config.IsAllowUserLogin)
                return new Tuple<ServiceResult, UserOutput>(ServiceResult.FailedWithMessage("系统维护升级中，禁止会员登录，请稍后重试"),
                    null);

            // 根据OpenId登录
            User find = null;
            if (!loginInput.OpenId.IsNullOrEmpty())
            {
                // 根据OpenId登录
                find = Resolve<IUserService>().GetUserDetailByOpenId(loginInput.OpenId);
            }
            else if (!loginInput.UserName.IsNullOrEmpty() && !loginInput.VerifyCode.IsNullOrEmpty())
            {
                if (!Resolve<IOpenService>()
                    .CheckVerifiyCode(loginInput.UserName, loginInput.VerifyCode.ConvertToLong()))
                    return new Tuple<ServiceResult, UserOutput>(ServiceResult.FailedWithMessage("手机验证码错误"), null);
            }
            else
            {
                // 根据用户名和密码登录
                if (loginInput.UserName.IsNullOrEmpty())
                    return new Tuple<ServiceResult, UserOutput>(ServiceResult.FailedWithMessage("用户名不能为空"), null);
                if (loginInput.Password.IsNullOrEmpty())
                    return new Tuple<ServiceResult, UserOutput>(ServiceResult.FailedWithMessage("密码不能为空"), null);

                find = Resolve<IUserService>().GetSingleByUserNameOrMobile(loginInput.UserName);
                if (find.Status != Status.Normal)
                    return new Tuple<ServiceResult, UserOutput>(ServiceResult.FailedWithMessage("用户状态不正确，禁止登录"), null);
                if (find.Detail.Password != loginInput.Password.ToMd5HashString())
                    if (!loginInput.Password.Equals(find.Detail.Password, StringComparison.OrdinalIgnoreCase))
                        return new Tuple<ServiceResult, UserOutput>(ServiceResult.FailedWithMessage("密码不正确"), null);
            }

            if (find == null)
                return new Tuple<ServiceResult, UserOutput>(ServiceResult.FailedWithMessage("用户不存在 "), null);

            if (find.Email.IsNullOrEmpty())
            {
                var userConfig = Resolve<IAutoConfigService>().GetValue<UserConfig>();
                find.Email = find.UserName + userConfig.RegEmailPostfix;
            }

            //更新用户信息
            find.Detail.LastLoginTime = DateTime.Now;
            find.Detail.LastLoginIp = HttpWeb.Ip;
            find.Detail.LoginNum += 1;

            if (!loginInput.OpenId.IsNullOrEmpty()) find.Detail.OpenId = loginInput.OpenId;
            Ioc.Resolve<IUserDetailService>().Update(find.Detail);

            //TODO 9月重构注释
            //初始化用户资产账户
            //   Resolve<IAccountService>().InitSingleUserAccount(find.Id);

            try
            {
                var userOutput = Resolve<IUserDetailService>().GetUserOutput(find.Id);
                return new Tuple<ServiceResult, UserOutput>(ServiceResult.Success, userOutput);
            }
            catch (Exception ex)
            {
                return new Tuple<ServiceResult, UserOutput>(ServiceResult.FailedWithMessage(ex.Message), null);
            }
        }

        public Tuple<ServiceResult, UserOutput> Reg(RegInput regInput)
        {
            var config = Resolve<IAutoConfigService>().GetValue<AdminCenterConfig>();
            if (!config.IsAllowUserReg)
                return new Tuple<ServiceResult, UserOutput>(ServiceResult.FailedWithMessage("系统维护升级中，禁止会员注册，请稍后重试"),
                    null);
            var userConfig = Resolve<IAutoConfigService>().GetValue<UserConfig>();
            if (userConfig.NeedPhoneVierfyCode)
            {
                //if (!_messageManager.CheckMobileVerifiyCode(parameter.Mobile,
                //    parameter.MobileVerifiyCode.ConvertToLong())) {
                //    return ApiResult.Failure<UserOutput>("手机验证码错误", MessageCodes.ParameterValidationFailure);
                //}
            }

            // 验证推荐账号
            if (!regInput.ParentUserName.IsNullOrEmpty())
            {
                if (regInput.ParentUserName == regInput.Mobile || regInput.ParentUserName == regInput.UserName)
                    return new Tuple<ServiceResult, UserOutput>(ServiceResult.FailedWithMessage("推荐账号不能为注册账号"), null);
                var parentUser = Resolve<IUserService>().GetSingle(s => s.UserName == regInput.ParentUserName);
                if (parentUser != null)
                {
                    if (parentUser.Status != Status.Normal)
                        return new Tuple<ServiceResult, UserOutput>(ServiceResult.FailedWithMessage("推荐人状态不正常"), null);
                    regInput.ParentId = parentUser.Id;
                }
                else
                {
                    return new Tuple<ServiceResult, UserOutput>(ServiceResult.FailedWithMessage("推荐账号不存在"), null);
                }
            }

            if (userConfig.NeedSelectServiceCenter && regInput.ParentId <= 0)
                return new Tuple<ServiceResult, UserOutput>(ServiceResult.FailedWithMessage("推荐人不能为空"), null);

            var user = new User
            {
                UserName = regInput?.UserName?.TrimEnd(' '),
                Mobile = regInput?.Mobile?.TrimEnd(' '),
                Email = regInput?.Email?.TrimEnd(' '),
                Name = regInput?.Name?.TrimEnd(' ')
            };

            if (regInput.UserName.IsNullOrEmpty()) user.UserName = regInput.Mobile;

            if (user.Email.IsNullOrEmpty()) user.Email = user.UserName + userConfig.RegEmailPostfix;

            user.Detail = new UserDetail
            {
                Password = regInput.Password,
                PayPassword = regInput.PayPassword,
                OpenId = regInput.OpenId
            };

            if (!regInput.ServiceCenter.IsNullOrEmpty())
            {
                var serviceUser = Resolve<IUserService>().GetSingle(regInput.ServiceCenter);
                if (serviceUser == null)
                    return new Tuple<ServiceResult, UserOutput>(ServiceResult.FailedWithMessage("服务中心不存在"), null);

                if (serviceUser.Status != Status.Normal)
                    return new Tuple<ServiceResult, UserOutput>(ServiceResult.FailedWithMessage("服务中心用户状态不正常"), null);
            }

            if (!regInput.OpenId.IsNullOrEmpty()) user.Detail.OpenId = regInput.OpenId;

            if (!user.Detail.PayPassword.IsNullOrEmpty())
                user.Detail.PayPassword = regInput.PayPassword.ToMd5HashString();

            var result = Register(user);
            if (result.Succeeded) return new Tuple<ServiceResult, UserOutput>(result, null);

            var userOutput = Resolve<IUserDetailService>().GetUserOutput(user.Id);
            return new Tuple<ServiceResult, UserOutput>(ServiceResult.Success, userOutput);
        }

        /// <summary>
        ///     register as an asynchronous operation.
        /// </summary>
        /// <param name="user">The 会员.</param>
        public ServiceResult Register(User user)
        {
            var userConfig = Ioc.Resolve<IAutoConfigService>().GetValue<UserConfig>();

            //密码不区分大小写
            if (user.Detail == null) user.Detail = new UserDetail();

            if (user.Map == null) user.Map = new UserMap();

            if (user.Name.IsNullOrEmpty()) user.Name = string.Empty;

            if (user.Email.IsNullOrEmpty()) user.Email = string.Empty;

            user.ParentId = user.ParentId;
            if (user.ParentId > 0)
                user.Map.ParentMap = Ioc.Resolve<IUserMapService>().GetParentMap(user.ParentId);
            else
                user.Map.ParentMap = new List<ParentMap>().ToJson();

            if (!user.UserName.StartsWith("WX"))
            {
                if (Resolve<IUserService>().ExistsMobile(user.Mobile))
                    return ServiceResult.FailedWithMessage("该手机号码已注册为会员，请选择新的手机号码!");

                if (string.IsNullOrWhiteSpace(user.Detail.Password)) return ServiceResult.FailedWithMessage("密码不能为空");

                if (Resolve<IUserService>().ExistsUserName(user.UserName))
                    return ServiceResult.FailedWithMessage("用户名已存在");

                if (!user.Email.IsNullOrEmpty() && Resolve<IUserService>().ExistsMail(user.Email))
                    return ServiceResult.FailedWithMessage("该手机号码已注册为会员，请选择新的手机号码!");
            }

            var rawPassword = user.Detail.Password;
            user.Detail.Password = user.Detail.Password.ToMd5HashString();

            if (user.Detail.PayPassword.IsNullOrEmpty())
                if (!userConfig.DefaultPassword.IsNullOrEmpty())
                    user.Detail.PayPassword = userConfig.DefaultPassword.ToMd5HashString(); //默认支付密码

            // 开启第一次设置时密码为空
            if (userConfig.TheFirstUserSetPayPassword) user.Detail.PayPassword = string.Empty;

            TypeLable:
            var userType = Resolve<IAutoConfigService>().GetList<UserTypeConfig>()
                .FirstOrDefault(r => r.TypeClass == UserTypeEnum.Member);
            if (userType == null)
            {
                new UserTypeConfig().SetDefault();
                goto TypeLable;
            }

            //如果等级不存在则插入默认等级
            if (user.GradeId.IsGuidNullOrEmpty())
            {
                var defaultGrade = Resolve<IGradeService>().DefaultUserGrade;
                if (defaultGrade == null) return ServiceResult.FailedWithMessage("默认等级不存在，请联系管理员添加默认等级!");

                user.GradeId = defaultGrade.Id;
            }

            Resolve<IUserService>().AddUser(user);

            return ServiceResult.Success;
        }
    }
}