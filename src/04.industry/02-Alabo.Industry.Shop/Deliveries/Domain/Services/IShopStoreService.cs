using System;
using System.Collections.Generic;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Domains.Services;
using Alabo.Framework.Basic.Address.Domain.Entities;
using Alabo.Industry.Shop.Deliveries.Domain.Dtos;
using Alabo.Industry.Shop.Deliveries.Domain.Entities.Extensions;
using Alabo.Industry.Shop.Deliveries.ViewModels;
using Alabo.Industry.Shop.Orders.Dtos;
using Alabo.Industry.Shop.Products.Dtos;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Deliveries.Domain.Services {

    /// <summary>
    ///     Interface IStoreService
    /// </summary>
    public interface IShopStoreService : IService<Entities.Store, long> {

        /// <summary>
        ///     获取自营店铺
        ///     平台店铺，后台添加的时候，为平台商品
        /// </summary>
        Entities.Store PlanformStore();

        /// <summary>
        ///     获取s the 会员 store.
        ///     获取会员店铺
        /// </summary>
        /// <param name="UserId">会员Id</param>
        Entities.Store GetUserStore(long UserId);

        /// <summary>
        ///     添加s the 或 更新.
        /// </summary>
        /// <param name="store">The store.</param>
        ServiceResult AddOrUpdate(ViewStore store);

        //ServiceResult AddOrUpdateTemplate(StoreDeliveryTemplate template);

        /// <summary>
        ///     Counts the express fee.
        ///     根据客户选择的物流方式，商品重量，计算快递费用
        /// </summary>
        /// <param name="storeId">The stroe identifier.</param>
        /// <param name="templateId">The express identifier.</param>
        /// <param name="userAddress">用户地址</param>
        /// <param name="weight">The weight.</param>
        Tuple<ServiceResult, decimal> CountExpressFee(long storeId, ObjectId templateId, UserAddress userAddress, decimal weight);

        /// <summary>
        ///     获取s the 视图 store 分页 list.
        /// </summary>
        /// <param name="dto">The dto.</param>
        PagedList<ViewStore> GetViewStorePageList(PagedInputDto dto);

        /// <summary>
        ///     Gets the store extension.d
        /// </summary>
        /// <param name="storeId">The store identifier.</param>
        StoreExtension GetStoreExtension(long storeId);

        /// <summary>
        ///     Updates the extensions.
        /// </summary>
        /// <param name="storeId">The store identifier.</param>
        /// <param name="storeExtension">The store extension.</param>
        ServiceResult UpdateExtensions(long storeId, StoreExtension storeExtension);

        /// <summary>
        ///     获取s the 视图 store.
        /// </summary>
        /// <param name="Id">Id标识</param>
        ViewStore GetViewStore(long Id);

        /// <summary>
        ///     获取s the 分页 list.
        /// </summary>
        /// <param name="query">查询</param>
        PagedList<ViewStore> GetPageList(object query);

        /// <summary>
        ///     Gets the store item list from cache.
        ///     从缓存中，读取StoreItem对象
        ///     如果店铺数据多的情况下，该方法需要优化
        /// </summary>
        IEnumerable<StoreItem> GetStoreItemListFromCache();

        /// <summary>
        ///     根据店铺Id列表，和商品skuId列表，输出店铺商品显示对象
        ///     用户购物车、客户下单
        ///     店铺数量多的情况下，改方法需要优化，店铺数量少的情况下，将店铺数据插入到了缓存了，所有性能较快
        ///     Gets the store product sku.
        /// </summary>
        /// <param name="storeProductSkuDtos">The store product sku dtos.</param>
        Tuple<ServiceResult, StoreProductSku> GetStoreProductSku(StoreProductSkuDtos storeProductSkuDtos);

        ViewStore GetView(long id);

        /// <summary>
        /// Counts the express fee.
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="userAddress"></param>
        /// <param name="productSkuItems"></param>
        /// <returns></returns>
        Tuple<ServiceResult, decimal> CountExpressFee(long storeId, UserAddress userAddress, IList<ProductSkuItem> productSkuItems);
    }
}