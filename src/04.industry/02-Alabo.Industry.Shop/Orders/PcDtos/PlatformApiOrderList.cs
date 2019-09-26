using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Core.Employes.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Framework.Core.WebUis.Design.AutoLists;
using Alabo.Framework.Core.WebUis.Design.AutoTables;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Order.Domain.PcDtos {

    /// <summary>
    /// 平台订单列表，只有管理员才可以查看
    /// </summary>
    [ClassProperty(Name = "平台订单列表", Description = "平台订单列表")]
    public class PlatformApiOrderList : BaseApiOrderList, IAutoTable<PlatformApiOrderList>, IAutoList {

        public List<TableAction> Actions() {
            var list = new List<TableAction>
            {
                ToLinkAction("查看订单", "/Admin/Order/Edit",TableActionType.ColumnAction),//管理员后端查看订单
                //ToLinkAction("查看订单", "/Admin/Purchase/Edit",TableActionType.ColumnAction),//采购订单查看
            };
            return list;
        }

        public PageResult<AutoListItem> PageList(object query, AutoBaseModel autoModel) {
            var dic = HttpWeb.HttpContext.ToDictionary();
            dic = dic.RemoveKey("userId");// 否则查出的订单都是同一个用户

            var model = ToQuery<PlatformApiOrderList>();

            var expressionQuery = new ExpressionQuery<Entities.Order>();
            if (model.OrderStatus > 0) {
                expressionQuery.And(e => e.OrderStatus == model.OrderStatus);
            }

            //var isAdmin = Resolve<IUserService>().IsAdmin(model.UserId);
            //if (!isAdmin) {
            //    throw new ValidException("非管理员不能查看平台订单");
            //}
            expressionQuery.And(e => e.StoreId > 0);

            model.UserId = 0;
            var pageList = Resolve<IOrderApiService>().GetPageList(dic.ToJson(), expressionQuery);

            var list = new List<AutoListItem>();
            foreach (var item in pageList) {
                var apiData = new AutoListItem {
                    Title = $"金额{item.TotalAmount}元",
                    Intro = item.UserName,
                    Value = item.TotalAmount,
                    Image = Resolve<IApiService>().ApiUserAvator(item.UserId),
                    Id = item.Id,
                    Url = $"/pages/user?path=Asset_recharge_view&id={item.Id}"
                };
                list.Add(apiData);
            }
            return ToPageList(list, pageList);
        }

        public PageResult<PlatformApiOrderList> PageTable(object query, AutoBaseModel autoModel) {
            var dic = HttpWeb.HttpContext.ToDictionary();
            dic = dic.RemoveKey("userId");// 否则查出的订单都是同一个用户

            var model = ToQuery<PlatformApiOrderList>();

            var expressionQuery = new ExpressionQuery<Entities.Order> {
                PageIndex = 1,
                EnablePaging = true
            };

            if (model.OrderStatus > 0) {
                expressionQuery.And(e => e.OrderStatus == model.OrderStatus);
            }

            expressionQuery.And(e => e.StoreId > 0);
            expressionQuery.And(e => e.UserId > 0);

            if (System.Enum.IsDefined(typeof(Domain.Enums.OrderType), model.OrderType)) {
                expressionQuery.And(e => e.OrderType == model.OrderType);
            }
            if (autoModel.Filter == FilterType.Admin) {
                var isAdmin = Resolve<IUserService>().IsAdmin(autoModel.BasicUser.Id);
                if (!isAdmin) {
                    throw new ValidException("非管理员不能查看平台订单");
                }
            } else if (autoModel.Filter == FilterType.Store) {
                var store = Resolve<IShopStoreService>().GetUserStore(autoModel.BasicUser.Id);
                if (store == null) {
                    throw new ValidException("您不是供应商,暂无店铺");
                }
                expressionQuery.And(e => e.StoreId == store.Id);
                // 供应商
                //expressionQuery.And(e => e.OrderExtension.IsSupplierView == true);
                expressionQuery.And(u => u.OrderStatus == OrderStatus.Remited);
            }
            //else if (autoModel.Filter == FilterType.User) {
            //    expressionQuery.And(e => e.UserId == autoModel.BasicUser.Id);
            //}
            else {
                //其他用户查看自己的订单
                expressionQuery.And(e => e.UserId == autoModel.BasicUser.Id);
            }
            var list = Resolve<IOrderApiService>().GetPageList(dic.ToJson(), expressionQuery);
            return ToPageResult<PlatformApiOrderList, ApiOrderListOutput>(list);
        }

        public Type SearchType() {
            throw new NotImplementedException();
        }
    }
}