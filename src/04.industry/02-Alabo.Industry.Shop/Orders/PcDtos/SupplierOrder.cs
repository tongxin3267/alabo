﻿using System;
using System.Collections.Generic;
using Alabo.Data.People.Stores.Domain.Services;
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
using OrderType = Alabo.Industry.Shop.Orders.Domain.Enums.OrderType;

namespace Alabo.Industry.Shop.Orders.PcDtos
{
    public class SupplierOrder : BaseApiOrderList, IAutoTable<SupplierOrder>, IAutoList
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
            //expressionQuery.And(e => e.OrderStatus != OrderStatus.WaitingBuyerPay);

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
                ToLinkAction("查看订单", "/User/order/Store/Edit", TableActionType.ColumnAction) //管理员后端查看订单
                //ToLinkAction("查看订单", "/Admin/Purchase/Edit",TableActionType.ColumnAction),//采购订单查看
            };
            return list;
        }

        public PageResult<SupplierOrder> PageTable(object query, AutoBaseModel autoModel)
        {
            var dic = HttpWeb.HttpContext.ToDictionary();
            dic = dic.RemoveKey("userId"); // 否则查出的订单都是同一个用户
            dic = dic.RemoveKey("filter");
            // var model = ToQuery<SupplierOrder>();
            var model = dic.ToJson().ToObject<SupplierOrder>();
            var expressionQuery = new ExpressionQuery<Order>
            {
                PageIndex = 1,
                EnablePaging = true
            };

            //供应商只能查看财务已打款后的订单以及该订单后相关的状态
            expressionQuery.And(e => e.OrderStatus == OrderStatus.Remited
                                     || e.OrderStatus == OrderStatus.WaitingReceiptProduct ||
                                     e.OrderStatus == OrderStatus.WaitingEvaluated
                                     || e.OrderStatus == OrderStatus.Success);

            // // expressionQuery.And(e => e.StoreId > 0);
            expressionQuery.And(e => e.UserId > 0);

            if (Enum.IsDefined(typeof(OrderType), model.OrderType)) {
                expressionQuery.And(e => e.OrderType == model.OrderType);
            }

            var store = Resolve<IStoreService>().GetSingle(u => u.UserId == autoModel.BasicUser.Id);
            if (store == null) {
                throw new ValidException("您不是供应商,暂无店铺");
            }

            expressionQuery.And(e => e.StoreId == store.Id.ToString());

            //if (autoModel.Filter == FilterType.Admin || autoModel.Filter == FilterType.All) {
            //var isAdmin = Resolve<IUserService>().IsAdmin(autoModel.BasicUser.Id);
            //if (!isAdmin) {
            //    throw new ValidException("非管理员不能查看平台订单");
            //}
            //} else if (autoModel.Filter == FilterType.Store) {
            //    var store = Resolve<IStoreService>().GetUserStore(autoModel.BasicUser.Id);
            //    if (store == null) {
            //        throw new ValidException("您不是供应商,暂无店铺");
            //    }
            //    expressionQuery.And(e => e.StoreId == store.Id);
            //    // 供应商
            //    //expressionQuery.And(e => e.OrderExtension.IsSupplierView == true);
            //    expressionQuery.And(u => u.OrderStatus == OrderStatus.Remited || u.OrderStatus == OrderStatus.Success
            //   || u.OrderStatus == OrderStatus.WaitingReceiptProduct || u.OrderStatus == OrderStatus.WaitingEvaluated);
            //} else if (autoModel.Filter == FilterType.User) {
            //    var store = Resolve<IStoreService>().GetUserStore(autoModel.BasicUser.Id);
            //    if (store == null) {
            //        throw new ValidException("您不是供应商,暂无店铺");
            //    }
            //    expressionQuery.And(e => e.UserId == autoModel.BasicUser.Id);
            //} else {
            //    throw new ValidException("方式不对");
            //}
            var list = Resolve<IOrderApiService>().GetPageList(dic.ToJson(), expressionQuery);
            return ToPageResult<SupplierOrder, ApiOrderListOutput>(list);
        }
    }
}