using System.ComponentModel.DataAnnotations;
using Alabo.Industry.Shop.Orders.Domain.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.AfterSales.Domain.Enums
{
    /// <summary>
    /// 退货原因
    /// </summary>
    [ClassProperty(Name = "退货原因")]
    public enum RefundReason
    {

        /// <summary>
        /// 不想要了
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "不想要了")]
        [RefundType(Type = OrderStatus.WaitingSellerSendGoods)]
        DontWant = 0,

        /// <summary>
        /// 选错产品
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "选错产品")]
        [RefundType(Type = OrderStatus.WaitingSellerSendGoods)]
        Wrong = 1,
        /// <summary>
        /// 商家没货
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "商家没货")]
        [RefundType(Type = OrderStatus.WaitingSellerSendGoods)]
        Stock = 2,
        /// <summary>
        /// 其他 注明原因
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "其他(注明原因)")]
        [RefundType(Type = OrderStatus.WaitingSellerSendGoods)]
        Other = 3,

        /// <summary>
        /// 质量
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "质量问题")]
        [RefundType(Type = OrderStatus.WaitingReceiptProduct)]
        Quality = 100,

        /// <summary>
        /// 少件漏件
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "少件漏件")]
        [RefundType(Type = OrderStatus.WaitingReceiptProduct)]
        Missing = 101,
        /// <summary>
        /// 规格不符
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "规格不符")]
        [RefundType(Type = OrderStatus.WaitingReceiptProduct)]
        specification = 102

    }
}
