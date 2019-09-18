using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities.Extensions;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.Domain.Entities.Extension {

    /// <summary>
    ///     Class WithDraw.
    /// </summary>
    public class TradeWithDraw : EntityExtension {

        /// <summary>
        ///     银行卡信息
        /// </summary>
        public BankCard BankCard { get; set; }

        /// <summary>
        ///     应付人民币
        /// </summary>
        [Display(Name = "应付人民币")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            LabelColor = LabelColor.Info, ListShow = true, GroupTabId = 1, Width = "80", SortOrder = 8)]
        public decimal CheckAmount { get; set; }

        /// <summary>
        ///     手续费
        /// </summary>
        [Display(Name = "手续费")]
        public decimal Fee { get; set; }

        /// <summary>
        ///     账后余额
        /// </summary>
        [Display(Name = "账后余额")]
        public decimal AfterAmount { get; set; }
    }
}