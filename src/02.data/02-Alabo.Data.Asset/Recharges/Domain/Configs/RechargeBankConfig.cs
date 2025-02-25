﻿using Alabo.AutoConfigs;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Asset.Recharges.Domain.Configs
{
    [ClassProperty(Name = "收款银行设置", Icon = "fa fa-external-link", Description = "收款设置", SortOrder = 23, PageType = ViewPageType.List)]
    public class RechargeBankConfig : AutoConfigBase, IAutoConfig
    {
        /// <summary>
        ///     银行名称
        /// </summary>
        [Display(Name = "银行名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true,
            DataSource = "Alabo.Framework.Core.Enums.Enum.BankType")]
        public BankType BankType { get; set; }

        /// <summary>
        ///     持卡人姓名
        /// </summary>
        [Display(Name = "持卡人姓名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        public string BankName { get; set; }

        /// <summary>
        ///     银行卡号
        /// </summary>
        [Display(Name = "银行卡号")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [StringLength(20, ErrorMessage = "银行卡号不能超过20位")]
        public string BankNumber { get; set; }

        /// <summary>
        ///     开户行
        /// </summary>
        [Display(Name = "开户行")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, ListShow = false)]
        public string BankAddress { get; set; }

        public void SetDefault() {
        }
    }
}