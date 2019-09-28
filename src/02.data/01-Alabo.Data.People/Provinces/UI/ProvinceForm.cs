using Alabo.Data.People.Provinces.Domain.Entities;
using Alabo.Data.People.Provinces.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Framework.Basic.Regions.Domain.Services;
using Alabo.Maps;
using Alabo.UI;
using Alabo.UI.Design.AutoForms;
using Alabo.UI.Design.AutoTables;
using System.Collections.Generic;

namespace Alabo.Data.People.Provinces.UI
{
    public class ProvinceForm : Province, IAutoForm, IAutoTable<ProvinceForm>
    {
        public List<TableAction> Actions()
        {
            return new List<TableAction>();
        }

        public PageResult<ProvinceForm> PageTable(object query, AutoBaseModel autoModel)
        {
            var list = Resolve<IProvinceService>().GetPagedList<ProvinceForm>(query);
            var result = new PagedList<ProvinceForm>();
            foreach (var item in list)
            {
                var model = new ProvinceForm
                {
                    CreateTime = item.CreateTime,
                    //GradeName = Resolve<IGradeService>().GetGrade(item.GradeId) != null ? Resolve<IGradeService>().GetGrade(item.GradeId).GetDisplayName() : "--",
                    Name = string.IsNullOrEmpty(item.Name) ? "--" : item.Name,
                };
                result.Add(model);
            }

            return ToPageResult(result);
        }

        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            var str = id.ToString();
            var model = Resolve<IProvinceService>().GetViewById(id);
            return ToAutoForm(model);
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            var city = model.MapTo<ProvinceForm>();
            var user = Resolve<IUserService>().GetSingle(u => u.UserName == city.UserName);
            if (user == null)
            {
                return ServiceResult.FailedWithMessage("您选择的用户不存在");
            }

            if (user.Status != Domains.Enums.Status.Normal)
            {
                return ServiceResult.FailedWithMessage("您选择的用户状态不正常");
            }

            if (city.ParentUserName.IsNotNullOrEmpty())
            {
                var parentUser = Resolve<IUserService>().GetSingle(u => u.UserName == city.ParentUserName);
                if (parentUser == null) { return ServiceResult.FailedWithMessage("推荐人用户名不存在"); }

                city.ParentId = parentUser.Id;
            }

            var view = city.MapTo<Province>();

            var partner = Resolve<IProvinceService>().GetSingle(u => u.RegionId == city.RegionId);

            view.Id = city.Id.ToObjectId();
            view.UserId = user.Id;
            view.FullName = Resolve<IRegionService>().GetRegionNameById(view.RegionId);
            if (city.Id.IsNullOrEmpty() && partner != null)
                return ServiceResult.FailedWithMessage("该地区已有合伙人，一个地区只允许有一个合伙人");

            var result = Resolve<IProvinceService>().AddOrUpdate(view);
            if (result) return ServiceResult.Success;
            return ServiceResult.FailedWithMessage("操作失败，请重试");
        }
    }
}