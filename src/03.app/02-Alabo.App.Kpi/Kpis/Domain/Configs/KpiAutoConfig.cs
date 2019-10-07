using Alabo.AutoConfigs;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.App.Kpis.Kpis.Domain.Configs
{
    /// <summary>
    ///     绩效配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "绩效配置", Icon = "fa fa-cny", Description = "绩效配置", GroupName = "基本设置,高级选项",
        PageType = ViewPageType.List, SortOrder = 20)]
    public class KpiAutoConfig : BaseViewModel, IAutoConfig
    {
        /// <summary>
        ///     Id自增，主键
        /// </summary>
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID", Order = 1)]
        [Key]
        [Field(ControlsType = ControlsType.Hidden, ListShow = true, SortOrder = 1, GroupTabId = 1, Width = "50")]
        public Guid Id { get; set; }

        /// <summary>
        ///     绩效名称
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 0, ListShow = true, GroupTabId = 1, Width = "10%")]
        [Required]
        [Display(Name = "绩效名称")]
        [Main]
        public string Name { get; set; }

        /// <summary>
        ///     考核周期
        /// </summary>
        [Display(Name = "考核周期")]
        [Field(ControlsType = ControlsType.RadioButton, ListShow = true, GroupTabId = 1, EditShow = true,
            SortOrder = 102, Width = "110", DataSource = "Alabo.Domains.Enums.TimeType")]
        public TimeType TimeType { get; set; }

        /// <summary>
        ///     考核范围
        /// </summary>
        [Display(Name = "考核范围")]
        [Field(ControlsType = ControlsType.RadioButton, ListShow = true, GroupTabId = 1, EditShow = true,
            SortOrder = 103, Width = "110", DataSource = "Alabo.Framework.Core.Enums.Enum.KpiTeamType")]
        public KpiTeamType KpiTeamType { get; set; }

        /// <summary>
        ///     动态标记数字
        /// </summary>
        /// <summary>
        ///     考核范围
        /// </summary>
        [Display(Name = "标记一")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 2, EditShow = true,
            SortOrder = 103, Width = "110")]
        [HelpBlock("后台计算标识一，不清楚意义的时候，请勿修改")]
        public int Mark { get; set; }

        /// <summary>
        ///     动态标记数字
        /// </summary>
        /// <summary>
        ///     考核范围
        /// </summary>
        [Display(Name = "标记二")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 2, EditShow = true,
            SortOrder = 103, Width = "110")]
        [HelpBlock("后台计算标识二，不清楚意义的时候，请勿修改")]
        public int MarkTwo { get; set; }

        /// <summary>
        ///     排序,越小排在越前面
        /// </summary>
        [Display(Name = "排序", Order = 1000)]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true, SortOrder = 10000,
            Width = "110")]
        [Range(0, 99999, ErrorMessage = "请输入0-99999之间的数字")]
        [HelpBlock("排序,越小排在越前面，请输入0-99999之间的数字")]
        public long SortOrder { get; set; } = 1000;

        /// 通用状态 状态：0正常,1冻结,2删除
        /// 实体的软删除通过此字段来实现
        /// 软删除：指的是将实体标记为删除状态，不是真正的删除，可以通过回收站找回来
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.RadioButton, ListShow = true, EditShow = true,
            SortOrder = 10003, Width = "110", DataSource = "Alabo.Domains.Enums.Status")]
        public Status Status { get; set; } = Status.Normal;

        public void SetDefault()
        {
        }
    }
}