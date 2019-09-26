using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Users.Enum;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.Data.People.Users.ViewModels {

    /// <summary>
    ///     用户身份认证
    /// </summary>
    [ClassProperty(Name = "实名认证", Icon = "fa fa-puzzle-piece", Description = "实名认证",
        SideBarType = SideBarType.IdentitySideBar)]
    public class IdentityView : BaseViewModel {

        /// <summary>
        ///     真实姓名
        /// </summary>
        [Display(Name = "真实姓名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.Label, IsMain = true, GroupTabId = 1, Width = "110", ListShow = true,
            SortOrder = 2)]
        public string RealName { get; set; }

        public long LoginUserId { get; set; }

        /// <summary>
        ///     性别
        /// </summary>
        [Display(Name = "性别")]
        [NotMapped]
        [Field(ControlsType = ControlsType.Label, EditShow = false, Width = "200", ListShow = true, SortOrder = 7)]
        public string Sex { get; set; }

        /// <summary>
        ///     证件类型0：身份证 1：护照 2：营业执照 3：驾照
        /// </summary>
        [Display(Name = "证件类型")]
        [Field(ControlsType = ControlsType.Label, Width = "200", DataSourceType = typeof(CardType), ListShow = true, SortOrder = 3)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public CardType CardType { get; set; }

        /// <summary>
        ///     证件号
        /// </summary>
        [Display(Name = "证件号码")]
        [Field(ControlsType = ControlsType.Label, GroupTabId = 1, Width = "110", ListShow = true, SortOrder = 5)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string CardNo { get; set; }

        /// <summary>
        ///     证件图片1url，证件正面
        /// </summary>
        [Display(Name = "证件正面照")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.ImagePreview, SortOrder = 8)]
        public string FrontImage { get; set; }

        /// <summary>
        ///     证件图片2url，证件反面
        /// </summary>
        [Display(Name = "证件反面照")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.ImagePreview, SortOrder = 9)]
        public string AntiImage { get; set; }

        /// <summary>
        ///     证件图小样
        /// </summary>
        [Display(Name = "手持证件正面照")]
        [Field(ControlsType = ControlsType.ImagePreview, SortOrder = 10)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string SmallImage { get; set; }

        /// <summary>
        ///     审核状态（0：已提交，1：审核中(暂时不用)，2：审核通过，3：审核不通过）
        /// </summary>
        [Display(Name = "审核状态")]
        [Field(ControlsType = ControlsType.RadioButton, DataSource = "Alabo.Framework.Core.Enums.Enum.IdentityStatus",
            Width = "110", ListShow = false, IsShowAdvancedSerach = false, IsShowBaseSerach = false,
            SortOrder = 11)]
        public IdentityStatus Status { get; set; }

        /// <summary>
        ///     审核备注，不通过时备注信息
        /// </summary>
        //[Display(Name = "审核备注")]
        public string CheckRemark { get; set; }

        /// <summary>
        ///     审核人
        /// </summary>
        [Display(Name = "审核人")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "110", EditShow = false, SortOrder = 13)]
        public string CheckUserName { get; set; }

        /// <summary>
        ///     审核时间
        /// </summary>
        [Display(Name = "审核时间")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "110", ListShow = true, EditShow = false,
            SortOrder = 14)]
        public DateTime CheckTime { get; set; }

        /// <summary>
        ///     获取链接
        /// </summary>
        public IEnumerable<ViewLink> ViewLinks() {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("实名审核", "/Admin/Basic/Edit?Service=IIdentityService&Method=GetView&userId=[[LoginUserId]]", Icons.Edit, LinkType.ColumnLink)
            };
            return quickLinks;
        }
    }
}