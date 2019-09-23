using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Alabo.App.Market.BookingSignup.Domain.Entities;
using Alabo.App.Market.BookingSignup.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Market.BookingSignup.UI {

    public class BookingSignAutoUserCount : UIBase, IAutoTable<BookingSignAutoUserCount> {

        /// <summary>
        ///     活动名称
        /// </summary>
        [Display(Name = "活动名称")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, SortOrder = 888)]
        public string Name { get; set; }

        /// <summary>
        ///    活动已报名用户数量
        /// </summary>
        [Display(Name = "活动已报名用户数量")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, SortOrder = 888)]
        public long Count { get; set; }

        /// <summary>
        ///     签到用户数量
        /// </summary>
        [Display(Name = "签到用户数量")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, SortOrder = 888)]
        public long SignUserCount { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<TableAction> Actions() {
            return new List<TableAction>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="query"></param>
        /// <param name="autoModel"></param>
        /// <returns></returns>
        public PageResult<BookingSignAutoUserCount> PageTable(object query, AutoBaseModel autoModel) {
            var dic = query.ToObject<Dictionary<string, string>>();
            long pageIndex = 1;
            dic.TryGetValue("pageIndex", out string pageIndexStr);
            if (!pageIndexStr.IsNullOrEmpty()) {
                pageIndex = pageIndexStr.ToInt64();
            }

            var signInput = new ExpressionQuery<Domain.Entities.BookingSignup> {
                EnablePaging = true,
                PageIndex = (int)pageIndex,
                PageSize = 15
            };

            var list = Resolve<IBookingSignupService>().GetPagedList(signInput);
            var result = new List<BookingSignAutoUserCount>();
            foreach (var tent in list) {
                var model = Resolve<IBookingSignupOrderService>().GetList(u => u.IsPay == true && u.BookingId == tent.Id).ToList();
                var users = new List<BookingSignupOrderUser>();
                foreach (var item in model) {
                    foreach (var temp in item.Contacts) {
                        var view = AutoMapping.SetValue<BookingSignupOrderUser>(temp);
                        users.Add(view);
                    }
                }
                var userCount = new BookingSignAutoUserCount {
                    Name = tent.Name,
                    Count = users.Count(),
                    SignUserCount = users.Where(u => u.IsSign == true).ToList().Count()
                };
                result.Add(userCount);
            }

            return ToPageResult(PagedList<BookingSignAutoUserCount>.Create(result, result.Count(), 15, (int)pageIndex));
        }
    }
}