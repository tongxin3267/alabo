using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.AfterSales.Domain.Enums
{

    /// <summary>
    ///     退货退款状态
    /// </summary>
    [ClassProperty(Name = "退货退款状态")]
    public enum RefundStatus
    {

        /// <summary>
        ///     买家已经申请退款，等待卖家同意
        /// </summary>

        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "买家申请退货退款")]
        BuyerApplyRefund = 0,

        /// <summary>
        ///     卖家已经同意退款，等待买家退货
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "卖家同意退货退款")]
        WaitSaleAllow = 1,
        /// <summary>
        ///     卖家拒绝退款
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "卖家拒绝退货退款")]
        WaitSaleRefuse = 2,

        /// <summary>
        ///     退款关闭
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "退款关闭")]
        Closed = 3,

        /// <summary>
        ///     退款成功
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "退款成功")]
        Sucess = 4,

    }
}