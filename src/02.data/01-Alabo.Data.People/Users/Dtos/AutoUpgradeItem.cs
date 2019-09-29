using Alabo.Domains.Enums;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Data.People.Users.Dtos
{
    /// <summary>
    ///     会员自动升级配置项
    /// </summary>
    [ClassProperty(Name = "会员自动升级配置项")]
    public class AutoUpgradeItem : BaseViewModel
    {
        /// <summary>
        ///     唯一标识(不能重复)
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "90", EditShow = true,
            SortOrder = 2)]
        [Display(Name = "唯一标识(大小字母A-Z)")]
        public string Key { get; set; }

        /// <summary>
        ///     考核范围
        /// </summary>
        [Display(Name = "考核范围")]
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, GroupTabId = 1, EditShow = true,
            SortOrder = 3, Width = "110", DataSourceType = typeof(KpiUpgradeType))]
        public KpiUpgradeType KpiUpgradeType { get; set; }

        /// <summary>
        ///     会员等级
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, DisplayMode = DisplayMode.Grade,
            EditShow = true, SortOrder = 5,
            DataSourceType = typeof(UserGradeConfig))]
        [Display(Name = "会员等级")]
        public Guid GradeId { get; set; }

        /// <summary>
        ///     考核基准数量(达到后满足,含等于)
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "200", EditShow = true, SortOrder = 10)]
        [Display(Name = "基准数量(达到后满足,含等于)")]
        public long Count { get; set; } = 0;
    }
}