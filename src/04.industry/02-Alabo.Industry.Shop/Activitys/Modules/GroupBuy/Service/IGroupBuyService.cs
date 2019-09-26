using System;
using System.Collections.Generic;
using Alabo.App.Shop.Activitys.Modules.GroupBuy.Dtos;
using Alabo.App.Shop.Product.Domain.Dtos;
using Alabo.Framework.Core.WebApis.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Activitys.Modules.GroupBuy.Service {

    /// <summary>
    ///     Interface IGroupBuyService
    ///     拼团服务
    /// </summary>
    public interface IGroupBuyService : IService {

        /// <summary>
        ///     获取s the group buy product records.
        ///     获取拼团商品记录
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        Tuple<ServiceResult, IList<GroupBuyProductRecord>> GetGroupBuyProductRecords(long productId);

        /// <summary>
        ///     获取s the grouy buy 会员 by order identifier.
        ///     根据订单Id，获取商品订单记录
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        Tuple<ServiceResult, IList<GroupBuyRecordUser>> GetGrouyBuyUserByOrderId(long orderId);

        /// <summary>
        /// 获取拼团商品列表
        /// </summary>
        /// <param name="parameter">参数</param>
        ProductItemApiOutput GetProductItems(ApiBaseInput parameter);

        /// <summary>
        /// 通过缓存获取所有的拼团商品Id
        /// </summary>
        List<long> GetAllProductIds();
    }
}