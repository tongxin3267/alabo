using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Core.UserType.Domain.Entities.Extensions;
using Alabo.App.Core.UserType.Modules.Partner;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Mapping;

namespace Alabo.App.Core.UserType.Domain.Services {

    /// <summary>
    ///     Class PartnerService.
    /// </summary>
    /// <seealso cref="Alabo.Domains.Services.ServiceBase" />
    /// <seealso cref="Alabo.App.Core.UserType.Domain.Services.IPartnerService" />
    public class PartnerService : ServiceBase, IPartnerService {

        public PartnerService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// 添加合伙人服务中心
        /// 已经验证过
        /// </summary>
        /// <param name="partnerView"></param>
        public ServiceResult AddOrUpdate(PartnerView partnerView) {
            var result = ServiceResult.Success;
            var partnerUserTypeConfig = Resolve<IAutoConfigService>().UserTypes()
                .FirstOrDefault(r => r.Id == partnerView.UserTypeId);
            if (partnerUserTypeConfig == null) {
                return ServiceResult.FailedWithMessage("用户类型不存在，或状态不正常，请配置");
            }

            // 所属会员处理
            var user = Resolve<IUserService>().GetSingle(partnerView.UserName);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户不存在");
            }

            partnerView.UserId = user.Id;

            // 判断会员类型是否重复添加,一个会员只有能有一个会员类型
            var userTypeExist = Resolve<IUserTypeService>().GetSingle(partnerView.UserId, partnerView.UserTypeId);
            if (userTypeExist != null && userTypeExist.Id != partnerView.Id) {
                return ServiceResult.FailedWithMessage($"该会员已经是{partnerUserTypeConfig.Name},不能重复添加");
            }

            var userType = AutoMapping.SetValue<Entities.UserType>(partnerView);
            var parentUser = Resolve<IUserService>().GetSingle(partnerView.ParentUserName);
            if (parentUser != null) {
                userType.ParentUserId = parentUser.Id;
            }

            userType.UserTypeExtensions = new UserTypeExtensions {
                Intro = partnerView.Intro,
                Remark = partnerView.Remark,
                ModifiedTime = DateTime.Now
            };
            userType.Extensions = userType.UserTypeExtensions.ToJson(); //  扩展属性
            if (userType.Id == 0) {
                userType.CreateTime = DateTime.Now;
            }

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
        public PagedList<PartnerView> GetPageList(object query) {
            var dictionary = new Dictionary<string, string>
            {
                {"UserTypeId", UserTypeEnum.Partner.GetFieldId().ToString()}
            };
            var userTypePageList = Resolve<IUserTypeService>().GetPagedList(query, dictionary);
            var gradeConfig = Resolve<IAutoConfigService>().GetList<PartnerGradeConfig>();

            var partnerViews = new List<PartnerView>();
            foreach (var item in userTypePageList) {
                var partnerView = AutoMapping.SetValue<PartnerView>(item);
                if (partnerView.UserTypeId == UserTypeEnum.Partner.GetFieldId()) {
                    partnerView.GradeName = gradeConfig.FirstOrDefault(r => r.Id == item.GradeId)?.Name;

                    // 此方法有性能问题，后期放到程序外 显示合伙人名称
                    // partnerView.RegionName = Service<IRegionService>().GetFullName(partnerView.EntityId);
                    var UserName = Resolve<IUserService>().GetSingle(u => u.Id == item.ParentUserId);
                    if (UserName != null) {
                        partnerView.ParentUserName = UserName.UserName;
                    }

                    partnerViews.Add(partnerView);
                }
            }

            return PagedList<PartnerView>.Create(partnerViews, userTypePageList.RecordCount, userTypePageList.PageSize,
                userTypePageList.PageIndex);
        }

        /// <summary>
        /// </summary>
        /// <param name="id">主键ID</param>
        public PartnerView GetView(long id) {
            var partnerView = new PartnerView();
            if (id > 0) {
                var userType = Resolve<IUserTypeService>().GetSingle(r => r.Id == id);
                if (userType != null) {
                    partnerView = AutoMapping.SetValue<PartnerView>(userType);
                    partnerView.UserName = Resolve<IUserService>().GetSingle(partnerView.UserId)?.UserName;
                    try {
                        partnerView.ParentUserName =
                            Resolve<IUserService>().GetSingle(partnerView.ParentUserId).UserName;
                    } catch {
                        partnerView.ParentUserName = null;
                    }

                    if (partnerView.Extensions != null) {
                        var partnerViewExtensions = partnerView.Extensions.DeserializeJson<PartnerView>();
                        partnerView.Intro = partnerViewExtensions.Intro;
                        partnerView.Remark = partnerViewExtensions.Remark;
                    }
                }
            }

            return partnerView;
        }

        /// <summary>
        ///     删除合伙人合伙人
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
        ///     根据区域获取合伙人代理
        /// </summary>
        /// <param name="regionId"></param>
        public User.Domain.Entities.User GetPartnerUserType(long regionId) {
            var userType = Resolve<IUserTypeService>()
                .GetSingle(r => r.UserTypeId == UserTypeEnum.Partner.GetFieldId() && r.EntityId == regionId);
            if (userType != null) {
                return Resolve<IUserService>().GetSingle(userType.UserId);
            }

            return null;
        }
    }
}