using System;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.Carts.Domain.Entities;
using Alabo.Industry.Shop.Orders.Dtos;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Carts.Domain.Services {

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