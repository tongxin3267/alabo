using System;
using System.Collections.Generic;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Shop.Order.ViewModels.OrderEdit {

    /// <summary>
    ///     发货
    /// </summary>
    public class OrderEditDelivery : BaseViewModel {

        /// <summary>
        ///     订单Id
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        ///     发货人员Id
        /// </summary>
        public long AdminUserId { get; set; }

        /// <summary>
        ///     快递公司guid
        /// </summary>
        public Guid ExpressGuid { get; set; }

        /// <summary>
        ///     物流单号
        /// </summary>
        public string ExpressNumber { get; set; }

        /// <summary>
        ///     发货数量Json
        /// </summary>
        public string DeliveryProducts { get; set; }

        // 发货数量
        public IList<OrderEditDeliveryProduct> DeliveryProductSkus { get; set; }
    }

    /// <summary>
    ///     发货数量
    /// </summary>
    public class OrderEditDeliveryProduct {

        /// <summary>
        ///     商品SKu
        /// </summary>
        public long SkuId { get; set; }

        /// <summary>
        ///     发货数量
        /// </summary>
        public long Count { get; set; }
    }
}