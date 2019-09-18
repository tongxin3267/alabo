using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Shop.Activitys.Domain.Entities;
using Alabo.App.Shop.Activitys.Domain.Enum;
using Alabo.App.Shop.Activitys.Dtos;
using Alabo.App.Shop.Activitys.ViewModels;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Store.Domain.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Activitys.Services
{
    /// <summary>
    /// activity api interface
    /// </summary>
    public interface IActivityApiService : IService
    {
        /// <summary>
        /// get view for activity
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        ActivityEditOutput GetView(ActivityEditInput activity);

        /// <summary>
        /// save activity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResult Save(ActivityEditOutput model);

        /// <summary>
        /// get list by activity type
        /// </summary>
        /// <param name="activityType"></param>
        /// <param name="productIds"></param>
        /// <returns></returns>
        IList<Activity> GetList(ProductActivityType activityType, IList<long> productIds);

        /// <summary>
        /// check buy permission activity
        /// </summary>
        /// <param name="storeOrders"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        ServiceResult CheckBuyPermissionActivity(IList<StoreOrderItem> storeOrders, User user);

        /// <summary>
        /// check presell activity
        /// </summary>
        /// <param name="storeOrders"></param>
        ServiceResult CheckPreSellActivity(IList<StoreOrderItem> storeOrders);

        /// <summary>
        /// check time limit buy activity
        /// </summary>
        /// <param name="storeOrders"></param>
        ServiceResult CheckTimeLimitBuyActivity(IList<StoreOrderItem> storeOrders);

        /// <summary>
        /// get current user price
        /// </summary>
        /// <param name="storeItems"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        List<ProductGradePrice> GetMemberDiscountPrice(List<StoreItem> storeItems, User user);
    }
}