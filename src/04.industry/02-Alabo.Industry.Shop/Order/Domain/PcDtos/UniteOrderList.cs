﻿using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.App.Core.Employes.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.UI;
using Alabo.UI.AutoLists;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Order.Domain.PcDtos {

    /// <summary>
    /// 统一订单类, 统一[User, Store, Platform]订单列表 到此类
    /// </summary>
    [ClassProperty(Name = "订单列表", Description = "订单列表")]
    public class UniteOrderList : BaseApiOrderList, IAutoTable<UniteOrderList> {

        public PageResult<UniteOrderList> PageTable(object query, AutoBaseModel autoModel) {
            var model = ToQuery<UniteOrderList>();
            var list = new PagedList<ApiOrderListOutput>();
            var user = Resolve<IUserService>().GetSingle(model.UserId);
            if (user == null) {
                throw new ValidException("您无权查看其他人订单");
            }

            var expressionQuery = new ExpressionQuery<Entities.Order>();
            switch (model.Filter) {
                case FilterType.All:
                    throw new ValidException("无权限查看订单");
                case FilterType.User: {
                        if (model.UserId > 0) {
                            expressionQuery.And(e => e.UserId == user.Id);
                        }

                        list = Resolve<IOrderApiService>().GetPageList(query, expressionQuery);
                        break;
                    }
                case FilterType.Admin: {
                        var dic = HttpWeb.HttpContext.ToDictionary();
                        dic = dic.RemoveKey("userId");// 否则查出的订单都是同一个用户

                        if (model.OrderStatus > 0) {
                            expressionQuery.And(e => e.OrderStatus == model.OrderStatus);
                        }

                        var isAdmin = Resolve<IUserService>().IsAdmin(model.UserId);
                        if (!isAdmin) {
                            throw new ValidException("非管理员不能查看平台订单");
                        }
                        expressionQuery.And(e => e.StoreId > 0);
                        expressionQuery.And(e => e.UserId > 0);

                        model.UserId = 0;
                        list = Resolve<IOrderApiService>().GetPageList(dic.ToJson(), expressionQuery);
                        break;
                    }
                case FilterType.Store: {
                        var dic = HttpWeb.HttpContext.ToDictionary();
                        dic = dic.RemoveKey("userId");// 否则查出的订单都是同一个用户
                        if (model.OrderStatus > 0) {
                            expressionQuery.And(e => e.OrderStatus == model.OrderStatus);
                        }

                        var store = Resolve<IStoreService>().GetUserStore(model.UserId);
                        if (store == null) {
                            throw new ValidException("您无权查看其他店铺订单");
                        }
                        expressionQuery.And(e => e.StoreId == store.Id);

                        model.UserId = 0;
                        list = Resolve<IOrderApiService>().GetPageList(dic.ToJson(), expressionQuery);
                        break;
                    }
                case FilterType.Offline:
                    break;

                case FilterType.City:
                    break;

                default:
                    break;
            }

            return ToPageResult<UniteOrderList, ApiOrderListOutput>(list);
        }

        public List<TableAction> Actions() {
            var list = new List<TableAction>
            {
                ToLinkAction("订单", "/Order/Index"),
                ToLinkAction("导出", "/Order/Export"),
            };
            return list;
        }
    }
}