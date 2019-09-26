using System;
using System.Collections.Generic;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Industry.Offline.Order.Domain.Dtos;
using Alabo.Industry.Offline.Order.Domain.Entities;
using Alabo.Industry.Offline.Order.ViewModels;
using MongoDB.Bson;

namespace Alabo.Industry.Offline.Order.Domain.Services
{
    public interface IMerchantCartService : IService<MerchantCart, ObjectId>
    {
        /// <summary>
        ///     Add cart
        /// </summary>
        /// <param name="orderProductInput"></param>
        ServiceResult AddCart(MerchantCartInput input);

        /// <summary>
        ///     Get cart
        /// </summary>
        Tuple<ServiceResult, MerchantCartOutput> GetCart(long userId, string merchantStoreId);

        /// <summary>
        ///     Get cart by ids
        /// </summary>
        /// <param name="cartIds"></param>
        /// <returns></returns>
        List<MerchantCartViewModel> GetCart(List<ObjectId> cartIds);

        /// <summary>
        ///     Remove cart
        /// </summary>
        /// <param name="orderProductInput"></param>
        ServiceResult RemoveCart(MerchantCartInput input);

        /// <summary>
        ///     Update cart
        /// </summary>
        /// <param name="orderProductInput"></param>
        ServiceResult UpdateCart(MerchantCartInput input);
    }
}