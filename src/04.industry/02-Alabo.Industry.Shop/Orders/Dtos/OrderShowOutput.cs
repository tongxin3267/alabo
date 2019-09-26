using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.App.Shop.Product.Domain.Dtos;
using Alabo.Data.People.Users.Dtos;

namespace Alabo.App.Shop.Order.Domain.Dtos {

    public class OrderShowOutput {
        /// <summary>
        ///     商品属性ID
        /// </summary>

        [Display(Name = "序号")]
        public long Id { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is group buy.
        ///     是否为拼团购买商品
        /// </summary>
        public bool IsGroupBuy { get; set; } = false;

        public string StoreName { get; set; }

        public string Serial { get; set; }

        /// <summary>
        ///     下单用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     所属店铺
        /// </summary>
        public long StoreId { get; set; }

        /// <summary>
        ///     订单交易状态,OrderStatus等待付款WaitingBuyerPay = 0,等待发货WaitingSellerSendGoods = 1,已发货WaitingBuyerConfirm = 2,交易成功Success =
        ///     3,已取消Cancelled = 4,已作废Invalid = 5,
        /// </summary>
        [Display(Name = "订单状态")]
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        ///     订单类型
        /// </summary>
        public OrderType OrderType { get; set; }

        /// <summary>
        ///     订单总金额
        ///     订单总金额=商品总金额-优惠金额-（+）调整金额+税费金额+邮费金额 -其他账户支出
        /// </summary>
        [Display(Name = "订单总金额")]
        public decimal TotalAmount { get; set; }

        public decimal ExpressAmount { get; set; }

        /// <summary>
        ///     订单总数量
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        ///     订单实际支付的金额
        ///     订单实际支付的金额=商品总金额-优惠金额-（+）调整金额+邮费金额 -其他账户支出
        /// </summary>
        public decimal PaymentAmount { get; set; }

        /// <summary>
        ///     选择的收货地址id
        /// </summary>
        public string AddressId { get; set; }

        /// <summary>
        ///     下单时间，订单创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        ///     支付时间
        /// </summary>
        public string PayTime { get; set; }

        /// <summary>
        ///     支付方式
        /// </summary>
        public string PaymentType { get; set; }

        /// <summary>
        ///     商品Sku列表
        /// </summary>
        public IList<ProductSkuItem> ProductSkuItems { get; set; }

        /// <summary>
        ///     卖家用户
        /// </summary>
        public UserOutput BuyUser { get; set; }

        /// <summary>
        ///     发货用户
        /// </summary>
        public UserOutput DeliverUser { get; set; }

        /// <summary>
        ///     订单信息
        /// </summary>
        public Entities.Order Order { get; set; }

        /// <summary>
        ///     物流信息
        /// </summary>
        public List<OrderDelivery> OrderDeliverys { get; set; } = new List<OrderDelivery>();

        /// <summary>
        ///     界面上操作的方法，根据订单状态来显示
        /// </summary>
        public IList<OrderActionTypeAttribute> Methods { get; set; }

        /// <summary>
        /// 物流信息
        /// </summary>
        public Deliver Deliver { get; set; }
    }
}