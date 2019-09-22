using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Order.Domain.Enums {

    /// <summary>
    ///     换货状态
    /// </summary>
    [ClassProperty(Name = "换货状态")]
    public enum ChangeStatus {

        /// <summary>
        ///     买家已经申请退款，等待卖家同意
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "已申请换货")]
        BuyerApplyChange = 0,

        /// <summary>
        ///     卖家已经同意退款，等待买家退货
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "等待买家退货")]
        WaitBuyerToSendProduct = 1,

        /// <summary>
        ///     买家已经退货，等待卖家确认收货)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "等待卖家确认收货")]
        WaitSellerToConfrimProduct = 2,

        /// <summary>
        ///     卖家换货，等待买家收货确认
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "卖家发货")]
        SellerReject = 3,

        /// <summary>
        ///     退款关闭
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "换货关闭")]
        Closed = 4,

        /// <summary>
        ///     退款成功
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "换货成功")]
        Sucess = 5
    }
}