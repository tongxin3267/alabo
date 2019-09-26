using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Framework.Core.WebUis.Domain.Services;
using Alabo.Industry.Shop.Deliveries.Domain.Services;
using Alabo.Industry.Shop.Orders.Domain.Entities;
using Alabo.Industry.Shop.Orders.Domain.Enums;
using Alabo.Industry.Shop.Orders.Domain.Services;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.Deliveries.Domain.Dtos
{
    public class StoreOrdersToExcel : UIBase, IAutoForm
    {
        /// <summary>
        ///     订单状态
        /// </summary>
        [Display(Name = "订单状态")]
        [Field(ControlsType = ControlsType.DropdownList, IsShowAdvancedSerach = true, Width = "180",
            ListShow = true, SortOrder = 7)]
        public OrderStatus Status { get; set; }

        /// <summary>
        ///     订单导出日数条件
        /// </summary>
        [Display(Name = "订单导出日数条件")]
        [Field(ControlsType = ControlsType.Numberic, IsShowAdvancedSerach = true, Width = "180",
            ListShow = true, SortOrder = 7)]
        public long Days { get; set; }

        public long UserId { get; set; }

        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            return ToAutoForm(new StoreOrdersToExcel());
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            var condition = (StoreOrdersToExcel) model;
            var store = Resolve<IShopStoreService>().GetSingle(u => u.UserId == autoModel.BasicUser.Id);
            if (store == null) return ServiceResult.FailedWithMessage("非法操作");

            var query = new ExpressionQuery<Order>
            {
                PageIndex = 1,
                PageSize = 15
            };
            query.And(u => u.StoreId == store.Id);
            query.And(u => u.OrderStatus == condition.Status);
            var view = Resolve<IOrderService>().GetPagedList(query);
            var orders = new List<Order>();
            foreach (var item in view)
            {
                var ts = DateTime.Now.Subtract(item.CreateTime);
                if (ts.Days < condition.Days) orders.Add(item);
            }

            view.Result = orders;
            var modelType = "Order".GetTypeByName();
            var result = Resolve<IAdminTableService>().ToExcel(modelType, view);
            return ServiceResult.Success;
        }
    }
}