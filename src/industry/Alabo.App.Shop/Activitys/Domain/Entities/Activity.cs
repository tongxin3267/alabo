using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Shop.Activitys.Domain.Entities.Extension;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Activitys.Domain.Entities
{

    /// <summary>
    ///     活动
    ///     活动类型：
    ///     活动条件：
    /// </summary>
    [ClassProperty(Name = "活动")]
    public class Activity : AggregateDefaultRoot<Activity>
    {

        /// <summary>
        ///     活动对应的店铺Id
        /// </summary>
        [Display(Name = "活动对应的店铺Id")]
        public long StoreId { get; set; }

        /// <summary>
        ///     活动名称:比如活动券，满就送，一元夺宝等等
        /// </summary>
        [Display(Name = "活动名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(IsShowBaseSerach = true, SortOrder = 1, ControlsType = ControlsType.TextBox, IsMain = true,
            EditShow = true, ListShow = true, Link = "/Admin/Activitys/Edit?Key=[[Key]]&Id=[[Id]]")]
        public string Name { get; set; } = "活动名称";

        /// <summary>
        ///     所属营销活动类型，如：Alabo.App.Shop.Activitys.Domain.Modules.PinTuanActivity
        ///     比如满就送，或者限量购
        /// </summary>
        [Display(Name = "所属营销活动类型")]
        public string Key { get; set; }

        /// <summary>
        ///     具体活动内容，活动类型的Json数据
        /// </summary>
        [Display(Name = "具体活动内容")]
        public string Value { get; set; }

        /// <summary>
        ///     根据开始时间自动开始
        ///     根据结束时间自动结束
        /// </summary>
        [Display(Name = "活动状态")]
        [Field(DataSource = "ActivityStatus", ControlsType = ControlsType.DropdownList, IsTabSearch = true,
            SortOrder = 20, EditShow = false, ListShow = true)]
        public ActivityStatus Status { get; set; }

        /// <summary>
        ///     活动是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        [Field(ControlsType = ControlsType.Switch, IsShowAdvancedSerach = true, SortOrder = 20, ListShow = true)]
        public bool IsEnable { get; set; } = true;

        /// <summary>
        ///     活动条件：如果用户范围为根据用户等级的时候，请设置该会员的等级Id
        /// </summary>
        [Display(Name = "用户等级")]
        public Guid LimitGradeId { get; set; }

        /// <summary>
        ///     活动允许的最大库存使用量，如果为0，则不限制
        /// </summary>
        [Display(Name = "最大库存")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowAdvancedSerach = true, SortOrder = 15, ListShow = true)]
        public long MaxStock { get; set; }

        /// <summary>
        ///     活动已经使用的库存量，随用户购买逐步增长，当与AllowStock相等时，活动失效
        /// </summary>
        [Display(Name = "已用库存")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowAdvancedSerach = true, SortOrder = 15, ListShow = true)]
        public long UsedStock { get; set; }

        /// <summary>
        ///     扩展属性
        /// </summary>
        [Field(ExtensionJson = "Alabo.App.Shop.Activitys.Domain.Entities.Extension.ActivityExtension")]
        [Display(Name = "扩展属性")]
        public string Extension { get; set; }

        /// <summary>
        ///     活动条件：活动开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        [Field(ControlsType = ControlsType.DateTimeRang, IsShowAdvancedSerach = true, SortOrder = 21, ListShow = true)]
        public DateTime StartTime { get; set; }

        /// <summary>
        ///     活动条件：活动结束时间
        /// </summary>
        [Field(ControlsType = ControlsType.DateTimeRang, IsShowAdvancedSerach = true, SortOrder = 22, ListShow = true)]
        [Display(Name = "结束时间")]
        public DateTime EndTime { get; set; }

        /// <summary>
        ///  活动商品
        /// </summary>
        [Display(Name = "活动商品")]
        public long ProductId { get; set; }

        ///// <summary>
        /////  活动商品列表
        ///// </summary>
        //[Display(Name = "活动商品列表")]
        //public IList<long> ProductIdList { get; set; } = new List<long>();

        /// <summary>
        ///     活动扩展属性
        /// </summary>
        [Display(Name = "活动扩展属性")]

        public ActivityExtension ActivityExtension { get; set; } = new ActivityExtension();

        public class ActivityTableMap : MsSqlAggregateRootMap<Activity>
        {

            protected override void MapTable(EntityTypeBuilder<Activity> builder)
            {
                builder.ToTable("ZKShop_Activity");
            }

            protected override void MapProperties(EntityTypeBuilder<Activity> builder)
            {
                //应用程序编号
                builder.HasKey(e => e.Id);
                builder.Ignore(e => e.ActivityExtension);
                builder.Ignore(e => e.Version);
                //builder.Ignore(e => e.ProductIdList);
                if (TenantContext.IsTenant)
                {
                    // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
                }
            }
        }
    }
}