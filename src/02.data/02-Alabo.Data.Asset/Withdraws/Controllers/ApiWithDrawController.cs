﻿using Alabo.App.Asset.Withdraws.Domain.Services;
using Alabo.App.Asset.Withdraws.Dtos;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Core.WebUis.Models.Lists;
using Alabo.UI.Design.AutoForms;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Asset.Withdraws.Controllers
{
    /// <summary>
    ///     提现相关的Api接口
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/WithDraw/[action]")]
    public class WithDrawApiController : ApiBaseController
    {
        /// <summary>
        ///     获取允许提现的账户类型
        /// </summary>
        [HttpGet]
        [Display(Description = "获取允许提现的账户类型")]
        [ApiAuth]
        public ApiResult GetAccountType([FromQuery] long loginUserId)
        {
            var result = Resolve<IWithdrawService>().GetWithDrawMoneys(loginUserId);
            if (result != null) {
                return ApiResult.Success(result);
            }

            return ApiResult.Failure("提现类型获取失败", MessageCodes.ParameterValidationFailure);
        }

        /// <summary>
        ///     用户申请提现
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpPost]
        [Display(Description = "用户申请提现")]
        [ApiAuth]
        public ApiResult Add([FromBody] WithDrawInput parameter)
        {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            parameter.UserId = parameter.LoginUserId;
            var serviceResult = Resolve<IWithdrawService>().Add(parameter);
            return ToResult(serviceResult);
        }

        /// <summary>
        ///     删除用户提现
        /// </summary>
        /// <param name="loginUserId">登录会员Id</param>
        /// <param name="id">Id标识</param>
        [HttpDelete]
        [Display(Description = "删除用户提现")]
        [ApiAuth]
        public ApiResult Delete([FromQuery] long loginUserId, long id)
        {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var serviceResult = Resolve<IWithdrawService>().Delete(loginUserId, id);
            return ToResult(serviceResult);
        }

        [HttpGet]
        [Display(Description = "用户提现列表")]
        [ApiAuth]
        public ApiResult<ListOutput> List([FromQuery] ListInput parameter)
        {
            //var model = Resolve<ITradeService>()
            //    .GetList(u => u.UserId == parameter.LoginUserId && u.Type == TradeType.Withdraw).ToList();
            //var apiOutput = new ListOutput {
            //    TotalSize = model.Count() / parameter.PageSize
            //};
            //foreach (var item in model) {
            //    var apiData = new ListItem {
            //        Title = $"提现金额{item.Amount}元",
            //        Intro = $"{item.CreateTime.ToString("yyyy-MM-dd hh:ss")}",
            //        Extra = "编号" + item.Serial,
            //        Image = Resolve<IApiService>().ApiUserAvator(item.Id),
            //        Id = item.Id,
            //        Url = $"/pages/user?path=Asset_withdraw_view&id={item.Id}"
            //    };
            //    apiOutput.ApiDataList.Add(apiData);
            //}

            //return ApiResult.Success(apiOutput);
            return null;
        }

        /// <summary>
        ///     获取提现详情
        /// </summary>
        /// <param name="loginUserId">登录会员Id</param>
        /// <param name="id">Id标识</param>
        [ApiAuth]
        [HttpGet]
        [Display(Description = "获取提现详情")]
        public ApiResult Get([FromQuery] long loginUserId, long id)
        {
            var result = Resolve<IWithdrawService>().GetSingle(loginUserId, id);
            if (result != null)
            {
                if (result.UserId != loginUserId) {
                    return ApiResult.Failure<WithDrawShowOutput>("对不起，您无权查看他人提现详情");
                }

                return ApiResult.Success(result.ToKeyValues());
            }

            return ApiResult.Failure("没有数据");
        }

        [HttpGet]
        [Display(Description = "获取转账视图")]
        public ApiResult<AutoForm> GetWithDrawForm()
        {
            var view = AutoFormMapping.Convert<WithDrawInput>();
            return ApiResult.Success(view);
        }

        [ApiAuth]
        [HttpGet]
        [Display(Description = "提现记录")]
        public ApiResult GetList()
        {
            //var result = new List<WithDrawOutput>();
            //var model = Resolve<ITradeService>().GetPagedList(Query, u => u.Type == TradeType.Withdraw);
            //var moneyConfigList = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            //foreach (var item in model) {
            //    var withDraw = AutoMapping.SetValue<WithDrawOutput>(item);
            //    withDraw.MoneyTypeName = moneyConfigList.FirstOrDefault(u => u.Id == item.MoneyTypeId)?.Name;
            //    result.Add(withDraw);
            //}
            //return ApiResult.Success(result);
            return null;
        }
    }
}