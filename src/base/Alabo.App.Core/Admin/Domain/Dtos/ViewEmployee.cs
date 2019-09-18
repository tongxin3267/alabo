﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Employes.Domain.Entities;
using Alabo.App.Core.Employes.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Maps;
using Alabo.UI;
using Alabo.UI.AutoForms;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Admin.Domain.Dtos {

    [AutoDelete(IsAuto = true)]
    public class ViewEmployee : UIBase, IAutoTable<ViewEmployee>, IAutoForm {

        /// <summary>
        /// id 编辑时候用
        /// </summary>
        [Display(Name = "id")]
        [HelpBlock("填写员工的名称，长度不能超过10个字")]
        [Field(ControlsType = ControlsType.Hidden, EditShow = true, ListShow = true)]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 员工名称
        /// </summary>
        [Display(Name = "员工名称")]
        [HelpBlock("填写员工的名称，长度不能超过10个字")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", IsShowBaseSerach = true, EditShow = true,
            IsShowAdvancedSerach = true, ListShow = true, SortOrder = 1)]
        public string Name { get; set; }

        /// <summary>
        /// 所属用户名
        /// </summary>
        [Display(Name = "所属用户名")]
        [HelpBlock("输入员工所关联的用户名。如果用户名不存在，则自动添加会员账号，默认密码为：<x-code>admin_112233</x-code>")]
        [Field(ControlsType = ControlsType.DataSelect, ApiDataSource = "Api/Auto/Table?type=ViewAdminUser", GroupTabId = 1, Width = "150", IsShowBaseSerach = true, EditShow = true,
            IsShowAdvancedSerach = true, ListShow = true, SortOrder = 2)]
        public string UserName { get; set; }

        /// <summary>
        /// 所属用户名
        /// </summary>
        [Display(Name = "用户id")]
        [Field(ControlsType = ControlsType.Hidden,
            EditShow = true, ListShow = true)]
        public long UserId { get; set; }

        /// <summary>
        /// 岗位、角色，部门ID
        /// </summary>
        [Display(Name = "岗位")]
        [Required]
        [HelpBlock("请选择岗位")]
        [Field(ControlsType = ControlsType.DropdownList, ApiDataSource = "Api/PostRole/GetKeyValue", GroupTabId = 1, Width = "150", IsShowBaseSerach = true,
           EditShow = true, ListShow = false, SortOrder = 1)]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId PostRoleId { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        [Display(Name = "岗位")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", IsShowBaseSerach = true,
               EditShow = false, ListShow = true, SortOrder = 1)]
        public string PostRoleName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否有效")]
        [Required]
        [HelpBlock("该员工是否有效")]
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1, Width = "150", IsShowBaseSerach = true,
         EditShow = true,
         ListShow = true, SortOrder = 1)]
        public bool IsEnable { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, ListShow = true, EditShow = false, SortOrder = 10001,
            Width = "160")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 员工详细说明
        /// </summary>
        [Display(Name = "详细说明")]
        [HelpBlock("输入员工的详细介绍")]
        [Field(ControlsType = ControlsType.TextArea, GroupTabId = 1, Width = "150", IsShowBaseSerach = true, EditShow = true,
            IsShowAdvancedSerach = true, ListShow = true, SortOrder = 99)]
        public string Intro { get; set; }

        public List<TableAction> Actions() {
            return new List<TableAction>
            {
                ToLinkAction("编辑", "Edit",TableActionType.ColumnAction),
                ToLinkAction("删除", "/Api/Employee/QueryDelete",ActionLinkType.Delete,TableActionType.ColumnAction)
            };
        }

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var model = new Employee();
            if (ObjectId.TryParse(id.ToString(), out ObjectId objectId)) {
                model = Resolve<IEmployeeService>().GetSingle(s => s.Id == objectId);
            } else {
                return ToAutoForm(new ViewEmployee());
            }

            if (model == null) {
                return ToAutoForm(new ViewEmployee());
            }

            var resultEntity = model.MapTo<ViewEmployee>();
            var user = Resolve<IUserService>().GetSingle(s => s.Id == resultEntity.UserId);
            resultEntity.UserName = user?.UserName;
            return ToAutoForm(resultEntity);
        }

        public PageResult<ViewEmployee> PageTable(object query, AutoBaseModel autoModel) {
            var model = Resolve<IEmployeeService>().GetPagedList(query);
            var result = model.Result.MapTo<List<ViewEmployee>>();
            result.ForEach(item => {
                //   item.PostRoleName = Resolve<IPostRoleOldService>().GetSingle(s => s.Id == item.PostRoleId)?.Name;
            });
            return ToPageResult(PagedList<ViewEmployee>.Create(result, model.RecordCount, 15, model.PageIndex));
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var result = ServiceResult.FailedMessage("保存失败");
            var entity = model.MapTo<Employee>();

            var user = Resolve<IUserService>().GetSingle(s => s.Name == entity.UserName || s.UserName == entity.UserName);
            if (user == null) {
                return ServiceResult.FailedMessage("找不到该用户");
            }

            if (!(user?.Id).HasValue) {
                return ServiceResult.FailedMessage("找不到该用户");
            }

            var employee = Resolve<IEmployeeService>().GetSingle(s => s.UserId == user.Id);
            //如果没有 就赋值
            if (employee == null) {
                entity.UserId = user.Id;
            } else if (employee.Id == entity.Id)//如果存在,判断id是否当前的id 如果相等 则为修改
            {
                entity.UserId = user.Id;
            } else {
                //其他均为非法
                return ServiceResult.FailedMessage($"该用户已关联员工:{employee.Name}");
            }
            if (Resolve<IEmployeeService>().AddOrUpdate(entity)) {
                result = ServiceResult.Success;
            }

            return result;
        }
    }
}