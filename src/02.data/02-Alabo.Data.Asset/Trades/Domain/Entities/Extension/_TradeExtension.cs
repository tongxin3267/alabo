using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities.Extensions;

namespace Alabo.App.Core.Finance.Domain.Entities.Extension {

    /// <summary>
    ///     Class WithDrawExtension.
    /// </summary>
    public class TradeExtension : EntityExtension {

        /// <summary>
        /// </summary>
        public TradeWithDraw WithDraw { get; set; } = new TradeWithDraw();

        /// <summary>
        /// </summary>
        public RechargeExtension Recharge { get; set; } = new RechargeExtension();

        /// <summary>
        /// </summary>
        public TradeRemark TradeRemark { get; set; } = new TradeRemark();
    }

    public class TradeRemark {

        /// <summary>
        ///     会员备注
        /// </summary>
        [Display(Name = "会员备注")]
        public string UserRemark { get; set; }

        /// <summary>
        ///     ss
        ///     备注，此备注表示管理员备注，前台会员不可以修改
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        ///     失败原因
        /// </summary>
        public string FailuredReason { get; set; }
    }
}