using System;
using System.Collections.Generic;
using Alabo.App.Core.Admin.Domain.CallBacks;
using Alabo.App.Shop.Activitys.Dtos;
using Alabo.App.Shop.Activitys.Extensions;
using Alabo.App.Shop.Activitys.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Activitys.Domain.Services {

    /// <summary>
    ///     Interface IActivityAdminService
    /// </summary>
    public interface IActivityAdminService : IService {

        /// <summary>
        ///     获取s the 所有 类型.
        /// </summary>
        IEnumerable<Type> GetAllTypes();

        /// <summary>
        ///     获取s the 所有 活动 attributes.
        /// </summary>
        IEnumerable<ActivityModuleAttribute> GetAllActivityAttributes();

        /// <summary>
        ///     获取s the 活动 模块 特性.
        /// </summary>
        /// <param name="key">The key.</param>
        ActivityModuleAttribute GetActivityModuleAttribute(string key);

        /// <summary>
        ///     添加s the 或 更新.
        /// </summary>
        /// <param name="model">The model.</param>
        ServiceResult AddOrUpdate(ViewActivityModel model);

        /// <summary>
        ///     获取s the 视图 活动 model.
        /// </summary>
        /// <param name="editInput">The edit input.</param>
        ViewActivityModel GetViewActivityModel(ActivityEditInput editInput);

        /// <summary>
        ///     删除s the specified identifier.
        /// </summary>
        /// <param name="id">Id标识</param>
        ServiceResult Delete(long id);

        /// <summary>
        ///     获取s the page list.
        /// </summary>
        /// <param name="querey">The querey.</param>
        PagedList<ViewActivityPage> GetPageList(object querey);

        /// <summary>
        ///     获取s the product 分页 list.
        ///     活动商品
        /// </summary>
        /// <param name="query">查询</param>
        PagedList<ViewActivityProductPage> GetProductPageList(object query);

        /// <summary>
        ///     获取s the order 分页 list.
        ///     活动订单
        /// </summary>
        /// <param name="query">查询</param>
        PagedList<ViewActivityOrderPage> GetOrderPageList(object query);

        /// <summary>
        ///     获取s the record 分页 list.
        ///     活动记录
        /// </summary>
        /// <param name="query">查询</param>
        PagedList<ViewActivityRecord> GetRecordPageList(object query);
    }
}