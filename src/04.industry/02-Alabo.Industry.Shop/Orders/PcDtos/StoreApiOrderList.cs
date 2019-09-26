﻿using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis.Design.AutoTables;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.UI;

namespace Alabo.App.Shop.Order.Domain.PcDtos {

    /// <summary>
    /// 供应商订单，店铺订单
    /// 只有当前供应商才可以查看
    /// </summary>
    public class StoreApiOrderList : BaseApiOrderList, IAutoTable<StoreApiOrderList> {

        public List<TableAction> Actions() {
            var list = new List<TableAction>
            {
                ToLinkAction("店铺订单", "/Order/Index")
            };
            return list;
        }

        public PageResult<StoreApiOrderList> PageTable(object query, AutoBaseModel autoModel) {
            var dic = HttpWeb.HttpContext.ToDictionary();
            dic = dic.RemoveKey("userId");// 否则查出的订单都是同一个用户
            var model = ToQuery<PlatformApiOrderList>();
            var expressionQuery = new ExpressionQuery<Entities.Order>();
            if (model.OrderStatus > 0) {
                expressionQuery.And(e => e.OrderStatus == model.OrderStatus);
            }

            var store = Resolve<IShopStoreService>().GetUserStore(model.UserId);
            if (store == null) {
                throw new ValidException("您无权查看其他店铺订单");
            }
            expressionQuery.And(e => e.StoreId == store.Id);

            model.UserId = 0;
            var list = Resolve<IOrderApiService>().GetPageList(dic.ToJson(), expressionQuery);
            return ToPageResult<StoreApiOrderList, ApiOrderListOutput>(list);
        }
    }
}