using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Core.Employes.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
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
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Order.Domain.PcDtos {

    /// <summary>
    /// 采购订单
    /// </summary>
    [ClassProperty(Name = "平台订单列表", Description = "平台订单列表")]
    public class PurchaseApiOrderList : BaseApiOrderList, IAutoTable<PurchaseApiOrderList> {

        /// <summary>
        /// 操作方式
        /// </summary>
        /// <returns></returns>
        public List<TableAction> Actions() {
            var list = new List<TableAction>
            {
                ToLinkAction("查看订单", "/User/Order/Purchase/Edit",TableActionType.ColumnAction),//采购订单查看
            };
            return list;
        }

        public PageResult<PurchaseApiOrderList> PageTable(object query, AutoBaseModel autoModel) {
            var dic = HttpWeb.HttpContext.ToDictionary();
            dic = dic.RemoveKey("userId");// 否则查出的订单都是同一个用户

            var model = ToQuery<PlatformApiOrderList>();

            var expressionQuery = new ExpressionQuery<Entities.Order>();
            if (model.OrderStatus > 0) {
                expressionQuery.And(e => e.OrderStatus == model.OrderStatus);
            }

            expressionQuery.And(e => e.StoreId > 0);
            expressionQuery.And(e => e.UserId > 0);

            if (Enum.IsDefined(typeof(Enums.OrderType), model.OrderType)) {
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
                expressionQuery.And(e => e.OrderExtension.IsSupplierView);
            } else if (autoModel.Filter == FilterType.User || autoModel.Filter == FilterType.All) {
                //var store = Resolve<IStoreService>().GetUserStore(autoModel.UserId);
                //if (store == null)
                //{
                //    throw new ValidException("您不是供应商,暂无店铺");
                //}
                expressionQuery.And(e => e.UserId == autoModel.BasicUser.Id);
            } else {
                throw new ValidException("方式不对");
            }
            var list = Resolve<IOrderApiService>().GetPageList(dic.ToJson(), expressionQuery);
            return ToPageResult<PurchaseApiOrderList, ApiOrderListOutput>(list);
        }
    }
}