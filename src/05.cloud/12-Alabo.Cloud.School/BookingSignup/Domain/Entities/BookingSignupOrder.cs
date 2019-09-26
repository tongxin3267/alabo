using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Alabo.Core.WebUis;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Market.BookingSignup.Domain.Entities {

    [BsonIgnoreExtraElements]
    [Table("BookingSignup_BookingSignupOrder")]
    [ClassProperty(Name = "预约订单", Description = "预约订单", Icon = IconFlaticon.route, SideBarType = SideBarType.SchoolSideBar)]
    public class BookingSignupOrder : AggregateMongodbUserRoot<BookingSignupOrder> {
        /// <summary>
        /// 课程预约订单
        /// </summary>

        public ObjectId BookingId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 单价格
        /// </summary>
        [Display(Name = "单价格")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, EditShow = true,
            IsShowBaseSerach = true, IsShowAdvancedSerach = true, SortOrder = 4)]
        public decimal Price { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        [Display(Name = "人数")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, EditShow = true,
            IsShowBaseSerach = true, IsShowAdvancedSerach = true, SortOrder = 4)]
        public long Count { get; set; }

        /// <summary>
        /// 总价格
        /// </summary>
        [Display(Name = "总价格")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, EditShow = true,
            IsShowBaseSerach = true, IsShowAdvancedSerach = true, SortOrder = 4)]
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// 推荐人姓名
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public List<BookingSignupOrderContact> Contacts { get; set; }

        /// <summary>
        /// 是否支付
        /// </summary>
        [Display(Name = "是否支付")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, EditShow = true,
            IsShowBaseSerach = true, IsShowAdvancedSerach = true, SortOrder = 4)]
        public bool IsPay { get; set; } = false;
    }

    public class BookingSignupOrderContact {

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 是否签到
        /// </summary>
        public bool IsSign { get; set; } = false;
    }
}