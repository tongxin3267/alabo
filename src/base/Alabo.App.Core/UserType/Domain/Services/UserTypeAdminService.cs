using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Core.UserType.Domain.Dtos;
using Alabo.App.Core.UserType.Domain.Entities.Extensions;
using Alabo.App.Core.UserType.Domain.Enums;
using Alabo.App.Core.UserType.Domain.Repositories;
using Alabo.App.Core.UserType.ViewModels;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Extensions;

namespace Alabo.App.Core.UserType.Domain.Services {

    public class UserTypeAdminService : ServiceBase, IUserTypeAdminService {
        private static readonly string _singleUserTypeCacheKey = "UserTypeCacheKey";
        private readonly IUserRepository _userRepository;
        private readonly IUserTypeRepository _userTypeRepository;

        public UserTypeAdminService(IUnitOfWork unitOfWork) : base(unitOfWork) {
            _userTypeRepository = Repository<IUserTypeRepository>();
            _userRepository = Repository<IUserRepository>();
        }

        public Tuple<ServiceResult, PagedList<ViewUserType>> GetViewUserTypePageList(UserTypeInput userTypeInput) {
            if (userTypeInput.typeId.IsGuidNullOrEmpty()) {
                return Tuple.Create(ServiceResult.FailedWithMessage("您访问的了类型不存在，请重新输入网址后再访问"),
                    new PagedList<ViewUserType>());
            }

            var userTypes = Resolve<IAutoConfigService>()
                .GetList<UserTypeConfig>(r => r.Status == Status.Normal && r.Id == userTypeInput.typeId);
            if (userTypes == null || userTypes.Count <= 0) {
                return Tuple.Create(ServiceResult.FailedWithMessage("您访问的系统类型不存在，或者状态不正常"),
                    new PagedList<ViewUserType>());
            }

            userTypeInput.UserTypeId = userTypeInput.typeId;
            var viewUserList = _userTypeRepository.GetViewUserTypeList(userTypeInput, out var count);
            var userTypeIds = viewUserList.Select(r => r.Id).Distinct().ToList();
            var userIds = viewUserList.Select(r => r.UserId).Distinct().ToList();
            var users = _userRepository.GetList(userIds); //用户

            var parentIds = viewUserList.Select(r => r.ParentUserId).Distinct().ToList();
            var parentUsers = _userRepository.GetList(parentIds); //上级用户
            if (!userTypeInput.UserName.IsNullOrEmpty()) {
                var user = new Entities.UserType();
                foreach (var item in viewUserList) {
                    var viewUser = users.FirstOrDefault(u => u.UserName == userTypeInput.UserName);
                    if (item.UserId == viewUser.Id) {
                        user = item;
                    }
                }

                ;
                viewUserList.Clear();
                viewUserList.Add(user);
            }

            IList<ViewUserType> userTypeResult = new List<ViewUserType>();
            foreach (var item in viewUserList) {
                var viewUserType = new ViewUserType {
                    Id = item.Id,
                    UserId = item.UserId,
                    Name = item.Name,
                    ParentUserId = item.ParentUserId,
                    EntityId = item.EntityId,
                    Status = item.Status,
                    GradeId = item.GradeId,
                    UserTypeId = item.UserTypeId,
                    CreateTime = item.CreateTime
                };
                var grade = Resolve<IGradeService>()
                    .GetGradeByUserTypeIdAndGradeId(userTypeInput.UserTypeId, item.GradeId);
                viewUserType.BaseGradeConfig = grade;

                if (grade != null) {
                    viewUserType.GradeName = grade.Name;
                }

                var parentItemUser = parentUsers.FirstOrDefault(r => r.Id == item.ParentUserId);
                if (parentItemUser != null) {
                    viewUserType.ParentUserName = $"{parentItemUser.UserName}({parentItemUser.Name})";
                }

                var itemUser = users.FirstOrDefault(r => r.Id == item.UserId);
                if (itemUser != null) {
                    viewUserType.UserName = $"{itemUser.UserName}({itemUser.Name})";
                }

                userTypeResult.Add(viewUserType);
            }

            return Tuple.Create(ServiceResult.Success,
                PagedList<ViewUserType>.Create(userTypeResult, count, userTypeInput.PageSize, userTypeInput.PageIndex));
        }

