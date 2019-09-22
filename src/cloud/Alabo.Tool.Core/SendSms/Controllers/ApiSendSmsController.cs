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
    [Route("Api/SendSms/[action]")]
    public class ApiSendSmsController : ApiBaseController {
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
    }
}