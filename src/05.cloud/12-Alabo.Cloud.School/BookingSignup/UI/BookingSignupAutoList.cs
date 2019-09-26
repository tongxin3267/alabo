using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Cloud.School.BookingSignup.Domain.Entities;
using Alabo.Cloud.School.BookingSignup.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoLists;

namespace Alabo.Cloud.School.BookingSignup.UI {

    public class BookingSignupAutoList : UIBase, IAutoList {

        public PageResult<AutoListItem> PageList(object query, AutoBaseModel autoModel) {
            var dic = query.ToObject<Dictionary<string, string>>();

            dic.TryGetValue("loginUserId", out string userId);
            dic.TryGetValue("pageIndex", out string pageIndexStr);
            var pageIndex = pageIndexStr.ToInt64();
            if (pageIndex <= 0) {
                pageIndex = 1;
            }
            var orderQuery = new ExpressionQuery<BookingSignupOrder> {
                EnablePaging = true,
                PageIndex = (int)pageIndex,
                PageSize = (int)15
            };
            orderQuery.And(e => e.UserId == userId.ToInt64());
            orderQuery.And(e => e.IsPay == true);
            var model = Resolve<IBookingSignupOrderService>().GetPagedList(orderQuery);
            var list = new List<AutoListItem>();
            foreach (var item in model.ToList()) {
                foreach (var temp in item.Contacts) {
                    var apiData = new AutoListItem {
                        Title = temp.Name,
                        Intro = temp.Mobile,
                        Image = "https://diyservice.5ug.com/wwwroot/uploads/api/2019-03-20/5c924388397d411c8c07de3e.png"
                    };
                    list.Add(apiData);
                }
            }
            return ToPageList(list, model);
        }

        public Type SearchType() {
            throw new NotImplementedException();
        }
    }
}