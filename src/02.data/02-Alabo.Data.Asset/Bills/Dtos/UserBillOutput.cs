﻿using System;
using System.Collections.Generic;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.Core.WebApis;
using Alabo.Core.WebApis.Service;
using Alabo.Core.WebUis;
using Alabo.Core.WebUis.Design.AutoLists;
using Alabo.Domains.Entities;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.ViewModels.Bill {

    /// <summary>
    /// 用户财务详情
    /// 查看当前登录会员自己的财务报表
    /// </summary>
    [ClassProperty(Name = "用户财务详情", Description = "用户财务详情")]
    public class UserBillOutput : UIBase, IAutoList {

        public PageResult<AutoListItem> PageList(object query, AutoBaseModel autoModel) {
            var model = Resolve<IBillService>().GetPagedList(query);
            var list = new List<AutoListItem>();
            foreach (var item in model) {
                var apiData = new AutoListItem {
                    Title = $"充值金额{item.Amount}元",
                    Intro = item.Intro,
                    Value = item.Amount,
                    Image = Resolve<IApiService>().ApiUserAvator(item.UserId),
                    Id = item.Id,
                    Url = $"/pages/user?path=Asset_recharge_view&id={item.Id}"
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