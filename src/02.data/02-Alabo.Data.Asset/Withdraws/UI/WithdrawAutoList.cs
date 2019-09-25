using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.UI;
using Alabo.UI.AutoLists;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.UI.AutoForm {

    [ClassProperty(Name = "提现记录AutoList", Description = "提现记录")]
    public class WithdrawAutoList : UIBase, IAutoList {

        public PageResult<AutoListItem> PageList(object query, AutoBaseModel autoModel) {
            //var dic = query.ToObject<Dictionary<string, string>>();

            //dic.TryGetValue("loginUserId", out string userId);
            //dic.TryGetValue("pageIndex", out string pageIndexStr);
            //var pageIndex = pageIndexStr.ToInt64();
            //if (pageIndex <= 0) {
            //    pageIndex = 1;
            //}
            //var temp = new ExpressionQuery<Trade> {
            //    EnablePaging = true,
            //    PageIndex = (int)pageIndex,
            //    PageSize = (int)15
            //};
            //temp.And(e => e.UserId == userId.ToInt64());
            //temp.And(u => u.Type == TradeType.Withdraw);

            //temp.OrderByDescending(s => s.CreateTime);

            //var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            //var model = Resolve<ITradeService>().GetPagedList(temp);
            //var users = Resolve<IUserDetailService>().GetList();
            //var list = new List<AutoListItem>();
            //foreach (var item in model) {
            //    var apiData = new AutoListItem {
            //        Title = moneyTypes.FirstOrDefault(u => u.Id == item.MoneyTypeId)?.Name + " - " + item.Type.GetDisplayName(),
            //        Intro = $"{item.CreateTime}",
            //        Value = item.Amount,
            //        Image = users.FirstOrDefault(u => u.UserId == item.UserId)?.Avator,
            //        Id = item.Id,
            //        Url = $"/pages/index?path=finance_withdraw_view&id={item.Id}"
            //    };
            //    list.Add(apiData);
            //}
            //return ToPageList(list, model);
            return null;
        }

        public Type SearchType() {
            throw new NotImplementedException();
        }
    }
}