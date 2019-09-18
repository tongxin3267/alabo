using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Core.UserType.Domain.Entities.Extensions;
using Alabo.App.Core.UserType.Modules.City;
using Alabo.App.Core.UserType.ViewModels;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Mapping;

namespace Alabo.App.Core.UserType.Domain.Services {

    /// <summary>
    ///     Class CityService.
    /// </summary>
    /// <seealso cref="Alabo.Domains.Services.ServiceBase" />
    /// <seealso cref="Alabo.App.Core.UserType.Domain.Services.ICityService" />
    public class CityService : ServiceBase, ICityService {

        public CityService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// 添加城市服务中心
        /// 已经验证过
        /// </summary>
        /// <param name="cityView"></param>
        public ServiceResult AddOrUpdate(CityView cityView) {
            var result = ServiceResult.Success;
            var ctiyUserTypeConfig = Resolve<IAutoConfigService>().UserTypes()
                .FirstOrDefault(r => r.Id == cityView.UserTypeId);
            if (ctiyUserTypeConfig == null) {
                return ServiceResult.FailedWithMessage("用户类型不存在，或状态不正常，请配置");
            }

            // 所属会员处理
            var user = Resolve<IUserService>().GetSingle(cityView.UserName);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户不存在");
            }

            cityView.UserId = user.Id;

            // 判断会员类型是否重复添加,一个会员只有能有一个会员类型
            var userTypeExist = Resolve<IUserTypeService>().GetSingle(cityView.UserId, cityView.UserTypeId);
            if (userTypeExist != null && userTypeExist.Id != cityView.Id) {
                return ServiceResult.FailedWithMessage($"该会员已经是{ctiyUserTypeConfig.Name},不能重复添加");
            }

            var userType = AutoMapping.SetValue<Entities.UserType>(cityView);
            var parentUser = Resolve<IUserService>().GetSingle(cityView.ParentUserName);
            if (parentUser != null) {
                userType.ParentUserId = parentUser.Id;
            }

            #region 城市区域验证

            // 城市区域Id
            userType.EntityId = cityView.HttpContext.Request.Form["EntityId"].ConvertToLong(0);

            var region = Resolve<IRegionService>().GetSingle(r => r.RegionId == userType.EntityId);
            if (region == null) {
                return ServiceResult.FailedWithMessage("城市区域不能为空");
            }

            if (region.ParentId == 0) {
                return ServiceResult.FailedWithMessage($"您选择的{region.Name}不是城市，请选择城市");
            }

            // 验证区域唯一性
            var regionUserType = Resolve<IUserTypeService>()
                .GetSingle(r => r.EntityId == userType.EntityId && r.UserTypeId == ctiyUserTypeConfig.Id);
            if (regionUserType != null) {
                if (regionUserType.UserId != userType.UserId) {
                    return ServiceResult.FailedWithMessage("该区域已存在所属人，不能重复添加");
                }
            }

            #endregion 城市区域验证

            userType.UserTypeExtensions = new UserTypeExtensions {
                Intro = cityView.Intro,
                Remark = cityView.Remark,
                ModifiedTime = DateTime.Now
            };
            userType.Extensions = userType.UserTypeExtensions.ToJson(); //  扩展属性

            try {
                if (userType.Id > 0) {
                    var dateUserType = Resolve<IUserTypeService>().GetSingle(r => r.Id == userType.Id);
                    dateUserType = AutoMapping.SetValue(userType, dateUserType);
                    Resolve<IUserTypeService>().Update(dateUserType);
                } else {
                    Resolve<IUserTypeService>().Add(userType);
                }
            } catch (Exception ex) {
                return ServiceResult.FailedWithMessage("更新失败" + ex.Message);
            }

            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="query"></param>
        public PagedList<CityView> GetPageList(object query) {
            var dictionary = new Dictionary<string, string>
            {
                {"UserTypeId", UserTypeEnum.City.GetFieldId().ToString()}
            };
            var userTypePageList = Resolve<IUserTypeService>().GetPagedList(query, dictionary);
            var Grades = Resolve<IAutoConfigService>()
                .GetSingle(u => u.Type == "Alabo.App.Core.UserType.Modules.City.CityGradeConfig");
            var GradesList = Grades.Value.DeserializeJson<List<ViewCityGrade>>();

            var cityViews = new List<CityView>();
            foreach (var item in userTypePageList) {
                var cityView = AutoMapping.SetValue<CityView>(item);
                if (cityView.UserTypeId == UserTypeEnum.City.GetFieldId()) {
                    cityView.GradeName = GradesList.FirstOrDefault(r => r.Id == item.GradeId)?.Name;

                    // 此方法有性能问题，后期放到程序外 显示城市名称
                    cityView.RegionName = Resolve<IRegionService>().GetFullName(cityView.EntityId);
                    var UserName = Resolve<IUserService>().GetSingle(u => u.Id == item.ParentUserId);
                    if (UserName != null) {
                        cityView.ParentUserName = UserName.UserName;
                    }

                    cityViews.Add(cityView);
                }
            }

            return PagedList<CityView>.Create(cityViews, userTypePageList.RecordCount, userTypePageList.PageSize,
                userTypePageList.PageIndex);
        }

        /// <summary>
        /// </summary>
        /// <param name="id">主键ID</param>
        public CityView GetView(long id) {
            var cityView = new CityView();
            if (id > 0) {
                var userType = Resolve<IUserTypeService>().GetSingle(r => r.Id == id);
                if (userType != null) {
                    cityView = AutoMapping.SetValue<CityView>(userType);
                    cityView.UserName = Resolve<IUserService>().GetSingle(cityView.UserId)?.UserName;
                    try {
                        cityView.ParentUserName = Resolve<IUserService>().GetSingle(cityView.ParentUserId).UserName;
                    } catch {
                        cityView.ParentUserName = null;
                    }

                    if (cityView.Extensions != null) {
                        var cityViewExtensions = cityView.Extensions.DeserializeJson<CityView>();
                        cityView.Intro = cityViewExtensions.Intro;
                        cityView.Remark = cityViewExtensions.Remark;
                    }
                }
            }

            return cityView;
        }

        /// <summary>
        ///     删除城市合伙人
        /// </summary>
        /// <param name="id">主键ID</param>
        public ServiceResult Delete(long id) {
            var result = Resolve<IUserTypeService>().Delete(r => r.Id == id);
            if (result) {
                return ServiceResult.Success;
            }

            return ServiceResult.Failed;
        }

        /// <summary>
        ///     根据区域获取城市代理
        /// </summary>
        /// <param name="regionId"></param>
        public User.Domain.Entities.User GetCityUserType(long regionId) {
            var userType = Resolve<IUserTypeService>()
                .GetSingle(r => r.UserTypeId == UserTypeEnum.City.GetFieldId() && r.EntityId == regionId);
            if (userType != null) {
                return Resolve<IUserService>().GetSingle(userType.UserId);
            }

            return null;
        }
    }
}