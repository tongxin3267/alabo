using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Asset.Coupons.Domain.Enums;
using Alabo.App.Asset.Coupons.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Framework.Core.WebApis;
using Alabo.UI;
using Alabo.UI.Design.AutoTables;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Alabo.App.Asset.Coupons.Domain.Entities
{
    /// <summary>
    ///     用户优惠券
    /// </summary>
    [Table("Asset_User")]
    [BsonIgnoreExtraElements]
    [ClassProperty(Name = "用户优惠券", PageType = ViewPageType.List)]
    public class UserCoupon : AggregateMongodbUserRoot<UserCoupon>, IAutoTable<UserCoupon>
    {
        /// <summary>
        ///     优惠券ID
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden)]
        public ObjectId CouponId { get; set; }

        /// <summary>
        ///     店铺Id，根据店铺来设置优惠券
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden)]
        [JsonConverter(typeof(ObjectIdConverter))] public ObjectId StoreId { get; set; }

        /// <summary>
        ///     优惠券名称
        /// </summary>
        [Display(Name = "优惠券名称")]
        [HelpBlock("必须填写,名称在10个字符以内")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            Width = "150", ListShow = true, EditShow = false, SortOrder = 2)]
        public string Name { get; set; }

        /// <summary>
        ///     消费满多少可用, 0不限制（使用条件）
        /// </summary>
        [Display(Name = "消费满多少可用")]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = false, SortOrder = 2)]
        public decimal MinOrderPrice { get; set; } = 0;

        /// <summary>
        ///     优惠方式
        /// </summary>
        [Display(Name = "优惠方式")]
        [HelpBlock("优惠方式分为：立减、打折")]
        [Field(ControlsType = ControlsType.RadioButton, IsShowBaseSerach = false, IsShowAdvancedSerach = false,
            Width = "150", ListShow = true, EditShow = true, SortOrder = 2)]
        public CouponType Type { get; set; }

        /// <summary>
        ///     优惠金额
        ///     包括立减多少元，和打折折扣（折扣1以内）
        /// </summary>
        [Display(Name = "优惠金额")]
        [HelpBlock("包括立减多少元，和打折折扣（折扣1以内）")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = false, IsShowAdvancedSerach = false,
            Width = "150", ListShow = true, EditShow = true, SortOrder = 2)]
        public decimal Value { get; set; }

        /// <summary>
        ///     开始有效期
        /// </summary>
        [Display(Name = "开始有效期")]
        [HelpBlock("优惠券可使用的开始有效期")]
        [Field(ControlsType = ControlsType.DateTimeRang, IsShowBaseSerach = false, IsShowAdvancedSerach = true,
            Width = "150", ListShow = true, EditShow = true, SortOrder = 2)]
        public DateTime StartValidityTime { get; set; }

        /// <summary>
        ///     结束有效期
        /// </summary>
        [Display(Name = "结束有效期")]
        [HelpBlock("优惠券可使用的到期时间")]
        [Field(ControlsType = ControlsType.DateTimeRang, IsShowBaseSerach = false, IsShowAdvancedSerach = true,
            Width = "150", ListShow = true, EditShow = true, SortOrder = 2)]
        public DateTime EndValidityTime { get; set; }

        /// <summary>
        ///     优惠券状态
        /// </summary>
        [Display(Name = "状态")]
        [HelpBlock("优惠券状态：正常、已使用、已过期")]
        [Field(ControlsType = ControlsType.DataSelect, DataSource = "Alabo.App.Shop.Coupons.Domain.Enums.CouponStatus",
            IsShowBaseSerach = false, IsShowAdvancedSerach = false,
            Width = "150", ListShow = true, SortOrder = 2)]
        public CouponStatus CouponStatus { get; set; }

        public List<TableAction> Actions()
        {
            return new List<TableAction>();
        }

        public PageResult<UserCoupon> PageTable(object query, AutoBaseModel autoModel)
        {
            var model = new PagedList<UserCoupon>();
            var couponList = Resolve<IUserCouponService>().GetPagedList(query);
            return null;
        }
    }
}