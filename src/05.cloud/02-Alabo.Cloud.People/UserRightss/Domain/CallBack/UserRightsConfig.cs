using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.AutoConfigs;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using Newtonsoft.Json;

namespace Alabo.Cloud.People.UserRightss.Domain.CallBack
{

    /// <summary>
    ///     等级权益设置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "等级权益设置", Icon = "fa fa-cny", Description = "等级权益设置",
        PageType = ViewPageType.List, SortOrder = 20,
        SideBarType = SideBarType.UserRightsSideBar)]
    public class UserRightsConfig : BaseViewModel, IAutoConfig
    {

        /// <summary>
        ///     Id自增，主键
        /// </summary>
        [Display(Name = "ID", Order = 1)]
        [Key]
        [Field(ControlsType = ControlsType.Hidden, ListShow = true, SortOrder = 1, Width = "50")]
        public Guid Id { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 0, ListShow = true, Width = "10%")]
        [Required]
        [Display(Name = "名称")]
        [Main]
        public string Name { get; set; }

        /// <summary>
        ///     背景图片
        /// </summary>
        [Field(ControlsType = ControlsType.AlbumUploder, SortOrder = 0, IsImagePreview = true, ListShow = true,
            Width = "10%")]
        [Display(Name = "背景图片")]
        public string BackGroundImage { get; set; }

        /// <summary>
        ///     介绍
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 1, Width = "10%")]
        [Required]
        [Display(Name = "简介")]
        public string Intro { get; set; }

        /// <summary>
        ///     介绍
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic, SortOrder = 1, Width = "10%")]
        [Display(Name = "排序号")]
        [HelpBlock("越小越在前面，降序排序")]
        public long SortOrder { get; set; } = 1000;

        /// <summary>
        ///     主题颜色
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 0, ListShow = true, Width = "10%")]
        [Display(Name = "主题颜色")]
        public string ThemeColor { get; set; }

        /// <summary>
        ///     会员等级
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, DisplayMode = DisplayMode.Grade,
            EditShow = true, SortOrder = 1,
            DataSourceType = typeof(UserGradeConfig))]
        [Display(Name = "考核等级")]
        [HelpBlock("考核等级，一个等级降职配置只能有一条，有多条时默认选择第一条")]
        public Guid GradeId { get; set; }

        [Field(ControlsType = ControlsType.Switch, PlaceHolder = "是否开放", ListShow = true, SortOrder = 109, EditShow = true)]
        [Display(Name = "是否开放")]
        public bool IsOpen { get; set; } = true;


        /// <summary>
        /// 有端口是否需要支付
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, PlaceHolder = "有端口是否需要支付", ListShow = true, SortOrder = 109, EditShow = true)]
        [Display(Name = "有端口是否需要支付,开启后，继续需要扣端口，也需要扣钱")]
        public bool IsHavePortNeedToPay { get; set; }


        /// <summary>
        /// 无端口能否付费开通
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, PlaceHolder = "无端口能否付费开通该等级", ListShow = true, SortOrder = 109, EditShow = true)]
        [Display(Name = "无端口能否付费开通该等级")]
        public bool HaveNotPortCanOpen { get; set; }


        /// <summary>
        /// 能否自己升级,付费升级
        /// 场景：比如免费会员不能付费升级
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, PlaceHolder = "无端口能否付费开通", ListShow = true, SortOrder = 109, EditShow = true)]
        [Display(Name = "该等级能否付费升级，场景：比如免费会员不能付费升级")]
        public bool CanUpgradeBySelf { get; set; }

        /// <summary>
        /// 是否显示该等级页面
        /// </summary>

        [Field(ControlsType = ControlsType.Switch, PlaceHolder = "是否显示该等级页面", ListShow = true, SortOrder = 109, EditShow = true)]
        [Display(Name = "是否显示该等级页面")]
        public bool IsShowGradePage { get; set; }


        /// <summary>
        /// 该等级是否只有管理员可以开通
        /// </summary>

        [Field(ControlsType = ControlsType.Switch, PlaceHolder = "该等级是否只有管理员可以开通", ListShow = true, SortOrder = 109, EditShow = true)]
        [Display(Name = "该等级是否只有管理员可以开通")]
        public bool IsCanOnlyAdminOpen { get; set; }


        /// <summary>
        ///     等级权益设置
        /// </summary>
        [Field(ControlsType = ControlsType.Json, PlaceHolder = "等级权益设置", ListShow = false,
            SortOrder = 109,
            EditShow = true, ExtensionJson = "UserRightItems")]
        [Display(Name = "等级权益设置")]
        [JsonIgnore]
        public string UserRightItemsJson { get; set; }

        /// <summary>
        ///     Gets or sets the team range rate items.
        /// </summary>
        [Display(Name = "等级权益设置")]
        public IList<UserRightItem> UserRightItems { get; set; } = new List<UserRightItem>();

        public void SetDefault()
        {
        }
    }
}