using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.Data.People.Internals.Domain.CallBacks;
using Alabo.Data.People.Internals.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Framework.Core.WebUis.Design.AutoTables;
using Alabo.Mapping;
using Alabo.Web.Mvc.Attributes;
using AutoMapper;

namespace Alabo.Data.People.Internals.Dtos {

    public class InternalView : UIBase, IAutoForm, IAutoTable {

        [Display(Name = "名称")]
        [Field(ListShow = true, EditShow = true, ControlsType = ControlsType.TextBox, SortOrder = 700)]
        public string Name { get; set; }

        [Display(Name = "所属用户名")]
        [Field(ListShow = true, EditShow = true, ControlsType = ControlsType.TextBox, SortOrder = 700)]
        public string UserName { get; set; }

        public long UserId { get; set; }

        [Display(Name = "合伙人等级")]
        [HelpBlock("请设置合伙人等级")]
        [Field(ControlsType = ControlsType.DropdownList,
            DataSourceType = typeof(InternalGradeConfig), GroupTabId = 1, Width = "180",
            ListShow = false, EditShow = true, SortOrder = 1)]
        public Guid Grade { get; set; }

        [Display(Name = "推荐人用户名")]
        [Field(ListShow = true, EditShow = true, ControlsType = ControlsType.TextBox, SortOrder = 700)]
        public string ParentUserName { get; set; }

        public long ParentUserId { get; set; }

        [Display(Name = "详细地址")]
        [Field(ListShow = true, EditShow = true, ControlsType = ControlsType.TextBox, SortOrder = 700)]
        public string Address { get; set; }

        [Display(Name = "状态")]
        [Field(ListShow = true, EditShow = true, ControlsType = ControlsType.RadioButton, SortOrder = 700)]
        public UserTypeStatus Status { get; set; } = UserTypeStatus.Success;

        public List<TableAction> Actions() {
            return new List<TableAction>();
        }

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var str = id.ToString();
            var model = Resolve<IParentInternalService>().GetSingle(u => u.Id == str.ToObjectId());
            if (model != null) {
                var view = Mapper.Map<InternalView>(model);
                view.UserName = Resolve<IUserService>().GetSingle(u => u.Id == view.UserId)?.UserName;
                return ToAutoForm(view);
            }

            return ToAutoForm(new InternalView());
        }

        public PageResult<InternalView> PageTable(object query, AutoBaseModel autoModel) {
            var model = Resolve<IParentInternalService>().GetPagedList(query);
            var view = new List<InternalView>();
            foreach (var item in model) {
                var cityView = Mapper.Map<InternalView>(item);
                view.Add(cityView);
            }

            var result = new PageResult<InternalView>();
            result = Mapper.Map<PageResult<InternalView>>(model);
            result.Result = view;

            return result;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var view = (InternalView)model;
            var users = Resolve<IUserService>().GetList(u => u.UserName == view.UserName || u.UserName == view.ParentUserName);
            if (users.FirstOrDefault(u => u.UserName == view.UserName) == null) {
                return ServiceResult.FailedWithMessage("所属用户名不存在");
            }
            if (users.FirstOrDefault(u => u.UserName == view.ParentUserName) == null) {
                return ServiceResult.FailedWithMessage("推荐人用户名不存在");
            }
            var viewModel = Resolve<IParentInternalService>().GetSingle(u => u.UserId == view.UserId);
            if (viewModel != null) {
                return ServiceResult.FailedWithMessage("该用户已经是合伙人了");
            }
            var saveModel = AutoMapping.SetValue<Domain.Entities.ParentInternal>(view);
            saveModel.ParentUserId = users.FirstOrDefault(u => u.UserName == view.ParentUserName).Id;
            var result = Resolve<IParentInternalService>().AddOrUpdate(saveModel);
            if (result) {
                return ServiceResult.Success;
            }
            return ServiceResult.FailedWithMessage("操作失败");
        }
    }
}