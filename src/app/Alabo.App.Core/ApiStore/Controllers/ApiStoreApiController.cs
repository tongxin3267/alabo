using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.ApiStore.AppVersion.Entities;
using Alabo.App.Core.ApiStore.AppVersion.Models;
using Alabo.App.Core.ApiStore.MiniProgram.Dtos;
using Alabo.App.Core.ApiStore.MiniProgram.Services;
using Alabo.App.Core.ApiStore.Sms.Entities;
using Alabo.App.Core.ApiStore.Sms.Enums;
using Alabo.App.Core.ApiStore.Sms.Models;
using Alabo.App.Core.ApiStore.WeiXinMp.Models;
using Alabo.App.Core.ApiStore.WeiXinMp.Services;
using Alabo.App.Core.Common;
using Alabo.App.Core.Common.Domain.CallBacks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Domains.Base.Services;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Core.ApiStore.Controllers {

    /// <summary>
    ///     ApiStore Api接口
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/ApiStore/[action]")]
    public class ApiStoreController : ApiBaseController {

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ApiStoreController" /> class.
        /// </summary>
        public ApiStoreController(
            ) : base() {
        }

        /// <summary>
        /// 微信小程序登录，微信公众号登录
        /// 根据后台设置确定是否获取头像
        /// </summary>
        /// <param name="jsCode">The js code.</param>
        [HttpGet]
        [Display(Description = "微信小程序登录，微信公众号登录")]
        public ApiResult<LoginOutput> Login([FromQuery] string jsCode) {
            var miniProgramLoginInput = new LoginInput {
                JsCode = jsCode
            };
            return Resolve<IMiniProgramService>().Login(miniProgramLoginInput);
        }

        [HttpGet]
        [Display(Description = "微信公众号登录")]
        public ApiResult<LoginOutput> WeixinPubLogin([FromQuery] string jsCode) {
            var miniProgramLoginInput = new LoginInput {
                JsCode = jsCode
            };

            return Resolve<IMiniProgramService>().PubLogin(miniProgramLoginInput);
        }

        /// <summary>
        /// 微信分享
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "微信分享")]
        public ApiResult<WeiXinShare> Share(WeiXinShareInput shareInput) {
            if (shareInput.Url.IsNullOrEmpty()) {
                return ApiResult.Failure<WeiXinShare>("分享网址不能为空");
            }

            //Resolve<IUserService>().Log("分享" + shareInput.ToJsons());

            var webSite = Resolve<IAutoConfigService>().GetValue<WebSiteConfig>();

            try {
                var result = Resolve<IWeixinMpService>().WeiXinShare(shareInput);
                return ApiResult.Success(result);
            } catch (Exception exception) {
                Resolve<ITableService>().Log("微信分享接口获取错误" + exception.Message);
                return ApiResult.Failure<WeiXinShare>(exception.Message);
            }
        }

        /// <summary>
        /// 发送短息
        /// </summary>
        /// <param name="input">内容</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult SendSms([FromBody]SmsInput input) {
            try {
                if (string.IsNullOrEmpty(input.Message)) {
                    return ApiResult.Failure("请输入message内容");
                }
                if (input.Type == Sms.Enums.SmsType.Phone && string.IsNullOrEmpty(input.Phone)) {
                    return ApiResult.Failure("type为指定手机号码时,phone 不能为空");
                }
                IList<SmsSend> phones = new List<SmsSend>();
                if (input.Type == Sms.Enums.SmsType.All) {
                    var users = Resolve<IUserService>().GetList();
                    if (Resolve<ApiStore.Sms.Services.ISmsSendService>().GetAll(SendState.Root).Count() <= 0) {
                        var sendList = users.Select(s => new SmsSend {
                            State = SendState.Root,
                            Phone = s.Mobile,
                            UserId = s.Id,
                            UserName = s.UserName,
                            CreateTime = DateTime.Now
                        }).ToList();
                        Resolve<ApiStore.Sms.Services.ISmsSendService>().Add(sendList);
                        phones = Resolve<ApiStore.Sms.Services.ISmsSendService>().GetAll(input.State);
                    } else {
                        phones = Resolve<ApiStore.Sms.Services.ISmsSendService>().GetAll(input.State);
                    }

                    //phones = users.Select(s => new SmsSend
                    //{
                    //    State = SendState.Root,
                    //    Phone = s.Mobile,
                    //    UserId=s.Id
                    //}).ToList();
                    //phones = input.Phone.Split(",").Select(s => new SmsSend
                    //{
                    //    State = SendState.Root,
                    //    Phone = s,
                    //}).ToList();
                } else {
                    phones = input.Phone.Split(",").Select(s => new SmsSend {
                        State = SendState.Root,
                        Phone = s,
                    }).ToList();
                }
                for (int i = 1249; i < phones.Count; i++) {
                    if (!string.IsNullOrEmpty(input.Message)) {
                        //Task.Factory.StartNew(() =>
                        //{
                        var result = Resolve<IOpenService>().SendRaw(phones[i].Phone, input.Message);
                        if (result.Status == ResultStatus.Success) {
                            phones[i].State = SendState.Success;
                            Resolve<ApiStore.Sms.Services.ISmsSendService>().Update(phones[i]);
                        } else {
                            phones[i].State = SendState.Fail;
                            Resolve<ApiStore.Sms.Services.ISmsSendService>().Update(phones[i]);
                        }
                        //});
                    }
                    //if (i % 50 == 0)
                    //{
                    Thread.Sleep(500);
                    //}
                    // i++;
                }
                return ApiResult.Success("执行完毕");
            } catch (Exception ex) {
                return ApiResult.Failure(ex.Message);
            }
        }

        /// <summary>
        /// APP版本检测
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<AppVersionOutput> AppCheckVersion(AppVersionInput input) {
            var config = Resolve<IAutoConfigService>().GetValue<AppVersionConfig>();
            //获取服务器app当前版本
            if (config.IsEnble && !input.Version.Equals(config.Version)) {
                //最新版本
                return new ApiResult<AppVersionOutput>() {
                    Result = new AppVersionOutput() {
                        Note = config.Note,
                        Status = AppVersion.Enums.AppVersionStatus.Use,
                        Url = config.Url
                    },
                    Status = ResultStatus.Success,
                    Message = string.Empty,
                    MessageCode = 200
                };
            } else {
                return new ApiResult<AppVersionOutput>() {
                    Result = new AppVersionOutput() {
                        Status = AppVersion.Enums.AppVersionStatus.UnUp,
                    },
                    Status = ResultStatus.Success,
                    Message = string.Empty,
                    MessageCode = 200
                };
            }
        }
    }
}