using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Linq.Dynamic;
using Alabo.Users.Entities;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Order.Domain.Dtos
{

    /// <summary>
    ///     用户购买
    ///     包括立即购买和购物车购买
    /// </summary>
    public class BuyInput : EntityDto
    {

        /// <summary>
        ///     Gets or sets 会员Id
        /// </summary>
        [Display(Name = "用户Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long UserId
        {
            get; set;
        }

        /// <summary>
        ///     购物订单签名
        ///     如果签名在缓存中不存在数据时，则通过BuyInfoInput获取
        /// </summary>
        [Display(Name = "缓存Key")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Sign
        {
            get; set;
        }

        /// <summary>
        ///     订单总金额
        /// </summary>
        [Display(Name = "订单总金额")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(0.00, long.MaxValue, ErrorMessage = ErrorMessage.NameNotInRang)]
        public decimal TotalAmount
        {
            get; set;
        }

        /// <summary>
        ///     订单商品总数
        /// </summary>
        [Display(Name = "订单商品总数")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, long.MaxValue, ErrorMessage = ErrorMessage.NameNotInRang)]
        public long TotalCount
        {
            get; set;
        }

        /// <summary>
        ///     支付金额，人民币
        /// </summary>
        [Display(Name = "支付金额")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(0.01, long.MaxValue, ErrorMessage = ErrorMessage.NameNotInRang)]
        public decimal PaymentAmount
        {
            get; set;
        }

        /// <summary>
        ///     地址Id
        ///     可以为空，必须虚拟商品的是
        /// </summary>
        [Display(Name = "地址Id")]
        public string AddressId
        {
            get; set;
        }

        /// <summary>
        ///     Gets or sets the store order json.
        /// </summary>
        [Display(Name = "订单Json")]
        public string StoreOrderJson
        {
            get; set;
        }

        /// <summary>
        ///     支付方式Id
        ///     与 Alabo.App.Core.Finance.Domain.CallBacks.PaymentConfig，的Id对应
        /// </summary>
        [Display(Name = "支付方式")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public PayType PayType
        {
            get; set;
        }

        /// <summary>
        ///     订单类型
        /// </summary>
        [Display(Name = "订单类型")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public OrderType OrderType
        {
            get; set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is from cart.
        ///     订单是否来自购物车
        /// </summary>
        public bool IsFromCart { get; set; } = false;

        /// <summary>
        ///     按店铺生成订单，每个订单一个条记录
        /// </summary>
        public IList<StoreOrderItem> StoreOrders
        {
            get; set;
        }

        /// <summary>
        ///     Gets or sets the reduce moneys.
        ///     减少的数字资产
        /// </summary>
        public IList<KeyValuePair<Guid, decimal>> ReduceMoneys { get; set; } = new List<KeyValuePair<Guid, decimal>>();

        /// <summary>
        ///     Gets or sets the reduce moneys json.
        /// </summary>
        public string ReduceMoneysJson
        {
            get; set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is group buy.
        ///     是否为拼团商品
        /// </summary>
        public bool IsGroupBuy { get; set; } = false;

        /// <summary>
        ///     是否来自于订购页面
        ///     订单从前台 /Order/Goods页面而来
        /// </summary>
        public bool IsFromOrder { get; set; } = false;

        /// <summary>
        ///     Gets or sets the 活动 identifier.
        ///     活动记录Id，拼团
        /// </summary>
        public long ActivityRecordId
        {
            get; set;
        }

        /// <summary>
        /// 优惠券信息, Json数组
        /// </summary>
        public string CouponJson { get; set; }
    }

    /// <summary>
    ///     店铺购买信息
    /// </summary>
    public class StoreOrderItem
    {
        /// <summary>
        ///     所属店铺
        /// </summary>
        [Display(Name = "店铺Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(0, 99999999, ErrorMessage = ErrorMessage.NameNotCorrect)]
        public long StoreId { get; set; }

        /// <summary>
        ///     配送方式 Id 与DeliveryTemplate 的Id对应
        /// </summary>
        [Display(Name = "配送方式")]
        public string DeliveryId { get; set; }

        /// <summary>
        ///     买家留言
        /// </summary>
        [Display(Name = "买家留言")]
        public string UserMessage { get; set; }

        /// <summary>
        ///     商品列表
        /// </summary>
        public IList<StoreOrderProductSkuItem> ProductSkuItems { get; set; }

        /// <summary>
        ///     店铺购买总价格
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     店铺购买总数量
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        ///     Gets or sets the express amount.
        ///     店铺总运费(实际邮费)
        /// </summary>
        public decimal ExpressAmount { get; set; }

        /// <summary>
        /// 邮费金额
        /// </summary>
        public decimal CalculateExpressAmount { get; set; }

        /// <summary>
        ///     Gets or sets the express amount.
        ///     店铺商品总费用
        /// </summary>
        public decimal ProductAmount { get; set; }

        /// <summary>
        /// 快递方式
        /// </summary>
        public ExpressType ExpressType { get; set; } = ExpressType.Express;
    }

    /// <summary>
    ///     Class StoreOrderProductSkuItem.
    /// </summary>
    public class StoreOrderProductSkuItem
    {

        /// <summary>
        ///     商品Id
        /// </summary>
        [Display(Name = "商品Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 99999999, ErrorMessage = ErrorMessage.NameNotCorrect)]
        public long ProductId
        {
            get; set;
        }

        /// <summary>
        ///     商品SKuID
        /// </summary>
        [Display(Name = "商品SKU")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 99999999, ErrorMessage = ErrorMessage.NameNotCorrect)]
        public long ProductSkuId
        {
            get; set;
        }

        /// <summary>
        ///     Gets or sets the store identifier.
        /// </summary>
        public long StoreId
        {
            get; set;
        }

        /// <summary>
        ///     商品数量
        /// </summary>
        [Display(Name = "商品数量")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 99999999, ErrorMessage = ErrorMessage.NameNotCorrect)]
        public long Count
        {
            get; set;
        }

        /// <summary>
        ///     Gets or sets the amount.
        /// </summary>
        [Display(Name = "订单金额")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(0, 99999999, ErrorMessage = ErrorMessage.NameNotCorrect)]
        public decimal Amount
        {
            get; set;
        }

        /// <summary>
        ///     Gets or sets the price style identifier.
        /// </summary>
        public Guid PriceStyleId
        {
            get; set;
        }
    }

    /// <summary>
    ///     Class SinglePayInput.
    /// </summary>
    public class SinglePayInput
    {

        /// <summary>
        ///     Gets or sets the orders.
        /// </summary>
        public IEnumerable<Entities.Order> Orders
        {
            get; set;
        }

        /// <summary>
        ///     Gets or sets the 会员.
        /// </summary>
        public User User
        {
            get; set;
        }

        /// <summary>
        ///     Gets or sets the reduce moneys.
        /// </summary>
        public List<OrderMoneyItem> ReduceMoneys
        {
            get; set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is admin pay.
        /// </summary>
        public bool IsAdminPay { get; set; } = false;

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is group buy.
        /// </summary>
        public bool IsGroupBuy { get; set; } = false;

        /// <summary>
        ///     Gets or sets the buyer count.
        /// </summary>
        public long BuyerCount { get; set; } = 0;

        /// <summary>
        ///     是否来自于订购页面
        ///     订单从前台 /Order/Goods页面而来
        /// </summary>
        public bool IsFromOrder { get; set; } = false;

        /// <summary>
        /// 支付成功后执行的方法
        /// 注意以下几点：否则无法执行
        /// 1. 类型必须为void
        /// 2. 参数
        /// </summary>
        public BaseServiceMethod AfterSuccess { get; set; }

        /// <summary>
        /// 返回Sql脚本对象
        /// 注意以下几点
        /// 1. 返回类型必须为List<String>
        ///
        /// </summary>
        public BaseServiceMethod ExcecuteSqlList { get; set; }

        /// <summary>
        /// 支付失败后执行的方法
        /// 注意以下几点：否则无法执行
        /// 1. 类型必须为void
        /// 2. 参数
        /// </summary>
        public BaseServiceMethod AfterFail { get; set; }

        /// <summary>
        /// 支付人
        /// </summary>
        public User OrderUser { get; set; }

        /// <summary>
        /// 分润触发类型
        /// </summary>
        public TriggerType TriggerType { get; set; }

        /// <summary>
        /// 支付订单类型
        /// </summary>
        public CheckoutType CheckoutType { get; set; } = CheckoutType.Order;

         /// <summary>
        /// 支付成功后跳转链接
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// 扩展字段,存json格式
        /// </summary>
        public string Expansion { get; set; }

        /// <summary>
        ///     结算类型
        ///     支付订单类型
        /// </summary>
        [Display(Name = "订单类型")]
        [Field(ControlsType = ControlsType.DropdownList, EditShow = true, Width = "150",
            DataSource = "Alabo.Core.Enums.Enum.CheckoutType", ListShow = true, IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, SortOrder = 5)]
        public CheckoutType Type { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public string EntityId { get; set; }

    }
}