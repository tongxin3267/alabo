using System;
using Alabo.Domains.Entities;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoLists;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Asset.Transfers.UI {

    [ClassProperty(Name = "转账记录AutoList", Description = "转账记录")]
    public class TransferAutoList : UIBase, IAutoList {

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
            //temp.And(u => u.Type == TradeType.Transfer);
            //var model = Resolve<ITradeService>().GetPagedList(temp);
            //var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            //var users = Resolve<IUserDetailService>().GetList();
            //var list = new List<AutoListItem>();
            //foreach (var item in model) {
            //    var apiData = new AutoListItem {
            //        Title = moneyTypes.FirstOrDefault(u => u.Id == item.MoneyTypeId)?.Name + "账户 - " + item.Type.GetDisplayName(),
            //        Intro = item.CreateTime.ToString(),
            //        Value = item.Amount,
            //        Image = users.FirstOrDefault(u => u.UserId == item.UserId)?.Avator,
            //        Id = item.Id,
            //        Url = $"/pages/index?path=Asset_transfer_view&id={item.Id}"
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