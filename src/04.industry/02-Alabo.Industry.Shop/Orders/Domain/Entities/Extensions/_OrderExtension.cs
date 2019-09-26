using System.Collections.Generic;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Shop.AfterSale.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Order.Dtos;
using Alabo.App.Shop.Product.Domain.Dtos;
using Alabo.Domains.Entities.Extensions;
using Alabo.Framework.Basic.Address.Domain.Entities;
using Alabo.Users.Entities;

namespace Alabo.App.Shop.Order.Domain.Entities.Extensions
{

    /// <summary>
    ///     订单扩展数据，以json格式保存到订单表
    /// </summary>
    public class OrderExtension : EntityExtension
    {

        /// <summary>
        ///    供应商是否可以查看
        /// 如果为False的时候，供应商不可以查看
        /// </summary>
        public bool IsSupplierView { get; set; } = false;


        /// <summary>
        ///     是否来自于订购页面
        ///     订单从前台 /Order/Goods页面而来
        /// </summary>
        public bool IsFromOrder { get; set; } = false;

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is group buy.
        ///     是否为拼团购买商品
        /// </summary>
        public bool IsGroupBuy { get; set; } = false;

        /// <summary>
        /// 允许使用的资产
        /// </summary>
        public IList<OrderMoneyItem> AllowMoneys { get; set; } = new List<OrderMoneyItem>();

        /// <summary>
        ///     Gets or sets the reduces moneys.
        ///     订单减少的虚拟资产
        /// </summary>
        /// <value>
        ///     The reduces moneys.
        /// </value>
        public IList<ReduceAmount> ReduceAmounts { get; set; } = new List<ReduceAmount>();

        /// <summary>
        ///     订单信息
        /// </summary>
        public OrderMessage Message { get; set; } = new OrderMessage();

        /// <summary>
        ///     订单备注
        /// </summary>
        public OrderRemark OrderRemark { get; set; } = new OrderRemark();

        /// <summary>
        ///     订单评价
        /// </summary>
        public OrderRate OrderRate { get; set; } = new OrderRate();

        /// <summary>
        ///     订单价格信息
        /// </summary>
        public OrderAmount OrderAmount { get; set; } = new OrderAmount();

        /// <summary>
        ///     买家的交易数据，快照数据，包括买家订单创建时候的数据
        /// </summary>
        public User User
        {
            get; set;
        }

        /// <summary>
        ///     订单所属店铺,快照数据
        /// </summary>
        public Store.Domain.Entities.Store Store { get; set; } = new Store.Domain.Entities.Store();

        /// <summary>
        ///     订单地址
        /// </summary>
        public UserAddress UserAddress
        {
            get; set;
        }

        /// <summary>
        ///     对应的账单信息
        /// </summary>
        public Pay Pay
        {
            get; set;
        }

        /// <summary>
        ///     Gets or sets the product.
        ///     商品快照
        /// </summary>
        /// <value>
        ///     The product.
        /// </value>
        public IList<ProductSkuItem> ProductSkuItems
        {
            get; set;
        }

        /// <summary>
        /// 额外的
        /// </summary>
        public string AttachContent { get; set; }


        /// <summary>
        /// 退货,退款 详情
        /// </summary>
        public Refund RefundInfo { get; set; }

        /// <summary>
        /// 货款
        /// </summary>
        public PayGoodsAmountInput PayGoods { get; set; }

        /// <summary>
        /// 租户信息
        /// </summary>
        public TenantOrderInfo TenantOrderInfo { get; set; }


    }

    /// <summary>
    /// 租户订单信息
    /// </summary>
    public class TenantOrderInfo
    {
        /// <summary>
        /// 如果存在主库, 代表租户的订单ID
        /// 如果存在租库, 代表主库的订单ID
        /// </summary>
        public  long OrderId { set; get; }


        /// <summary>
        /// 租户标识
        /// </summary>
        public string Tenant { get; set; }
    }
}