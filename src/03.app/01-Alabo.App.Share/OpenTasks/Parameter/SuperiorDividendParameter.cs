using System.Collections.Generic;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Open.Tasks.Base;

namespace Alabo.App.Open.Tasks.Parameter {

    public class SuperiorDividendParameter {

        /// <summary>
        /// 触发类型 默认为订单触发
        /// </summary>
        public TriggerType TriggerType { get; set; } = TriggerType.Order;

        /// <summary>
        /// 金额
        /// </summary>
        public decimal PriceAmount { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// 分润记录额外信息
        /// </summary>
        public string ExtraData { get; set; }

        /// <summary>
        /// 上级资产分配规则
        /// </summary>
        public IList<AssetsRule> ParentRuleItem { get; set; } = new List<AssetsRule>();
    }
}