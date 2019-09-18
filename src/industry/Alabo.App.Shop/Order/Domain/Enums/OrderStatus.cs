using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Order.Domain.Enums {

    /// <summary>
    ///     订单状态,订单状态足够，不需要再加额外业务处理
    ///     状态能够处理各种复杂的订单业务逻辑
    /// </summary>
    [ClassProperty(Name = "订单状态")]
    public enum OrderStatus {

        /// <summary>
        ///     等待付款,可取消
        /// </summary>
        [Display(Name = "待付款")]
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Field(Icon = "flaticon-apps")]
        WaitingBuyerPay = 1,

        /// <summary>
        ///     等待审核，可退款
        ///     财务审核订单,支付给商户后订单状态改为待发货
        /// </summary>
        [Display(Name = "审核中")]
        [LabelCssClass("m-badge--brand")]
        [Field(Icon = "la la-check-circle-o")]
        WaitingSellerReview = 0,

        /// <summary>
        ///     等待发货，可退款
        ///     如果线下商品，改状态表示未消费
        ///     部分发货,为等待发货
        /// </summary>
        [Display(Name = "待发货")]
        [LabelCssClass("m-badge--brand")]
        [Field(Icon = "la la-check-circle-o")]
        WaitingSellerSendGoods = 2,

        /// <summary>
        ///     待收货，等待确认
        /// </summary>
        [Display(Name = "待收货")]
        [LabelCssClass(BadgeColorCalss.Primary)]
        [Field(Icon = "la la-check-circle-o")]
        WaitingReceiptProduct = 3,

        /// <summary>
        ///     待评价
        /// </summary>
        [Display(Name = "待评价")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(Icon = "la la-check-circle-o")]
        WaitingEvaluated = 4,

        /// <summary>
        ///     待分享
        ///     拼团商品时候，需要使用拼团流程:待付款->待分享->代发货
        ///     在代发货之前，有个待分享流程
        /// </summary>
        [Display(Name = "待分享")]
        [LabelCssClass(BadgeColorCalss.Primary)]
        [Field(Icon = "la la-check-circle-o")]
        WaitingShared = 10,

        /// <summary>
        /// 已发货
        ///     申请退货中，退款/售后
        /// </summary>
        [Display(Name = "退款/退货")]
        [LabelCssClass(BadgeColorCalss.Info)]
        [Field(Icon = "la la-check-circle-o")]
        AfterSale = 50,

        /// <summary>
        /// 未发货
        ///     申请退货中，退款/售后
        /// </summary>
        [Display(Name = "待退款")]
        [LabelCssClass(BadgeColorCalss.Info)]
        [Field(Icon = "la la-check-circle-o")]
        Refund = 51,

        /// <summary>
        ///     已完成
        /// </summary>
        [Display(Name = "订单完成")]
        [Field(Icon = "la la-check-circle-o")]
        [LabelCssClass(BadgeColorCalss.Success)]
        Success = 100,

        /// <summary>
        ///     已关闭
        ///     指买家操作取消，或管理员关闭，或计划任务自动关闭
        ///     订单关闭后，库存恢复
        /// </summary>
        [Display(Name = "订单关闭")]
        [LabelCssClass(BadgeColorCalss.Default)]
        [Field(Icon = "la la-check-circle-o")]
        Closed = 200,

        /// <summary>
        ///     已关闭
        ///     退款后的状态
        /// </summary>
        [Display(Name = "订单关闭,已退款")]
        [LabelCssClass(BadgeColorCalss.Default)]
        [Field(Icon = "la la-check-circle-o")]
        Refunded = 201,

        /// <summary>
        ///     已关闭
        ///     退款后的状态
        /// </summary>
        [Display(Name = "下架")]
        [LabelCssClass(BadgeColorCalss.Default)]
        [Field(Icon = "la la-check-circle-o")]
        UnderShelf = 300,

        /// <summary>
        ///     已打款
        ///     财务已打款 待供应商发货的状态
        /// </summary>
        [Display(Name = "待出库")]
        [LabelCssClass(BadgeColorCalss.Default)]
        [Field(Icon = "la la-check-circle-o")]
        Remited = 400,
    }
}