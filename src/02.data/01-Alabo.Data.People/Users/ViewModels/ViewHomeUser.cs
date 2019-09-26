using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Users.Enum;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.User.ViewModels {

    /// <summary>
    ///     会员管理
    /// </summary>
    [ClassProperty(Name = "我推荐的会员", Icon = "fa fa-puzzle-piece", Description = "我推荐的会员")]
    public class ViewHomeUser : BaseViewModel {

        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden, GroupTabId = 1, ListShow = false)]
        [Key]
        public long Id { get; set; }

        /// <summary>
        ///     用户名（以字母开头，包括a-z,0-9和_）：[a-zA-z][a-zA-Z0-9_]{2,15}
        /// </summary>
        [Display(Name = "用户名")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, PlaceHolder = "请输入用户名",
            IsShowAdvancedSerach = true, DataField = "UserId", GroupTabId = 1, IsMain = true, Width = "150",
            ListShow = true, SortOrder = 2, Link = "/Admin/User/Edit?id=[[Id]]")]
        public string UserName { get; set; }

        /// <summary>
        ///     姓名
        /// </summary>
        [Display(Name = "姓名")]
        [Field(ControlsType = ControlsType.TextBox, IsMain = true, GroupTabId = 1, Width = "150", ListShow = true,
            SortOrder = 3, Link = "/Admin/User/Edit?id=[[Id]]")]
        public string Name { get; set; }

        /// <summary>
        ///     手机号码
        /// </summary>
        [Display(Name = "手机号")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true,
            IsShowBaseSerach = true, IsShowAdvancedSerach = true, SortOrder = 4)]
        public string Mobile { get; set; }

        /// <summary>
        ///     邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, ListShow = true, SortOrder = 5)]
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the parent identifier.
        /// </summary>
        public long ParentId { get; set; } = 0L;

        /// <summary>
        ///     Gets or sets the name of the parent.
        /// </summary>
        [Display(Name = "推荐人")]
        //[Field(ControlsType = ControlsType.TextBox, Width = "150", IsShowBaseSerach = true, IsShowAdvancedSerach = true, ListShow = true, Link = "/Admin/User/Edit?id=[[ParentId]]", SortOrder = 6)]
        public string ParentName { get; set; }

        /// <summary>
        ///     用户等级Id
        ///     每个类型对应一个等级
        /// </summary>
        public Guid GradeId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the grade.
        /// </summary>
        [Display(Name = "等级")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Info, IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, GroupTabId = 1, Width = "150", ListShow = true, SortOrder = 5)]
        public string GradeName { get; set; }

        /// <summary>
        ///     是否认证通过
        /// </summary>
        [Display(Name = "实名?")]
        [Field(ControlsType = ControlsType.DropdownList, GroupTabId = 1, Width = "150",
            DataSource = "Alabo.Framework.Core.Enums.Enum.IdentityStatus", ListShow = true, SortOrder = 8)]
        public IdentityStatus IdentityStatus { get; set; } = IdentityStatus.IsNoPost;

        /// <summary>
        ///     性别
        /// </summary>
        [Display(Name = "性别")]
        [Field(ControlsType = ControlsType.DropdownList, GroupTabId = 1, DataSource = "Alabo.Framework.Core.Enums.Enum.Sex",
            Width = "150", ListShow = true, SortOrder = 10)]
        public Sex Sex { get; set; } = Sex.Man;

        /// <summary>
        ///     用户头像
        /// </summary>
        [Display(Name = "头像")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", IsImagePreview = true,
            ListShow = true, SortOrder = 1)]
        public string Avator { get; set; } = @"/wwwroot/static/images/avator/Man_48.png";

        /// <summary>
        ///     用户状态
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.DropdownList, IsTabSearch = true,
            DataSource = "Alabo.Domains.Enums.Status",
            GroupTabId = 1, Width = "150", ListShow = true, SortOrder = 1005)]
        public Status Status { get; set; }

        /// <summary>
        ///     注册时间
        /// </summary>
        [Display(Name = "注册时间")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, SortOrder = 1000)]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     Gets or sets the 会员 grade configuration.
        /// </summary>
        public UserGradeConfig UserGradeConfig { get; set; }

        /// <summary>
        ///     获取s the avator.
        /// </summary>
        /// <param name="size">The size.</param>
        public string GetAvator(int size = 48) {
            if (Avator.IsNullOrEmpty()) {
                return $@"/wwwroot/static/images/avator/{Sex}_{size}.png";
            }

            return Avator;
        }

        /// <summary>
        ///     操作链接
        /// </summary>
        public IEnumerable ViewLinks() {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("详情", "/Admin/User/Edit?id=[[Id]]", Icons.Edit, LinkType.ColumnLink)
            };
            return quickLinks;
        }
    }
}