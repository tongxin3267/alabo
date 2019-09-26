using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.Core.WebApis;
using Alabo.Core.WebUis.Design.AutoTables;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.UI;

namespace Alabo.App.Shop.Order.Domain.PcDtos {

    /// <summary>
    /// 会员订单
    /// 登录会员查看当前登录订单
    /// </summary>
    public class UserApiOrderList : BaseApiOrderList, IAutoTable<UserApiOrderList> {

        public List<TableAction> Actions() {
            var list = new List<TableAction>
            {
                ToLinkAction("会员订单", "/Order/Index")
            };
            return list;
        }

        public PageResult<UserApiOrderList> PageTable(object query, AutoBaseModel autoModel) {
            var model = ToQuery<PlatformApiOrderList>();
            var user = Resolve<IUserService>().GetSingle(model.UserId);
            if (user == null) {
                throw new ValidException("您无权查看其他人订单");
            }
            var expressionQuery = new ExpressionQuery<Entities.Order>();
            if (model.UserId > 0) {
                expressionQuery.And(e => e.UserId == user.Id);
            }
            var list = Resolve<IOrderApiService>().GetPageList(query, expressionQuery);
            return ToPageResult<UserApiOrderList, ApiOrderListOutput>(list);
        }
    }
}