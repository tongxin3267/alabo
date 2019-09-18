using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.UI;
using Alabo.UI.AutoForms;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Store.Domain.Dtos {

    public class StoreOrdersToExcel : UIBase, IAutoForm {

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

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            return ToAutoForm(new StoreOrdersToExcel());
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var condition = (StoreOrdersToExcel)model;
            var store = Resolve<IStoreService>().GetSingle(u => u.UserId == autoModel.BasicUser.Id);
            if (store == null) {
                return ServiceResult.FailedWithMessage("非法操作");
            }

            var query = new ExpressionQuery<Order.Domain.Entities.Order> {
                PageIndex = 1,
                PageSize = 15
            };
            query.And(u => u.StoreId == store.Id);
            query.And(u => u.OrderStatus == condition.Status);
            var view = Resolve<IOrderService>().GetPagedList(query);
            var orders = new List<Order.Domain.Entities.Order>();
            foreach (var item in view) {
                TimeSpan ts = DateTime.Now.Subtract(item.CreateTime);
                if (ts.Days < condition.Days) {
                    orders.Add(item);
                }
            }

            view.Result = orders;
            var modelType = "Order".GetTypeByName();
            var result = Resolve<IAdminTableService>().ToExcel(modelType, view);
            return ServiceResult.Success;
        }
    }
}