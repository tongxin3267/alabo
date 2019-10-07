using Alabo.App.Kpis.Kpis.Domain.Configs;
using Alabo.AutoConfigs;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.App.Kpis.GradeKpis.Domain.Configs
{
    /// <summary>
    ///     绩效配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "绩效晋升配置", Icon = "fa fa-cny", Description = "绩效晋升配置",
        PageType = ViewPageType.List, SortOrder = 20)]
    public class PromotedKpiConfig : BaseViewModel, IAutoConfig
    {
        /// <summary>
        ///     Id自增，主键
        /// </summary>
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID", Order = 1)]
        [Key]
        [Field(ControlsType = ControlsType.Hidden, ListShow = true, SortOrder = 1, Width = "50")]
        public Guid Id { get; set; }

        /// <summary>
        ///     绩效名称
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 0, ListShow = true, Width = "10%")]
        [Required]
        [Display(Name = "绩效名称")]
        [Main]
        public string Name { get; set; }

        /// <summary>
        ///     会员等级
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, DisplayMode = DisplayMode.Grade,
            EditShow = true, SortOrder = 1,
            DataSource = "Alabo.App.Core.User.Domain.Callbacks.UserGradeConfig")]
        [Display(Name = "会员等级")]
        [HelpBlock("考核等级，一个等级晋升配置只能有一条，有多条时默认选择第一条")]
        public Guid GradeId { get; set; }

        /// <summary>
        ///     晋升等级
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, DisplayMode = DisplayMode.Grade,
            EditShow = true, SortOrder = 1,
            DataSource = "Alabo.App.Core.User.Domain.Callbacks.UserGradeConfig")]
        [Display(Name = "晋升等级")]
        [HelpBlock("晋升等级")]
        public Guid ChangeGradeId { get; set; }

        /// <summary>
        ///     考核周期
        /// </summary>
        [Display(Name = "考核周期")]
        [Field(ControlsType = ControlsType.RadioButton, ListShow = true, EditShow = true,
            SortOrder = 102, Width = "110", DataSource = "Alabo.Domains.Enums.TimeType")]
        public TimeType TimeType { get; set; } = TimeType.Quarter;

        /// <summary>
        ///     Gets or sets the team range rate json.
        ///     团队极差
        /// </summary>
        [Field(ControlsType = ControlsType.Json, PlaceHolder = "设置晋升考核基准,按照逻辑预算符计算结果，配置唯一标识不能重复、不能为空", ListShow = false,
            SortOrder = 109,
            EditShow = true, ExtensionJson = "KpiItems")]
        [JsonIgnore]
        [Display(Name = "晋升Kpi考核基准配置")]
        public string TeamRangeRateJson { get; set; }

        /// <summary>
        ///     Gets or sets the team range rate items.
        /// </summary>
        public IList<KpiItem> KpiItems { get; set; } = new List<KpiItem>();

        /// <summary>
        ///     高级逻辑预算符
        /// </summary>

        [Field(ControlsType = ControlsType.TextBox, SortOrder = 2000, EditShow = true, Width = "10%")]
        [Required]
        [Display(Name = "逻辑预算符")]
        [HelpBlock("高级逻辑计算符,根据KPI唯一表示来配置。支持括号<code>()</code>、并且<code>&&</code>、或<code>||</code>预算符，逻辑与数学公式一样" +
                   "<br/>唯一标识：大写字母A-Z，比如A、B、F等，不能重复" +
                   "<br/>示范一：<span class='m-badge m-badge--metal m-badge--wide'>（A&&B)||C&&(D||F)</span>" +
                   "<br/>示范二：<span class='m-badge m-badge--metal m-badge--wide'>(A||C)&&B</span>" +
                   "<br>其中A、B、C、D、F为KPI配置的唯一标识，大小字母A-Z")]
        public string LogicalOperator { get; set; }

        public void SetDefault()
        {
        }
    }
}