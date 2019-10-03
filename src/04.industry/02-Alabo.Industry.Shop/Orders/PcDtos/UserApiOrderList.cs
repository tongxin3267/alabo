using System.Collections.Generic;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Exceptions;
using Alabo.Framework.Core.WebApis;
using Alabo.Industry.Shop.Orders.Domain.Entities;
using Alabo.Industry.Shop.Orders.Domain.Services;
using Alabo.UI;
using Alabo.UI.Design.AutoTables;

namespace Alabo.Industry.Shop.Orders.PcDtos
{
    /// <summary>
    ///     会员订单
    ///     登录会员查看当前登录订单
    /// </summary>
    public class UserApiOrderList : BaseApiOrderList, IAutoTable<UserApiOrderList>
    {
        public List<TableAction> Actions()
        {
            var list = new List<TableAction>
            {
                ToLinkAction("会员订单", "/Order/Index")
            };
            return list;
        }

        public PageResult<UserApiOrderList> PageTable(object query, AutoBaseModel autoModel)
        {
            var model = ToQuery<PlatformApiOrderList>();
            var user = Resolve<IUserService>().GetSingle(model.UserId);
            if (user == null) {
                throw new ValidException("您无权查看其他人订单");
            }

            var expressionQuery = new ExpressionQuery<Order>();
            if (model.UserId > 0) {
                expressionQuery.And(e => e.UserId == user.Id);
            }

            var list = Resolve<IOrderApiService>().GetPageList(query, expressionQuery);
            return ToPageResult<UserApiOrderList, ApiOrderListOutput>(list);
        }
    }
}