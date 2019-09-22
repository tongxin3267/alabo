using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.App.Core.Api.Dtos;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.Dtos.Account;
using Alabo.App.Core.Finance.Domain.Dtos.Bill;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.Themes.DiyModels.Lists;
using Alabo.App.Core.Themes.DiyModels.Previews;
using Alabo.App.Core.Themes.Extensions;
using Alabo.App.Core.User;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Core.Finance.Controllers {

    /// <summary>
    ///     Class UserAccountApiController.
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/User/account/[action]")]
    public class UserAccountApiController : ApiBaseController {
        /// <summary>
        ///     The automatic configuration manager
        /// </summary>

        /// <summary>
        ///     The 会员 manager
        /// </summary>

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserAccountApiController" /> class.
        /// </summary>
        public UserAccountApiController() : base() {
        }

        /// <summary>
        ///     所有s the accounts.
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpGet]
        [Display(Description = "所有S the accounts")]
        [ApiAuth]
        public ApiResult<IList<AccountOutput>> AllAccounts([FromQuery] ApiBaseInput parameter) {
            var httpss = HttpContext.Request.QueryString;
            var model = Resolve<IAccountService>().GetUserAllAccount(parameter.LoginUserId);
            var moneys = Resolve<IAutoConfigService>().MoneyTypes();
            IList<AccountOutput> result = new List<AccountOutput>();
            foreach (var item in model) {
                var config = moneys.FirstOrDefault(r => r.Id == item.MoneyTypeId && r.IsShowFront && r.Status == Status.Normal);
                if (config != null) {
                    var apiOutput = new AccountOutput {
                        MoneyTypeName = config.Name,
                        Amount = item.Amount,
                        MoneyTypeId = item.MoneyTypeId
                    };
                    result.Add(apiOutput);
                }
            }

            return ApiResult.Success(result);
        }

        /// <summary>
        ///     Bills the specified parameter.
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpGet]
        [Display(Description = "bill  the  soecified parameter")]
        [ApiAuth]
        public ApiResult<ListOutput> Bill([FromQuery] ListInput parameter) {
            var moneyTypes = Resolve<IAutoConfigService>().MoneyTypes();
            var billApiInput = new BillApiInput {
                LoginUserId = parameter.LoginUserId,
                PageIndex = parameter.PageIndex,
                PageSize = parameter.PageSize
            };

            var billList = Resolve<IFinanceAdminService>().GetApiBillPageList(billApiInput, out var count);
            var result = new ListOutput {
                StyleType = 1, //采用第一种样式
                TotalSize = count / parameter.PageSize
            };
            foreach (var item in billList.ToList()) {
                var index = new Random().Next(1, 8);
                var apiData = new ListItem {
                    Id = item.Id,
                    Intro = $"账后{item.AfterAmount} 时间{item.CreateTime.ToString("yyyy-MM-dd hh:ss")}",
                    Title = $"{moneyTypes.FirstOrDefault(e => e.Id == item.MoneyTypeId)?.Name}-{item.Type.GetDisplayName()}",
                    Image = Resolve<IApiService>().ApiImageUrl($"/assets/mobile/images/icon/demo{index}.png"),
                    Url = $"/finance/bill/view?id={item.Id}".ToClientUrl(ClientType.WapH5),
                    Extra = item.Amount.ToStr()
                };
                result.ApiDataList.Add(apiData);
            }

            return ApiResult.Success(result);
        }

        /// <summary>
        ///     Bills the 视图.
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpGet]
        [Display(Description = "bills the 视图 ")]
        public ApiResult BillView([FromQuery] PreviewInput parameter) {
            var view = Resolve<IFinanceAdminService>().GetViewBillSingle(parameter.Id.ConvertToLong());
            if (view == null) {
                return ApiResult.Failure("账单不存在或者已经删除！");
            }

            if (view.Bill.UserId != parameter.LoginUserId) {
                return ApiResult.Failure("您无权查看该账单！");
            }

            var apiOutput = Resolve<IFinanceAdminService>().GetBillOutput(view);
            var temp = apiOutput.ToKeyValues();

            return ApiResult.Success(temp);
        }

        /// <summary>
        ///     Transfers the specified parameter.
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpGet]
        [Display(Description = "transfers the specified parameter")]
        public ApiResult Transfer([FromQuery] ApiBaseInput parameter) {
            //var model = Resolve<IFinanceAdminService>().GetViewBillPageList(parameter);
            //return ApiResult.Success(model);
            return null;
        }

        /// <summary>
        ///转账
        /// </summary>

        //[HttpPost]
        // [Display(Description = "转账")]
        //public ApiResult Transfer([FromBody]TransferApiInput view) {
        //    var adminConfig = Resolve<IAutoConfigService>().GetValue<AdminCenterConfig>();
        //    if (!adminConfig.IsAllowUserTransfer) {
        //        ModelState.AddModelError(string.Empty, $"当前时间段系统正在进行内部调整,请不要转账。请稍后再试，给您带来的不便深感抱歉!");
        //        return View(view);
        //    }

        //    if (!this.IsFormValid()) {
        //        ModelState.AddModelError(string.Empty, this.FormInvalidReason());
        //        return View(view);
        //    }
        //    var loginUser = Resolve<IUserService>().GetUserDetail(AutoModel.BasicUser.Id);
        //    if (loginUser.Status != Status.Normal) {
        //        ModelState.AddModelError(string.Empty, "转账失败：您的账户不正常，不能进行转账");
        //        return View(view);
        //    }
        //    if (loginUser.Detail.PayPassword != view.PayPassword.ToMd5HashString()) {
        //        ModelState.AddModelError(string.Empty, "转账失败：支付密码不正确");
        //        return View(view);
        //    }
        //    var transferConfig = Resolve<IAutoConfigService>().GetList<TransferConfig>().FirstOrDefault(r => r.Id == view.TransferId);
        //    if (transferConfig == null) {
        //        ModelState.AddModelError(string.Empty, "转账类型不存在！");
        //        return View(view);
        //    }
        //    //倍数
        //    if (transferConfig.Multiple > 0) {
        //        if (view.Amount % transferConfig.Multiple != 0) {
        //            ModelState.AddModelError(string.Empty, $"转账金额输入不对，必须是{transferConfig.Multiple}的倍数");
        //            return View(view);
        //        }
        //    }

        //    var OtherUser = Resolve<IUserService>().GetUserDetail(view.OtherUserName);
        //    if (OtherUser == null) {
        //        ModelState.AddModelError(string.Empty, "收款人不存在,请重新输入");
        //        return View(view);
        //    }

        //    if (OtherUser.Status != Status.Normal) {
        //        ModelState.AddModelError(string.Empty, "转账失败：收款人账户不正常，已删除或者被冻结");
        //        return await Task.FromResult(View(view));
        //    }

        //    //如果可以转账给他人
        //    //if (transferConfig.CanTransferOther) {
        //    //开启推荐关系限制 推荐关系限制
        //    if (transferConfig.IsRelation) {
        //        var isRelation = false;
        //        var loginUserMap = loginUser.Map.ParentMap.Deserialize(new { ParentLevel = 0L, UserId = 0L });
        //        if (loginUserMap != null) {
        //            var loginPartertIds = loginUserMap.Select(r => r.UserId);
        //            if (loginPartertIds.Contains(OtherUser.Id)) {
        //                isRelation = true;
        //            }
        //        }
        //        if (!isRelation) {
        //            var otherUserMap = OtherUser.Map.ParentMap.Deserialize(new { ParentLevel = 0L, UserId = 0L });
        //            if (otherUserMap != null) {
        //                var otherUserParentIds = otherUserMap.Select(r => r.UserId);
        //                if (otherUserParentIds.Contains(loginUser.Id)) {
        //                    isRelation = true;
        //                }
        //            }
        //        }
        //        if (!isRelation) {
        //            ModelState.AddModelError("OtherUserName", "您选择的转账用户和当前登录用户推荐关系不符合转账要求！");
        //            return View(view);
        //        }
        //    }

        //    //只能转账给自己
        //    if (!transferConfig.CanTransferOther && transferConfig.CanTransferSelf) {
        //        if (OtherUser.Id != AutoModel.BasicUser.Id) {
        //            ModelState.AddModelError("OtherUserName", "当前配置要求您只能转账给自己！");
        //            return View(view);
        //        }
        //    }
        //    var result = Resolve<IBillService>().Transfer(loginUser, OtherUser, transferConfig, view.Amount);
        //    if (!result.Succeeded) {
        //        ModelState.AddModelError(string.Empty, result.ToString());
        //        return View(view);
        //    } else {
        //        string userMessage = $"尊敬{loginUser.UserName}您好，您刚刚操作的{transferConfig.Name}转账已成功，转账金额为{view.Amount}";
        //        //await Resolve<ISendTemplatesService>().SendSingleAsync(loginUser.Mobile, userMessage);

        //        userMessage = $"尊敬{OtherUser.UserName}您好，您成功收到{loginUser.UserName}的转账，转金额为{view.Amount},转账类型为{transferConfig.Name}";
        //        // await Resolve<ISendTemplatesService>().SendSingleAsync(OtherUser.Mobile, userMessage);
        //    }

        //    return InformationMessage("系统信息", "您的转账申请已经提交成功！",
        //        MessageLink.CreateRouteLink("返回", "返回会员中心", new { controller = "User", action = "Index", app = "User" }),
        //        MessageLink.CreateRouteLink("继续转账", "继续转账", new { controller = "UserAccount", action = "Transfer", app = "Finance" }),
        //        MessageLink.CreateRouteLink("转账明细", "前往转账明细", new { controller = "UserAccount", action = "TransferBill", app = "Finance" }));
        //}

        //[HttpGet]
        //public ApiResult Recharge([FromQuery]RechargeApiInput parameter) {
        //    ExpressionQuery<Recharge> query = new ExpressionQuery<Recharge> {
        //        //query.And(e => e.UserId.Equals(AutoModel.BasicUser.Id));
        //        PageSize = 15,
        //        PageIndex = 1
        //    };
        //    query.OrderByDescending(e => e.Id);
        //    var model = Resolve<IRechargeService>().GetPagedList(query);
        //    return ApiResult.Success(model);
        //}

        //[HttpGet]
        //public ApiResult WithDraw([FromQuery]WithDrawApiInput parameter) {
        //    ExpressionQuery<Recharge> query = new ExpressionQuery<Recharge>();
        //    //query.And(e => e.UserId.Equals(AutoModel.BasicUser.Id));
        //    query.OrderByDescending(e => e.Id);
        //    query.PageSize = 15;
        //    query.PageIndex = 1;
        //    var model = Resolve<IRechargeService>().GetPagedList(query);
        //    return ApiResult.Success(model);
        //}
        //}
    }
}