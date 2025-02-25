﻿using Alabo.App.Asset.Pays.Domain.Entities;
using Alabo.App.Asset.Recharges.Domain.Entities;
using Alabo.App.Asset.Recharges.Domain.Services;
using Alabo.App.Asset.Recharges.Dtos;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Core.WebUis.Models.Lists;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Asset.Recharges.Controllers
{
    /// <summary>
    ///     充值相关的Api接口
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/Recharge/[action]")]
    public class ApiRechargeController : ApiBaseController<Recharge, long>
    {
        public ApiRechargeController()
        {
            BaseService = Resolve<IRechargeService>();
        }

        /// <summary>
        ///     获取允许充值的账户类型
        /// </summary>
        [HttpGet]
        [Display(Description = "获取允许充值的账户类型")]
        public ApiResult GetAccountType()
        {
            var result = Resolve<IRechargeService>().GetRechargeMoneys();
            if (result != null) {
                return ApiResult.Success(result);
            }

            return ApiResult.Failure("充值类型获取失败", MessageCodes.ParameterValidationFailure);
        }

        /// <summary>
        ///     充值，包括线下充值和线下充值
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpPost]
        [Display(Description = "充值，包括线下充值和线下充值")]
        [ApiAuth]
        public ApiResult Add([FromBody] RechargeAddInput parameter)
        {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var serviceResult = ServiceResult.Success;
            serviceResult = Resolve<IRechargeService>().AddOffOnline(parameter);
            return ToResult(serviceResult);
        }

        /// <summary>
        ///     线下充值
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpPost]
        [Display(Description = "线下充值")]
        [ApiAuth]
        public ApiResult AddOffOnline([FromBody] RechargeAddInput parameter)
        {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var serviceResult = ServiceResult.Success;
            serviceResult = Resolve<IRechargeService>().AddOffOnline(parameter);
            return ToResult(serviceResult);
        }

        /// <summary>
        ///     线上充值
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpPost]
        [Display(Description = "线上充值")]
        [ApiAuth]
        public ApiResult<Pay> AddOnline([FromBody] RechargeOnlineAddInput parameter)
        {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<Pay>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var result = Resolve<IRechargeService>().AddOnline(parameter);
            if (result.Item1.Succeeded) {
                return ApiResult.Success(result.Item2);
            }

            return ApiResult.Failure<Pay>(result.ToString(), MessageCodes.ServiceFailure);
        }

        /// <summary>
        ///     删除用户充值
        /// </summary>
        /// <param name="loginUserId">登录会员Id</param>
        /// <param name="id">Id标识</param>
        [HttpDelete]
        [Display(Description = "删除用户充值")]
        [ApiAuth]
        public ApiResult Delete([FromQuery] long loginUserId, long id)
        {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var serviceResult = Resolve<IRechargeService>().Delete(loginUserId, id);
            return ToResult(serviceResult);
        }

        /// <summary>
        ///     获取充值详情
        /// </summary>
        /// <param name="loginUserId">登录会员Id</param>
        /// <param name="id">Id标识</param>
        [ApiAuth]
        [HttpGet]
        [Display(Description = "获取充值详情")]
        public ApiResult Get([FromQuery] long loginUserId, long id)
        {
            var result = Resolve<IRechargeService>().GetSingle(id);
            if (result != null)
            {
                if (result.UserId != loginUserId) {
                    return ApiResult.Failure("对不起，您无权查看他人充值详情");
                }

                return ApiResult.Success(result);
            }

            return ApiResult.Failure("没有数据");
        }

        [ApiAuth]
        [HttpGet]
        [Display(Description = "获取充值列表")]
        public ApiResult<ListOutput> List([FromQuery] ListInput parameter)
        {
            //var model = Resolve<ITradeService>()
            //    .GetList(u => /*u.UserId == parameter.LoginUserId &&*/ u.Type == TradeType.Recharge).ToList();
            //var apiOutput = new ListOutput {
            //    TotalSize = model.Count() / parameter.PageSize
            //};
            //foreach (var item in model) {
            //    var apiData = new ListItem {
            //        Title = $"充值金额{item.Amount}元",
            //        Intro = $"{item.CreateTime.ToString("yyyy-MM-dd hh:ss")}",
            //        Extra = "编号" + item.Serial,
            //        Image = Resolve<IApiService>().ApiUserAvator(item.Id),
            //        Id = item.Id,
            //        Url = $"/pages/user?path=Asset_recharge_view&id={item.Id}"
            //    };
            //    apiOutput.ApiDataList.Add(apiData);
            //}

            // return ApiResult.Success();
            return null;
        }

        [ApiAuth]
        [HttpGet]
        [Display(Description = "充值记录")]
        public ApiResult GetRechargeList()
        {
            //var result = new List<RechargeOutput>();
            //var model = Resolve<ITradeService>().GetPagedList(Query, u => u.Type == TradeType.Recharge);
            //var moneyConfigList = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            //foreach (var item in model) {
            //    var recharge = AutoMapping.SetValue<RechargeOutput>(item);
            //    recharge.StatusName = item.Status.GetDisplayName();
            //    recharge.MoneyTypeName = moneyConfigList.FirstOrDefault(u => u.Id == item.MoneyTypeId)?.Name;
            //    result.Add(recharge);
            //}
            //return ApiResult.Success(result);
            return null;
        }
    }
}