﻿using Alabo.Data.People.Users.Domain.Services;
using Alabo.Data.People.Users.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Maps;
using Alabo.UI;
using Alabo.UI.Design.AutoForms;
using Alabo.UI.Design.AutoLists;
using Alabo.UI.Design.AutoTables;
using Alabo.Users.Enum;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Data.People.Users.ViewModels
{
    /// <summary>
    ///     会员管理 (租户)
    /// </summary>
    [ClassProperty(Name = "会员管理", Icon = "fa fa-puzzle-piece", Description = "管理系统所有会员",
        
        PageType = ViewPageType.List )]
    [BsonIgnoreExtraElements]
    public class ViewCrmUser : UIBase, IAutoTable<ViewUser>, IAutoForm, IAutoList
    {
        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            return null;
        }

        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            var view = new ViewUser();

            if (!string.IsNullOrEmpty(id.ToString()))
            {
                var result = Resolve<IUserService>().GetSingle(id.ConvertToLong());
                if (result == null) {
                    return ToAutoForm(view);
                }

                var model = result.MapTo<AutoForm>();

                return ToAutoForm(model);
            }

            return ToAutoForm(view);
        }

        public PageResult<AutoListItem> PageList(object query, AutoBaseModel autoModel)
        {
            var dic = query.ToObject<Dictionary<string, string>>();

            dic.TryGetValue("loginUserId", out var userId);

            dic.TryGetValue("pageIndex", out var pageIndexStr);
            dic.TryGetValue("Status", out var userStatus);
            if (string.IsNullOrEmpty(userStatus)) {
                userStatus = "1";
            }

            var pageIndex = pageIndexStr.ToInt64();
            if (pageIndex <= 0) {
                pageIndex = 1;
            }

            var userInput = new UserInput
            {
                PageIndex = (int)pageIndex,
                //ParentId = userId.ToInt64(),
                PageSize = 15,
                Status = userStatus == "1" ? Status.Normal : userStatus == "2" ? Status.Freeze : Status.Deleted
            };

            var model = Resolve<IUserService>().GetViewUserPageList(userInput);

            var list = new List<AutoListItem>();
            foreach (var item in model)
            {
                // var grade = Resolve<IGradeService>().GetGrade(item.GradeId);
                var apiData = new AutoListItem
                {
                    Title = item.UserName.ReplaceHtmlTag(), //标题
                    Intro = $"{item.Mobile}/{item.CreateTime.ToString("yyyy-MM-dd hh:ss")}", //简介
                    Value = item.GradeName, //会员等级
                    Image = Resolve<IApiService>()
                        .ApiImageUrl(item.Avator), //users.FirstOrDefault(u => u.UserId == item.Id)?.Avator,//左边头像
                    Id = item.Id //id
                };

                list.Add(apiData);
            }

            return ToPageList(list, model);
        }

        public Type SearchType()
        {
            return typeof(ViewCrmUser);
        }

        public List<TableAction> Actions()
        {
            var rsList = new List<TableAction>
            {
                ToLinkAction("编辑", "Edit", TableActionType.ColumnAction),
                ToLinkAction("资产", "Account", TableActionType.ColumnAction),
                ToLinkAction("删除", "Api/User/Delete", ActionLinkType.Delete, TableActionType.ColumnAction)
            };

            return rsList;
        }

        public PageResult<ViewUser> PageTable(object query, AutoBaseModel autoModel)
        {
            var userInput = ToQuery<UserInput>();
            if (autoModel.Filter == FilterType.Admin)
            {
                var model = Resolve<IUserService>().GetViewUserPageList(userInput);
                return ToPageResult(model);
            }

            if (autoModel.Filter == FilterType.User)
            {
                // 查找自己推荐的会员
                // userInput.ParentId = autoModel.BasicUser.Id;
                var model = Resolve<IUserService>().GetViewUserPageList(userInput);
                return ToPageResult(model);
            }

            throw new ValidException("类型权限不正确");
        }

        /// <summary>
        ///     获取s the avator.
        /// </summary>
        /// <param name="size">The size.</param>
        public string GetAvator(int size = 48)
        {
            if (Avator.IsNullOrEmpty()) {
                return $@"/wwwroot/static/images/avator/{Sex}_{size}.png";
            }

            return Avator;
        }

        /// <summary>
        ///     获取s the name of the 会员.
        /// </summary>
        public string GetUserName()
        {
            var name = $@"{UserName}({Name})";
            return name;
        }

        /// <summary>
        ///     操作链接
        /// </summary>
        public IEnumerable ViewLinks()
        {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("添加会员", "/Admin/User/Add", Icons.Edit, LinkType.TableQuickLink),
                new ViewLink("会员等级",
                    "/Admin/AutoConfig/List?key=Alabo.App.Core.User.Domain.CallBacks.UserGradeConfig", "fa fa-signal",
                    LinkType.TableQuickLink),
                new ViewLink("详情", "/Admin/User/Edit?id=[[Id]]", Icons.Edit, LinkType.ColumnLink),
                new ViewLink("Ta的推荐", "/Admin/user/ParentUser?UserId=[[Id]]", Icons.Edit, LinkType.ColumnLink)
                //new ViewLink("前台登录", "/admin/user/loginother?userId=[[Id]]", Icons.Edit, LinkType.ColumnLink)
            };
            return quickLinks;
        }

        #region

        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden, EditShow = false, GroupTabId = 1, ListShow = false)]
        [Key]
        public long Id { get; set; }

        /// <summary>
        ///     用户名（以字母开头，包括a-z,0-9和_）：[a-zA-z][a-zA-Z0-9_]{2,15}
        /// </summary>
        [Display(Name = "用户名")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, PlaceHolder = "请输入用户名，一般与手机号相同",
            IsShowAdvancedSerach = true, DataField = "UserId", GroupTabId = 1, IsMain = true, Width = "150",
            EditShow = true,
            ListShow = true, SortOrder = 2, Link = "/Admin/User/Edit?id=[[Id]]")]
        public string UserName { get; set; }

        /// <summary>
        ///     姓名
        /// </summary>
        [Display(Name = "姓名")]
        [HelpBlock("请输入姓名")]
        [Field(ControlsType = ControlsType.TextBox, IsMain = true, GroupTabId = 1, Width = "150", ListShow = true,
            EditShow = true,
            SortOrder = 3, Link = "/Admin/User/Edit?id=[[Id]]")]
        public string Name { get; set; }

        /// <summary>
        ///     手机号码
        /// </summary>
        [Display(Name = "手机号")]
        [HelpBlock("请输入手机号")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, EditShow = true,
            IsShowBaseSerach = true, IsShowAdvancedSerach = true, SortOrder = 4)]
        public string Mobile { get; set; }

        /// <summary>
        ///     邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        [HelpBlock("请输入邮箱")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", IsShowBaseSerach = true,
            EditShow = true,
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
        [HelpBlock("推荐人用户名")]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true,
            Link = "/Admin/User/Edit?id=[[ParentId]]", SortOrder = 6)]
        public string ParentName { get; set; }

        /// <summary>
        ///     用户等级Id
        ///     每个类型对应一个等级
        /// </summary>
        [Display(Name = "等级")]
        [Field(ControlsType = ControlsType.DropdownList, LabelColor = LabelColor.Info, IsShowAdvancedSerach = true,
            DataSource = "Alabo.App.Core.User.Domain.Callbacks.UserGradeConfig", EditShow = false, GroupTabId = 1,
            Width = "150",
            ListShow = false, SortOrder = 5)]
        public Guid GradeId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the grade.
        /// </summary>
        [Display(Name = "等级")]
        [HelpBlock("请输入等级")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Info, EditShow = false, GroupTabId = 1,
            Width = "150",
            ListShow = true, SortOrder = 5)]
        public string GradeName { get; set; }

        /// <summary>
        ///     是否认证通过
        /// </summary>
        [Display(Name = "实名?")]
        [HelpBlock("请确定实名")]
        [Field(ControlsType = ControlsType.DropdownList, GroupTabId = 1, Width = "150",
            DataSource = "Alabo.Framework.Core.Enums.Enum.IdentityStatus", ListShow = true, EditShow = false,
            SortOrder = 8)]
        public IdentityStatus IdentityStatus { get; set; } = IdentityStatus.IsNoPost;

        /// <summary>
        ///     性别
        /// </summary>
        [Display(Name = "性别")]
        [HelpBlock("选择您的性别")]
        [Field(ControlsType = ControlsType.RadioButton, GroupTabId = 1,
            DataSource = "Alabo.Framework.Core.Enums.Enum.Sex",
            Width = "150", ListShow = true, SortOrder = 10)]
        public Sex Sex { get; set; } = Sex.Man;

        /// <summary>
        ///     用户头像
        /// </summary>
        [Display(Name = "头像")]
        [HelpBlock("点击可上传头像，如留空将使用系统默认头像")]
        [Field(ControlsType = ControlsType.AlbumUploder, GroupTabId = 1, Width = "150", IsImagePreview = true,
            ListShow = true, SortOrder = 1)]
        public string Avator { get; set; } = @"/wwwroot/static/images/avator/Man_48.png";

        /// <summary>
        ///     用户状态
        /// </summary>
        [Display(Name = "状态")]
        [HelpBlock("请输入状态")]
        [Field(ControlsType = ControlsType.DropdownList, IsTabSearch = true,
            DataSource = "Alabo.Domains.Enums.Status",
            GroupTabId = 1, Width = "150", ListShow = true, EditShow = false, SortOrder = 1005)]
        public Status Status { get; set; }

        /// <summary>
        ///     注册时间
        /// </summary>
        [Display(Name = "注册时间")]
        [HelpBlock("请输入注册时间")]
        [Field(ControlsType = ControlsType.TimePicker, GroupTabId = 1, Width = "150", EditShow = false,
            ListShow = true, SortOrder = 1000)]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     Gets or sets the 会员 grade configuration.
        /// </summary>
        public UserGradeConfig UserGradeConfig { get; set; }

        #endregion
    }
}