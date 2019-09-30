using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Alabo.AutoConfigs;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Asset.Refunds.Domain.Configs
{
    /// <summary>
    /// 退款原因
    /// </summary>
    [ClassProperty(Name = "退款原因", Icon = "fa fa-plus-square", Description = "充值设置", SortOrder = 23, PageType = ViewPageType.List)]
    [NotMapped]
    public class RefundReasonConfig : AutoConfigBase, IAutoConfig
    {
        /// <summary>
        ///     原因
        /// </summary>
        [Display(Name = "原因")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        public string Name { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        [Display(Name = "手续费")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.Decimal, ListShow = true)]
        public decimal Fee { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [Display(Name = "说明")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextArea, ListShow = true)]
        public string Intro { get; set; }

        public void SetDefault()
        {
        }
    }
}