using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.Categories.Domain.Enums
{
    [ClassProperty(Name = "控件显示类型")]
    public enum PropertyControlType
    {
        /// <summary>
        ///     文本框
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)] [Display(Name = "文本框")]
        TextBox = 1,

        /// <summary>
        ///     多选框
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)] [Display(Name = "多选框")]
        CheckBox = 3,

        /// <summary>
        ///     单选框
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)] [Display(Name = "单选框")]
        RadioButton = 4,

        /// <summary>
        ///     下拉列表
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)] [Display(Name = "下拉列表")]
        DropdownList = 5,

        /// <summary>
        ///     数字框
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)] [Display(Name = "数字框")]
        Numberic = 6,

        /// <summary>
        ///     时间框
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "时间框")]
        DateTimePicker = 8,

        /// <summary>
        ///     样式参考：http://ui.5ug.com/metronic_v4.5.4/theme/admin_4/components_bootstrap_switch.html
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)] [Display(Name = "Switch切换")]
        Switch = 14
    }
}