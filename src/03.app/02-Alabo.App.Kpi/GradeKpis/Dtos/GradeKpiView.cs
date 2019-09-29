using Alabo.App.Kpis.Kpis.Domain.Enum;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Kpis.GradeKpis.Dtos
{
    /// <summary>
    /// </summary>
    [ClassProperty(Name = "等级考核", Icon = "flaticon-route", SideBarType = SideBarType.KpiSideBar)]
    public class GradeKpiView : BaseViewModel
    {
        /// <summary>
        ///     用户类型Id
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public long UserId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the user.
        /// </summary>
        /// <value>
        ///     The name of the user.
        /// </value>
        [Field(ListShow = true, GroupTabId = 1, IsShowAdvancedSerach = true, IsShowBaseSerach = true, Width = "120",
            SortOrder = 2, LabelColor = LabelColor.Info)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        ///     当前用户等级Id
        /// </summary>
        /// <value>
        ///     The grade identifier.
        /// </value>
        public Guid GradeId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the grade.
        /// </summary>
        /// <value>
        ///     The name of the grade.
        /// </value>
        [Field(ListShow = true, GroupTabId = 1, Width = "100", SortOrder = 2, LabelColor = LabelColor.Info)]
        [Display(Name = "现在等级")]
        public string GradeName { get; set; }

        /// <summary>
        ///     考核后等级
        /// </summary>
        /// <value>
        ///     The change grade identifier.
        /// </value>
        public Guid ChangeGradeId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the change grade.
        /// </summary>
        /// <value>
        ///     The name of the change grade.
        /// </value>
        [Field(ListShow = true, GroupTabId = 1, Width = "100", SortOrder = 2, LabelColor = LabelColor.Info)]
        [Display(Name = "考核后等级")]
        public string ChangeGradeName { get; set; }

        /// <summary>
        ///     考核周期
        /// </summary>
        /// <value>
        ///     The type of the time.
        /// </value>
        [Display(Name = "考核周期")]
        [Field(ControlsType = ControlsType.RadioButton, IsMain = true, ListShow = true, EditShow = true,
            SortOrder = 102, Width = "110", DataSource = "Alabo.Domains.Enums.TimeType")]
        public TimeType TimeType { get; set; } = TimeType.Quarter;

        /// <summary>
        ///     考核结果
        /// </summary>
        /// <value>
        ///     The kpi result.
        /// </value>
        [Field(ListShow = true, DataSource = "Alabo.App.Share.Kpi.Domain.Enum.KpiResult", GroupTabId = 1,
            Width = "100", SortOrder = 2, LabelColor = LabelColor.Info)]
        [Display(Name = "考核结果")]
        public KpiResult KpiResult { get; set; }
    }

    /// <summary>
    /// </summary>
    [ClassProperty(Name = "考核详情", Icon = "flaticon-route", SideBarType = SideBarType.GradeKpiSideBar)]
    public class GradeKpiDetailView : BaseViewModel
    {
        /// <summary>
        ///     用户类型Id
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public long UserId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the user.
        /// </summary>
        /// <value>
        ///     The name of the user.
        /// </value>
        [Field(ListShow = true, GroupTabId = 1, IsShowAdvancedSerach = true, IsShowBaseSerach = true, Width = "120",
            SortOrder = 2, LabelColor = LabelColor.Info)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        ///     当前用户等级Id
        /// </summary>
        /// <value>
        ///     The grade identifier.
        /// </value>
        public Guid GradeId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the grade.
        /// </summary>
        [Field(ListShow = true, GroupTabId = 1, Width = "100", SortOrder = 2, LabelColor = LabelColor.Info)]
        [Display(Name = "现在等级")]
        public string GradeName { get; set; }

        /// <summary>
        ///     考核后等级
        /// </summary>
        public Guid ChangeGradeId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the change grade.
        /// </summary>

        [Field(ListShow = true, GroupTabId = 1, Width = "100", SortOrder = 2, LabelColor = LabelColor.Info)]
        [Display(Name = "考核后等级")]
        public string ChangeGradeName { get; set; }

        /// <summary>
        ///     考核周期
        /// </summary>
        /// <value>
        ///     The type of the time.
        /// </value>
        [Display(Name = "考核周期")]
        [Field(ControlsType = ControlsType.RadioButton, IsMain = true, ListShow = true, EditShow = true,
            SortOrder = 102, Width = "110", DataSource = "Alabo.Domains.Enums.TimeType")]
        public TimeType TimeType { get; set; } = TimeType.Quarter;

        /// <summary>
        ///     考核结果
        /// </summary>
        /// <value>
        ///     The kpi result.
        /// </value>
        [Field(ListShow = true, DataSource = "Alabo.App.Share.Kpi.Domain.Enum.KpiResult", GroupTabId = 1,
            Width = "100", SortOrder = 2, LabelColor = LabelColor.Info)]
        [Display(Name = "考核结果")]
        public KpiResult KpiResult { get; set; }
    }
}