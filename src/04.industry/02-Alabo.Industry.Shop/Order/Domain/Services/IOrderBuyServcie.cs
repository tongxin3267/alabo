using System;
using System.Collections.Generic;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Order.Domain.Services {

    /// <summary>
    ///     订单购物、订单价格计算相关的订单服务
    ///     会员订单业务，管理员订单业务，请不要写到此处
    /// </summary>
    /// <seealso cref="Alabo.Domains.Services.IService" />
    public interface IOrderBuyServcie : IService {

        /// <summary>
        /// 订单支付
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Tuple<ServiceResult, BuyOutput> Pay(long orderId, long userId);

        /// <summary>
        ///     订单购买，包括立即购买、购物车购买
        ///     支持多种货币类型
        ///     整个系统使用该服务即可，是系统非常核心的业务服务，请谨慎维护
        /// </summary>
        /// <param name="orderBuyInput"></param>
        Tuple<ServiceResult, BuyOutput> Buy(BuyInput orderBuyInput);

        /// <summary>
        ///     生成订单确定页面信息
        ///     每次客户端更改数量，通过请求该方法重新计算价格
        ///     后期可以考虑客服端计算价格
        /// </summary>
        /// <param name="buyInfoInput"></param>
        Tuple<ServiceResult, StoreProductSku> BuyInfo(BuyInfoInput buyInfoInput);

        /// <summary>
        ///     获取店铺订单价格
        ///     根据用户前台输入和签名信息，获取价格
        ///     通过签名信息获取从缓存中获取价格信息
        /// </summary>
        /// <param name="userOrderInput"></param>
        Tuple<ServiceResult, StoreOrderPrice> GetPrice(UserOrderInput userOrderInput);

        /// <summary>
        ///     Count the price.
        ///     计算价格：整个系统计算价格的核心公式，也是唯一公式，计算价格只维护改方法
        ///     根据用户输入，比如运费，快递方式，地址等
        ///     和店铺商品Sku信息获取价格
        /// </summary>
        /// <param name="storeProductSku">The store product sku.</param>
        /// <param name="userOrderInput"></param>
        /// <param name="user"></param>
        /// <param name="userAddress">用户地址信息，可以为空，批量传入加速价格计算</param>
        /// <param name="orderMoneys"></param>
        Tuple<ServiceResult, StoreOrderPrice> CountPrice(ref StoreProductSku storeProductSku,
            UserOrderInput userOrderInput, User user, IEnumerable<UserAddress> userAddress = null,
            IList<OrderMoneyItem> orderMoneys = null, bool isUpdateSkuPrice = true);
    }
}