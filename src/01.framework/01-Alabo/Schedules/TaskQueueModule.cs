using Alabo.Exceptions;
using Alabo.Reflections;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Schedules
{
    public static class TaskQueueModuleId
    {
        /// <summary>
        ///     按照升级点升级
        /// </summary>
        [Display(Name = "按升级点升级")]
        public static Guid UserUpgradeByUpgradePoints = Guid.Parse("FFC4E2F5-976A-4A02-B1A9-6A78EB5C5039");

        /// <summary>
        ///     团队等级自动更新
        ///     团队等级自动更新，包括直推会员数量会员等级数，团队会员数量，会员等级数，比如包含VIP等级10个，团队中有Vip会员10个等
        /// </summary>
        [Display(Name = "团队等级更新")]
        public static Guid TeamUserGradeAutoUpdate = Guid.Parse("FFC4E2F5-976A-4A02-B1A9-6A08EB5C5000");

        /// <summary>
        ///     用户注册后事件
        /// </summary>
        [Display(Name = "用户注册后事件")]
        public static Guid AfterUserReg = Guid.Parse("FF000999-1111-1111-2222-600000000001");

        /// <summary>
        ///     二维码生成
        /// </summary>
        [Display(Name = "二维码生成")]
        public static Guid UserQrcodeCreate = Guid.Parse("FF000999-1111-1111-2222-600000000002");

        /// <summary>
        ///     组织架构图更新
        /// </summary>
        [Display(Name = "组织架构图更新")]
        public static Guid UserParentUpdate = Guid.Parse("FF000999-1111-1111-2222-600000000003");

        /// <summary>
        ///     奖金池统计
        /// </summary>
        [Display(Name = "奖金池统计")]
        public static Guid BonusPoolReport = Guid.Parse("FF000999-1111-1111-2222-600000000004");

        /// <summary>
        ///     订单支付成功
        /// </summary>
        [Display(Name = "订单支付成功")]
        public static Guid AfterOrderPay = Guid.Parse("FF000999-1111-1111-2222-600000000005");

        /// <summary>
        ///     用户等级Kpi绩效
        /// </summary>
        [Display(Name = "用户等级Kpi绩效")]
        public static Guid UserGradeKpiReport = Guid.Parse("FF000999-1111-1111-2222-600000000006");

        /// <summary>
        ///     共享分润任务
        /// </summary>
        [Display(Name = "共享分润任务")]
        public static Guid SharedAccountModuleGuid = Guid.Parse("FF000999-1111-1111-2222-600000000007");

        /// <summary>
        ///     内部合伙人关系梳理
        /// </summary>
        [Display(Name = "内部合伙人关系梳理")]
        public static Guid ParnterModuleGuid = Guid.Parse("FF000999-1111-1111-2222-600000000008");
    }

    public static class TaskQueueModule
    {
        /// <summary>
        ///     升级任务等级Id
        /// </summary>
        /// <returns></returns>
        public static Dictionary<Guid, string> GetTaskQueueModuleIds()
        {
            var result = new Dictionary<Guid, string>();
            var fields = typeof(TaskQueueModuleId).GetFields();
            foreach (var item in fields)
            {
                var displayAttribute = item.GetAttribute<DisplayAttribute>();
                if (displayAttribute == null) {
                    throw new ValidException("请设置DisplayAttribute特性");
                }

                var value = item.GetValue(null);
                result.Add(Guid.Parse(value.ToString()), displayAttribute.Name);
            }

            return result;
        }
    }
}