using Alabo.App.Agent.Province.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Maps;
using Alabo.UI;
using System;
using Alabo.Framework.Basic.Address.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;

namespace Alabo.App.Agent.Province.Domain.Dtos {

    public class ProvinceView : UIBase, IAutoForm {
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属区域
        /// </summary>
        public long RegionId { get; set; }

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

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var str = id.ToString();
            var model = Resolve<IProvinceService>().GetSingle(u => u.Id == str.ToObjectId());
            if (model != null) {
                return ToAutoForm(model);
            }

            return ToAutoForm(new Entities.Province());
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var city = (ProvinceView)model;
            var user = Resolve<IUserService>().GetSingle(u => u.UserName == city.UserName);
            var parentUser = Resolve<IUserService>().GetSingle(u => u.UserName == city.ParentUserName);
            var view = city.MapTo<Entities.Province>();
            if (user == null) {
                return ServiceResult.FailedWithMessage("所属用户名不存在");
            }

            if (parentUser == null) {
                return ServiceResult.FailedWithMessage("推荐人用户名不存在");
            }

            var partner = Resolve<IProvinceService>().GetSingle(u => u.RegionId == city.RegionId);

            view.Id = city.Id.ToObjectId();
            view.UserId = user.Id;
            view.ParentUserId = parentUser.Id;
            view.RegionName = Resolve<IRegionService>().GetRegionNameById(view.RegionId);
            if (city.Id.IsNullOrEmpty() && partner != null) {
                return ServiceResult.FailedWithMessage("该地区已有合伙人，一个地区只允许有一个合伙人");
            }

            //var result = false;
            //if (partner != null) {
            //    result = Resolve<ICountyService>().Update(view);
            //} else {
            //    result = Resolve<ICountyService>().Add(view);
            //}
            var result = Resolve<IProvinceService>().AddOrUpdate(view);
            if (result) {
                return ServiceResult.Success;
            }
            return ServiceResult.FailedWithMessage("操作失败，请重试");
        }
    }
}