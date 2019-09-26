using System;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Tool.Payment;

namespace Alabo.App.Asset.Accounts.Dtos {

    public class RechargeAccountInput {

        /// <summary>
        /// 用户id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 支付方式 默认支付宝网页支付
        /// </summary>
        public PayType Type { get; set; } = PayType.AlipayWeb;

        /// <summary>
        /// 货币类型 默认 人民币 储值
        /// </summary>
        public Guid MoneyTypeId { get; set; } = MoneyTypeConfig.CNY;

        /// <summary>
        /// 是否管理员充值
        /// </summary>
        public bool IsAdmin { get; set; } = false;
    }
}