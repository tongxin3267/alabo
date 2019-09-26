using System;
using System.Collections.Generic;
using Alabo.Framework.Tasks.Queues.Models;

namespace Alabo.Data.Things.Orders.ResultModel {

    /// <summary>
    /// </summary>
    public class UserAssetsModuleConfigBase : IModuleConfig {
        public int Id { get; set; }

        /// <summary>
        ///     限制用户类型
        /// </summary>
        public Guid UserTypeId { get; set; }

        /// <summary>
        ///     是否开启用户类型限制
        /// </summary>
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsEnableUserTypeLimit { get; set; }

        /// <summary>
        ///     限制用户等级
        /// </summary>
        public Guid UserGradeId { get; set; }

        /// <summary>
        ///     是否开启用户等级限制
        /// </summary>
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsEnableUserGradeLimit { get; set; }

        /// <summary>
        ///     日志模板
        /// </summary>
        public string LoggerTemplate { get; set; }

        public string SmsTemplate { get; set; }

        /// <summary>
        ///     分配规则集合列表
        /// </summary>
        public IList<AssetsDistributeRuleItem> RuleItems { get; set; } = new List<AssetsDistributeRuleItem>();

        public string Name { get; set; }

        long IModuleConfig.Id {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }

    public class ShareModuleDistriRatio {
        public string DistriRatio { get; set; }
    }
}