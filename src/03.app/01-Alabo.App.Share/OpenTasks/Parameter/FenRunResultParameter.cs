using Alabo.App.Share.OpenTasks.Base;
using Alabo.App.Share.Rewards.Domain.Enums;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Tasks.Queues.Models;
using Alabo.Helpers;
using Alabo.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.App.Share.OpenTasks.Parameter
{
    public class FenRunResultParameter
    {
        public FenRunResultParameter()
        {
        }

        public FenRunResultParameter(FenRunResultParameter para)
        {
            ModuleId = para.ModuleId;
            ModuleName = para.ModuleName;
            MoneyTypeId = para.MoneyTypeId;
            TriggerUserId = para.TriggerUserId;
            OrderUserName = para.OrderUserName;
            TriggerUserTypeId = para.TriggerUserTypeId;
            TriggerGradeId = para.TriggerGradeId;
            ReceiveUserId = para.ReceiveUserId;
            ReceiveUserName = para.ReceiveUserName;
            UserRemark = para.UserRemark;
            ShareLevel = para.ShareLevel;
            Amount = para.Amount;
            BillStatus = para.BillStatus;
            ShareStatus = para.ShareStatus;
            Summary = para.Summary;
            TriggerType = para.TriggerType;
            Order = para.Order;
            ExtraDate = para.ExtraDate;
            ModuleConfigId = para.ModuleConfigId;
            Fee = para.Fee;
            BonusId = para.BonusId;
            ModuleTypeName = para.ModuleTypeName;
        }

        /// <summary>
        ///     模块Guid
        /// </summary>
        public Guid ModuleId { get; set; }

        /// <summary>
        ///     模块名
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        ///     moneytype对应id
        /// </summary>
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     触发用户id
        /// </summary>
        public long TriggerUserId { get; set; }

        /// <summary>
        ///     触发用户名
        /// </summary>
        public string OrderUserName { get; set; }

        /// <summary>
        ///     触发用户类型id
        /// </summary>
        public Guid TriggerUserTypeId { get; set; }

        /// <summary>
        ///     触发用户gradeid
        /// </summary>
        public Guid TriggerGradeId { get; set; }

        /// <summary>
        ///     接收用户id
        /// </summary>
        public long ReceiveUserId { get; set; }

        /// <summary>
        ///     接收用户name
        /// </summary>
        public string ReceiveUserName { get; set; }

        /// <summary>
        ///     管理员备注信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        ///     用户remark信息
        /// </summary>
        public string UserRemark { get; set; }

        /// <summary>
        ///     等级
        /// </summary>
        public long ShareLevel { get; set; }

        /// <summary>
        ///     金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///     支付状态
        /// </summary>
        public BillStatus BillStatus { get; set; }

        /// <summary>
        ///     执行状态
        /// </summary>
        public FenRunStatus ShareStatus { get; set; }

        /// <summary>
        ///     文字说明
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        ///     分润费率
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        ///     对应订单信息
        /// </summary>
        public InvoiceOrder Order { get; set; }

        /// <summary>
        ///     分润触发类型 （1：报单 2：订单）
        /// </summary>
        public TriggerType TriggerType { get; set; }

        public long ModuleConfigId { get; set; }
        public long BonusId { get; set; }
        public string ExtraDate { get; set; }

        /// <summary>
        ///     分润类型名称
        ///     比如说裂变分佣，省代理分润等
        /// </summary>
        public string ModuleTypeName { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="shareAamount"></param>
        /// <param name="triggerUser"></param>
        /// <param name="shareUser"></param>
        /// <param name="config"></param>
        /// <param name="parameter"></param>
        /// <param name="shareLevel"></param>
        public static List<FenRunResultParameter> ResultParameters(decimal shareAamount, User triggerUser,
            User shareUser, ShareBaseConfig config, TaskParameter parameter, long shareLevel = 0)
        {
            //如果分润金额小于等于0,则退出
            if (shareAamount <= 0) {
                return null;
            }

            //判断用户是否满足type与grade要求，不满足则跳过当前用户

            var moneyTypes = Ioc.Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>(r => r.Status == Status.Normal);
            var ParameterList = new List<FenRunResultParameter>();
            foreach (var rule in config.RuleItems)
            {
                var shareAmount = shareAamount * rule.Ratio;
                var ruleMoneyType = moneyTypes.FirstOrDefault(r => r.Id == rule.MoneyTypeId);
                var Parameter = Create(shareAmount, triggerUser, shareUser, ruleMoneyType, config, parameter,
                    shareLevel);
                ParameterList.Add(Parameter);
            }

            return ParameterList;
        }

        /// <summary>
        ///     创建分润参数
        /// </summary>
        /// <param name="shareAamount"></param>
        /// <param name="triggerUser">触发用户</param>
        /// <param name="shareUser"></param>
        /// <param name="moneyType">分润金额到账账户类型</param>
        /// <param name="config">分润维度配置数据</param>
        /// <param name="parameter">分润触发通用参数</param>
        /// <param name="shareLevel"></param>
        public static FenRunResultParameter Create(decimal shareAamount, User triggerUser, User shareUser,
            MoneyTypeConfig moneyType, ShareBaseConfig config, TaskParameter parameter, long shareLevel = 0)
        {
            parameter.TryGetValue("OrderId", out long orderId);
            parameter.TryGetValue("OrderSerial", out string orderSerial);
            var Order = new InvoiceOrder
            {
                Id = orderId,
                Serial = orderSerial,
                Amount = shareAamount
            };

            //替换分润描述
            var summary = config.TemplateRule.LoggerTemplate.Replace("{OrderUserName}", triggerUser.UserName)
                .Replace("{ShareUserNickName}", triggerUser.Name).Replace("{ShareUserRealName}", triggerUser.Name)
                .Replace("{GainerUserName}", shareUser.UserName).Replace("{GainerNickName}", shareUser.Name)
                .Replace("{GainerRealName}", shareUser.Name)
                .Replace("{OrderSerial}", orderSerial).Replace("{AccountName}", moneyType.Name)
                .Replace("{ShareUserAmount}", triggerUser.ToString())
                .Replace("{DividendAmount}", shareAamount.ToString());

            return new FenRunResultParameter
            {
                Amount = shareAamount,
                ModuleName = "招商奖", //读取配置的的名称，未实现

                //分润记录信息
                ShareStatus = FenRunStatus.Success,
                ShareLevel = shareLevel,
                UserRemark = string.Empty,
                Summary = summary,
                ExtraDate = "额外数据",

                //模块信息
                ModuleConfigId = config.Id,
                //BonusId = config.BonusId,
                ModuleTypeName = config.GetType().Name,
                TriggerType = config.TriggerType,

                //触发用户
                TriggerGradeId = config.ShareUser.ShareUserGradeId,
                TriggerUserTypeId = config.ShareUser.ShareUserTypeId,
                TriggerUserId = triggerUser.Id,
                OrderUserName = triggerUser.GetUserName(),

                //获得分润的用户
                ReceiveUserId = shareUser.Id,
                ReceiveUserName = shareUser.GetUserName(),

                //订单信息
                Order = Order,

                //财务信息
                MoneyTypeId = moneyType.Id,
                BillStatus = BillStatus.Success
            };
        }
    }

    public class InvoiceOrder
    {
        /// <summary>
        ///     dingdan
        /// </summary>
        public long Id { get; set; }

        public string Serial { get; set; }

        public decimal Amount { get; set; }
    }
}