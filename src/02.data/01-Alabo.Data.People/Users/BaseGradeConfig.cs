using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Data.People.Users {

    /// <summary>
    ///     会员等级基类函数
    /// </summary>
    public class BaseGradeConfig : AutoConfigBase {

        /// <summary>
        ///     Gets or sets the user type identifier.
        /// </summary>
        /// <value>
        ///     The user type identifier.
        /// </value>
        [Display(Name = "用户类型")]
        [Field(ControlsType = ControlsType.Hidden, SortOrder = 2, EditShow = true, ListShow = false, Width = "10%")]
        public Guid UserTypeId { get; set; }

        /// <summary>
        ///     等级名称
        /// </summary>
        [Main]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 2, ListShow = true, Width = "10%")]
        [Display(Name = "等级名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("等级名称，比如高级会员，中级会员，钻石会员，黄金股东等等")]
        public string Name { get; set; }

        public string Intro { get; set; }

        /// <summary>
        ///     升级升级点
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, Width = "10%")]
        [Display(Name = "升级升级点")]
        [HelpBlock("会员升级升级点大小,会员升级根据升级点的大小自动升级")]
        public long Contribute { get; set; } = 0;

        /// <summary>
        ///     升级价格
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, Width = "10%")]
        [Display(Name = "升级价格")]
        public decimal Price { get; set; } = 0;

        /// <summary>
        ///     分润基数
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = false, Width = "10%", EditShow = true)]
        [Display(Name = "分润基数")]
        public decimal Radix { get; set; } = 0;

        /// <summary>
        ///     Gets or sets the grade privileges.
        /// </summary>
        /// <value>
        ///     The grade privileges.
        /// </value>
        [Field(ControlsType = ControlsType.CheckBoxMultipl, SortOrder = 2, DataSourceType = typeof(GradePrivilegesConfig))]//DataSource = "Alabo.App.Core.UserType.Domain.CallBacks.GradePrivilegesConfig")]
        [Display(Name = "等级特权")]
        [HelpBlock(
            "等级特权，等级特权可以在控制面板中设置<a href='/Admin/AutoConfig/List?key=Alabo.App.Core.UserType.Domain.CallBacks.GradePrivilegesConfig'>等级特权管理</a>")]
        public string GradePrivileges { get; set; }

        /// <summary>
        ///     默认用户等级
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, ListShow = true, EditShow = true)]
        [Display(Name = "默认等级")]
        [HelpBlock("一种类型的有且只能有一个默认等级，在设置的是请注意，否则是导致数据出错")]
        public bool IsDefault { get; set; } = false;

        /// <summary>
        ///     Gets or sets the icon.
        /// </summary>
        /// <value>
        ///     The icon.
        /// </value>
        [Field(ControlsType = ControlsType.AlbumUploder, ListShow = true, SortOrder = 1, IsImagePreview = true)]
        [Display(Name = "等级勋章")]
        public string Icon { get; set; }

        /// <summary>
        /// 会员卡背景图
        /// </summary>
        [Field(ControlsType = ControlsType.AlbumUploder, ListShow = true, SortOrder = 1, IsImagePreview = true)]
        [Display(Name = "会员卡背景图")]
        public string CardImage { get; set; }

        /// <summary>
        ///     Gets or sets the equity.
        /// </summary>
        /// <value>
        ///     The equity.
        /// </value>
        [Field(ControlsType = ControlsType.TextArea, SortOrder = 20, ListShow = false, Width = "10%")]
        [Display(Name = "会员权益")]
        [HelpBlock("会员权益")]
        public string Equity { get; set; }
    }
}