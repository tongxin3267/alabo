using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoLists;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.UI.AutoForm {

    [ClassProperty(Name = "账单AutoList", Description = "账单")]
    public class BillAutoList : UIBase, IAutoList {

        public PageResult<AutoListItem> PageList(object query, AutoBaseModel autoModel) {
            var dic = query.ToObject<Dictionary<string, string>>();

            dic.TryGetValue("loginUserId", out string userId);
            dic.TryGetValue("pageIndex", out string pageIndexStr);
            var pageIndex = pageIndexStr.ToInt64();
            if (pageIndex <= 0) {
                pageIndex = 1;
            }
            var temp = new ExpressionQuery<Bill> {
                EnablePaging = true,
                PageIndex = (int)pageIndex,
                PageSize = (int)15
            };
            temp.And(e => e.UserId == userId.ToInt64());
            var model = Resolve<IBillService>().GetListByPageDesc(15, (int)pageIndex, u => u.UserId == userId.ToInt64());
            var page = Resolve<IBillService>().GetPagedList(temp);
            page.Result = model.ToList();
            var users = Resolve<IUserDetailService>().GetList();
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var list = new List<AutoListItem>();
            var apiStore = Resolve<IApiService>();
            foreach (var item in model.ToList()) {
                var apiData = new AutoListItem {
                    Title = moneyTypes.FirstOrDefault(u => u.Id == item.MoneyTypeId)?.Name + "账户 - " + item.Flow.GetDisplayName(),
                    Intro = $"账后{item.AfterAmount}时间{item.CreateTime.ToString()}",
                    Value = item.Amount,
                    Image = apiStore.ApiUserAvator(item.UserId),
                    Id = item.Id,
                    Url = $"/pages/index?path=Asset_bill_view&id={item.Id}"
                };
                list.Add(apiData);
            }
            return ToPageList(list, page);
        }

        public Type SearchType() {
            return typeof(Bill);
        }
    }
}