        public ServiceResult AddOrUpdate(ViewUserTypeEdit viewUserType, HttpRequest httpRequest) {
            var result = ServiceResult.Success;
            if (viewUserType.UserName.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("所属用户名不能为空，请重新输入");
            }

            var user = Resolve<IUserService>().GetSingle(viewUserType.UserName);
            if (user == null) {
                return ServiceResult.FailedWithMessage("输入的所属用户不存在，请重新输入");
            }

            if (viewUserType.GradeId.IsGuidNullOrEmpty()) {
            }

            // 判断会员类型是否重复添加,一个会员只有能有一个会员类型
            var userTypeExist = Resolve<IUserTypeService>()
                .GetSingle(viewUserType.UserType.UserId, viewUserType.UserType.UserTypeId);
            if (userTypeExist != null && userTypeExist.Id != viewUserType.Id) {
                return ServiceResult.FailedWithMessage($"该会员已经是{viewUserType.UserTypeConfig.Name},不能重复添加");
            }

            viewUserType.UserType.UserId = user.Id;

            if (!viewUserType.ParentUserName.IsNullOrEmpty()) {
                var parentUser = Resolve<IUserService>().GetSingle(viewUserType.ParentUserName);
                if (parentUser == null) {
                    return ServiceResult.FailedWithMessage("推荐人用户名不存在，请重新输入");
                }

                viewUserType.UserType.ParentUserId = parentUser.Id;
            }

            viewUserType.UserType.Status = viewUserType.Status;
            viewUserType.UserType.GradeId = viewUserType.GradeId;
            viewUserType.UserType.UserTypeExtensions = new UserTypeExtensions {
                Intro = viewUserType.Remark,
                Remark = viewUserType.Remark,
                ModifiedTime = DateTime.Now
            };
            //如果用户类型是员工
            if (viewUserType.UserType.UserTypeId == UserTypeEnum.Employees.GetFieldId()) {
                // 获取员工类型
                var roleId = httpRequest.Form["RoldId"].ConvertToGuid();
                if (roleId.IsGuidNullOrEmpty()) {
                    return ServiceResult.FailedWithMessage("请选择员工岗位权限");
                }

                viewUserType.UserType.UserTypeExtensions.RoldId = roleId;
            }

            viewUserType.UserType.Extensions = viewUserType.UserType.UserTypeExtensions.ToJson(); //  扩展属性
            var context = _userTypeRepository.RepositoryContext;
            context.BeginTransaction();
            try {
                if (viewUserType.Id == 0) {
                    //新增
                    viewUserType.UserType.CreateTime = DateTime.Now;
                    Resolve<IUserTypeService>().Add(viewUserType.UserType);
                } else {
                    //编辑
                    //var userType = Service<IUserTypeService>().GetSingle(viewUserType.Id);
                    //userType.Name = viewUserType.UserType.Name;
                    viewUserType.UserType.Id = viewUserType.Id;
                    Resolve<IUserTypeService>().Update(viewUserType.UserType);

                    //var userTypeDetail = Service<IUserTypeAdminService>().GetSingle(r => r.UserTypeId == viewUserType.UserType.Id);
                    //userTypeDetail.Remark = viewUserType.Detail.Remark;

                    // Service<IUserTypeAdminService>().Update(viewUserType.Detail);
                    DeleteUserCache(viewUserType.UserType); //删除缓存
                }

                context.SaveChanges();
                context.CommitTransaction();
            } catch (Exception ex) {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("服务异常:" + ex.Message);
            } finally {
                context.DisposeTransaction();
            }

            return result;
        }

        public ViewUserTypeEdit GetUserTypeEdit(Guid userTypeId, long id) {
            var userTypes = Resolve<IAutoConfigService>()
                .GetList<UserTypeConfig>(r => r.Status == Status.Normal && r.Id == userTypeId);
            if (userTypes == null) {
                return null;
            }

            var view = new ViewUserTypeEdit {
                Key = Resolve<IUserTypeService>().GetUserTypeKey(userTypeId)
            };
            if (view.Key.IsNullOrEmpty()) {
                return null;
            }

            if (id > 0) {
                view.Id = id;
                var userType = Resolve<IUserTypeService>().GetSingleDetail(id);
                if (userType == null) {
                    return null;
                }

                var users = Resolve<IUserService>().GetList(new List<long> { userType.UserId, userType.ParentUserId });
                view.UserName = users.FirstOrDefault(e => e.Id == userType.UserId)?.UserName;
                view.ParentUserName = users.FirstOrDefault(e => e.Id == userType.ParentUserId)?.UserName;
                // view.Detail = userType.Detail;
                view.Status = userType.Status;
                view.GradeId = userType.GradeId;
                view.UserType = userType;
                //设置默认等级
                if (userTypeId == UserTypeEnum.Employees.GetFieldId()) {
                    view.RoldId = userType.UserTypeExtensions.RoldId.ToString();
                }

                view.Intro = userType.UserTypeExtensions.Intro;
                view.Remark = userType.UserTypeExtensions.Remark;
            } else {
                view.UserType = new Entities.UserType();
                view.Status = UserTypeStatus.Pending;
                view.UserType.UserTypeId = userTypes.FirstOrDefault().Id;
            }

            return view;
        }

        /// <summary>
        ///     删除缓存
        /// </summary>
        /// <param name="userType"></param>
        private void DeleteUserCache(Entities.UserType userType) {
            var cacheKey = _singleUserTypeCacheKey + "_Id_" + userType.Id;
            ObjectCache.Remove(cacheKey);
            cacheKey = $"{_singleUserTypeCacheKey}_{userType.UserId}_{userType.UserTypeId}";
            ObjectCache.Remove(cacheKey);
            cacheKey = "UserAllGradeId" + userType.UserId;
            ObjectCache.Remove(cacheKey);
        }
    }
}