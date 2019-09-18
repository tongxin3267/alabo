using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Core.UserType.Domain.Entities.Extensions;
using Alabo.App.Core.UserType.Domain.Enums;
using Alabo.App.Core.UserType.Domain.Repositories;
using Alabo.App.Core.UserType.Modules.ServiceCenter;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Mapping;

namespace Alabo.App.Core.UserType.Domain.Services {

    /// <summary>
    ///     Class ServiceCenterService.
    /// </summary>
    /// <seealso cref="Alabo.Domains.Services.ServiceBase" />
    /// <seealso cref="Alabo.App.Core.UserType.Domain.Services.IServiceCenterService" />
    public class ServiceCenterService : ServiceBase, IServiceCenterService {

        public ServiceCenterService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// 添加门店
        /// 已经验证过
        /// </summary>
        /// <param name="serviceCenterView"></param>
        public ServiceResult AddOrUpdate(ServiceCenterView serviceCenterView) {
            var result = ServiceResult.Success;
            var serviceCenterConfig = Resolve<IAutoConfigService>().GetValue<ServiceCenterConfig>();
            var serviceUserTypeConfig = Resolve<IAutoConfigService>().UserTypes()
                .FirstOrDefault(r => r.Id == serviceCenterView.UserTypeId);
            if (serviceUserTypeConfig == null) {
                return ServiceResult.FailedWithMessage("用户类型不存在，或状态不正常，请配置");
            }

            // 所属会员处理
            var user = Resolve<IUserService>().GetSingle(serviceCenterView.UserName);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户不存在");
            }

            serviceCenterView.UserId = user.Id;

            // 判断会员类型是否重复添加,一个会员只有能有一个会员类型
            var userTypeExist = Resolve<IUserTypeService>()
                .GetSingle(serviceCenterView.UserId, serviceCenterView.UserTypeId);
            if (userTypeExist != null && userTypeExist.Id != serviceCenterView.Id) {
                return ServiceResult.FailedWithMessage($"该会员已经是{serviceUserTypeConfig.Name},不能重复添加");
            }

            var userType = AutoMapping.SetValue<Entities.UserType>(serviceCenterView);
            var parentUser = Resolve<IUserService>().GetSingle(serviceCenterView.ParentUserName);
            if (parentUser != null) {
                var parentUserTypeIds = serviceCenterConfig.ParentUserTypes.Split(",").ToList();
                //检查推荐人是满足用户类型
                if (!Resolve<IUserTypeService>().HasUserType(parentUser.Id, parentUserTypeIds)) {
                    return ServiceResult.FailedWithMessage("推荐人用户类型不满足要求，请在配置中设置，或输入正确的推荐人类型");
                }

                userType.ParentUserId = parentUser.Id;
            }

            // 城市区域Id
            userType.EntityId = serviceCenterView.HttpContext.Request.Form["EntityId"].ConvertToLong(0);

            userType.UserTypeExtensions = new UserTypeExtensions {
                Intro = serviceCenterView.Remark,
                Remark = serviceCenterView.Remark,
                ModifiedTime = DateTime.Now
            };
            userType.Extensions = userType.UserTypeExtensions.ToJson(); //  扩展属性

            var context = Repository<IUserTypeRepository>().RepositoryContext;
            context.BeginTransaction();
            try {
                if (userType.Id > 0) {
                    if (user.Id != userType.UserId) {
                        return ServiceResult.FailedWithMessage("编辑门店时，不能更换用户名");
                    }

                    var dateUserType = Resolve<IUserTypeService>().GetSingle(r => r.Id == userType.Id);
                    dateUserType = AutoMapping.SetValue(userType, dateUserType);
                    Resolve<IUserTypeService>().Update(dateUserType);
                } else {
                    // 新增
                    Resolve<IUserTypeService>().Add(userType);
                    var userDetial = Resolve<IUserDetailService>().GetSingle(r => r.UserId == user.Id);
                    // 更新会员的IsServiceCenter字段
                    userDetial.IsServiceCenter = true;
                    // 设置当前会员的服务中心为自己
                    if (serviceCenterConfig.IsSetUserSelf) {
                        userDetial.ServiceCenterUserId = userDetial.UserId;
                    }

                    Resolve<IUserDetailService>().Update(userDetial);
                }

                context.SaveChanges();
                context.CommitTransaction();
            } catch (Exception ex) {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
            } finally {
                context.DisposeTransaction();
            }

            Resolve<IUserService>().DeleteUserCache(user.Id, user.UserName);
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="query"></param>
        public PagedList<ServiceCenterView> GetPageList(object query) {
            var dictionary = query.DeserializeJson<Dictionary<string, string>>();
            // 查询条件加上UserTypeId
            dictionary.Add("UserTypeId", UserTypeEnum.ServiceCenter.GetFieldId().ToString());
            var userTypePageList = Resolve<IUserTypeService>().GetPagedList(dictionary.ToJson());

            // userTypePageList = userTypePageList.Where(r => r.UserTypeId == UserTypeEnum.ServiceCenter.GetFieldId());

            var ServiceCenterGrades =
                Resolve<IGradeService>().GetGradeListByGuid(UserTypeEnum.ServiceCenter.GetFieldId());

            var serviceCenterViews = new List<ServiceCenterView>();
            foreach (var item in userTypePageList) {
                var serviceCenterView = AutoMapping.SetValue<ServiceCenterView>(item);
                if (serviceCenterView.UserTypeId == UserTypeEnum.ServiceCenter.GetFieldId()) {
                    serviceCenterView.GradeName = ServiceCenterGrades.FirstOrDefault(r => r.Id == item.GradeId)?.Name;

                    serviceCenterView.GradeName = ServiceCenterGrades.FirstOrDefault(r => r.Id == item.GradeId)?.Name;

                    // 此方法有性能问题，后期放到程序外 显示城市名称
                    serviceCenterView.RegionName = Resolve<IRegionService>().GetFullName(serviceCenterView.EntityId);
                    var user = Resolve<IUserService>().GetSingle(u => u.Id == item.ParentUserId);
                    if (user != null) {
                        serviceCenterView.ParentUserName = user.UserName;
                    }

                    serviceCenterViews.Add(serviceCenterView);
                }
            }

            return PagedList<ServiceCenterView>.Create(serviceCenterViews, userTypePageList.RecordCount,
                userTypePageList.PageSize, userTypePageList.PageIndex);
        }

