using System.ComponentModel.DataAnnotations;
using Alabo.Industry.Shop.Orders;
using Alabo.UI.Enum;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.OrderActions.Domain.Enums {

    /// <summary>
    ///     订单操作类型
    ///     用户操作类型：从100 -199
    ///     卖家操作类型：从200-299
    ///     管理员操作类型：从300-399
    ///     线下店铺用户操作：从400-499
    /// </summary>
    [ClassProperty(Name = "订单操作类型")]
    public enum OrderActionType {

        #region 用户操作

        /// <summary>
        ///     会员创建订单
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "会员创建订单")]
        [OrderActionType(Intro = "会员{OrderUserName}创建订单，订单金额为{OrderAmount}")]
        UserCreateOrder = 101,

        /// <summary>
        ///     会员支付订单
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "会员支付订单")]
        [OrderActionType(Intro = "会员{OrderUserName}支付订单，支付方式为{PayMenent}", AllowStatus = "WaitingBuyerPay", Name = "支付",
            Method = "Api/Order/Pay", AllowUser = 0, Type = "Pay")]
        UserPayOrder = 102,

        /// <summary>
        ///     会员取消订单
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "会员取消订单")]
        [OrderActionType(Intro = "会员{OrderUserName}取消订单", AllowStatus = "WaitingBuyerPay", Name = "取消",
            Method = "Api/Order/Cancel", AllowUser = 0, Type = "Cancel")]
        UserCancleOrder = 103,

        /// <summary>
        /// 已发货
        ///     会员退换货
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "会员退货退款")]
        [OrderActionType(Intro = "会员{OrderUserName}申请退货退款", AllowStatus = "WaitingReceiptProduct,WaitingBuyerConfirm",
            Name = "退货退款", Method = "Api/Refund/Apply", AllowUser = 0, Type = "AfterSale")]
        UserReturnProduct = 104,

        /// <summary>
        /// 未发货
        ///     会员退款
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "会员退款")]
        [OrderActionType(Intro = "会员{OrderUserName}申请退退款", AllowStatus = "WaitingSellerSendGoods",
            Name = "申请退款", Method = "Api/Refund/Apply", AllowUser = 0, Type = "Refund")]
        UserRefund = 105,

        /// <summary>
        ///     订单评价
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "订单评价")]
        [OrderActionType(Intro = "会员{OrderUserName}对订单进行评价", AllowStatus = "WaitingBuyerConfirm", Name = "评价",
            Method = "UserOrderEvaluate", AllowUser = -1)]
        UserEvaluate = 106,

        /// <summary>
        ///     查看物流
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "查看物流")]
        [OrderActionType(Intro = "会员{OrderUserName}查看订单的物流", AllowStatus = "WaitingBuyerConfirm,WaitingReceiptProduct",
            Name = "查看物流", Method = "Api/Order/GetExpressInfo", AllowUser = 0, Type = "ExpressInfo")]
        UserCheckLogistics = 107,

        /// <summary>
        ///     确认收货
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "确认收货")]
        [OrderActionType(Intro = "会员{OrderUserName}确认收货", AllowStatus = "WaitingReceiptProduct", Name = "确认收货",
            Method = "Api/Order/Confirm", AllowUser = 0, Type = "Confirm")]
        UserConfirmOrder = 108,

        /// <summary>
        ///     用户查看退款详情
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "用户查看退款详情")]
        [OrderActionType(Intro = "用户{OrderUserName}查看退款详情", AllowStatus = "Refunded,AfterSale,Refund", Name = "售后详情",
            Method = "Api/Refund/Get", Type = "UserRefundInfo", AllowUser = 0)]
        UserRefundInfo = 109,

        /// <summary>
        ///     用户查看退款详情
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "用户取消售后")]
        [OrderActionType(Intro = "用户{OrderUserName}取消售后", AllowStatus = "AfterSale,Refund", Name = "取消售后",
            Method = "Api/Refund/Get", Type = "UserRefundCencal", AllowUser = -1)]
        UserRefundCencal = 120,

        #endregion 用户操作

        #region 供应商操作

        /// <summary>
        ///     卖家代付
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "卖家代付")]
        [OrderActionType(Intro = "卖家{SellerUserName}后台手动支付订单", AllowStatus = "WaitingBuyerPay", Name = "代付",
            Method = "OrderSellerPay")]
        SellerPay = 201,

        /// <summary>
        ///     卖家发货
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "卖家发货")]
        [OrderActionType(Intro = "卖家{SellerUserName}发货", AllowStatus = "Remited,WaitingReceiptProduct,WaitingSellerSendGoods",  //WaitingBuyerConfirm
            Name = "发货", Method = "OrderAdminDelivery", AllowUser = 1)]
        SellerDelivery = 202,

        /// <summary>
        ///     卖家取消订单
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "卖家取消订单")]
        [OrderActionType(Intro = "卖家{SellerUserName}后台取消", AllowStatus = "WaitingBuyerPay", Name = "取消",
            Method = "OrderSellerCancle")]
        SellerCancelOrder = 203,

        /// <summary>
        ///     卖家删除订单
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "卖家删除订单")]
        [OrderActionType(Intro = "卖家{SellerUserName}后台取消", AllowStatus = "WaitingBuyerPay", Name = "取消",
            Method = "OrderSellerCancle")]
        SellerDeleteOrder = 204,

        /// <summary>
        ///     卖家修改订单地址
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "卖家修改订单地址")]
        [OrderActionType(Intro = "卖家{SellerUserName}后台取消", AllowStatus = "WaitingBuyerPay", Name = "取消",
            Method = "OrderSellerCancle")]
        SellerEditAddress = 205,

        /// <summary>
        ///     卖家关闭订单
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "卖家关闭订单")]
        [OrderActionType(Intro = "卖家{SellerUserName}后台取消订单", AllowStatus = "WaitingBuyerPay", Name = "取消",
            Method = "OrderSellerCancle")]
        SellerClose = 212,

        /// <summary>
        ///     卖家手动完成订单
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "卖家手动完成订单")]
        [OrderActionType(Intro = "卖家{SellerUserName}后台关闭订单", AllowStatus = "WaitingBuyerPay", Name = "关闭",
            Method = "OrderSellerCancle", Type = "SellerClose", AllowUser = 1)]
        SellerSucess = 213,

        /// <summary>
        ///     卖家删除订单
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "卖家手动删除订单")]
        [OrderActionType(Intro = "卖家{SellerUserName}后台删除订单", AllowStatus = "WaitingBuyerPay", Name = "删除",
            Method = "OrderSellerDelete")]
        SellerDelete = 214,

        /// <summary>
        ///     卖家退款
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "卖家手动退款")]
        [OrderActionType(Intro = "卖家{SellerUserName}退款", AllowStatus = "AfterSale,Refund", Name = "退款",
            Method = "Api/Refund/Process", Type = "SellerRefund", AllowUser = 1)]
        SellerRefund = 215,

        #endregion 供应商操作

        #region 管理员操作

        /// <summary>
        ///     管理员代付
        ///     对应方法：IOrderAdminService.Pay
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "管理员代付")]
        [OrderActionType(Intro = "管理员{AdminUserName}后台手动支付订单", Icon = FontAwesomeIcon.Paragraph,
            AllowStatus = "WaitingBuyerPay", Color = "danger", Name = "代付", Method = "OrderAdminPay")]
        AdminPay = 301,

        /// <summary>
        ///     管理员发货
        ///     /// 对应方法：IOrderAdminService.Delivery
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "管理员发货")]
        [OrderActionType(Intro = "管理员{AdminUserName}发货", Icon = FontAwesomeIcon.ProductHunt,
            AllowStatus = "WaitingSellerSendGoods,WaitingBuyerConfirm", Name = "发货", Color = "brand",
            Method = "OrderAdminDelivery", AllowUser = 2)]
        AdminDelivery = 302,

        /// <summary>
        ///     管理员修改邮费金额
        ///     对应方法：IOrderService.UpdateExpressAmount
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "管理员修改邮费金额")]
        [OrderActionType(Intro = "管理员{AdminUserName}后台修改邮费{ExpressAmount}", Icon = FontAwesomeIcon.ProductHunt,
            AllowStatus = "WaitingBuyerPay", Name = "修改邮费", Color = "brand",
            Method = "UpdateExpressAmount", AllowUser = 2)]
        AdminEditExpressAmount = 304,

        /// <summary>
        ///     管理员修改订单地址
        ///     对应方法：IOrderAdminService.ChangeAddress
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "管理员修改订单地址")]
        [OrderActionType(Intro = "管理员{AdminUserName}后台取消", Icon = FontAwesomeIcon.AddressCard,
            AllowStatus = "WaitingBuyerPay,WaitingSellerSendGoods", Name = "修改地址", Color = "info",
            Method = "OrderAdminChangeAddress", AllowUser = 2)]
        AdminEditAddress = 305,

        /// <summary>
        ///     管理员手动完成订单
        ///     对应方法：IOrderAdminService.Sucess
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "管理员手动完成订单")]
        [OrderActionType(Intro = "管理员{AdminUserName}后台关闭订单", Icon = FontAwesomeIcon.Superscript,
            AllowStatus = "WaitingReceiptProduct,WaitingEvaluated", Name = "完成", Color = "success",
            Method = "OrderAdminSucess")]
        AdminSucess = 313,

        /// <summary>
        ///     备忘
        ///     /// 对应方法：IOrderAdminService.Remark
        ///     数据保存：OrderExtension.OrderRemark.PlatplatformRemark 中
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "管理员备忘")]
        [OrderActionType(Intro = "管理员{AdminUserName}后台删除订单", Icon = FontAwesomeIcon.Medium,
            AllowStatus = "WaitingBuyerPay,WaitingSellerSendGoods,WaitingReceiptProduct,Success,Closed", Name = "备忘",
            Method = "OrderAdminRemark", AllowUser = 2)]
        AdminRemark = 315,

        /// <summary>
        ///     备忘
        ///     /// 对应方法：IOrderAdminService.Message
        ///     数据保存：OrderExtension.Message.PlatplatformMessage
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "管理员回复留言")]
        [OrderActionType(Intro = "管理员{AdminUserName}后台删除订单", Icon = FontAwesomeIcon.AddressBook,
            AllowStatus = "WaitingBuyerPay,WaitingSellerSendGoods", Name = "留言", Method = "OrderAdminMessage", AllowUser = 2)]
        AdminMessage = 316,

        /// <summary>
        ///     备忘
        ///     对应方法：IOrderAdminService.Rate
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "评价")]
        [OrderActionType(Intro = "管理员{AdminUserName}后台删除订单", Color = "danger", Icon = FontAwesomeIcon.Calculator,
            AllowStatus = "WaitingEvaluated,WaitingReceiptProduct,Success,Closed", Name = "评价",
            Method = "OrderAdminRate ")]
        AdminRate = 317,

        /// <summary>
        ///     管理员关闭订单
        ///     对应方法：IOrderAdminService.Cancle
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "管理员关闭订单")]
        [OrderActionType(Intro = "管理员{AdminUserName}后台取消订单", Icon = FontAwesomeIcon.WindowClose,
            AllowStatus = "WaitingBuyerPay", Name = "关闭", Type = "AdminClose", Color = "metal", Method = "Api/OrderAdmin/AdminCancel", AllowUser = 2)]
        AdminClose = 319,

        /// <summary>
        ///     管理员删除订单
        ///     /// 对应方法：IOrderAdminService.Delete
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "管理员手动删除订单")]
        [OrderActionType(Intro = "管理员{AdminUserName}后台删除订单", AllowStatus = "WaitingBuyerPay,Closed",
            Icon = FontAwesomeIcon.Recycle, Name = "删除", Method = "OrderAdminDelete")]
        AdminDelete = 320,

        /// <summary>
        ///     查看物流
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "管理员查看物流")]
        [OrderActionType(Intro = "管理员{AdminUserName}查看订单的物流", AllowStatus = "WaitingBuyerConfirm,WaitingReceiptProduct",
            Name = "查看物流", Method = "Api/Order/GetExpressInfo", AllowUser = 2)]
        AdminCheckLogistics = 321,

        /// <summary>
        ///    管理员退款
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "管理员手动退款")]
        [OrderActionType(Intro = "管理员{AdminUserName}退款", AllowStatus = "AfterSale,Refund", Name = "退款",
            Method = "Api/Refund/Process", Type = "AdminRefund", AllowUser = 2)]
        AdminRefund = 322,

        /// <summary>
        ///     管理员查看退款详情
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "管理员查看退款详情")]
        [OrderActionType(Intro = "管理员{AdminUserName}查看退款详情", AllowStatus = "Refunded,AfterSale,Refund", Name = "退款详情",
            Method = "Api/Refund/Get", Type = "AdminRefundInfo", AllowUser = 2)]
        AdminRefundInfo = 323,

        /// <summary>
        ///     管理员同意退货退款
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "管理员拒绝退货退款")]
        [OrderActionType(Intro = "管理员{AdminUserName}同意退货退款", AllowStatus = "AfterSale", Name = "同意退货退款",
            Method = "AdminAllowRefund", Type = "AdminAllowRefund", AllowUser = 2)]
        AdminAllowRefund = 324,

        /// <summary>
        ///     管理员拒绝退货退款
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "管理员拒绝退货退款")]
        [OrderActionType(Intro = "管理员{AdminUserName}拒绝退货退款", AllowStatus = "AfterSale,Refund", Name = "拒绝退货退款",
            Method = "AdminRefuseRefund", Type = "AdminRefuseRefund", AllowUser = 2)]
        AdminRefuseRefund = 325,

        /// <summary>
        ///     管理员拒绝退货退款
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "管理员支付货款")]
        [OrderActionType(Intro = "管理员{AdminUserName}支付货款", AllowStatus = "WaitingSellerSendGoods", Name = "管理员支付货款",
            Method = "AdminPayment", Type = "AdminRefuseRefund", AllowUser = 2)]
        AdminPayment = 326,

        #endregion 管理员操作

        #region 线下店铺用户操作

        /// <summary>
        /// 会员创建订单(线下店铺)
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "会员创建订单")]
        [OrderActionType(Intro = "会员{OrderUserName}创建订单，订单金额为{OrderAmount}")]
        OfflineUserCreateOrder = 401,

        /// <summary>
        /// 会员支付订单(线下店铺)
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "会员支付订单")]
        [OrderActionType(Intro = "会员{OrderUserName}支付订单，支付方式为{PayMenent}", AllowStatus = "WaitingBuyerPay", Name = "支付",
            Method = "api/order/pay", AllowUser = 3)]
        OfflineUserPayOrder = 402,

        /// <summary>
        /// 会员取消订单(线下店铺)
        /// </summary>
        [LabelCssClass("text-success")]
        [Display(Name = "会员取消订单")]
        [OrderActionType(Intro = "会员{OrderUserName}取消订单", AllowStatus = "WaitingBuyerPay", Name = "取消",
            Method = "api/order/cancel", AllowUser = 3)]
        OfflineUserCancleOrder = 403,

        #endregion 线下店铺用户操作
    }
}