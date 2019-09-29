using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Asset.Accounts.Domain.Entities;
using Alabo.App.Asset.Pays.Domain.Entities;
using Alabo.Industry.Shop.Orders.Domain.Enums;
using Alabo.Industry.Shop.Products.ViewModels;

namespace Alabo.Industry.Shop.Orders.ViewModels
{
    public class ViewBuyOrder
    {
        /// <summary>
        ///     选中的支付方式
        /// </summary>
        public string SelectedPayment { get; set; }

        //public BuyerAddress Address { get; set; } = new BuyerAddress();

        /// <summary>
        ///     选择的收货地址id
        /// </summary>
        public long AddressId { get; set; }

        /// <summary>
        ///     是否需要收货地址
        /// </summary>
        public bool NeedBuyerAddress { get; set; }

        /// <summary>
        ///     订单总金额
        /// </summary>
        [Display(Name = "订单总金额")]
        [Range(0, 99999999, ErrorMessage = "订单总金额必须为大于等于0的整数")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     订单实际支付的金额
        /// </summary>
        [Display(Name = "订单实际支付的金额")]
        [Range(0, 99999999, ErrorMessage = "订单实际支付的金额必须为大于等于0的整数")]
        [DataType(DataType.Currency)]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        ///     商品总金额
        /// </summary>
        [Display(Name = "商品总金额的金额")]
        [Range(0, 99999999, ErrorMessage = "商品总金额必须为大于等于0的整数")]
        public decimal TotalProductAmount { get; set; }

        /// <summary>
        ///     邮费金额
        /// </summary>
        [Display(Name = "邮费金额的金额")]
        [Range(0, 99999999, ErrorMessage = "邮费金额必须为大于等于0的整数")]
        public decimal PostAmount { get; set; }

        /// <summary>
        ///     优惠金额（如打折，VIP，满就送等），精确到2位小数
        /// </summary>
        [Display(Name = "优惠金额")]
        [Range(0, 99999999, ErrorMessage = "优惠金额必须为大于等于0的整数")]
        public decimal DiscountAmount { get; set; }

        /// <summary>
        ///     卖家手工调整金额，
        /// </summary>
        [Display(Name = "卖家手工调整金额")]
        [Range(0, 99999999, ErrorMessage = "卖家手工调整金额必须为大于等于0的整数")]
        public decimal AdjustAmount { get; set; }

        /// <summary>
        ///     税费金额
        /// </summary>
        [Display(Name = " 税费金额")]
        public decimal TaxAmount { get; set; }

        /// <summary>
        ///     买家留言
        /// </summary>
        [Display(Name = " 买家留言")]
        public string BuyerMessage { get; set; }

        /// <summary>
        ///     用户账号状态
        /// </summary>
        public List<Account> UserAccounts { get; set; }

        /// <summary>
        ///     用户账户使用限制
        /// </summary>
        public List<AccountUse> OtherAccountUses { get; set; }

        /// <summary>
        ///     该商品为纯非现金商品
        /// </summary>
        public bool IsNotCash { get; set; }

        /// <summary>
        ///     订单序列号，用于支付页面使用
        /// </summary>
        public string OrderSerial { get; set; }

        /// <summary>
        ///     订单Id，用于支付页面使用
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        ///     票据
        /// </summary>
        public Pay Invoice { get; set; }

        /// <summary>
        ///     行业扩展信息
        /// </summary>
        public string IndustryExtInfo { get; set; }

        /// <summary>
        ///     用户自提地址 Id
        /// </summary>
        public long SinceAddressId { get; set; }

        /// <summary>
        ///     线下门店 Id
        /// </summary>
        public long OfflineStoreId { get; set; }

        public string AttachData { get; set; }

        /// <summary>
        ///     订单类型
        /// </summary>
        public OrderType Type { get; set; }

        /// <summary>
        ///     订单扩展信息，预约服务项
        /// </summary>
        public long SupplyId { get; set; }

        /// <summary>
        ///     预约项Id（“,”分隔）
        /// </summary>
        public string BookingRecordIds { get; set; }

        /// <summary>
        ///     购物车（“,”分隔）
        /// </summary>
        public string Carts { get; set; }

        /// <summary>
        ///     服务是否预约
        /// </summary>
        public bool IsBooking { get; set; }

        /// <summary>
        ///     是否免支付预约
        /// </summary>
        public bool IsNoPayBooking { get; set; } = false;

        /// <summary>
        ///     用于校验
        /// </summary>
        // public OrderRequestModel RequestModel => JsonConvert.DeserializeObject<OrderRequestModel>(AttachData);

        public List<ProductActivityInfo> ProductActivityInfos { get; set; } = new List<ProductActivityInfo>();
    }

    /// <summary>
    ///     AccountUse 账户使用情况
    /// </summary>
    public class AccountUse
    {
        /// <summary>
        ///     限制的用户的账户ID
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        ///     货币类型 ID
        /// </summary>
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     该账户最终提交的数额
        /// </summary>
        public decimal Uses { get; set; }

        /// <summary>
        ///     提交数额的转换比率
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        ///     使用的上限额度
        /// </summary>
        public decimal Max { get; set; }

        /// <summary>
        ///     使用的下限额度
        /// </summary>
        public decimal Min { get; set; }

        /// <summary>
        ///     工厂方法创建 AccountUse 类的一个新实例
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="max">允许使用的上限率</param>
        /// <param name="min">允许使用的下限额度</param>
        /// <param name="moneyTypeId">货币类型ID</param>
        /// <param name="rate">转换比率</param>
        public static AccountUse Create(long accountId, decimal max, decimal min, Guid moneyTypeId, decimal rate)
        {
            return new AccountUse
            {
                AccountId = accountId,
                Rate = rate,
                Max = max,
                MoneyTypeId = moneyTypeId,
                Min = min
            };
        }
    }
}