        public ServiceCenterView GetView(long id) {
            var serviceCenterView = new ServiceCenterView();
            if (id > 0) {
                var userType = Resolve<IUserTypeService>().GetSingle(r => r.Id == id);
                if (userType != null) {
                    serviceCenterView = AutoMapping.SetValue<ServiceCenterView>(userType);
                    serviceCenterView.UserName = Resolve<IUserService>().GetSingle(serviceCenterView.UserId)?.UserName;
                    serviceCenterView.ParentUserName =
                        Resolve<IUserService>().GetSingle(serviceCenterView.ParentUserId)?.UserName;
                }
            }

            return serviceCenterView;
        }

        /// <summary>
        ///     删除城市合伙人
        /// </summary>
        /// <param name="id">主键ID</param>
        public ServiceResult Delete(long id) {
            var userDetial = Resolve<IUserDetailService>().GetSingle(r => r.UserId == id);
            if (userDetial == null) {
                return ServiceResult.FailedWithMessage("用户详情未找到");
            }

            var count = Resolve<IUserDetailService>()
                .Count(r => r.ServiceCenterUserId == id && r.UserId != userDetial.Id);
            if (count > 0) {
                return ServiceResult.FailedWithMessage("改服务中心下面存在着会员，请先移除改服务中心下的会员");
            }

            var context = Repository<IUserTypeRepository>().RepositoryContext;
            context.BeginTransaction();
            try {
                var usertype = Resolve<IUserTypeService>().GetSingle(r => r.Id == id);
                Resolve<IUserTypeService>().Delete(r => r.Id == usertype.Id);

                // 更新会员的IsServiceCenter字段
                userDetial.IsServiceCenter = false;
                Resolve<IUserDetailService>().Update(userDetial);

                context.SaveChanges();
                context.CommitTransaction();
            } catch (Exception ex) {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("删除失败:" + ex.Message);
            } finally {
                context.DisposeTransaction();
            }

            return ServiceResult.Success;
        }

        /// <summary>
        ///     获取服务中心，和服务中心用户
        /// </summary>
        /// <param name="userId">用户Id</param>
        public Tuple<Entities.UserType, User.Domain.Entities.User> GetServiceUser(long userId) {
            var userDetail = Resolve<IUserService>().GetUserDetail(userId);
            if (userDetail == null) {
                return Tuple.Create<Entities.UserType, User.Domain.Entities.User>(null, null);
            }

            var serviceUserType = Resolve<IUserTypeService>().GetSingle(r =>
                r.UserTypeId == UserTypeEnum.ServiceCenter.GetFieldId() && r.Status == UserTypeStatus.Success
                                                                        && r.UserId == userDetail.Detail
                                                                            .ServiceCenterUserId);
            if (serviceUserType == null) {
                return Tuple.Create<Entities.UserType, User.Domain.Entities.User>(null, null);
            }

            var servcieUser = Resolve<IUserService>().GetSingle(serviceUserType.UserId);
            return Tuple.Create(serviceUserType, servcieUser);
        }

        /// <summary>
        ///     获取服务中心或门店的
        ///     推荐用户和推荐服务中心
        ///     返回推荐用户
        ///     同时判断该用户是否拥有服务中心
        /// </summary>
        /// <param name="parentUserId">服务中心推荐用户Id</param>
        public Tuple<Entities.UserType, User.Domain.Entities.User> GetServiceParentUser(long parentUserId) {
            var user = Resolve<IUserService>().GetSingle(parentUserId);
            if (user == null) {
                return Tuple.Create<Entities.UserType, User.Domain.Entities.User>(null, null);
            }

            var serviceUserType = Resolve<IUserTypeService>().GetSingle(r =>
                r.UserTypeId == UserTypeEnum.ServiceCenter.GetFieldId() && r.UserId == user.Id &&
                r.Status == UserTypeStatus.Success);
            if (serviceUserType == null) {
                return Tuple.Create<Entities.UserType, User.Domain.Entities.User>(null, user);
            }

            return Tuple.Create(serviceUserType, user);
        }

        /// <summary>
        ///     会员升级自动升级为服务中心，在UserType中添加一条记录
        ///     更新下面所有用户的ServiceCenterId
        /// </summary>
        /// <param name="userId">当前要升级的用户Id</param>
        /// <param name="isSelf">当期升级会员，serviceUserId是否==UserID</param>
        public void UserUpgradServiceCenter(long userId, bool isSelf = true) {
            throw new NotImplementedException();
        }
    }
}