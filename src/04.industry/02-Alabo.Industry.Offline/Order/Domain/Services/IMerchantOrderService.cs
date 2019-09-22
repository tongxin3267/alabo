using System;
using System.Collections.Generic;
using Alabo.App.Offline.Order.Domain.Dtos;
using Alabo.App.Offline.Order.Domain.Entities;
using Alabo.App.Offline.Order.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Offline.Order.Domain.Services
{
    public interface IMerchantOrderService : IService<MerchantOrder, long>
    {
        /// <summary>
        /// Buy info
        /// </summary>
        /// <param name="buyInfo"></param>
        /// <returns></returns>
        Tuple<ServiceResult, MerchantCartOutput> BuyInfo(MerchantBuyInfoInput buyInfo);

        /// <summary>
        /// Calculator price
        /// </summary>
        /// <param name="cartProducts"></param>
        /// <returns></returns>
        Tuple<ServiceResult, MerchantCartOutput> CountPrice(List<MerchantCartViewModel> cartProducts);

        /// <summary>
        /// Submit order
        /// </summary>
        /// <param name="orderBuyInput"></param>
        /// <returns></returns>
        Tuple<ServiceResult, MerchantOrderBuyOutput> Buy(MerchantOrderBuyInput orderBuyInput);

        /// <summary>
        /// Execute sql 
        /// </summary>
        /// <param name="entityIdList"></param>
        /// <returns></returns>
        List<string> ExcecuteSqlList(List<object> entityIdList);

        /// <summary>
        /// Get order list
        /// </summary>
        /// <param name="orderInput"></param>
        /// <returns></returns>
        PagedList<MerchantOrderList> GetOrderList(MerchantOrderListInput orderInput);

        /// <summary>
        /// Get order singel
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Tuple<ServiceResult, MerchantOrderList> GetOrderSingle(long id, long userId);
        /// <summary>
        /// Get order singel
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Tuple<ServiceResult, MerchantOrderList> GetOrderSingle(long id);
    }
}