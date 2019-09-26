using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.App.Open.Tasks.Base;
using Alabo.Helpers;
using System.Collections.Generic;
using System.Linq;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Users.Entities;

namespace Alabo.App.Open.Tasks.Modules {

    using User = User;

    public abstract class AssetAllocationShareModuleBase<TConfiguration> : ShareModuleBase<TConfiguration>
        where TConfiguration : ShareBaseConfig {

        public AssetAllocationShareModuleBase(TaskContext context, TConfiguration configuration)
            : base(context, configuration) {
        }

        protected void CreateResultList(decimal shareAmount, User shareUser, TaskParameter parameter, ShareBaseConfig config, IList<ITaskResult> resultList, string configName = "") {
            CreateResultList(shareAmount, base.ShareOrderUser, shareUser, parameter, config, resultList, configName = "");
        }

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="shareAmount">分润金额</param>
        /// <param name="orderUser">下单用户</param>
        /// <param name="shareUser">收益用户</param>
        /// <param name="parameter">参数</param>
        /// <param name="config">分润配置</param>
        /// <param name="resultList"></param>
        /// <param name="configName"></param>
        protected void CreateResultList(decimal shareAmount, User orderUser, User shareUser, TaskParameter parameter, ShareBaseConfig config, IList<ITaskResult> resultList, string configName = "") {
            //如果分润金额小于等于0,则退出
            if (shareAmount > 0 && shareUser != null) {
                if (shareUser.Status == Alabo.Domains.Enums.Status.Normal) {
                    parameter.TryGetValue("OrderId", out long orderId);

                    // 如果限制供应商购买过的店铺
                    // 检查该会员是否购买过该店铺的商品,核对User_TypeUser表
                    if (Configuration.ProductRule.IsLimitStoreBuy) {
                        //// TODO 如果是订单用户
                        //if (TriggerType == TriggerType.Order) {
                        //    var order = Ioc.Resolve<IOrderService>().GetSingle(orderId);
                        //    if (order != null) {
                        //        var storeUser = Ioc.Resolve<ITypeUserService>()
                        //            .GetStoreUser(order.StoreId, shareUser.Id);
                        //        if (storeUser == null) {
                        //            //分润用户不是该店铺的用户 退出
                        //            return;
                        //            // ExecuteResult<ITaskResult>.Cancel($"分润用户不是该店铺的用户");
                        //        }
                        //    }
                        //}
                    }

                    var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>(r => r.Status == Alabo.Domains.Enums.Status.Normal);
                    foreach (var rule in Configuration.RuleItems) {
                        var ruleAmount = shareAmount * rule.Ratio;
                        var ruleMoneyType = moneyTypes.FirstOrDefault(r => r.Id == rule.MoneyTypeId);
                        if (ruleMoneyType == null) {
                            continue;
                            //ExecuteResult<ITaskResult>.Cancel($"资产分润规则设置错误，货币类型Id{ruleMoneyType.Id}");
                        }

                        var shareResult = new ShareResult {
                            OrderUser = orderUser,
                            ShareUser = shareUser,
                            ShareOrder = base.ShareOrder,
                            Amount = ruleAmount,
                            MoneyTypeId = rule.MoneyTypeId,
                            ModuleConfigId = config.Id,
                            ModuleId = config.ModuleId,
                            SmsNotification = config.TemplateRule.SmsNotification
                        };
                        //描述
                        shareResult.Intro = Configuration.TemplateRule.LoggerTemplate.Replace("{OrderUserName}", orderUser.GetUserName())
                        .Replace("{ShareUserName}", shareUser.GetUserName())
                        .Replace("{ConfigName}", configName)
                        .Replace("{AccountName}", ruleMoneyType.Name)
                        .Replace("{OrderId}", orderId.ToString())
                        .Replace("{OrderPrice}", base.ShareOrder.Amount.ToString("F2"))
                        .Replace("{ShareAmount}", ruleAmount.ToString("F2"));

                        //短信内容
                        shareResult.SmsIntro = Configuration.TemplateRule.LoggerTemplate.Replace("{OrderUserName}", orderUser.GetUserName())
                        .Replace("{ShareUserName}", shareUser.GetUserName())
                         .Replace("{ConfigName}", configName)
                        .Replace("{AccountName}", ruleMoneyType.Name)
                        .Replace("{OrderId}", orderId.ToString())
                         .Replace("{OrderPrice}", base.ShareOrder.Amount.ToString("F2"))
                        .Replace("{ShareAmount}", ruleAmount.ToString("F2"));

                        var queueResult = new TaskQueueResult<ITaskResult>(Context) {
                            ShareResult = shareResult
                        };
                        resultList.Add(queueResult);
                    }
                }
            }
        }
    }
}