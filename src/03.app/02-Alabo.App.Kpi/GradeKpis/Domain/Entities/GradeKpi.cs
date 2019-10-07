using Alabo.App.Kpis.GradeKpis.Domain.Enum;
using Alabo.App.Kpis.Kpis.Domain.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.App.Kpis.GradeKpis.Domain.Entities
{
    /// <summary>
    ///     等级考核
    /// </summary>
    [ClassProperty(Name = "等级考核")]
    [BsonIgnoreExtraElements]
    [Table("Kpi_GradeKpi")]
    public class GradeKpi : AggregateMongodbUserRoot<GradeKpi>
    {
        /// <summary>
        ///     编号
        /// </summary>
        [Display(Name = "编号")]
        [Field(ListShow = true, IsShowBaseSerach = true, ControlsType = ControlsType.TextBox, SortOrder = 1)]
        public string Serial
        {
            get
            {
                var searSerial = Id.ToString();
                return searSerial;
            }
        }

        /// <summary>
        ///     用户类型
        /// </summary>
        [Display(Name = "用户类型")]
        public Guid UserTypeId { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Display(Name = "用户名")]
        [Field(ListShow = true, IsShowBaseSerach = true, ControlsType = ControlsType.TextBox, SortOrder = 2)]
        public new string UserName { get; set; }

        /// <summary>
        ///     用户类型Id
        /// </summary>
        [Display(Name = "用户类型Id")]
        public long TypeId { get; set; }

        /// <summary>
        ///     配置名称
        /// </summary>
        public string ConfigName { get; set; }

        /// <summary>
        ///     当前用户等级Id
        /// </summary>
        [Display(Name = "当前用户等级")]
        [Field(ListShow = true, DisplayMode = DisplayMode.Grade)]
        public Guid GradeId { get; set; }

        /// <summary>
        ///     考核后等级
        /// </summary>
        [Display(Name = "考核后等级")]
        [Field(ListShow = true, DisplayMode = DisplayMode.Grade)]
        public Guid ChangeGradeId { get; set; }

        /// <summary>
        ///     考核周期
        /// </summary>
        [Display(Name = "考核周期")]
        [Field(ControlsType = ControlsType.RadioButton, ListShow = true, EditShow = true,
            SortOrder = 1002, Width = "110", IsTabSearch = true, IsShowBaseSerach = false, IsShowAdvancedSerach = false,
            DataSource = "Alabo.Domains.Enums.TimeType")]
        public TimeType TimeType { get; set; } = TimeType.Quarter;

        /// <summary>
        ///     考核结果
        /// </summary>
        [Display(Name = "考核结果")]
        [Field(ControlsType = ControlsType.DropdownList, IsShowAdvancedSerach = true, IsShowBaseSerach = true,
            ListShow = true, EditShow = true,
            SortOrder = 102, Width = "110", DataSource = "Alabo.App.Share.Kpi.Domain.Enum.KpiResult")]
        public KpiResult KpiResult { get; set; }

        /// <summary>
        ///     等级考核值
        /// </summary>
        [Display(Name = "等级考核值")]
        [Field(ControlsType = ControlsType.ChildList,
            GroupTabId = 1, Width = "120", ListShow = true, SortOrder = 2)]
        public IList<GradeKpiItem> KpiItems { get; set; }

        public IEnumerable<ViewLink> ViewLinks()
        {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("等级修改", "/Admin/GradeKpi/Edit?Id=[[Id]]", Icons.Edit, LinkType.ColumnLink)
            };
            return quickLinks;
        }
    }

    [ClassProperty(Name = "等级考核详情")]
    public class GradeKpiItem : BaseViewModel
    {
        [Display(Name = "KPI类型")]
        [Field(ListShow = true)]
        public GradeKpiType KpiType { get; set; }

        /// <summary>
        ///     与API数据对应
        /// </summary>
        [Display(Name = "KPI配置")]
        public long KpiId { get; set; }

        /// <summary>
        ///     与KpiAutoConfig对应
        /// </summary>
        [Display(Name = "KPI配置")]
        public Guid? KpiConfigId { get; set; }

        [Display(Name = "名称")]
        [Field(ListShow = true)]
        public string KpiName { get; set; }

        [Display(Name = "数量")]
        [Field(ListShow = true)]
        public decimal Amount { get; set; }
    }
}