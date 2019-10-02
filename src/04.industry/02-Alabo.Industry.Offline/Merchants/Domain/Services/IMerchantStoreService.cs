using System;
using System.Collections.Generic;
using Alabo.Data.People.Merchants.Domain.Entities;
using Alabo.Domains.Services;
using Alabo.Industry.Offline.Merchants.Domain.Dtos;
using Alabo.Industry.Offline.Merchants.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Offline.Merchants.Domain.Services
{
    public interface IMerchantStoreService : IService<MerchantStore, ObjectId>
    {
        /// <summary>
        ///     Get merchant by merchant store id
        /// </summary>
        /// <param name="merchantStoreId"></param>
        /// <returns></returns>
        Tuple<Merchant, MerchantStore> GetMerchantByMerchantStoreId(string merchantStoreId);

        /// <summary>
        ///     获取商家和店铺(一个)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Tuple<Merchant, MerchantStore> GetMerchantByUserId(long userId);

        /// <summary>
        ///     Get merchant stores
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="merchantStoreId"></param>
        /// <returns></returns>
        List<MerchantStore> GetMerchantStore(long userId);

        /// <summary>
        ///     Save store info
        /// </summary>
        /// <param name="input"></param>
        void Save(MerchantStoreInput input);
    }
}