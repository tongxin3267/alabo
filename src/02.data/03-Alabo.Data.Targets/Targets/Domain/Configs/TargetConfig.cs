using System;
using System.ComponentModel.DataAnnotations;
using Alabo.AutoConfigs;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Maps;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Data.Targets.Targets.Domain.Configs
{
    /// <summary>
    /// 目标设置
    /// </summary>
    [ClassProperty(Name = "目标设置", Icon = "fa fa-external-link", Description = "目标设置", SortOrder = 23, PageType = ViewPageType.List)]
    public class TargetConfig : AutoConfigBase, IAutoConfig
    {
        /// <summary>
        ///     是否启用贡献值
        /// </summary>
        [Display(Name = "是否启用贡献值")]
        [Field(ControlsType = ControlsType.DropdownList, DataSourceType = typeof(MoneyTypeConfig))]
        [HelpBlock("是否启用贡献值，启用贡献值后，任务完成后会根据贡献值来获取奖励")]
        public bool Contribution { get; set; } = false;

        /// <summary>
        ///     奖励货币类型
        /// </summary>
        [Display(Name = "奖励货币类型")]
        [Field(ControlsType = ControlsType.DropdownList, DataSourceType = typeof(MoneyTypeConfig))]
        [HelpBlock("奖励货币类型,任务完成后，奖金会自动到任务处理人相应的账号上")]
        public Guid MoneyTypeId { get; set; } = Guid.Parse("E97CCD1E-1478-49BD-BFC7-E73A5D699000");

        /// <summary>
        /// 任务安排人奖金获取比例
        /// </summary>
        [Display(Name = "任务安排人奖金获取比例")]
        [Field(ControlsType = ControlsType.Decimal, DataSourceType = typeof(MoneyTypeConfig))]
        [HelpBlock("任务安排人奖金获取比例,是以任务的奖励为基数.比如一个任务奖金为100元，如果比例为10%，则任务安排人可获得10元的奖励")]
        public decimal ArrangeUserBonusRatio { get; set; } = 0m;

        /// <summary>
        /// 任务安排人奖金获取比例
        /// </summary>
        [Display(Name = "任务安排人共贡献值获取比例")]
        [Field(ControlsType = ControlsType.DropdownList, DataSourceType = typeof(MoneyTypeConfig))]
        [HelpBlock("任务安排人贡献值获取比例,是以任务的贡献值为基数.比如一个任务贡献值为100，如果比例为10%，则任务安排人可获得10贡献值")]
        public decimal ArrangeUserContributionRatio { get; set; } = 0m;

        /// <summary>
        /// 任务安排人奖金获取比例
        /// </summary>
        [Display(Name = "任务检视人奖金获取比例")]
        [Field(ControlsType = ControlsType.Decimal, DataSourceType = typeof(MoneyTypeConfig))]
        [HelpBlock("任务检视人奖金获取比例,是以任务的奖励为基数.比如一个任务奖金为100元，如果比例为10%，则任务检视人可获得10元的奖励")]
        public decimal AuditorUserBonusRatio { get; set; } = 0m;

        /// <summary>
        /// 任务安排人奖金获取比例
        /// </summary>
        [Display(Name = "任务检视人共贡献值获取比例")]
        [Field(ControlsType = ControlsType.DropdownList, DataSourceType = typeof(MoneyTypeConfig))]
        [HelpBlock("任务检视人贡献值获取比例,是以任务的贡献值为基数.比如一个任务贡献值为100，如果比例为10%，则任务检视人可获得10贡献值")]
        public decimal AuditorUserContributionRatio { get; set; } = 0m;

        public void SetDefault() {
        }
    }
}