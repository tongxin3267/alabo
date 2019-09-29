using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Framework.Basic.Address.Domain.Entities;
using Alabo.Industry.Shop.Orders.Domain.Enums;
using Alabo.Validations;

namespace Alabo.Industry.Shop.Orders.Dtos
{
    /// <summary>
    ///     用户输入参数
    /// </summary>
    public class UserOrderInput
    {
        /// <summary>
        ///     Gets or sets the store express.
        ///     店铺和运费方式列表
        /// </summary>
        /// <value>
        ///     The store express.
        /// </value>
        public List<StoreExpress> StoreExpress = new List<StoreExpress>();

        /// <summary>
        ///     购物订单签名
        ///     如果签名在缓存中不存在数据时，则通过BuyInfoInput获取
        /// </summary>
        [Display(Name = "缓存Key")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Sign { get; set; }

        /// <summary>
        ///     Gets or sets the address identifier.
        /// </summary>
        /// <value>
        ///     The address identifier.
        /// </value>
        [Display(Name = "地址")]
        public string AddressId { get; set; }

        /// <summary>
        ///     登陆用户Id
        /// </summary>
        [Display(Name = "登陆用户Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long LoginUserId { get; set; }

        /// <summary>
        ///     Gets or sets the store express json.
        /// </summary>
        /// <value>
        ///     The store express json.
        /// </value>
        [Display(Name = "店铺运费方式")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string StoreExpressJson { get; set; }

        /// <summary>
        ///     Gets or sets the reduce moneys.
        ///     减少的数字资产
        /// </summary>
        /// <value>
        ///     The reduce moneys.
        /// </value>
        public IList<KeyValuePair<Guid, decimal>> ReduceMoneys { get; set; } = new List<KeyValuePair<Guid, decimal>>();

        /// <summary>
        ///     Gets or sets the reduce moneys json.
        /// </summary>
        /// <value>
        ///     The reduce moneys json.
        /// </value>
        public string ReduceMoneysJson { get; set; }

        /// <summary>
        ///     优惠券信息, Json数组
        /// </summary>
        public string CouponJson { get; set; }
    }

    /// <summary>
    ///     订单价格缓存对象
    /// </summary>
    public class OrderPriceCache
    {
        /// <summary>
        ///     下单的用户地址列表
        /// </summary>
        public List<UserAddress> UserAddresses { get; set; }

        /// <summary>
        ///     Gets or sets the store product sku.
        /// </summary>
        /// <value>
        ///     The store product sku.
        /// </value>
        public StoreProductSku StoreProductSku { get; set; }

        /// <summary>
        ///     Gets or sets the order moneys.
        /// </summary>
        /// <value>
        ///     The order moneys.
        /// </value>
        public IList<OrderMoneyItem> OrderMoneys { get; set; }
    }

    /// <summary>
    ///     StoreExpress
    /// </summary>
    public class StoreExpress
    {
        /// <summary>
        ///     key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///     value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        ///     快递方式
        /// </summary>
        public ExpressType ExpressType { get; set; } = ExpressType.Express;
    }
}