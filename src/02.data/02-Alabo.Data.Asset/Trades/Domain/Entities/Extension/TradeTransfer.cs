using System;
using Alabo.Domains.Entities.Extensions;

namespace Alabo.App.Core.Finance.Domain.Entities.Extension {

    /// <summary>
    ///     转账扩展
    /// </summary>
    public class TradeTransfer : EntityExtension {

        /// <summary>
        ///     对方用户
        /// </summary>
        public long OtherUserId { get; set; }

        public Guid InMoneyTypeId { get; set; }

        // 服务费
        public decimal ServiceAmount { get; set; }

        /// <summary>
        ///     转账Id
        /// </summary>
        public Guid TransConfigId { get; set; }

        // 交易金额 实际获得金额
        public decimal Amount { get; set; }
    }
}