using Alabo.Cloud.School.BookingSignup.Domain.Entities;
using Alabo.Cloud.School.BookingSignup.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.UI.Design.AutoTables;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Cloud.School.BookingSignup.UI
{
    public class BookingSignupOrderUser : UIBase, IAutoTable<BookingSignupOrderUser>
    {
        /// <summary>
        ///     课程预约订单
        /// </summary>
        public ObjectId BookingId { get; set; }

        /// <summary>
        ///     活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     单价格
        /// </summary>
        [Display(Name = "单价格")]
        public decimal Price { get; set; }

        /// <summary>
        ///     人数
        /// </summary>
        [Display(Name = "人数")]
        public long Count { get; set; }

        /// <summary>
        ///     总价格
        /// </summary>
        [Display(Name = "总价格")]
        public decimal TotalPrice { get; set; }

        /// <summary>
        ///     推荐人姓名
        /// </summary>
        [Display(Name = "推荐人姓名")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, Width = "120", IsShowAdvancedSerach = true,
            ListShow = true, SortOrder = 3)]
        public string ParentName { get; set; }

        /// <summary>
        ///     手机号
        /// </summary>
        [Display(Name = "手机号")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, Width = "120", IsShowAdvancedSerach = true,
            ListShow = true, SortOrder = 3)]
        public string Mobile { get; set; }

        /// <summary>
        ///     姓名
        /// </summary>
        [Display(Name = "姓名")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, Width = "120", IsShowAdvancedSerach = true,
            ListShow = true, SortOrder = 1)]
        public string ContactName { get; set; }

        /// <summary>
        ///     签到
        /// </summary>
        [Display(Name = "是否签到")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, Width = "120", IsShowAdvancedSerach = true,
            ListShow = true, SortOrder = 888)]
        public bool IsSign { get; set; }

        /// <summary>
        ///     预约时间
        /// </summary>
        [Display(Name = "预约时间")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, Width = "120", IsShowAdvancedSerach = true,
            ListShow = true, SortOrder = 999)]
        public DateTime CreateTime { get; set; }

        public PageResult<BookingSignupOrderUser> PageTable(object query, AutoBaseModel autoModel)
        {
            var dic = query.ToObject<Dictionary<string, string>>();
            long pageIndex = 1;
            dic.TryGetValue("pageIndex", out var pageIndexStr);
            if (!pageIndexStr.IsNullOrEmpty()) pageIndex = pageIndexStr.ToInt64();

            var input = new ExpressionQuery<BookingSignupOrder>
            {
                EnablePaging = true,
                PageIndex = (int)pageIndex,
                PageSize = 15
            };
            input.And(u => u.IsPay);
            input.And(u => u.ParentName != "测试");

            //  var model = Resolve<IBookingSignupOrderService>().GetList(u => u.IsPay == true && u.ParentName != "测试").ToList();
            var model = Resolve<IBookingSignupOrderService>().GetPagedList(input);

            var list = new List<BookingSignupOrderUser>();
            model.ForEach(u =>
            {
                u.Contacts.ForEach(z =>
                {
                    var view = AutoMapping.SetValue<BookingSignupOrderUser>(u);
                    view.Mobile = z.Mobile;
                    view.ContactName = z.Name;
                    view.IsSign = z.IsSign;
                    list.Add(view);
                });
            });

            return ToPageResult(PagedList<BookingSignupOrderUser>.Create(list, model.RecordCount, 15, pageIndex));
        }

        public List<TableAction> Actions()
        {
            return new List<TableAction>();
        }
    }
}