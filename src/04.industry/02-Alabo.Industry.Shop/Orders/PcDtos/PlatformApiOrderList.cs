﻿using System;
using System.Collections.Generic;
using Alabo.Data.People.Stores.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Helpers;
using Alabo.Industry.Shop.Deliveries.Domain.Services;
using Alabo.Industry.Shop.Orders.Domain.Entities;
using Alabo.Industry.Shop.Orders.Domain.Enums;
using Alabo.Industry.Shop.Orders.Domain.Services;
using Alabo.UI;
using Alabo.UI.Design.AutoLists;
using Alabo.UI.Design.AutoTables;
using Alabo.Web.Mvc.Attributes;
using OrderType = Alabo.Industry.Shop.Orders.Domain.Enums.OrderType;

namespace Alabo.Industry.Shop.Orders.PcDtos
{
    /// <summary>
    ///     平台订单列表，只有管理员才可以查看
    /// </summary>
    [ClassProperty(Name = "平台订单列表", Description = "平台订单列表")]
    public class PlatformApiOrderList : BaseApiOrderList, IAutoTable<PlatformApiOrderList>, IAutoList
    {
        public PageResult<AutoListItem> PageList(object query, AutoBaseModel autoModel)
        {
            var dic = HttpWeb.HttpContext.ToDictionary();
            dic = dic.RemoveKey("userId"); // 否则查出的订单都是同一个用户

            var model = ToQuery<PlatformApiOrderList>();

            var expressionQuery = new ExpressionQuery<Order>();
            if (model.OrderStatus > 0) {
                expressionQuery.And(e => e.OrderStatus == model.OrderStatus);
            }

            //var isAdmin = Resolve<IUserService>().IsAdmin(model.UserId);
            //if (!isAdmin) {
            //    throw new ValidException("非管理员不能查看平台订单");
            //}
            // expressionQuery.And(e => e.StoreId > 0);

            model.UserId = 0;
            var pageList = Resolve<IOrderApiService>().GetPageList(dic.ToJson(), expressionQuery);

            var list = new List<AutoListItem>();
            foreach (var item in pageList)
            {
                var apiData = new AutoListItem
                {
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

        public Type SearchType()
        {
            throw new NotImplementedException();
        }

        public List<TableAction> Actions()
        {
            var list = new List<TableAction>
            {
                ToLinkAction("查看订单", "/Admin/Order/Edit", TableActionType.ColumnAction) //管理员后端查看订单
                //ToLinkAction("查看订单", "/Admin/Purchase/Edit",TableActionType.ColumnAction),//采购订单查看
            };
            return list;
        }

        public PageResult<PlatformApiOrderList> PageTable(object query, AutoBaseModel autoModel)
        {
            var dic = HttpWeb.HttpContext.ToDictionary();
            dic = dic.RemoveKey("userId"); // 否则查出的订单都是同一个用户

            var model = ToQuery<PlatformApiOrderList>();

            var expressionQuery = new ExpressionQuery<Order>
            {
                PageIndex = 1,
                EnablePaging = true
            };

            if (model.OrderStatus > 0) {
                expressionQuery.And(e => e.OrderStatus == model.OrderStatus);
            }

            // expressionQuery.And(e => e.StoreId > 0);
            expressionQuery.And(e => e.UserId > 0);

            if (Enum.IsDefined(typeof(OrderType), model.OrderType)) {
                expressionQuery.And(e => e.OrderType == model.OrderType);
            }

            if (autoModel.Filter == FilterType.Admin)
            {
                var isAdmin = Resolve<IUserService>().IsAdmin(autoModel.BasicUser.Id);
                if (!isAdmin) {
                    throw new ValidException("非管理员不能查看平台订单");
                }
            }
            else if (autoModel.Filter == FilterType.Store)
            {
                var store = Resolve<IStoreService>().GetUserStore(autoModel.BasicUser.Id);
                if (store == null) {
                    throw new ValidException("您不是供应商,暂无店铺");
                }

                expressionQuery.And(e => e.StoreId == store.Id.ToString());
                // 供应商
                //expressionQuery.And(e => e.OrderExtension.IsSupplierView == true);
                expressionQuery.And(u => u.OrderStatus == OrderStatus.Remited);
            }
            //else if (autoModel.Filter == FilterType.User) {
            //    expressionQuery.And(e => e.UserId == autoModel.BasicUser.Id);
            //}
            else
            {
                //其他用户查看自己的订单
                expressionQuery.And(e => e.UserId == autoModel.BasicUser.Id);
            }

            var list = Resolve<IOrderApiService>().GetPageList(dic.ToJson(), expressionQuery);
            return ToPageResult<PlatformApiOrderList, ApiOrderListOutput>(list);
        }
    }
}