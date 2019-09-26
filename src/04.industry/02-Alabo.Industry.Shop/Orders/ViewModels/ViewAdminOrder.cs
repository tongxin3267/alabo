using System.Collections.Generic;
using Alabo.App.Asset.Pays.Domain.Entities;
using Alabo.Data.Things.Orders.Domain.Entities;
using Alabo.Industry.Shop.OrderActions.Domain.Entities;
using Alabo.Industry.Shop.OrderDeliveries.Domain.Entities;
using Alabo.Industry.Shop.Orders.Domain.Entities;
using Alabo.Industry.Shop.Orders.Domain.Entities.Extensions;
using Alabo.Industry.Shop.Orders.ViewModels.OrderEdit;
using Alabo.Users.Entities;

namespace Alabo.Industry.Shop.Orders.ViewModels
{
    public class ViewAdminOrder
    {
        public Order Order { get; set; } = new Order();

        /// <summary>
        ///     订单所包含的所有商品
        /// </summary>
        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        /// <summary>
        ///     订单操作类型，包括发货，等等
        /// </summary>
        /// <value>
        ///     The actions.
        /// </value>
        public IList<OrderAction> Actions { get; set; } = new List<OrderAction>();

        /// <summary>
        ///     发货记录
        /// </summary>
        public IList<OrderDelivery> OrderDeliveries { get; set; } = new List<OrderDelivery>();

        public List<OrderEditDeliveryProduct> DeliveryProduct { get; set; } = new List<OrderEditDeliveryProduct>();

        public long? PrexOrder { get; set; }
        public long? NextOrder { get; set; }

        /// <summary>
        ///     分润订单
        /// </summary>
        public ShareOrder ShareOrder { get; set; }

        /// <summary>
        ///     订单扩展数据
        /// </summary>
        public OrderExtension OrderExtension { get; set; }

        /// <summary>
        ///     下单用户
        /// </summary>
        public User User { get; set; }

        /// <summary>
        ///     界面上操作的方法，根据订单状态来显示
        /// </summary>
        public IList<OrderActionTypeAttribute> Methods { get; set; }

        /// <summary>
        ///     支付方式
        /// </summary>
        public Pay Pay { get; set; }

        /// <summary>
        ///     虚拟资产扣除说明
        /// </summary>
        public string ReduceMoneyIntro { get; set; }
    }
}