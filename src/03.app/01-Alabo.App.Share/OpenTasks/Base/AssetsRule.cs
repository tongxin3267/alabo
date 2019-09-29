using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Share.OpenTasks.Base
{
    /// <summary>
    ///     资产分配
    /// </summary>
    [ClassProperty(Name = "拼团商品价格设置")]
    public class AssetsRule : BaseViewModel
    {
        /// <summary>
        ///     资产账户类型
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, DataSourceType = typeof(MoneyTypeConfig), ListShow = true,
            EditShow = true, SortOrder = 5)]
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     分配比例
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 5)]
        [Display(Name = "分配比例(总和为1)")]
        public decimal Ratio { get; set; }
    }
}