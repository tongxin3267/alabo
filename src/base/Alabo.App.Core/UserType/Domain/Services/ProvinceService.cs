using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Core.UserType.Domain.Entities.Extensions;
using Alabo.App.Core.UserType.Modules.Province;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Mapping;

namespace Alabo.App.Core.UserType.Domain.Services {

    /// <summary>
    ///     Class ProvinceService.
    /// </summary>
    /// <seealso cref="Alabo.Domains.Services.ServiceBase" />
    /// <seealso cref="Alabo.App.Core.UserType.Domain.Services.IProvinceService" />
    public class ProvinceService : ServiceBase, IProvinceService {

        public ProvinceService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// 添加城市服务中心
        /// 已经验证过
        /// </summary>
        /// <param name="ProvinceView"></param>
        public ServiceResult AddOrUpdate(ProvinceView ProvinceView) {
            var result = ServiceResult.Success;
            var ctiyUserTypeConfig = Resolve<IAutoConfigService>().UserTypes()
                .FirstOrDefault(r => r.Id == ProvinceView.UserTypeId);
            if (ctiyUserTypeConfig == null) {
                return ServiceResult.FailedWithMessage("用户类型不存在，或状态不正常，请配置");
            }

            // 所属会员处理
            var user = Resolve<IUserService>().GetSingle(ProvinceView.UserName);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户不存在");
            }

            ProvinceView.UserId = user.Id;

            // 判断会员类型是否重复添加,一个会员只有能有一个会员类型
            var userTypeExist = Resolve<IUserTypeService>().GetSingle(ProvinceView.UserId, ProvinceView.UserTypeId);
            if (userTypeExist != null && userTypeExist.Id != ProvinceView.Id) {
                return ServiceResult.FailedWithMessage($"该会员已经是{ctiyUserTypeConfig.Name},不能重复添加");
            }

            var userType = AutoMapping.SetValue<Entities.UserType>(ProvinceView);
            var parentUser = Resolve<IUserService>().GetSingle(ProvinceView.ParentUserName);
            if (parentUser != null) {
                userType.ParentUserId = parentUser.Id;
            }

            #region 城市区域验证

            // 城市区域Id
            userType.EntityId = ProvinceView.HttpContext.Request.Form["EntityId"].ConvertToLong(0);

            var region = Resolve<IRegionService>().GetSingle(r => r.RegionId == userType.EntityId);
            if (region == null) {
                return ServiceResult.FailedWithMessage("城市区域不能为空");
            }

            // 验证区域唯一性
            var regionUserType = Resolve<IUserTypeService>()
                .GetSingle(r => r.EntityId == userType.EntityId && r.UserTypeId == ctiyUserTypeConfig.Id);
            if (regionUserType != null) {
                if (regionUserType.UserId != userType.UserId) {
                    return ServiceResult.FailedWithMessage("改区域已存在所属人，不能重复添加");
                }
            }

            #endregion 城市区域验证

            userType.UserTypeExtensions = new UserTypeExtensions {
                Intro = ProvinceView.Remark,
                Remark = ProvinceView.Remark,
                ModifiedTime = DateTime.Now
            };
            userType.Extensions = userType.UserTypeExtensions.ToJson(); //  扩展属性

            try {
                if (userType.Id > 0) {
                    var dateUserType = Resolve<IUserTypeService>().GetByIdNoTracking(userType.Id);
                    dateUserType = AutoMapping.SetValue<Entities.UserType>(userType);
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
        public PagedList<ProvinceView> GetPageList(object query) {
            var dictionary = query.DeserializeJson<Dictionary<string, string>>();
            // 查询条件加上UserTypeId
            dictionary.Add("UserTypeId", UserTypeEnum.Province.GetFieldId().ToString());
            var userTypePageList = Resolve<IUserTypeService>().GetPagedList(dictionary.ToJson());

            // userTypePageList = userTypePageList.Where(r => r.UserTypeId == UserTypeEnum.Province.GetFieldId());

            var ProvinceGrades = Resolve<IGradeService>().GetGradeListByGuid(UserTypeEnum.Province.GetFieldId());
            var ProvinceViews = new List<ProvinceView>();
            foreach (var item in userTypePageList) {
                var ProvinceView = AutoMapping.SetValue<ProvinceView>(item);
                if (ProvinceView.UserTypeId == UserTypeEnum.Province.GetFieldId()) {
                    ProvinceView.GradeName = ProvinceGrades.FirstOrDefault(r => r.Id == item.GradeId)?.Name;
                    // 此方法有性能问题，后期放到程序外 显示城市名称
                    ProvinceView.RegionName = Resolve<IRegionService>().GetFullName(ProvinceView.EntityId);
                    var UserName = Resolve<IUserService>().GetSingle(u => u.Id == item.ParentUserId);
                    if (UserName != null) {
                        ProvinceView.ParentUserName = UserName.UserName;
                    }

                    ProvinceViews.Add(ProvinceView);
                }
            }

            return PagedList<ProvinceView>.Create(ProvinceViews, userTypePageList.RecordCount,
                userTypePageList.PageSize, userTypePageList.PageIndex);
        }

        public ProvinceView GetView(long id) {
            var provinceView = new ProvinceView();
            if (id > 0) {
                var userType = Resolve<IUserTypeService>().GetByIdNoTracking(id);
                if (userType != null) {
                    provinceView = AutoMapping.SetValue<ProvinceView>(userType);
                    provinceView.UserName = Resolve<IUserService>().GetSingle(provinceView.UserId)?.UserName;
                    provinceView.ParentUserName = Resolve<IUserService>().GetSingle(provinceView.ParentUserId).UserName;
                }
            }

            return provinceView;
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