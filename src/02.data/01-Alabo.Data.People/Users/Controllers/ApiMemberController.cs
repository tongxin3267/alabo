using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Core.ApiStore.CallBacks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Domains.Base.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using ZKCloud.Open.ApiBase.Models;
using Alabo.Web.Mvc.Controllers;
using UserDetail = Alabo.Users.Entities.UserDetail;

namespace Alabo.App.Core.User.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Member/[action]")]
    public class ApiMemberController : ApiBaseController {

        #region 会员注册

        /// <summary>
        ///     会员注册
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpPost]
        [Display(Description = "会员注册")]
        public ApiResult<UserOutput> Reg([FromBody] RegInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<UserOutput>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }
            var result = Resolve<IUserBaseService>().Reg(parameter);
            return ToResult(result);
        }

        #endregion 会员注册

        /// <summary>
        ///     Logins the specified parameter.
        ///     会员登录
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpPost]
        [Display(Description = "会员登录")]
        public ApiResult<UserOutput> Login([FromBody] LoginInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<UserOutput>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }
            var result = Resolve<IUserBaseService>().Login(parameter);
            return ToResult(result);
        }

        #region 使用OpenId完成会员登录

        /// <summary>
        ///     使用OpenId完成会员登录
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpPost]
        [Display(Description = "使用openid 完成会员登录")]
        public ApiResult<UserOutput> LoginByOpenId([FromBody] LoginInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<UserOutput>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var result = Resolve<IUserBaseService>().Login(parameter);
            return ToResult(result);
        }

        #endregion 使用OpenId完成会员登录

        #region 使用手机号和验证登录

        /// <summary>
        /// 使用手机号和验证登录
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<UserOutput> LoginByMobile([FromBody] LoginInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<UserOutput>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var result = Resolve<IUserBaseService>().Login(parameter);
            return ToResult(result);
        }

        #endregion 使用手机号和验证登录

        #region 小程序 相关操作未整理后的代码

        /// <summary>
        ///     小程序获取Openid
        ///     并通过Open完成注册或登录
        /// </summary>
        [HttpGet]
        [Display(Description = "小程序获取Openid")]
        public async Task<ApiResult<UserOutput>> MpLogin([FromQuery] MpLoginInput input) {
            var config = Resolve<IAutoConfigService>().GetValue<MiniProgramConfig>();
            var openId = Senparc.Weixin.WxOpen.AdvancedAPIs.Sns.SnsApi.JsCode2Json(config.AppID, config.AppSecret, input.code)
                .openid;
            if (openId.IsNullOrEmpty()) {
                return ApiResult.Failure<UserOutput>("获取openId失败");
            }

            return null;

            //try {
            //    var result = await _userManager.LoginByOpenIdAsync(openId);
            //    if (result.Succeeded) {
            //        var _user = Resolve<IUserService>().GetUserDetail(AutoModel.BasicUser.Id);
            //        var userOutput = Resolve<IUserDetailService>().GetUserOutput(_user.Id);
            //        return ApiResult.Success(userOutput); //登录成功，返回用户的详细信息
            //    } else {
            //        var user = new Domain.Entities.User {
            //            UserName = "WX" + new Random(DateTime.Now.Millisecond).Next(int.MaxValue).ToString()
            //                           .PadLeft(10, '0') + Resolve<IUserService>().MaxUserId(),
            //            Name = input.nickname,
            //            //ParentId = usercode,
            //            Detail = new UserDetail {
            //                Password = "111111".ToMd5HashString(),
            //                PayPassword = "111111".ToMd5HashString(),
            //                Avator = input.avatarUrl,
            //                OpenId = openId,
            //                Sex = input.gender == 1 ? Sex.Man : (input.gender == 2 ? Sex.WoMan : Sex.UnKnown)
            //            }
            //        };
            //        user.Email = user.UserName + "@qnn.com";
            //        user.Mobile = user.UserName;
            //      //  _userManager.RegisterAsync(user, true).GetAwaiter().GetResult();

            //        var userOutput = Resolve<IUserDetailService>().GetUserOutput(user.Id);
            //        return ApiResult.Success(userOutput); //登录成功，返回用户的详细信息
            //    }
            //} catch (Exception e) {
            //    return ApiResult.Failure<UserOutput>(e.Message);
            //}
        }

        /// <summary>
        ///     找回密码，密码传入明文
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpPost]
        [Display(Description = "找回密码，密码传入明文")]
        public ApiResult<UserOutput> MpBind([FromBody] MpBindInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<UserOutput>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            ////if (!_messageManager.CheckMobileVerifiyCode(parameter.Mobile, parameter.MobileVerifiyCode.ConvertToLong())) {
            ////    return ApiResult.Failure<UserOutput>("手机验证码错误", MessageCodes.ParameterValidationFailure);
            ////}

            //var fromUser = Resolve<IUserService>().GetSingle(parameter.CurUserId);
            //var fromDetail = Resolve<IUserDetailService>().GetSingle(r => r.UserId == parameter.CurUserId);

            //if (fromUser == null || fromDetail == null) {
            //    return ApiResult.Failure<UserOutput>("当前账号异常，无法绑定", MessageCodes.ParameterValidationFailure);
            //}

            //var result = _userManager.LoginByMobileAsync(parameter.Mobile).GetAwaiter().GetResult();
            //if (!result.Succeeded) {
            //    // 新账号不存在，更新原有账号mobile
            //    fromUser.Mobile = parameter.Mobile;
            //    Resolve<IUserService>().UpdateUser(fromUser);

            //    var ran = new Random(DateTime.Now.Millisecond);
            //    var pwd = ran.Next(1000000).ToString().PadLeft(6, '0');
            //    var payPwd = ran.Next(1000000).ToString().PadLeft(6, '0');
            //    fromDetail.Password = pwd.ToMd5HashString();
            //    fromDetail.PayPassword = payPwd.ToMd5HashString();
            //    Resolve<IUserDetailService>().UpdateSingle(fromDetail);

            //    var userOutput = Resolve<IUserDetailService>().GetUserOutput(parameter.CurUserId);

            //    // _messageManager.SendRaw(fromUser.Mobile, $"已为您自动创建平台账号，登录密码（{pwd}），支付密码（{payPwd}）");

            //    return ApiResult.Success(userOutput); //登录成功，返回用户的详细信息
            //} else {
            //    // 绑定新账号成功，更新新账号openid
            //    var toDetail = Resolve<IUserDetailService>().GetSingle(r => r.UserId == AutoModel.BasicUser.Id);

            //    if (fromDetail != null && toDetail != null) {
            //        toDetail.OpenId = fromDetail.OpenId;
            //        if (toDetail.NickName.IsNullOrEmpty() && !fromDetail.NickName.IsNullOrEmpty()) {
            //            toDetail.NickName = fromDetail.NickName;
            //        }

            //        if (toDetail.Avator.IsNullOrEmpty() && !fromDetail.Avator.IsNullOrEmpty()) {
            //            toDetail.Avator = fromDetail.Avator;
            //        }

            //        fromDetail.OpenId = null;
            //        Resolve<IUserDetailService>().UpdateSingle(fromDetail);
            //        Resolve<IUserDetailService>().UpdateSingle(toDetail);
            //    }
            //    var userOutput = Resolve<IUserDetailService>().GetUserOutput(AutoModel.BasicUser.Id);
            //    return ApiResult.Success(userOutput); //登录成功，返回用户的详细信息
            //}
            return null;
        }

        #endregion 小程序 相关操作未整理后的代码

        #region 微信相关操作，未整理

        ///// <summary>
        ///// 小程序, 手机 & OpenId 绑定与返回登录信息
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ApiResult LoginBindWX([FromBody] LoginWxInput input) {
        //    try {
        //        if (input.Mobile.IsNullOrEmpty()) {
        //            return ApiResult.Failure<UserOutput>("手机号码不能为空!");
        //        }

        //        if (!Resolve<IOpenService>().CheckVerifiyCode(input.Mobile, input.MobileVerifiyCode.ConvertToLong())) {
        //            return ApiResult.Failure("手机验证码错误", MessageCodes.ParameterValidationFailure);
        //        }

        //        // 传进Mobile, 尝试获取是否已经存在绑定信息
        //        var user = Resolve<IUserService>().GetSingleByUserNameOrMobile(input.Mobile);
        //        if (user != null) {
        //            var detail = Resolve<IUserDetailService>().GetSingle(x => x.UserId == user.Id);
        //            if (detail != null) {
        //                detail.OpenId = input.OpenId;
        //                detail.Avator = input.avatarUrl;

        //                var rsUpdate = Resolve<IUserDetailService>().Update(detail);
        //            }

        //            var userOutput = Resolve<IUserDetailService>().GetUserOutput(user.Id);
        //            var _user = Resolve<IUserService>().GetUserDetail(user.Id);
        //            userOutput.Token = Resolve<IUserService>().GetUserToken(_user);
        //            return ApiResult.Success(userOutput);
        //        } else   // 根据手机号注册, 更新Detail信息
        //          {
        //            var openId = "";

        //            if (input.OpenId.IsNotNullOrEmpty()) {
        //                openId = input.OpenId;
        //            } else {
        //                var config = Resolve<IAutoConfigService>().GetValue<MiniProgramConfig>();
        //                openId = SnsApi.JsCode2Json(config.AppID, config.AppSecret, input.code).openid;
        //                if (openId.IsNullOrEmpty()) {
        //                    return ApiResult.Failure<UserOutput>("获取openId失败");
        //                }
        //            }

        //            user = new Domain.Entities.User {
        //                UserName = input.Mobile,
        //                Name = input.nickname,
        //                Detail = new UserDetail {
        //                    Password = "111111",
        //                    PayPassword = "111111".ToMd5HashString(),
        //                    Avator = input.avatarUrl,
        //                    OpenId = openId,
        //                    Sex = input.gender == 1 ? Sex.Man : (input.gender == 2 ? Sex.WoMan : Sex.UnKnown)
        //                }
        //            };
        //            user.Email = user.UserName + "@qnn.com";
        //            user.Mobile = input.Mobile;
        //            //var result = _userManager.RegisterAsync(user, true).GetAwaiter().GetResult();
        //            //if (result.Succeeded) {
        //            //    var userOutput = Resolve<IUserDetailService>().GetUserOutput(user.Id);
        //            //    var _user = Resolve<IUserService>().GetUserDetail(user.Id);
        //            //    userOutput.Token = Resolve<IUserService>().GetUserToken(_user);
        //            //    return ApiResult.Success(userOutput); //登录成功，返回用户的详细信息
        //            //}

        //            return ApiResult.Failure($"注册失败"); //登录成功，返回用户的详细信息
        //        }
        //    } catch (Exception e) {
        //        return ApiResult.Failure<UserOutput>(e.Message);
        //    }
        //}

        #endregion 微信相关操作，未整理
    }
}