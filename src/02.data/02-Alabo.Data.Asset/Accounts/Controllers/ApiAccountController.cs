using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Alabo.App.Asset.Accounts.Domain.Entities;
using Alabo.App.Asset.Accounts.Domain.Services;
using Alabo.App.Asset.Accounts.Dtos;
using Alabo.App.Asset.Bills.Domain.Enums;
using Alabo.App.Asset.Bills.Domain.Services;
using Alabo.App.Asset.Recharges.Dtos;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Data.People.Users.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Dtos;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Maps;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Asset.Accounts.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Account/[action]")]
    public class ApiAccountController : ApiBaseController {

        public ApiAccountController(
           ) : base() {
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

        [HttpGet]
        [Display(Description = "区块链钱包")]
        public ApiResult<PagedList<ViewBlockChain>> BlockChainList([FromQuery] PagedInputDto parameter) {
            var model = Resolve<IAccountService>().GetBlockChainList(Query);
            return ApiResult.Success(model);
        }

        /// <summary>
        /// 获取账号金额
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="moneyConfigId"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<Account> GetAccount(long userId, Guid moneyConfigId) {
            var result = Resolve<IAccountService>().GetAccount(userId, moneyConfigId);
            return ApiResult.Success(result);
        }

        /// <summary>
        /// 资产列表
        /// </summary>
        /// <param name="parentName"></param>
        /// <param name="mobile"></param>
        /// <param name="UserName"></param>
        /// <param name="name"></param>
        /// <param name="serviceCenterId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult DetailList(string parentName, string mobile, string UserName, string name, long serviceCenterId, long pageIndex = 1, long pageSize = 20) {
            string getCondition() {
                var sb = new StringBuilder();
                if (!parentName.IsNullOrEmpty()) {
                    sb.AppendFormat("{0}={1}&", nameof(parentName), parentName);
                }

                if (!mobile.IsNullOrEmpty()) {
                    sb.AppendFormat("{0}={1}&", nameof(mobile), mobile);
                }

                if (!UserName.IsNullOrEmpty()) {
                    sb.AppendFormat("{0}={1}&", nameof(UserName), UserName);
                }

                if (!name.IsNullOrEmpty()) {
                    sb.AppendFormat("{0}={1}&", nameof(name), name);
                }

                if (sb.Length > 0) {
                    sb.Insert(0, "?");
                    sb.Remove(sb.Length - 1, 1);
                }

                return sb.ToString();
            }

            ViewBag.Condition = getCondition();
            ViewBag.MoneyTypes = Resolve<IAutoConfigService>().MoneyTypes();

            var userInput = new UserInput {
                Mobile = mobile,
                Name = name,
                UserName = UserName,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var parentUser = Resolve<IUserService>().GetSingle(parentName);
            if (parentUser != null) {
                userInput.ParentId = parentUser.Id;
            }

            var model = Resolve<IFinanceAdminService>().GetViewUserPageList(userInput);
            return ApiResult.Success(model);
        }

        /// <summary>
        /// 资产明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult DetailEdit(long id) {
            var user = Resolve<IUserService>().GetSingle(id);
            if (user == null) {
                return ApiResult.Failure("用户不存在", "当前用户不存在");
            }

            //初始化账户
            Resolve<IAccountService>().InitSingleUserAccount(user.Id);
            var montypeList = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>(r => r.IsShowFront);
            var accountList = Resolve<IAccountService>().GetUserAllAccount(user.Id);
            var rsAccList = accountList.MapTo<List<AccountApiView>>();

            rsAccList = rsAccList.Where(s => montypeList.Select(m => m.Id).Contains(s.MoneyTypeId)).ToList();

            rsAccList.ForEach(x => x.MoneyName = montypeList.FirstOrDefault(y => y.Id == x.MoneyTypeId).Name);

            var view = new {
                User = user,
                AccountList = rsAccList,
                UserId = user.Id,
                ActionType = 0,
                MoneyTypeId = Resolve<IAutoConfigService>().MoneyTypes()?.FirstOrDefault().Id,
            };

            return ApiResult.Success(view);
        }

        /// <summary>
        /// 资产明细
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult DetailEdit([FromBody] AccountViewIn view) {
            var chargeUser = Resolve<IUserService>().GetSingle(view.ChargeUserId);
            if (chargeUser == null) {
                return ApiResult.Failure("对应编辑的用户ID不存在");
            }
            view.User = chargeUser;
            view.AccountList = Resolve<IAccountService>().GetUserAllAccount(chargeUser.Id);
            //view.UserId = chargeUser.Id;

            if (!this.IsFormValid()) {
                return ApiResult.Failure(string.Empty, this.FormInvalidReason());
            }

            var loginUser = Resolve<IUserService>().GetUserDetail(view.UserId);
            if (loginUser.Status != Status.Normal) {
                return ApiResult.Failure(string.Empty, "操作失败：您的账户不正常，不能进行资产操作");
            }

            //var safePassword = HttpContext.Request.Form["SafePassWord"].ToString();
            if (view.AdminPayPassword.IsNullOrEmpty()) {
                return ApiResult.Failure(string.Empty, "操作失败：支付密码不能为空");
            }

            if (loginUser.Detail.PayPassword != view.AdminPayPassword.ToMd5HashString()) {
                return ApiResult.Failure(string.Empty, "操作失败：支付密码不正确");
            }

            var moneyType = Resolve<IAutoConfigService>().MoneyTypes().FirstOrDefault(r => r.Id == view.MoneyTypeId);
            if (moneyType == null) {
                return ApiResult.Failure("操作失败：对应的货币类型不支持!");
            }
            var result = ServiceResult.Success;
            if (view.ActionType == (int)FinanceActionType.Add) {
                if (view.Amount > 1000000) {
                    return ApiResult.Failure("Amount", "资产增加单次金额不能超过100W");
                }

                result = Resolve<IBillService>().Increase(chargeUser, moneyType, view.Amount, "管理员后台手动增加资产" + view.Remark);
            }

            if (view.ActionType == (int)FinanceActionType.Reduce) {
                result = Resolve<IBillService>().Reduce(chargeUser, moneyType, view.Amount, "管理员后台减少" + view.Remark);
            }

            if (view.ActionType == (int)FinanceActionType.Freeze) {
                result = Resolve<IBillService>().Treeze(chargeUser, moneyType, view.Amount, "管理员后台冻结" + view.Remark);
            }

            if (view.ActionType == (int)FinanceActionType.Unfreeze) {
                result = Resolve<IBillService>().DeductTreeze(chargeUser, moneyType, view.Amount, "管理员后台解结" + view.Remark);
            }

            if (!result.Succeeded) {
                return ApiResult.Failure(string.Empty, result.ToString());
            }

            Resolve<IAccountService>().Log(
                $@"管理员{AutoModel.BasicUser.UserName}{GetAcctionName(view.ActionType)}会员{chargeUser.UserName}的资产，账户为:{moneyType.Name}，金额为:{view.Amount}"
                );
            //通知会员后台手动添加资产
            var userAccount = Resolve<IAccountService>().GetAccount(chargeUser.Id, moneyType.Id);
            IDictionary<string, string> parameter = new Dictionary<string, string>
            {
                {"accountName", moneyType.Name},
                {"account", view.Amount.ToString()},
                {"userName", chargeUser.UserName},
                {"balance", userAccount.Amount.ToString()},
                {"acctionname", GetAcctionName(view.ActionType)}
            };

            //   _messageManager.AddTemplateQueue(2001, chargeUser.Mobile, parameter);
            // _messageManager.AddTemplateQueue(2002, _messageManager.OpenMobile, parameter);

            return ApiResult.Success($"{GetAcctionName(view.ActionType)}资产成功");
        }

        private string GetAcctionName(long type) {
            var name = "手动增加";
            if (type == (int)FinanceActionType.Reduce) {
                name = "手动减少";
            }

            if (type == (int)FinanceActionType.Freeze) {
                name = "冻结";
            }

            if (type == (int)FinanceActionType.Unfreeze) {
                name = "解结";
            }

            return name;
        }

        /// <summary>
        /// 储值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<RechageAccountOutput> Recharge([FromBody] RechargeAccountInput input) {
            var result = Resolve<IAccountPayService>().BuyAsync(input);
            if (result.Result.Item1.Succeeded) {
                return ApiResult.Result(result.Result.Item2);
            }

            return ApiResult.Failure<RechageAccountOutput>(null, "储值失败");
        }
    }
}