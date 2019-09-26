using System;
using System.Collections.Generic;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoLists;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.UI.AutoForm {

    [ClassProperty(Name = "我的银行卡AutoList", Description = "我的银行卡")]
    public class BankCardAutoList : UIBase, IAutoList {

        public PageResult<AutoListItem> PageList(object query, AutoBaseModel autoModel) {
            var dic = query.ToObject<Dictionary<string, string>>();

            dic.TryGetValue("loginUserId", out string userId);
            dic.TryGetValue("pageIndex", out string pageIndexStr);
            var pageIndex = pageIndexStr.ToInt64();
            if (pageIndex <= 0) {
                pageIndex = 1;
            }
            var temp = new ExpressionQuery<BankCard> {
                EnablePaging = true,
                PageIndex = (int)pageIndex,
                PageSize = (int)15
            };
            temp.And(e => e.UserId == userId.ToInt64());
            var model = Resolve<IBankCardService>().GetPagedList(temp);
            var users = Resolve<IUserDetailService>().GetList();
            var list = new List<AutoListItem>();
            foreach (var item in model) {
                var apiData = new AutoListItem {
                    Title = $"{item.Name}",
                    Intro = item.Number,
                    Value = item.Type.GetDisplayName(),
                    Image = $@"http://s-test.qiniuniu99.com/wwwroot/uploads/bankcard/{item.Type.ToString()}.jpg",
                    Id = item.Id,
                    Url = $"/pages/index?path=Asset_BankCard_add&id={item.Id}"
                };
                list.Add(apiData);
            }
            return ToPageList(list, model);
        }

        public Type SearchType() {
            throw new NotImplementedException();
        }
    }
}