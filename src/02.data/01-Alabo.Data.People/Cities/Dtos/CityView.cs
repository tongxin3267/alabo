using Alabo.App.Agent.Citys.Domain.Entities;
using Alabo.App.Agent.Citys.Domain.Services;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Maps;
using Alabo.UI;
using Alabo.UI.AutoForms;
using Alabo.Web.Mvc.Attributes;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Core.WebUis.Design.AutoTables;

namespace Alabo.App.Agent.Citys.Domain.Dtos {

    public class CityView : UIBase, IAutoForm, IAutoTable<CityView> {
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "280", ListShow = true,
            SortOrder = 601)]
        public string Name { get; set; }

        /// <summary>
        /// 所属区域
        /// </summary>
        public long RegionId { get; set; }

        /// <summary>
        /// 所属区域
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 代理费
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     推荐人用户Id
        /// </summary>
        public long ParentUserId { get; set; }

        /// <summary>
        ///     推荐人用户Id
        /// </summary>
        [Display(Name = "推荐人")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "280", ListShow = true,
            SortOrder = 601)]
        public string ParentUserName { get; set; }

        /// <summary>
        ///     用户等级Id
        ///     每个类型对应一个等级
        /// </summary>
        public Guid GradeId { get; set; } = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a368000");

        /// <summary>
        ///     用户名
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 所属用户名
        /// </summary>
        [Display(Name = "所属用户名")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "280", ListShow = true,
            SortOrder = 601)]
        public string UserName { get; set; }

        /// <summary>
        ///     详细地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        public UserTypeStatus Status { get; set; } = UserTypeStatus.Pending;

        public List<TableAction> Actions() {
            return new List<TableAction>();
        }

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var str = id.ToString();
            var model = Resolve<ICityService>().GetSingle(u => u.Id == str.ToObjectId());
            if (model != null) {
                return ToAutoForm(model);
            }

            return ToAutoForm(new City());
        }

        public PageResult<CityView> PageTable(object query, AutoBaseModel autoModel) {
            var model = Resolve<ICityService>().GetPagedList(query);
            var citys = new List<CityView>();
            foreach (var item in model) {
                var cityView = Mapper.Map<CityView>(item);
                citys.Add(cityView);
            }

            var result = new PageResult<CityView>();
            result = Mapper.Map<PageResult<CityView>>(model);
            result.Result = citys;

            return result;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var city = (CityView)model;
            var user = Resolve<IUserService>().GetSingle(u => u.UserName == city.UserName);
            var parentUser = Resolve<IUserService>().GetSingle(u => u.UserName == city.ParentUserName);
            var view = city.MapTo<City>();
            if (user == null) {
                return ServiceResult.FailedWithMessage("所属用户名不存在");
            }

            if (parentUser == null) {
                return ServiceResult.FailedWithMessage("推荐人用户名不存在");
            }
            var partner = Resolve<ICityService>().GetSingle(u => u.RegionId == city.RegionId);

            view.Id = city.Id.ToObjectId();
            view.UserId = user.Id;
            view.ParentUserId = parentUser.Id;
            view.RegionName = Resolve<IRegionService>().GetRegionNameById(view.RegionId);
            if (city.Id.IsNullOrEmpty() && partner != null) {
                return ServiceResult.FailedWithMessage("该地区已有合伙人，一个地区只允许有一个合伙人");
            }

            var result = Resolve<ICityService>().AddOrUpdate(view);
            if (result) {
                return ServiceResult.Success;
            }
            return ServiceResult.FailedWithMessage("操作失败，请重试");
        }
    }
}