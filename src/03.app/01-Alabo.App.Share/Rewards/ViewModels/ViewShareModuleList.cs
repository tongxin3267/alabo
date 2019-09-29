using System;
using Alabo.App.Share.OpenTasks.Base;
using Alabo.Framework.Core.Enums.Enum;

namespace Alabo.App.Share.Rewards.ViewModels
{
    public class ViewShareModuleList
    {
        public long Id { get; set; }

        public Guid ModuleId { get; set; }

        /// <summary>
        ///     配置名称
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        ///     维度名称
        /// </summary>
        public string ConfigName { get; set; }

        public string DistriRatio { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public bool IsEnable { get; set; }

        public TriggerType TriggerType { get; set; }

        /// <summary>
        ///     商品范围说明
        /// </summary>
        public string ProductRangIntro { get; set; }

        /// <summary>
        ///     用户类型说明
        /// </summary>
        public string ShareUserIntro { get; set; }

        /// <summary>
        ///     用户类型说明
        /// </summary>
        public string OrderUserIntro { get; set; }

        /// <summary>
        ///     资产分配说明
        /// </summary>
        public string RuleItemsIntro { get; set; }

        /// <summary>
        ///     最小触发金额
        /// </summary>
        public decimal MinimumAmount { get; set; }

        /// <summary>
        ///     短信通知
        /// </summary>
        public bool SmsNotification { get; set; }

        public bool IsLock { get; set; } = false;
    }

    public class ShareBaseConfigList : ShareBaseConfig
    {
    }
}