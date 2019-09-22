using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Core.User.ViewModels;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.User.Controllers {

    [ApiExceptionFilter]
    [Route("Api/UserAdmin/[action]")]
    public class ApiUserAdminController : ApiBaseController {

        public ApiUserAdminController() : base() {
        }

        [HttpGet]
        [Display(Description = "用户信息")]
        public ApiResult<PagedList<ViewUser>> UserList([FromQuery] PagedInputDto parameter) {
            var model = Resolve<IUserService>().GetViewUserPageList(Query);
            return ApiResult.Success(model);
        }

        /// <summary>
        ///     Edits the specified identifier.
        /// </summary>
        /// <param name="id">Id标识</param>
        [HttpGet]
        public ApiResult Edit(long id) {
            var user = Resolve<IUserService>().GetUserDetail(id);
            if (user == null) {
                return ApiResult.Failure("您访问的用户不存在");
            }

            //ViewBag.ServiceCenterTypeName = "所属" + typeName;
            var parent = Resolve<IUserService>().GetSingle(u => u.Id == user.ParentId);
            var view = new ViewAdminEdit {
                ServiceCenterName = parent == null ? "" : parent.GetUserName(),
                User = user,
                UserDetail = user.Detail,
                Status = user.Status,
                UserGradeConfig = Resolve<IGradeService>().GetGrade(user.GradeId),
                Sex = user.Detail.Sex,
                Avator = Resolve<IApiService>().ApiUserAvator(user.Id),
                Parent = parent,
                GradeList = Resolve<IAutoConfigService>().GetList<UserGradeConfig>(),
                StatusList = Enum.GetValues(typeof(Status)).Cast<Status>().ToDictionary(x => (long)x, x => x.GetDisplayName()).Select(x => new { Name = x.Value, Value = x.Key }).ToList(),
            };

            view.RegionId = user.Detail.RegionId;
            var userAddress = Resolve<IUserAddressService>()
                .GetSingle(r => r.UserId == user.Id && r.Type == AddressLockType.UserInfoAddress);
            if (userAddress != null) {
                view.Address = userAddress.Address;
                view.RegionName = Resolve<IRegionService>().GetFullName(userAddress.RegionId);
            }

            return ApiResult.Success(view);
        }

        [HttpPost]
        public ApiResult EditBasic([FromBody] ViewAdminEdit view) {
            if (view.User.Name.IsNullOrEmpty() || view.User.Email.IsNullOrEmpty()) {
                return ApiResult.Failure("操作失败", "姓名或邮箱不能为空");
            }

            var user = view.User;
            user.Status = view.Status;
            user.Detail.ModifiedTime = DateTime.Now;
            var result = Resolve<IUserAdminService>().UpdateUser(user);
            if (!result.Succeeded) {
                return ApiResult.Failure(result.ErrorMessages.Join());
            }

            //  _messageManager.Keep("信息修改成功");
            //Resolve<IUserService>().Log($"修改会员基本信息，用户名{user.UserName},姓名{user.Name},手机{user.Mobile}");
            return ApiResult.Success("用户信息保存成功");
        }

        /// <summary>
        ///     Edits the specified 视图.
        /// </summary>
        /// <param name="view">The 视图.</param>
        [HttpPost]
        public ApiResult Edit([FromBody] ViewAdminEdit view) {
            try {
                var find = Resolve<IUserDetailService>().GetSingle(r => r.Id == view.UserDetail.Id);
                if (find == null) {
                    return ApiResult.Failure("该用户的详细资料不存在");
                }

                find.Remark = view.UserDetail.Remark;
                find.Sex = view.Sex;

                find.Birthday = view.UserDetail.Birthday;
                find.RegionId = view.UserDetail.RegionId;
                find.RegionId = view.RegionId;

                if (view.UserDetail != null && view.UserDetail.RegionId >= 0) {
                    find.RegionId = view.UserDetail.RegionId;
                }

                if (!view.ServiceCenterUserName.IsNullOrEmpty()) {
                    var serviceUserTypeName = Resolve<IAutoConfigService>().UserTypes()
                        .First(r => r.TypeClass == UserTypeEnum.ServiceCenter)?.Name;
                    var serviceUser = Resolve<IUserService>().GetSingle(view.ServiceCenterUserName);
                    if (serviceUser == null) {
                        return ApiResult.Failure($"您输入的{serviceUserTypeName}不存在，请重新输入");
                    }
                }

                var result = Resolve<IUserAdminService>().UpdateUserDetail(find);
                if (!result) {
                    return ApiResult.Failure("服务异常:会员资料修改失败");
                } else {
                    var userInfoAddress = new UserInfoAddressInput {
                        UserId = view.User.Id,
                        Id = view.UserDetail.AddressId,
                        RegionId = view.UserDetail.RegionId,
                        Address = view.Address,
                        Type = AddressLockType.UserInfoAddress
                    };

                    Resolve<IUserAddressService>().SaveUserInfoAddress(userInfoAddress);
                }

                //  _messageManager.Keep("修改会员详细信息");
                Resolve<IUserService>().Log($"修改会员详细信息,会员ID为{find.UserId}");
                return ApiResult.Success("信息编辑成功");
            } catch (Exception exc) {
                return ApiResult.Failure($"信息编辑失败: {exc.Message}");
            }
        }

        /// <summary>
        ///     更新s the password.
        /// </summary>
        /// <param name="view">The 视图.</param>
        [HttpPost]
        public ApiResult UpdatePassword([FromBody] ViewAdminEdit view) {
            var passwordInput = new PasswordInput {
                Password = view.Password,
                ConfirmPassword = view.ConfirmPassword,
                UserId = view.EditUserId
            };

            var editUser = Resolve<IUserService>().GetSingle(view.EditUserId);
            if (editUser == null) {
                return ApiResult.Failure("要编辑的用户ID对应用户信息不存在");
            }
            view.User = editUser;

            //修改登录密码
            if (view.Type == 1) {
                passwordInput.Type = PasswordType.LoginPassword;
                var reuslt = Resolve<IUserDetailService>().ChangePassword(passwordInput, false);
                if (reuslt.Succeeded) {
                    Resolve<IUserService>().Log($"管理员修改会员的登录密码,会员ID为{view.User.Id},会员名{view.User.UserName},姓名{view.User.Name}");
                    if (view.SendPassword) {
                        //  _messageManager.AddRawQueue(view.User.Mobile,
                        //    $"管理员已成功修改了您的登录密码，新的登录密码为{view.Password}，请尽快登录系统，并修改登录密码");
                    }
                } else {
                    return ApiResult.Failure("服务异常:登录密码修改失败，请稍后在试");
                }
            }

            if (view.Type == 2) {
                passwordInput.Type = PasswordType.PayPassword;
                var reuslt = Resolve<IUserDetailService>().ChangePassword(passwordInput, false);
                if (reuslt.Succeeded) {
                    Resolve<IUserService>().Log($"管理员修改会员的支付密码,会员ID为{view.User.Id},会员名{view.User.UserName},姓名{view.User.Name}"
                        );
                    if (view.SendPassword) {
                        //  _messageManager.AddRawQueue(view.User.Mobile,
                        //   $"管理员已成功修改了您的支付密码，新的登录密码为{view.Password}，请尽快登录系统，并修改支付密码");
                    }
                } else {
                    return ApiResult.Failure("服务异常:支付密码修改失败，请稍后在试" + reuslt);
                }
            }

            return ApiResult.Success("密码修改成功");
        }
    }
}