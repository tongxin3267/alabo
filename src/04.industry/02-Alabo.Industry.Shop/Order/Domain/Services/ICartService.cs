using MongoDB.Bson;
using System;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Order.Domain.Services {

    public interface ICartService : IService<Cart, ObjectId> {

        /// <summary>
        ///     添加购物车
        /// </summary>
        /// <param name="orderProductInput"></param>
        ServiceResult AddCart(OrderProductInput orderProductInput);

        /// <summary>
        ///     获取用户的购物车
        /// </summary>
        /// <param name="userId"></param>
        Tuple<ServiceResult, StoreProductSku> GetCart(long userId);

        /// <summary>
        ///     移除购物车
        /// </summary>
        /// <param name="orderProductInput"></param>
        ServiceResult RemoveCart(OrderProductInput orderProductInput);

        /// <summary>
        ///     更新购物车
        /// </summary>
        /// <param name="orderProductInput"></param>
        ServiceResult UpdateCart(OrderProductInput orderProductInput);
    }
}