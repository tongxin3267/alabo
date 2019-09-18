using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.App.Share.HuDong.Domain.Entities;
using Alabo.App.Share.HuDong.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.UI;
using Alabo.UI.AutoLists;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.HuDong.UI {

    /// <summary>
    ///用户中奖记录
    /// </summary>
    [ClassProperty(Name = "互动中奖记录")]
    public class UserAwardAutoList : UIBase, IAutoList {

        public PageResult<AutoListItem> PageList(object query, AutoBaseModel autoModel) {
            var dic = query.ToObject<Dictionary<string, string>>();

            dic.TryGetValue("loginUserId", out string userId);
            dic.TryGetValue("pageIndex", out string pageIndexStr);
            var pageIndex = pageIndexStr.ToInt64();
            if (pageIndex <= 0) {
                pageIndex = 1;
            }
            var orderQuery = new ExpressionQuery<HudongRecord> {
                EnablePaging = true,
                PageIndex = (int)pageIndex,
                PageSize = (int)15
            };
            orderQuery.And(e => e.UserId == userId.ToInt64());
            var model = Resolve<IHudongRecordService>().GetPagedList(orderQuery);
            var list = new List<AutoListItem>();
            foreach (var item in model.ToList()) {
                var apiData = new AutoListItem {
                    Id = item.Id,
                    Image = GetAvator(item.UserId),
                    Title = item.Grade,
                    Intro = "中奖时间：" + item.CreateTime.ToString(),
                    Value = item.HuDongStatus.Value() == 1 ? "已兑奖" : "未兑奖" //item.CreateTime.ToUniversalTime()
                };
                list.Add(apiData);
            }
            return ToPageList(list, model);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetAvator(long userId) {
            var avator = Resolve<IApiService>().ApiImageUrl(Resolve<IApiService>().ApiUserAvator(userId));
            return avator;
        }

        public Type SearchType() {
            throw new NotImplementedException();
        }
    }
}