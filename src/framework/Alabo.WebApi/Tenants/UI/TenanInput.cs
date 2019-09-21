using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Tenants.Callbacks;
using Alabo.App.Core.Tenants.Domains.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Helpers;
using Alabo.Maps;
using Alabo.Tenants.Domain.Entities;
using Alabo.Tenants.Domain.Services;
using Alabo.UI;
using Alabo.UI.AutoForms;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Tenants.UI {

    /// <summary>
    /// 自定义商城管理
    /// </summary>
    [ClassProperty(Name = "自定义商城管理", Icon = "fa fa-puzzle-piece", Description = "自定义商城管理", ListApi = "",
        PageType = ViewPageType.List, PostApi = "")]
    public class TenantInput : UIBase, IAutoForm, IAutoTable<TenantInput> {

        [Field(ControlsType = ControlsType.Hidden, SortOrder = 4, EditShow = false, ListShow = false)]
        public string Id { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        [Display(Name = "用户名")]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 4, EditShow = false, ListShow = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 租户用户名
        /// </summary>
        [Display(Name = "用户名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 4, EditShow = true, ListShow = true)]
        [HelpBlock("租户用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 租户用户名
        /// </summary>
        [Display(Name = "租户标识")]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 4, EditShow = false, ListShow = true)]
        [HelpBlock("租户标识")]
        public string Sign { get; set; }

        [Display(Name = "数据库名称")]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 4, EditShow = false, ListShow = true)]
        public string DatabaseName { get; set; }

        /// <summary>
        /// Api服务器
        /// </summary>
        [Display(Name = "Api服务器")]
        [Field(ControlsType = ControlsType.DropdownList, DataSourceType = typeof(TenantServiceHostConfig),
            Width = "200", SortOrder = 4, EditShow = true)]
        [HelpBlock("租户Api后台网址服务器，对应后台.net core 程序，一个Api服务器地址可以对应多个地址")]
        public Guid TenantServiceHostConfigId { get; set; }

        /// <summary>
        /// 前台链接
        /// </summary>
        [Display(Name = "前台链接")]
        [Field(ControlsType = ControlsType.DropdownList, DataSourceType = typeof(TenantClientHostConfig), Width = "200",
            SortOrder = 4, EditShow = true)]
        [HelpBlock("租户前台访问地址，对应后台zkweb程序")]
        public Guid TenantClientHostConfigId { get; set; }

        /// <summary>
        /// Api服务器
        /// </summary>
        [Display(Name = "Api服务器")]
        [Field(Width = "200", SortOrder = 4, EditShow = false, ListShow = true)]
        public string ServiceUrl { get; set; }

        [Display(Name = "前台链接")]
        [Field(Width = "200", SortOrder = 4, EditShow = false, ListShow = true)]
        public string ClientUrl { get; set; }

        /// <summary>
        /// </summary>
        [Display(Name = "管理员支付密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.Password, SortOrder = 300, EditShow = true)]
        [HelpBlock("输入当前管理员密码，必须是超级管理员Admin才可以操作该功能")]
        public string PayPassword { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, ListShow = true, EditShow = false, SortOrder = 10001,
            Width = "160")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        public List<TableAction> Actions() {
            var list = new List<TableAction>
            {
                ToLinkAction("编辑", $"Edit"),
                ToLinkAction("管理", $"view")
            };
            return list;
        }

        [ApiAuth(Filter = FilterType.Admin)]
        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var model = Resolve<ITenantService>().GetSingle(id) ?? new Tenant();
            var viewModel = model.MapTo<TenantInput>();
            return ToAutoForm(viewModel);
        }

        [ApiAuth(Filter = FilterType.Admin)]
        public PageResult<TenantInput> PageTable(object query, AutoBaseModel autoModel) {
            var listTenant = Resolve<ITenantService>().GetPagedList(query);
            var viewList = listTenant.MapTo<PagedList<TenantInput>>();

            viewList.ForEach(x => {
                x.ClientUrl = $"{x.ClientUrl}?tenant={x.Sign}";
                x.UserName = GetUserStyle(x.UserId);
            });
            return ToPageResult(viewList);
        }

        public string GetUserStyle(long userId) {
            var user = Resolve<IUserService>().GetSingle(userId);
            if (user == null) {
                return string.Empty;
            }

            var gradeConfig = Resolve<IGradeService>().GetGrade(user.GradeId);
            var userName =
                $" <img src='{Resolve<IApiService>().ApiImageUrl(gradeConfig.Icon)}' alt='{gradeConfig.Name}' class='user-pic' style='width:18px;height:18px;' /><a class='primary-link margin-8' href='/Admin/User/Edit?id=" +
                $"{user.Id}' title='{user.UserName}({user.Name}) 等级:{gradeConfig?.Name}'>{user.UserName}({user.Name})</a>";

            return userName;
        }

        [ApiAuth(Filter = FilterType.Admin)]
        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var find = model.MapTo<TenantInput>();
            var user = Resolve<IUserService>().GetSingle(u => u.UserName == find.UserName);
            if (user == null) {
                return new ServiceResult(false, new List<string> { "不存在的用户名" });
            }
            find.UserId = user.Id;
            try {
                return Ioc.Resolve<ITenantCreateService>().Create(find);
            } catch (Exception exc) {
                return new ServiceResult(false, new List<string> { $"发生错误, {exc.Message}!" });
            }
        }
    }
}