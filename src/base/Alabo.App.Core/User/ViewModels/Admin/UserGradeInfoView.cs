using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using Alabo.Web.Validations;

namespace Alabo.App.Core.User.ViewModels.Admin {

    /// <summary>
    ///     直推会员等级报表
    /// </summary>
    [ClassProperty(Name = "会员等级报表", Icon = "fa fa-puzzle-piece", Description = "会员等级报表",
        SideBarType = SideBarType.FullScreen,
        GroupName = "基本信息,高级选项")]
    public class UserGradeInfoView : BaseViewModel {
        [Field(ListShow = true)] [Key] public long Id { get; set; }

        /// <summary>
        ///     用户Id
        /// </summary>
        [Display(Name = "用户名称")]
        public long UserId { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Display(Name = "用户名")]
        [HelpBlock("请输入合伙人所属用户名")]
        [Field(IsShowBaseSerach = true, PlaceHolder = "请输入用户名", DataField = "UserId",
            ValidType = ValidType.UserName, IsMain = true, ControlsType = ControlsType.TextBox, GroupTabId = 1,
            Width = "120", ListShow = true, EditShow = true, SortOrder = 2)]
        public string UserName { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Display(Name = "等级名称")]
        [HelpBlock("请输入合伙人所属用户名")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Default, GroupTabId = 1,
            Width = "120", ListShow = true, SortOrder = 2)]
        public string GradeName { get; set; }

        /// <summary>
        ///     Ta的推荐
        /// </summary>
        [Display(Name = "Ta的推荐")]
        [HelpBlock("请输入合伙人所属用户名")]
        [Field(ControlsType = ControlsType.TextBox, Link = "/Admin/User/ParentUser?UserId=[[Id]]",
            GroupTabId = 1, Width = "100", ListShow = true, EditShow = false, SortOrder = 3)]
        public string DisplayName { get; set; } = "Ta的推荐";

        /// <summary>
        ///     Ta的推荐
        /// </summary>
        [Display(Name = "总数")]
        [HelpBlock("请输入合伙人所属用户名")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Brand, GroupTabId = 1, Width = "120",
            ListShow = true, EditShow = false, SortOrder = 3)]
        public string TotalCountString { get; set; }

        /// <summary>
        ///     推荐等级信息
        /// </summary>
        [Display(Name = "等级信息")]
        [Field(ValidType = ValidType.UserName, ControlsType = ControlsType.TextBox, GroupTabId = 1,
            Width = "80", ListShow = true, DataSource = "UserGradeConfig", SortOrder = 5)]
        public Dictionary<Guid, long> GradeInfo { get; set; }

        /// <summary>
        ///     推荐等级信息
        /// </summary>
        [Display(Name = "等级信息")]
        [Field(ValidType = ValidType.UserName, ControlsType = ControlsType.TextBox, GroupTabId = 1,
            Width = "400", ListShow = false, SortOrder = 5)]
        public string GradeInfoString { get; set; }

        /// <summary>
        ///     最后更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = false, SortOrder = 102, Width = "150")]
        public DateTime ModifiedTime { get; set; }

        /// <summary>
        ///     获取链接
        /// </summary>
        public IEnumerable<ViewLink> ViewLinks() {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("推荐会员等级报表",
                    "/Admin/Basic/List?Service=IUserMapService&Method=GetUserGradeInfoPageList&type=recomend&PlatformId=997051",
                    Icons.List, LinkType.TableQuickLink),
                new ViewLink("间推会员等级报表",
                    "/Admin/Basic/List?Service=IUserMapService&Method=GetUserGradeInfoPageList&type=second&PlatformId=997052",
                    Icons.List, LinkType.TableQuickLink),
                new ViewLink("团队会员等级报表",
                    "/Admin/Basic/List?Service=IUserMapService&Method=GetUserGradeInfoPageList&type=team&PlatformId=997053",
                    Icons.List, LinkType.TableQuickLink),
                new ViewLink("数据更新", "/Admin/Basic/Run?Service=IUserMapService&Method=UpdateUserTeamGrade&query=[[Id]]",
                    Icons.Settings, LinkType.ColumnLink),
                new ViewLink("Ta的推荐", "/Admin/User/ParentUser?UserId=[[Id]]", Icons.Run, LinkType.ColumnLink),
                new ViewLink("会员详情", "/Admin/User/Edit?Id=[[Id]]", Icons.List, LinkType.ColumnLink),
                new ViewLink("财务详情", "/Admin/Account/Edit?Id=[[Id]]", Icons.Coins, LinkType.ColumnLink)
            };

            return quickLinks;
        }
    }
}