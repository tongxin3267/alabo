using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.Finance.Domain.Dtos.Transfer;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.Themes.DiyModels.Lists;
using Alabo.App.Core.User;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Core.Finance.Controllers {

    /// <summary>
    ///     转账相关的Api接口
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/Transfer/[action]")]
    public class TransferApiController : ApiBaseController {
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
        ///     Initializes a new instance of the <see cref="TransferApiController" /> class.
        /// </summary>
        public TransferApiController(
            ) : base() {
        }

        /// <summary>
        ///     获取后台转账配置
        /// </summary>
        [HttpGet]
        public ApiResult GetTransferConfis() {
            var result = Resolve<ITransferService>().GetTransferConfig();
            if (result != null) {
                return ApiResult.Success(result);
            }

            return ApiResult.Failure("转账类型获取失败", MessageCodes.ParameterValidationFailure);
        }

        /// <summary>
        ///     转账接口：包括线上转账和
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpPost]
        [ApiAuth]
        public ApiResult Add([FromBody] TransferAddInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var serviceResult = Resolve<ITransferService>().Add(parameter);

            return ToResult(serviceResult);
        }

        ///// <summary>
        /////     获取用户所有转账数据
        ///// </summary>
        ///// <param name="parameter">参数</param>
        //[HttpGet]
        //[ApiAuth]
        //public ApiResult<ZKListApiOutput> GetUserList([FromQuery] TransferApiInput parameter) {
        //    var result = Resolve<ITransferService>().GetUserList(parameter);
        //    if (result != null)
        //        return ApiResult.Success(result);
        //    return ApiResult.Failure<ZKListApiOutput>("用户转账不存在", MessageCodes.ParameterValidationFailure);
        //}

        /// <summary>
        ///     获取转账详情
        /// </summary>
        /// <param name="loginUserId">登录会员Id</param>
        /// <param name="id">Id标识</param>
        [ApiAuth]
        [HttpGet]
        public ApiResult Get([FromQuery] long loginUserId, long id) {
            var result = Resolve<ITransferService>().GetSingle(id, loginUserId);
            if (result != null) {
                return ApiResult.Success(result);
            }

            return ApiResult.Failure("没有数据");
        }

        [ApiAuth]
        [Display(Description = "转账记录")]
        [HttpGet]
        public ApiResult<ListOutput> List([FromQuery] ListInput parameter) {
            var model = Resolve<ITradeService>()
                .GetList(u => /*u.UserId == parameter.LoginUserId &&*/ u.Type == TradeType.Transfer).ToList();
            var userList = Resolve<IUserService>().GetList();
            var apiOutput = new ListOutput {
                TotalSize = model.Count() / parameter.PageSize
            };
            foreach (var item in model) {
                var apiData = new ListItem {
                    Title = $"转账金额{item.Amount}元",
                    Intro =
                        $"转账对象{userList.FirstOrDefault(u => u.Id == item.UserId)?.UserName} {item.CreateTime.ToString("yyyy-MM-dd hh:ss")}",
                    Extra = "编号" + item.Serial,
                    Image = Resolve<IApiService>().ApiUserAvator(item.Id),
                    Id = item.Id,
                    Url = $"/pages/user?path=finance_transfer_view&id={item.Id}"
                };
                apiOutput.ApiDataList.Add(apiData);
            }

            return ApiResult.Success(apiOutput);
        }
    }
}