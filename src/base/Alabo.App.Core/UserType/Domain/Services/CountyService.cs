using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Common.Domain.Enum;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Core.UserType.Domain.Entities.Extensions;
using Alabo.App.Core.UserType.Modules.County;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Mapping;

namespace Alabo.App.Core.UserType.Domain.Services {

    /// <summary>
    ///     Class countyService.
    /// </summary>
    /// <seealso cref="Alabo.Domains.Services.ServiceBase" />
    /// <seealso cref="Alabo.App.Core.UserType.Domain.Services.IcountyService" />
    public class CountyService : ServiceBase, ICountyService {

        public CountyService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// 添加城市服务中心
        /// 已经验证过
        /// </summary>
        /// <param name="CountyView"></param>
        public ServiceResult AddOrUpdate(CountyView countyView) {
            var result = ServiceResult.Success;
            var ctiyUserTypeConfig = Resolve<IAutoConfigService>().UserTypes()
                .FirstOrDefault(r => r.Id == countyView.UserTypeId);
            if (ctiyUserTypeConfig == null) {
                return ServiceResult.FailedWithMessage("用户类型不存在，或状态不正常，请配置");
            }

            // 所属会员处理
            var user = Resolve<IUserService>().GetSingle(countyView.UserName);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户不存在");
            }

            countyView.UserId = user.Id;

            // 判断会员类型是否重复添加,一个会员只有能有一个会员类型
            var userTypeExist = Resolve<IUserTypeService>().GetSingle(countyView.UserId, countyView.UserTypeId);
            if (userTypeExist != null && userTypeExist.Id != countyView.Id) {
                return ServiceResult.FailedWithMessage($"该会员已经是{ctiyUserTypeConfig.Name},不能重复添加");
            }

            var userType = AutoMapping.SetValue<Entities.UserType>(countyView);
            var parentUser = Resolve<IUserService>().GetSingle(countyView.ParentUserName);
            if (parentUser != null) {
                userType.ParentUserId = parentUser.Id;
            }

            #region 城市区域验证

            // 城市区域Id,从当前表单中获取值
            userType.EntityId = Alabo.Helpers.HttpWeb.GetValue<long>("EntityId");

            var region = Resolve<IRegionService>().GetSingle(r => r.RegionId == userType.EntityId);
            if (region == null) {
                return ServiceResult.FailedWithMessage("请选择正确的区县");
            }

            if (region.Level != RegionLevel.County) {
                return ServiceResult.FailedWithMessage("请选择正确的区县，您选中的不是区县");
            }

            if (region.RegionId == 0) {
                return ServiceResult.FailedWithMessage($"您选择的{region.Name}不是区县，请选择区县");
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
                Intro = countyView.Remark,
                Remark = countyView.Remark,
                Address = countyView.Address,
                ModifiedTime = DateTime.Now
            };
            userType.Extensions = userType.UserTypeExtensions.ToJson(); //  扩展属性

            try {
                if (userType.Id > 0) {
                    var dateUserType = Resolve<IUserTypeService>().GetSingle(r => r.Id == userType.Id);
                    dateUserType.EntityId = userType.EntityId;
                    dateUserType.ParentMap = userType.ParentMap;
                    dateUserType.Extensions = userType.Extensions;
                    dateUserType.GradeId = userType.GradeId;
                    dateUserType.Name = userType.Name;
                    dateUserType.UserId = userType.UserId;
                    dateUserType.ParentUserId = userType.ParentUserId;
                    dateUserType.Status = userType.Status;
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
        public PagedList<CountyView> GetPageList(object query) {
            var dictionary = query.DeserializeJson<Dictionary<string, string>>();
            //  // 查询条件加上UserTypeId
            // dictionary.Add("UserTypeId", UserTypeEnum.County.GetFieldId().ToString());

            var userTypePageList = Resolve<IUserTypeService>()
                .GetPagedList(query, r => r.UserTypeId == UserTypeEnum.County.GetFieldId());

            // userTypePageList = userTypePageList.Where(r => r.UserTypeId == UserTypeEnum.county.GetFieldId());

            var countyGrades = Resolve<IGradeService>().GetGradeListByGuid(UserTypeEnum.County.GetFieldId());
            var countyViews = new List<CountyView>();
            foreach (var item in userTypePageList) {
                var countyView = AutoMapping.SetValue<CountyView>(item);
                if (countyView.UserTypeId == UserTypeEnum.County.GetFieldId()) {
                    countyView.GradeName = countyGrades.FirstOrDefault(r => r.Id == item.GradeId)?.Name;
                    // 此方法有性能问题，后期放到程序外 显示城市名称
                    countyView.RegionName = Resolve<IRegionService>().GetFullName(item.EntityId);
                    var userName = Resolve<IUserService>().GetSingle(u => u.Id == item.ParentUserId);
                    if (userName != null) {
                        countyView.ParentUserName = userName.UserName;
                    }

                    countyViews.Add(countyView);
                }
            }

            return PagedList<CountyView>.Create(countyViews, userTypePageList.RecordCount, userTypePageList.PageSize,
                userTypePageList.PageIndex);
        }

        public CountyView GetView(long id) {
            var countyView = new CountyView();
            if (id > 0) {
                var userType = Resolve<IUserTypeService>().GetSingle(r => r.Id == id);
                if (userType != null) {
                    countyView = AutoMapping.SetValue<CountyView>(userType);
                    countyView.UserName = Resolve<IUserService>().GetSingle(countyView.UserId)?.UserName;
                    countyView.EntityId = userType.EntityId;
                    countyView.Address = userType.UserTypeExtensions.Address;
                    countyView.Remark = userType.UserTypeExtensions.Remark;
                    countyView.Intro = userType.UserTypeExtensions.Intro;
                    countyView.ParentUserName = Resolve<IUserService>().GetSingle(countyView.ParentUserId)?.UserName;
                }
            }

            return countyView;
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
    }
}