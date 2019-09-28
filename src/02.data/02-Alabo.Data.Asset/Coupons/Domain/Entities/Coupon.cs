using Alabo.App.Asset.Coupons.Domain.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.App.Asset.Coupons.Domain.Entities
{
    /// <summary>
    ///     优惠券或打折券
    /// </summary>
    [Table("Asset_Coupon")]
    [BsonIgnoreExtraElements]
    [ClassProperty(Name = "优惠券", PageType = ViewPageType.List)]
    public class Coupon : AggregateMongodbRoot<Coupon>
    {
        /// <summary>
        ///     优惠券名称
        /// </summary>
        [Display(Name = "优惠券名称")]
        [HelpBlock("必须填写,名称在10个字符以内")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            Width = "150", ListShow = true, EditShow = true, SortOrder = 2, Link = "/Admin/coupon/Edit?Id=[[Id]]")]
        public string Name { get; set; }

        /// <summary>
        ///     店铺Id，根据店铺来设置优惠券
        /// </summary>
        public ObjectId StoreId { get; set; }

        /// <summary>
        ///     消费满多少可用, 0不限制（使用条件）
        /// </summary>
        [Display(Name = "消费满多少可用")]
        [HelpBlock("消费满多少可用, 0表示不限制")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true, SortOrder = 2)]
        public decimal MinOrderPrice { get; set; } = 0;

        /// <summary>
        ///     发放总数
        /// </summary>
        [Display(Name = "发放总数")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true, SortOrder = 2)]
        public long TotalCount { get; set; }

        /// <summary>
        ///     已发放总数
        /// </summary>
        [Display(Name = "已发放总数")]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true, SortOrder = 2)]
        public long UsedCount { get; set; } = 0;

        /// <summary>
        ///     优惠方式
        /// </summary>
        [Display(Name = "优惠方式")]
        public CouponType Type { get; set; }

        /// <summary>
        ///     优惠金额
        ///     包括立减多少元，和打折折扣（折扣1以内）
        /// </summary>
        [Display(Name = "优惠金额")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public decimal Value { get; set; }

        /// <summary>
        ///     使用时间限制
        ///     包括：领券后多少天，和指定时间类有效
        /// </summary>
        [Display(Name = "使用时间限制")]
        public CouponTimeLimit TimeLimit { get; set; }

        /// <summary>
        ///     领券后多少天内
        ///     TimeLimit=Days是有效
        /// </summary>
        [Display(Name = "领券后多少天内")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 8)]
        public long AfterDays { get; set; }

        /// <summary>
        ///     有效期内（开始时间）
        ///     TimeLimit=PeriodOfValidity是有效
        /// </summary>
        [Display(Name = "开始时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, ListShow = true, EditShow = true, SortOrder = 9)]
        public DateTime StartPeriodOfValidity { get; set; }

        /// <summary>
        ///     有效期内（结束时间）
        ///     TimeLimit=PeriodOfValidity是有效
        /// </summary>
        [Display(Name = "到期时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, ListShow = true, EditShow = true, SortOrder = 10)]
        public DateTime EndPeriodOfValidity { get; set; }
    }

    /// <summary>
    ///     优惠券下拉列表
    /// </summary>
    public class CouponDrList
    {
        /// <summary>
        ///     优惠券Id
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        ///     优惠券名称
        /// </summary>
        public string Name { get; set; }
    }
}