using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Finance.Domain.Dtos.Pay;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.Finance.Dtos;
using Alabo.App.Core.Themes.DiyModels.Lists;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Mapping;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.Finance.Controllers {

    /// <summary>
    ///     Class PayApiController.
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/Pay/[action]")]
    public class ApiPayController : ApiBaseController<Pay, long> {
        /// <summary>
        ///     The automatic configuration manager
        /// </summary>

        /// <summary>
        ///     The message manager
        /// </summary>

        /// <summary>
        ///     The 会员 manager
        /// </summary>

        /// <summary>
        ///     Initializes a new instance of the
        /// </summary>
        public ApiPayController(
            ) : base() {
            BaseService = Resolve<IPayService>();
        }

        /// <summary>
        ///     客户端传入访问终端类型，比如是App，还是微信小程序，还是电脑端，还是移动Web等，
        ///     服务端返回可用的支付类型
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpGet]
        [Display(Description = "获取终端类型，返回支付类型")]
        [ApiAuth]
        public ApiResult<PayTypeOutput> GetList([FromQuery] ClientInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<PayTypeOutput>(this.FormInvalidReason(),
                    MessageCodes.ParameterValidationFailure);
            }

            var result = Resolve<IPayService>().GetPayType(parameter);
            if (result.Item1.Succeeded) {
                return ApiResult.Success(result.Item2);
            }

            return ApiResult.Failure<PayTypeOutput>(result.Item1.ToString());
        }

        /// <summary>
        ///     客户端传入通用订单，以及支付方式
        ///     完成相对用的支付
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpPost]
        [Display(Description = "传入通用订单和支付方式并完成相对应的支付")]
        [ApiAuth]
        public ApiResult<PayOutput> Pay([FromBody] PayInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<PayOutput>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }
            //Resolve<IPayService>().Log($"支付参数:{parameter.ToJson()}");
            var result = Resolve<IPayService>().Pay(parameter, HttpContext);
            Resolve<IPayService>().Log($"支付返回结果:{result.Item2.ToJson()}");
            if (result.Item1.Succeeded) {
                return ApiResult.Success(result.Item2);
            }

            return ApiResult.Failure<PayOutput>(result.Item1.ToString(), MessageCodes.ServiceFailure);
        }

        public ApiResult<PayOutput> PaySync(PayInput parameter) {
            //if (!this.IsFormValid()) {
            //    return ApiResult.Failure<PayOutput>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            //}
            //Resolve<IPayService>().Log($"支付参数:{parameter.ToJson()}");
            var result = Resolve<IPayService>().Pay(parameter, HttpContext);
            Resolve<IPayService>().Log($"支付返回结果:{result.Item2.ToJson()}");
            if (result.Item1.Succeeded) {
                return ApiResult.Success(result.Item2);
            }

            return ApiResult.Failure<PayOutput>(result.Item1.ToString(), MessageCodes.ServiceFailure);
        }

        /// <summary>
        ///     客户端传入通用订单，以及支付方式
        ///     完成相对用的支付
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpPost]
        [Display(Description = "传入通用订单和支付方式并完成相对应的支付")]
        [ApiAuth]
        public ApiResult WechatAppPay([FromBody] PayInput parameter) {
            var pay = Resolve<IPayService>().GetSingle(parameter.PayId);
            // 获取前台Url，实现跳转支付完成后跳转功能
            var url = HttpWeb.ClientHost;
            // 后台服务端Url
            var serviceUrl = HttpWeb.ServiceHost;

            var result = Resolve<IPayService>().WechatAppPayment(ref pay, url, serviceUrl);//(parameter, HttpContext);
            if (result.Item1.Succeeded) {
                return ApiResult.Success(result.Item2);
            }
            return ApiResult.Failure<PayOutput>(result.Item1.ToString(), MessageCodes.ServiceFailure);
        }

        /// <summary>
        ///     收银台
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "收银台")]
        [ApiAuth]
        public ApiResult<ListOutput> PayCheckList([FromQuery] ListInput parameter) {
            var model = Resolve<IPayService>().GetList(u => u.UserId == parameter.LoginUserId).ToList();
            var apiOutput = new ListOutput {
                TotalSize = model.Count() / parameter.PageSize
            };
            foreach (var item in model) {
                var apiData = new ListItem {
                    Title = $"金额{item.Amount}元",
                    Intro = $"{item.CreateTime.ToString("yyyy-MM-dd hh:ss")}",
                    //   Extra = "编号" + item.Serial,
                    Image = Resolve<IApiService>().ApiUserAvator(item.Id),
                    Id = item.Id,
                    Url = $"/pages/user?path=Asset_pay_view&id={item.Id}"
                };
                apiOutput.ApiDataList.Add(apiData);
            }

            return ApiResult.Success(apiOutput);
        }

        /// <summary>
        ///     收银详情
        /// </summary>
        /// <param name="loginUserId">登录会员Id</param>
        /// <param name="id">Id标识</param>
        [ApiAuth]
        [HttpGet]
        [Display(Description = "收银详情")]
        public ApiResult<PayPrivew> Preview([FromQuery] long loginUserId, long id) {
            var result = Resolve<IPayService>().GetSingle(r => r.Id == id);
            if (result != null) {
                if (result.UserId != loginUserId) {
                    return ApiResult.Failure<PayPrivew>("对不起，您无权查看他人提现详情");
                }

                var payprivew = AutoMapping.SetValue<PayPrivew>(result);
                return ApiResult.Success(payprivew);
            }

            return ApiResult.Failure<PayPrivew>("没有数据");
        }
    }
}