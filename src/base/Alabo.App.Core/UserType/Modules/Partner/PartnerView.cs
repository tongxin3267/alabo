﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.UserType.Domain.Enums;
using Alabo.App.Core.UserType.Domain.Services;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.UI;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using Alabo.Web.Validations;

namespace Alabo.App.Core.UserType.Modules.Partner {

    [ClassProperty(Name = "内部合伙人", Icon = "fa fa-puzzle-piece", Description = "内部合伙人", PostApi = "Api/AdminBasic/GetPageList",
        SideBarType = SideBarType.PartnerSideBar, ListApi = "Api/AdminBasic/GetPageList",
        GroupName = "基本信息,高级选项")]
    public class PartnerView : UIBase, IAutoTable<PartnerView> {
        public long Id { get; set; }

        /// <summary>
        ///     用户类型ID
        /// </summary>
        [Display(Name = "用户类型")]
        public Guid UserTypeId { get; set; } = Guid.Parse("71BE65E6-3A64-414D-972E-1A3D4A365112");

        /// <summary>
        ///     用户Id
        /// </summary>
        [Display(Name = "用户名称")]
        public long UserId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请输入合伙人名称，名称不能为空并且不超过8个字符")]
        [StringLength(20, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "180", ListShow = true, IsMain = true,
            SortOrder = 0, Link = "/Admin/Basic/Edit?Service=IPartnerService&View=GetView&Id=[[Id]]")]
        public string Name { get; set; }

        /// <summary>
        ///     推荐人用户Id
        /// </summary>
        [Display(Name = "推荐人ID")]
        [HelpBlock("填写正确的推荐人用户名")]
        [Field(ListShow = false, EditShow = false, SortOrder = 700)]
        public long ParentUserId { get; set; }

        /// <summary>
        ///     推荐人
        /// </summary>
        [Display(Name = "推荐人用户名")]
        [HelpBlock("请输入合伙人推荐人的用户名")]
        [NotMapped]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, ValidType = ValidType.ParentUserName, Width = "150",
            ListShow = true, EditShow = true, SortOrder = 3)]
        public string ParentUserName { get; set; }

        /// <summary>
        ///     用户等级Id
        ///     每个类型对应一个等级
        /// </summary>
        [Display(Name = "合伙人等级")]
        [HelpBlock("请设置合伙人等级")]
        [Field(ControlsType = ControlsType.RadioButton,
            DataSource = "Alabo.App.Core.UserType.Modules.Partner.PartnerGradeConfig", GroupTabId = 1, Width = "180",
            ListShow = false, EditShow = true, SortOrder = 1)]
        public Guid GradeId { get; set; } = Guid.Parse("72be65e6-3a64-abcd-9845-10054a36e000");

        /// <summary>
        ///     Gets or sets the name of the grade.
        /// </summary>
        /// <value>The name of the grade.</value>
        [Display(Name = "等级名称")]
        //[HelpBlock(helpText: "请设置合伙人等级")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Info, GroupTabId = 1, Width = "180",
            ListShow = true, EditShow = false, SortOrder = 1)]
        public string GradeName { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        [HelpBlock("只有通过审核的情况下才能进行操作")]
        [Field(ControlsType = ControlsType.RadioButton, GroupTabId = 1, IsTabSearch = true,
            IsShowAdvancedSerach = false,
            DataSource = "Alabo.App.Core.UserType.Domain.Enums.UserTypeStatus", Width = "150", ListShow = true,
            EditShow = true, SortOrder = 1005)]
        public UserTypeStatus Status { get; set; } = UserTypeStatus.Pending;

        /// <summary>
        ///     类型的组织架构图
        ///     计算分润时，可加快速度
        /// </summary>
        [Display(Name = "类型的组织架构图")]
        public string ParentMap { get; set; }

        /// <summary>
        ///     扩展属性
        /// </summary>
        [Display(Name = "扩展属性")]
        public string Extensions { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "180", ListShow = true, EditShow = false,
            SortOrder = 1006)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Display(Name = "用户名")]
        [HelpBlock("请输入合伙人所属用户名")]
        [Field(IsShowBaseSerach = true, PlaceHolder = "请输入用户名", DataField = "UserId",
            ValidType = ValidType.UserName, ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "110",
            ListShow = true, EditShow = true, SortOrder = 2)]
        [Required]
        public string UserName { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Display(Name = "介绍")]
        [Field(ControlsType = ControlsType.Editor, GroupTabId = 2, Width = "280", ListShow = false, EditShow = true,
            SortOrder = 2500)]
        public string Intro { get; set; }

        /// <summary>
        ///     详细地址
        /// </summary>
        [Display(Name = "详细地址")]
        [HelpBlock("请输入详细的地址")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "280", ListShow = false, EditShow = true,
            SortOrder = 601)]
        public string Address { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        [Display(Name = "备注")]
        [Field(ControlsType = ControlsType.Editor, GroupTabId = 2, Width = "280", ListShow = false, EditShow = true,
            SortOrder = 2501)]
        public string Remark { get; set; }

        /// <summary>
        ///     获取链接
        /// </summary>
        public IEnumerable<ViewLink> ViewLinks() {
            var userTypes = Ioc.Resolve<IAutoConfigService>().UserTypes();
            if (userTypes == null || userTypes.Count == 0) {
                return null;
            }
            var cityUserType = userTypes?.First(r => r.Id == UserTypeEnum.Partner.GetFieldId());
            var quickLinks = new List<ViewLink>
            {
                new ViewLink($"{cityUserType?.Name}添加", "/Admin/Basic/Edit?Service=IPartnerService&View=GetView",
                    Icons.Add,
                    LinkType.TableQuickLink),
                new ViewLink("编辑", "/Admin/Basic/Edit?Service=IPartnerService&View=GetView&Id=[[Id]]", Icons.Edit,
                    LinkType.ColumnLink),
                new ViewLink("删除", "/Admin/Basic/Delete?Service=IPartnerService&Method=Delete&Id=[[Id]]", Icons.Delete,
                    LinkType.ColumnLinkDelete),
                new ViewLink($"{cityUserType?.Name}管理",
                    "/Admin/Basic/Delete?Service=IPartnerService&Method=Delete&Id=[[Id]]", Icons.List,
                    LinkType.FormQuickLink)
            };

            return quickLinks;
        }

        /// <summary>
        /// 通用分页查询列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PageResult<PartnerView> PageTable(object query, AutoBaseModel autoModel) {
            var list = Resolve<IPartnerService>().GetPageList(query);
            return ToPageResult(list);
        }

        public List<TableAction> Actions() {
            var list = new List<TableAction>
            {
                ToLinkAction("内部合伙人", "/PartnerView/list")
            };
            return list;
        }
    }
}