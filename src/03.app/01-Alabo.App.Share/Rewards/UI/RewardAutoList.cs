using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Share.Share.Domain.Entities;
using Alabo.App.Share.Share.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoLists;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.Share.UI.AutoForm {

    [ClassProperty(Name = "分润记录", Description = "分润记录")]
    public class RewardAutoList : UIBase, IAutoList {

        public PageResult<AutoListItem> PageList(object query, AutoBaseModel autoModel) {
            var dic = query.ToObject<Dictionary<string, string>>();

            dic.TryGetValue("loginUserId", out string userId);
            dic.TryGetValue("pageIndex", out string pageIndexStr);
            var pageIndex = pageIndexStr.ToInt64();
            if (pageIndex <= 0) {
                pageIndex = 1;
            }
            var temp = new ExpressionQuery<Reward> {
                EnablePaging = true,
                PageIndex = (int)pageIndex,
                PageSize = (int)15
            };
            temp.And(e => e.UserId == userId.ToInt64());
            //temp.And(u => u.Type == TradeType.Withraw);
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var model = Resolve<IRewardService>().GetPagedList(temp);
            var users = Resolve<IUserDetailService>().GetList();
            var list = new List<AutoListItem>();
            foreach (var item in model) {
                var apiData = new AutoListItem {
                    Title = moneyTypes.FirstOrDefault(u => u.Id == item.MoneyTypeId)?.Name,// + " - " + item.Type.GetDisplayName(),
                    Intro = item.Intro,//$"{item.CreateTime}",
                    Value = item.Amount,
                    Image = users.FirstOrDefault(u => u.UserId == item.UserId)?.Avator,
                    Id = item.Id,
                    Url = $"/pages/index?path=share_show&id={item.Id}"
                };
                list.Add(apiData);
            }
            return ToPageList(list, model);
        }

        public Type SearchType() {
            return typeof(Reward);
        }
    }
}