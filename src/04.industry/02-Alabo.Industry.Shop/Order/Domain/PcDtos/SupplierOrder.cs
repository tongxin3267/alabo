using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.App.Core.Employes.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.UI.AutoLists;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Order.Domain.PcDtos {

    public class SupplierOrder : BaseApiOrderList, IAutoTable<SupplierOrder>, IAutoList {

        public List<TableAction> Actions() {
            var list = new List<TableAction>
            {
                ToLinkAction("查看订单", "/User/order/Store/Edit",TableActionType.ColumnAction),//管理员后端查看订单
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
                //expressionQuery.And(e => e.OrderStatus != OrderStatus.WaitingBuyerPay);
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
                    Url = $"/pages/user?path=finance_recharge_view&id={item.Id}"
                };
                list.Add(apiData);
            }
            return ToPageList(list, pageList);
        }

        public PageResult<SupplierOrder> PageTable(object query, AutoBaseModel autoModel) {
            var dic = HttpWeb.HttpContext.ToDictionary();
            dic = dic.RemoveKey("userId");// 否则查出的订单都是同一个用户
            dic = dic.RemoveKey("filter");
            // var model = ToQuery<SupplierOrder>();
            var model = dic.ToJson().ToObject<SupplierOrder>();
            var expressionQuery = new ExpressionQuery<Entities.Order> {
                PageIndex = 1,
                EnablePaging = true
            };

            //供应商只能查看财务已打款后的订单以及该订单后相关的状态
            expressionQuery.And(e => e.OrderStatus == OrderStatus.Remited
            || e.OrderStatus == OrderStatus.WaitingReceiptProduct || e.OrderStatus == OrderStatus.WaitingEvaluated
                                     || e.OrderStatus == OrderStatus.Success);

            expressionQuery.And(e => e.StoreId > 0);
            expressionQuery.And(e => e.UserId > 0);

            if (System.Enum.IsDefined(typeof(Domain.Enums.OrderType), model.OrderType)) {
                expressionQuery.And(e => e.OrderType == model.OrderType);
            }

            var store = Resolve<IShopStoreService>().GetSingle(u => u.UserId == autoModel.BasicUser.Id);
            if (store == null) {
                throw new ValidException("您不是供应商,暂无店铺");
            }
            expressionQuery.And(e => e.StoreId == store.Id);

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

        public Type SearchType() {
            throw new NotImplementedException();
        }
    }
}