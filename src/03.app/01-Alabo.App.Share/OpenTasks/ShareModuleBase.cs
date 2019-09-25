using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Open.Tasks.Base;
using Alabo.App.Open.Tasks.Parameter;
using Alabo.App.Open.Tasks.Result;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using ZKCloud.Open.ApiBase.Models;
using User = Alabo.Users.Entities.User;

namespace Alabo.App.Open.Tasks {

    /// <summary>
    /// Class ShareModuleBase.
    /// </summary>
    public abstract class ShareModuleBase<TConfiguration> : UserAssetsModuleBase<TConfiguration>
        where TConfiguration : ShareBaseConfig {

        /// <summary>
        /// 售价金额对应的参数key，默认为"PriceAmount"
        /// </summary>
        protected string PriceAmountKey { get; set; } = "PriceAmount";

        /// <summary>
        /// 分润金额对应的参数key，默认为"BaseFenRunAmount"
        /// </summary>
        protected string FenRunAmountKey { get; set; } = "BaseFenRunAmount";

        /// <summary>
        /// 默认amount的参数key，通常在内部触发使用
        /// </summary>
        protected string DefaultAmountKey { get; set; } = "Amount";

        /// <summary>
        /// 触发类型对应的参数key，默认为"TriggerType"
        /// </summary>
        protected string TriggerTypeKey { get; set; } = "TriggerType";

        /// <summary>
        /// 触发时间对应的参数key，默认为"TriggerTime"
        /// </summary>
        protected string TriggerTimeKey { get; set; } = "TriggerTime";

        /// <summary>
        /// 触发货币类型对应的参数key，默认为"Currency"
        /// </summary>
        protected string TriggerCurrencyKey { get; set; } = "Currency";

        /// <summary>
        /// 订单Id
        /// </summary>
        protected long OrderId { get; set; } = 0;

        /// <summary>
        /// 分润基数
        /// 获取到参数FenRunAmount中的Amount
        /// 通常为实际分润价格
        /// </summary>
        protected decimal BaseFenRunAmount { get; set; }

        /// <summary>
        /// 触发类型
        /// </summary>
        protected TriggerType TriggerType { get; set; }

        /// <summary>
        /// Gets or sets the team level.
        /// 团队有效层数 全局定义
        /// </summary>
        protected long TeamLevel { get; set; }

        /// <summary>

        /// 触发货币类型
        /// </summary>
        protected Currency TriggerCurrency { get; set; } = Currency.Cny;

        /// <summary>
        /// 分润基类
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="configuration">The configuration.</param>
        public ShareModuleBase(TaskContext context, TConfiguration configuration)
            : base(context, configuration) {
        }

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        protected T Resolve<T>()
           where T : IService {
            return Alabo.Helpers.Ioc.Resolve<T>();
        }

        /// <summary>
        /// 对module配置与参数进行基础验证，子类重写后需要显式调用并判定返回值，如返回值不为Success，则不再执行子类后续逻辑
        /// </summary>
        /// <param name="parameter">参数</param>

        public override ExecuteResult<ITaskResult[]> Execute(TaskParameter parameter) {
            var baseResult = base.Execute(parameter);
            if (baseResult.Status != ResultStatus.Success) {
                return baseResult;
            }

            if (parameter.TryGetValue(TriggerTypeKey, out int triggerType)) {
                TriggerType = (TriggerType)triggerType;
                if (Configuration.TriggerType != TriggerType) {
                    //触发类型与配置的触发类型不一致，直接退出
                    return ExecuteResult<ITaskResult[]>.Cancel($"模块实际触发类型{triggerType}与模块设置的触发类型{Configuration.TriggerType}不一致,退出模块.");
                }
            } else {
                TriggerType = Configuration.TriggerType;
            }
            // 触发类型为会员注册
            if (TriggerType == TriggerType.UserReg) {
                parameter.TryGetValue(FenRunAmountKey, out decimal amount);
                BaseFenRunAmount = amount;
                if (BaseFenRunAmount <= 0) {
                    return ExecuteResult<ITaskResult[]>.Cancel($"分润基数价格获取为0. BaseFenRunAmount={BaseFenRunAmount}");
                }
            }

            // 触发类型为会员升级
            if (TriggerType == TriggerType.UserUpgrade) {
                parameter.TryGetValue(FenRunAmountKey, out decimal amount);
                BaseFenRunAmount = amount;
                if (BaseFenRunAmount <= 0) {
                    return ExecuteResult<ITaskResult[]>.Cancel($"分润基数价格获取为0. BaseFenRunAmount={BaseFenRunAmount},会员Id为{ShareOrder.UserId}");
                }
            }

            // TODO 2019年9月24日 触发类型为商城购物
            if (TriggerType == TriggerType.Order) {
                ////获取价格基数
                //if (parameter.TryGetValue("OrderId", out long orderId)) {
                //    // 获取符合条件的商品 价格依赖范围 所有商品 按所属商品线选择 按所属商城选择
                //    var effectiveOrderProductListAll = GetEffectiveOrderProduct(orderId);
                //    if (effectiveOrderProductListAll == null || effectiveOrderProductListAll.Count == 0) {
                //        return ExecuteResult<ITaskResult[]>.Cancel($"没有符合的产品可分润,退出模块.");
                //    }
                //    var effectiveOrderProductList = new List<OrderProduct>();

                //    // 商品单价限制

                //    if (Configuration.PriceLimitType == PriceLimitType.ProductPrice) {
                //        foreach (var productItem in effectiveOrderProductListAll) {
                //            if (productItem.Amount > Configuration.BaseRule.MaxAmount && Configuration.BaseRule.MaxAmount > 0) {
                //                continue;
                //            }
                //            if (productItem.Amount < Configuration.BaseRule.MinimumAmount && Configuration.BaseRule.MinimumAmount > 0) {
                //                continue;
                //            }
                //            effectiveOrderProductList.Add(productItem);
                //        }
                //    } else {
                //        effectiveOrderProductList = effectiveOrderProductListAll;
                //    }

                //    //如果价格模式为销售价，则触发金额为有效的商品实际售价之和
                //    if (Configuration.ProductRule.AmountType == PriceType.Price) {
                //        // 根据实际支付方式获取价格 （PaymentAmount为人民币支付的价格）
                //        BaseFenRunAmount = effectiveOrderProductList.Sum(e => e.PaymentAmount);
                //    }
                //    //如果价格模式为分润价，则触发金额为有效的商品分润价之和
                //    if (Configuration.ProductRule.AmountType == PriceType.FenRun) {
                //        BaseFenRunAmount = effectiveOrderProductList.Sum(e => e.FenRunAmount);
                //    }
                //    //如果价格模式为商品数 ：有效商品数量之和
                //    if (Configuration.ProductRule.AmountType == PriceType.ProductNum) {
                //        BaseFenRunAmount = effectiveOrderProductList.Sum(e => e.Count);
                //    }
                //    //如果价格模式为商品数 ：有效商品数量之和
                //    if (Configuration.ProductRule.AmountType == PriceType.OrderNum) {
                //        BaseFenRunAmount = 1;
                //    }
                //    //如果价格模式为服务费
                //    if (Configuration.ProductRule.AmountType == PriceType.OrderFeeAmount) {
                //        BaseFenRunAmount = effectiveOrderProductList.Sum(e => e.OrderProductExtension?.OrderAmount?.FeeAmount).ConvertToDecimal();
                //    }
                //}

                //OrderId = orderId;
                //if (BaseFenRunAmount <= 0) {
                //    return ExecuteResult<ITaskResult[]>.Cancel($"分润基数价格获取为0. BaseFenRunAmount={BaseFenRunAmount},订单id为{OrderId}");
                //}
            }

            // 触发类型为内部连锁
            if (TriggerType == TriggerType.Chain) {
                if (!parameter.TryGetValue(DefaultAmountKey, out decimal amount)) {
                    amount = 0;
                }

                BaseFenRunAmount = amount;
            }

            // 触发类型为其他、提现、充值,订单金额为ShareOrder的Amount
            if (TriggerType == TriggerType.Other || TriggerType == TriggerType.WithDraw || TriggerType == TriggerType.Recharge) {
                parameter.TryGetValue(FenRunAmountKey, out decimal amount);
                BaseFenRunAmount = amount;
                if (BaseFenRunAmount <= 0) {
                    return ExecuteResult<ITaskResult[]>.Cancel($"分润基数价格获取为0. BaseFenRunAmount={BaseFenRunAmount}");
                }
            }

            if (!parameter.TryGetValue("ShareOrderId", out long shareOrderId)) {
                return ExecuteResult<ITaskResult[]>.Fail("分润订单ShareOrderId未找到.");
            }
            // 线程停止3s中，重新读取数据库，保证ShardeOdeer状态正确，不重复触发
            //Thread.Sleep(TimeSpan.FromSeconds(3));
            var shareOrder = Resolve<IShareOrderService>().GetSingleNative(shareOrderId);
            if (shareOrder == null) {
                return ExecuteResult<ITaskResult[]>.Fail("分润订单未找到.");
            }
            // TODO 2019年9月24日 重构 商城模式
            //if (shareOrder.Status != ShareOrderStatus.Pending) {
            //    return ExecuteResult<ITaskResult[]>.Fail("分润订单原生查询，状态不正常.");
            //}

            var teamConfig = Resolve<IAutoConfigService>().GetValue<TeamConfig>();
            this.TeamLevel = teamConfig.TeamLevel;
            return ExecuteResult<ITaskResult[]>.Success();
        }

        //将分期添加到task队列中
        /// <summary>
        /// 添加s the stages list to queue.
        /// </summary>
        /// <param name="isAddContributionChangeToQueue">if set to <c>true</c> [is 添加 contribution change to queue].</param>
        /// <param name="parameter">参数</param>
        /// <param name="resultList">The result list.</param>
        protected void AddStagesListToQueue(bool isAddContributionChangeToQueue, IList<FenRunResultParameter> parameter, IList<ITaskResult> resultList) {
            if (Configuration.StageRule.StagePeriods > 1) {
                var timeList = GetTimeList();
                for (var i = 0; i < Configuration.StageRule.StagePeriods; i++) {
                    IList<FenRunResultParameter> parametertmpList = new List<FenRunResultParameter>();
                    foreach (var item in parameter) {
                        var para = new FenRunResultParameter(item) {
                            Amount = item.Amount / Configuration.StageRule.StagePeriods,
                            UserRemark = $"第{i + 1}期"
                        };
                        parametertmpList.Add(para);
                    }
                    var ResultParameter = new FenRunStagesResultParameter {
                        // IsAddContributionChangeToQueue = isAddContributionChangeToQueue,
                        // IsAddIncomeChangeToQueue = Configuration.IsAddIncomeChangeToQueue,
                        FenRunResultParameterList = parametertmpList
                    };
                    var queueResult = new TaskQueueResult<FenRunStagesResultParameter>(Context) {
                        //  ModuleId = new Guid(FenRunStagesResultDealwithModule.Id),
                        UserId = 0,
                        Parameter = ResultParameter,
                        ExecutionTime = timeList[i]
                    };
                    resultList.Add(queueResult);
                }
            } else {
                foreach (var item in parameter) {
                    var account = Alabo.Helpers.Ioc.Resolve<IAccountService>().GetAccount(item.ReceiveUserId, item.MoneyTypeId);
                    if (account == null) {
                        continue;
                    }

                    var moneyType = Alabo.Helpers.Ioc.Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>().FirstOrDefault(r => r.Id == item.MoneyTypeId);
                    //AddContributionChangeToQueue(item.ReceiveUserId, moneyType, item.Amount, resultList);
                    resultList.Add(new AssetTaskResult(Context) { UserId = item.ReceiveUserId, AccountId = account.Id, ChangeAmount = item.Amount });
                    var invoiceResult = new InvoiceResult(Context) {
                        Parameter = item
                    };
                    resultList.Add(invoiceResult);
                }
            }
        }

        /// <summary>
        /// Bills the logger.
        /// </summary>
        /// <param name="TriggerUser">The trigger 会员.</param>
        /// <param name="GainerUser">The gainer 会员.</param>
        /// <param name="accountName">Name of the account.</param>
        /// <param name="TriggerAmount">The trigger amount.</param>
        /// <param name="DividendAmount">The dividend amount.</param>
        /// <param name="orderSerial">The order serial.</param>
        protected string BillLogger(User TriggerUser, User GainerUser, string accountName, decimal TriggerAmount, decimal DividendAmount, string orderSerial = "") {
            //日志模板对应字段替换
            if (string.IsNullOrWhiteSpace(Configuration.TemplateRule.LoggerTemplate)) {
                return string.Empty;
            }

            return Configuration.TemplateRule.LoggerTemplate.Replace("{OrderUserName}", TriggerUser.UserName).Replace("{ShareUserNickName}", TriggerUser.Name).Replace("{ShareUserRealName}", TriggerUser.Name)
                                 .Replace("{GainerUserName}", GainerUser.UserName).Replace("{GainerNickName}", GainerUser.Name).Replace("{GainerRealName}", GainerUser.Name)
                                 .Replace("{OrderSerial}", orderSerial).Replace("{AccountName}", accountName).Replace("{ShareUserAmount}", TriggerAmount.ToString()).Replace("{DividendAmount}", DividendAmount.ToString());
        }

        /// <summary>
        /// 获取s the time list.
        /// </summary>
        public List<DateTime> GetTimeList() {
            var resultList = new List<DateTime>();
            //当天晚上1点
            var StartTime = DateTime.Today.AddHours(1);
            var TimeNow = DateTime.Now;

            if (Configuration.StageRule.StagePeriods > 0) {
                //间隔多少天
                if (Configuration.StageRule.timeType == TimeType.Day) {
                    //如果已经过1点则第二天执行
                    if (TimeNow > StartTime) {
                        StartTime = StartTime.AddDays(1);
                    }
                    for (var i = 0; i < Configuration.StageRule.StagePeriods; i++) {
                        resultList.Add(StartTime.AddDays(i * Configuration.StageRule.StageInterval));
                    }
                }
                //每个星期几
                else if (Configuration.StageRule.timeType == TimeType.Week) {
                    var week = Convert.ToInt32(TimeNow.DayOfWeek);
                    //获取第一期分润开始时间
                    StartTime = StartTime.AddDays(Configuration.StageRule.StageInterval - week);
                    if (TimeNow > StartTime) {
                        StartTime.AddDays(7);
                    }
                    for (var i = 0; i < Configuration.StageRule.StagePeriods; i++) {
                        resultList.Add(StartTime.AddDays(i * 7));
                    }
                }
                //每月几号
                else if (Configuration.StageRule.timeType == TimeType.Month) {
                    //获取第一期分润开始时间
                    StartTime = StartTime.AddDays(Configuration.StageRule.StageInterval - StartTime.Day);
                    if (TimeNow > StartTime) {
                        StartTime = StartTime.AddMonths(1);
                    }
                    for (var i = 0; i < Configuration.StageRule.StagePeriods; i++) {
                        resultList.Add(StartTime.AddMonths(i));
                    }
                }
            }
            return resultList;
        }

        ///// <summary>
        ///// TODO 2019年9月24日 重构 获取商品有效范围
        ///// </summary>
        ///// <param name="OrderId">The order identifier.</param>

        //protected List<OrderProduct> GetEffectiveOrderProduct(long OrderId) {
        //    // 所有符合条件的订单商品
        //    var orderProductList = Alabo.Helpers.Ioc.Resolve<IOrderProductService>().GetList(e => e.OrderId == OrderId).ToList();
        //    orderProductList.ForEach(r => {
        //        r.OrderProductExtension = r.Extension.DeserializeJson<OrderProductExtension>();
        //    });
        //    // 所有服务条件的商品Id
        //    var productIds = orderProductList.Select(r => r.ProductId).Distinct().ToList();
        //    //如果商品范围为:所有商品，则直接返回所有商品
        //    if (Configuration.ProductRule.ProductModel == ProductModelType.All) {
        //        return orderProductList;
        //    }
        //    // 如果商品范围为：产品线，则返回产品线商品
        //    if (Configuration.ProductRule.ProductModel == ProductModelType.ProductLine) {
        //        //检查是否符合商品限制要求
        //        if (!string.IsNullOrWhiteSpace(Configuration.ProductRule.ProductLines)) {
        //            // 获取符合条件的产品线
        //            var limitProductIdArray = Configuration.ProductRule.ProductLines.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Where(e => e.IsNumber()).Select(e => Convert.ToInt64(e)).ToArray();
        //            var productIdlist = Alabo.Helpers.Ioc.Resolve<IGoodsLineService>().GetProductIds(limitProductIdArray.ToList());
        //            orderProductList = orderProductList.Where(e => productIdlist.Contains(e.ProductId)).ToList();
        //            return orderProductList;
        //        } else {
        //            return null;
        //        }
        //    }
        //    //如果商品范围为:按所属商城选择 ，则直接返回所有商品
        //    if (Configuration.ProductRule.ProductModel == ProductModelType.ShoppingMall) {
        //        if (!Configuration.ProductRule.PriceStyleId.IsGuidNullOrEmpty()) {
        //            var productMallIds = Alabo.Helpers.Ioc.Resolve<IProductService>().GetList(r => (r.PriceStyleId == Configuration.ProductRule.PriceStyleId) && productIds.Contains(r.Id)).Select(e => e.Id).ToList();
        //            orderProductList = orderProductList.Where(e => productMallIds.Contains(e.ProductId)).ToList();
        //            return orderProductList;
        //        }
        //        return null;
        //    }

        //    return orderProductList;
        //}

        /// <summary>
        /// 校验会员某奖金最低剩余额度
        /// </summary>
        /// <param name="UserId">会员Id</param>
        protected decimal CheckTopAmount(long UserId) {
            //decimal result = -1;
            //var BonusLimitList = Alabo.Helpers.Ioc.Resolve<IStatisticsGroupService>().GetTopStatisticsGroupByConfigId(Configuration.Id);
            //if (BonusLimitList.Count == 0)
            //    return result;
            //foreach (var item in BonusLimitList) {
            //    if (item.UserTypeLimits.Count == 0)
            //        throw new ArgumentException($"奖金配置{item.Id}开启了限制但没有设置限制数据.");
            //    var userType = Alabo.Helpers.Ioc.Resolve<IUserTypeService>().GetSingle(UserId, item.UserTypeLimits.FirstOrDefault().UserTypeId);
            //    var limitType = item.UserTypeLimits.FirstOrDefault(e => e.GradeId == userType.GradeId);
            //    if (limitType == null)
            //        continue;
            //    var TopAmount = limitType.TopAmount;
            //    var userBonusStatistics = Alabo.Helpers.Ioc.Resolve<IUserBonusStatisticsService>().GetSingle(e => e.UserId == UserId && e.BonusId == item.Id && e.Status == Status.Normal);
            //    if (userBonusStatistics != null) {
            //        if (item.UpperLimitType == UpperLimitType.Undefined) {
            //            var balanceAmount = TopAmount - userBonusStatistics.PeriodsBonus;
            //            if (result == -1 || (balanceAmount >= 0 && balanceAmount < result))
            //                result = balanceAmount;
            //        } else if (item.UpperLimitType == UpperLimitType.Days) {
            //            if (userBonusStatistics.PeriodsTime.AddDays(1) > DateTime.Now) {
            //                var balanceAmount = TopAmount - userBonusStatistics.PeriodsBonus;
            //                if (result == -1 || (balanceAmount >= 0 && balanceAmount < result))
            //                    result = balanceAmount;
            //            } else
            //                result = TopAmount;
            //        } else if (item.UpperLimitType == UpperLimitType.Weeks) {
            //            if (userBonusStatistics.PeriodsTime.AddDays(7) > DateTime.Now) {
            //                var balanceAmount = TopAmount - userBonusStatistics.PeriodsBonus;
            //                if (result == -1 || (balanceAmount >= 0 && balanceAmount < result))
            //                    result = balanceAmount;
            //            } else
            //                result = TopAmount;
            //        } else if (item.UpperLimitType == UpperLimitType.Months) {
            //            if (userBonusStatistics.PeriodsTime.AddMonths(1) > DateTime.Now) {
            //                var balanceAmount = TopAmount - userBonusStatistics.PeriodsBonus;
            //                if (result == -1 || (balanceAmount >= 0 && balanceAmount < result))
            //                    result = balanceAmount;
            //            } else
            //                result = TopAmount;
            //        } else if (item.UpperLimitType == UpperLimitType.Years) {
            //            if (userBonusStatistics.PeriodsTime.AddYears(1) > DateTime.Now) {
            //                var balanceAmount = TopAmount - userBonusStatistics.PeriodsBonus;
            //                if (result == -1 || (balanceAmount >= 0 && balanceAmount < result))
            //                    result = balanceAmount;
            //            } else
            //                result = TopAmount;
            //        }
            //    } else if (result == -1 || (TopAmount >= 0 && TopAmount < result))
            //        result = TopAmount;
            //}
            return 0;
        }

        /// <summary>
        /// Splits the ratio string.
        /// </summary>
        /// <param name="Ratio">The ratio.</param>
        protected List<decimal[]> SplitRatioString(string Ratio) {
            var DistriRatio = Ratio.Split('|');
            var result = new List<decimal[]>();
            for (var i = 0; i < DistriRatio.Length; i++) {
                var tmp = DistriRatio[i].ToSplitList().Select(e => e.ToDecimal()).ToArray();
                result.Add(tmp);
            }
            return result;
        }

        /// <summary>
        /// Tries the 添加 to queue.
        /// </summary>
        /// <param name="resultList">The result list.</param>
        /// <param name="moduleId">The 模块 identifier.</param>
        protected void TryAddToQueue(IList<ITaskResult> resultList, Guid moduleId) {
            var queue = Alabo.Helpers.Ioc.Resolve<ITaskQueueService>().GetSingle(moduleId);
            if (queue == null) {
                return;
            }
            var result = new TaskQueueResult<TaskQueueParameter>(Context, queue.Id);
            resultList.Add(result);
        }
    }
}