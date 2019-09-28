using Alabo.Data.People.Users.Domain.Services;
using Alabo.Data.People.Users.Dtos;
using Alabo.Data.People.Users.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Framework.Basic.Regions.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Dtos;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Framework.Core.WebUis.Models.Lists;
using Alabo.Framework.Core.WebUis.Models.Previews;
using Alabo.Framework.Themes.Extensions;
using Alabo.Mapping;
using Alabo.Maps;
using Alabo.Tables.Domain.Services;
using Alabo.UI;
using Alabo.UI.Design.AutoForms;
using Alabo.UI.Design.AutoPreviews;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZKCloud.Open.ApiBase.Models;
using User = Alabo.Users.Entities.User;
using UserDetail = Alabo.Users.Entities.UserDetail;

namespace Alabo.Data.People.Users.Controllers
{
    /// <summary>
    ///     用户相关Api接口
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/User/[action]")]
    public class ApiUserController : ApiBaseController<User, long>
    {
        //private readonly Alabo.App.Core.Api.Domain.Service.IApiService _ApiServer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ApiUserController" /> class.
        /// </summary>
        public ApiUserController(
        )
        {
            BaseService = Resolve<IUserService>();
        }

        /// <summary>
        ///     推荐会员
        /// </summary>
        [HttpGet]
        [Display(Description = "推荐会员")]
        public ApiResult<ListOutput> Recommend([FromQuery] RecommendInput parameter)
        {
            var userInput = new UserInput
            {
                PageIndex = parameter.PageIndex,
                ParentId = parameter.LoginUserId,
                GradeId = parameter.GradeId,
                PageSize = parameter.PageSize
            };
            var apiOutput = new ListOutput();
            var model = Resolve<IUserService>().GetViewUserPageList(userInput);
            apiOutput.TotalSize = model.PageCount;

            foreach (var item in model)
            {
                var apiData = new ListItem
                {
                    Title = item.UserName.ReplaceHtmlTag(),
                    Intro = $"{item.Mobile} {item.CreateTime.ToString("yyyy-MM-dd hh:ss")}",
                    Extra = item.GradeName,
                    Image = Resolve<IApiService>().ApiUserAvator(item.Id),
                    Id = item.Id,
                    Url = $"/pages/user?path=user_view&id={item.Id}".ToClientUrl(ClientType.WapH5)
                };
                apiOutput.ApiDataList.Add(apiData);
            }

            return ApiResult.Success(apiOutput);
        }

        [HttpPost]
        public ApiResult AddUser([FromBody] ViewUser view)
        {
            if (!this.IsFormValid())
                return ApiResult.Failure<UserOutput>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            var model = view.MapTo<RegInput>();
            var result = Resolve<IUserBaseService>().Reg(model);
            if (result.Item1.Succeeded)
                return ApiResult.Success();
            return ApiResult.Failure(result.Item1.ErrorMessages);
        }

        /// <summary>
        ///     获取会员注册视图
        /// </summary>
        [HttpGet]
        [Display(Description = "获取会员注册视图")]
        public ApiResult<AutoForm> GetRegForm()
        {
            var regForm = AutoFormMapping.Convert<RegInput>();
            return ApiResult.Success(regForm);
        }

        [HttpGet]
        [Display(Description = "获取会员登陆视图")]
        public ApiResult<AutoForm> GetLoginForm()
        {
            var form = AutoFormMapping.Convert<LoginInput>();
            return ApiResult.Success(form);
        }

        [HttpGet]
        [Display(Description = "获取会员找回密码视图")]
        public ApiResult<AutoForm> GetFindPasswordForm()
        {
            var form = AutoFormMapping.Convert<FindPasswordInput>();
            return ApiResult.Success(form);
        }

        [HttpGet]
        [Display(Description = "获取会员修改密码视图")]
        public ApiResult<AutoForm> GetPasswordForm()
        {
            var form = AutoFormMapping.Convert<PasswordInput>();
            return ApiResult.Success(form);
        }

        /// <summary>
        ///     Regs the specified parameter.
        ///     会员注册
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpPost]
        [Display(Description = "会员注册")]
        public ApiResult<UserOutput> Reg([FromBody] RegInput parameter)
        {
            if (!this.IsFormValid())
                return ApiResult.Failure<UserOutput>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);

            var result = Resolve<IUserBaseService>().Reg(parameter);
            return ToResult(result);
        }

        /// <summary>
        ///     修该用户信息
        /// </summary>
        /// <param name="parameter">用户详细信息</param>
        [HttpPost]
        [Display(Description = "修该用户信息")]
        [ApiAuth]
        public ApiResult Update([FromBody] UserDetail parameter)
        {
            //var loginUserId = HttpContext.Request.Query["loginuserid"].ConvertToLong();
            var userDetail = Resolve<IUserDetailService>().GetSingle(r => r.UserId == parameter.UserId);
            if (userDetail == null) return ApiResult.Failure();

            userDetail.Avator = parameter.Avator;
            //userDetail.NickName = parameter.NickName;
            userDetail.Birthday = parameter.Birthday;
            userDetail.Sex = parameter.Sex;
            if (!parameter.Email.IsNullOrEmpty())
            {
                var user = Resolve<IUserService>().GetSingle(userDetail.UserId);
                user.Email = parameter.Email;
                Resolve<IUserService>().UpdateUser(user);
            }

            var result = Resolve<IUserDetailService>().UpdateSingle(userDetail);
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     检查手机号码是否属于自己的下级
        /// </summary>
        /// <param name="parameter">用户详细信息</param>
        [HttpPost]
        [Display(Description = "检查手机号码是否属于自己的下级")]
        [ApiAuth]
        public ApiResult CheckMobile([FromBody] CheckMobileInput parameter)
        {
            //var loginUserId = HttpContext.Request.Query["loginuserid"].ConvertToLong();
            var user = Resolve<IUserService>().GetSingle(s => s.Mobile == parameter.Mobile);
            if (user != null)
            {
                if (user.ParentId <= 0) return ApiResult.Success(true, "该手机用户无人推荐!");
                if (user.ParentId == parameter.UserId)
                    return ApiResult.Success(true, "该手机用户已是您的推荐用户");
                return ApiResult.Success(false, "该手机用户不是您的推荐用户,请谨慎操作!");
            }

            return ApiResult.Success(true, "该手机暂未注册");
        }

        /// <summary>
        ///     修改密码，密码传入明文
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpPost]
        [Display(Description = "修改密码，密码传入明文")]
        [ApiAuth]
        public ApiResult ChangePassword([FromBody] PasswordInput parameter)
        {
            //暂时取消判断
            //var loginUserId = HttpContext.Request.Query["loginUserId"].ConvertToLong();
            //if (parameter.UserId != loginUserId) {
            //    return ApiResult.Failure("您的访问不合法，请确认正确的访问方式", MessageCodes.ParameterValidationFailure);
            //}
            //if (!parameter.UserName.IsNullOrEmpty()) {
            //    var userByName = Resolve<IUserService>().GetSingleByUserNameOrMobile(parameter.UserName);

            //    if (userByName != null) {
            //        parameter.UserId = userByName.Id;
            //    }
            //}

            if (parameter.UserId != 0)
            {
                var userModel = Resolve<IUserService>().GetSingle(parameter.UserId);
                if (userModel == null) return ApiResult.Failure("您访问的用户不存在！", MessageCodes.ParameterValidationFailure);
            }
            else
            {
                return ApiResult.Failure("您访问的用户不存在！", MessageCodes.ParameterValidationFailure);
            }

            if (parameter.Password != parameter.ConfirmPassword) return ApiResult.Failure("两次密码输入不一致");

            if (!this.IsFormValid())
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            if (parameter.Type.IsDefault()) parameter.Type = PasswordType.LoginPassword;

            var result = Resolve<IUserDetailService>().ChangePassword(parameter);
            if (!result.Succeeded) return ApiResult.Failure(result.ToString(), MessageCodes.ParameterValidationFailure);

            return ApiResult.Success("密码修改成功");
        }

        /// <summary>
        ///     修改密码，密码传入明文
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpPost]
        [Display(Description = "修改密码，密码传入明文")]
        [ApiAuth]
        public ApiResult ChangePayPassword([FromBody] PasswordInput parameter)
        {
            if (parameter.UserId != 0)
            {
                var userModel = Resolve<IUserService>().GetSingle(parameter.UserId);
                if (userModel == null) return ApiResult.Failure("您访问的用户不存在！", MessageCodes.ParameterValidationFailure);
            }
            else
            {
                return ApiResult.Failure("您访问的用户不存在！", MessageCodes.ParameterValidationFailure);
            }

            if (parameter.Password != parameter.ConfirmPassword) return ApiResult.Failure("两次密码输入不一致");

            if (!this.IsFormValid())
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);

            parameter.Type = PasswordType.PayPassword;
            var result = Resolve<IUserDetailService>().ChangePassword(parameter);
            if (!result.Succeeded) return ApiResult.Failure(result.ToString(), MessageCodes.ParameterValidationFailure);
            return ApiResult.Success("密码修改成功");
        }

        /// <summary>
        ///     找回密码，密码传入明文
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpPost]
        [Display(Description = "找回密码，密码传入明文")]
        public ApiResult FindPassword([FromBody] FindPasswordInput parameter)
        {
            parameter.UserName = parameter.Mobile;
            //if (parameter.Email.IsNullOrEmpty()) {
            //    parameter.Email = parameter.UserName + "@5ug.com";
            //}
            if (!this.IsFormValid())
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);

            if (!Resolve<IOpenService>().CheckVerifiyCode(parameter.Mobile, parameter.MobileVerifiyCode.ConvertToLong())
            ) return ApiResult.Failure("手机验证码错误", MessageCodes.ParameterValidationFailure);

            var result = Resolve<IUserDetailService>().FindPassword(parameter);
            if (!result.Succeeded) return ApiResult.Failure(result.ToString(), MessageCodes.ParameterValidationFailure);

            return ApiResult.Success("密码修改成功");
        }

        /// <summary>
        ///     找回密码，密码传入明文
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpPost]
        [Display(Description = "找回密码，密码传入明文")]
        public ApiResult FindPayPassword([FromBody] FindPasswordInput parameter)
        {
            if (!this.IsFormValid())
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            parameter.UserName = parameter.Mobile;
            if (!Resolve<IOpenService>().CheckVerifiyCode(parameter.Mobile, parameter.MobileVerifiyCode.ConvertToLong())
            ) return ApiResult.Failure("手机验证码错误", MessageCodes.ParameterValidationFailure);

            var result = Resolve<IUserDetailService>().FindPayPassword(parameter);
            if (!result.Succeeded) return ApiResult.Failure(result.ToString(), MessageCodes.ParameterValidationFailure);

            return ApiResult.Success("支付密码修改成功");
        }

        /// <summary>
        ///     推荐会员详情
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpGet]
        [Display(Description = "获取推荐会员详情")]
        [ApiAuth]
        public ApiResult<UserOutput> View([FromQuery] ApiBaseInput parameter)
        {
            var userOutPut = Resolve<IUserDetailService>().GetUserOutput(parameter.LoginUserId.ConvertToLong());
            if (userOutPut == null) return ApiResult.Failure<UserOutput>("用户不存在或者已经删除！");

            if (!(userOutPut.ParentId == parameter.LoginUserId || userOutPut.Id == parameter.LoginUserId))
                return ApiResult.Failure<UserOutput>("对不起您无权查看！");

            return ApiResult.Success(userOutPut);
        }

        /// <summary>
        ///     推荐会员详情
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpGet]
        [Display(Description = "获取推荐会员详情")]
        [ApiAuth]
        public ApiResult<AutoPreview> Preview([FromQuery] PreviewInput parameter)
        {
            if (!this.IsFormValid())
                return ApiResult.Failure<AutoPreview>(this.FormInvalidReason(),
                    MessageCodes.ParameterValidationFailure);

            var userOutPut = Resolve<IUserDetailService>().GetUserOutput(parameter.Id.ConvertToLong());
            if (userOutPut == null) return ApiResult.Failure<AutoPreview>("用户不存在或者已经删除！");

            //if (!(userOutPut.ParentId == parameter.LoginUserId || userOutPut.Id == parameter.LoginUserId)) {
            //    return ApiResult.Failure<List<KeyValue>>("对不起您无权查看！");
            //}
            //var result = userOutPut.ToKeyValues();

            return ApiResult.Success(new AutoPreview
            {
                KeyValues = userOutPut.ToKeyValues()
            });
        }

        /// <summary>
        ///     会员详情信息、包括用户名、姓名、手机号地址等新
        /// </summary>
        /// <param name="parameter">Id标识</param>
        [HttpGet]
        [Display(Description = "会员详细信息、包括用户名、姓名、手机号地址等新")]
        public ApiResult<UserOutput> Info(ListInput parameter)
        {
            if (parameter.LoginUserId < 1) return ApiResult.Failure<UserOutput>($"用户ID不能为{parameter.LoginUserId}！");

            var user = Resolve<IUserService>().GetSingle(parameter.LoginUserId);
            if (user == null) return ApiResult.Failure<UserOutput>($"用户ID={parameter.LoginUserId}没有对应数据记录！");
            var userGrade = Resolve<IAutoConfigService>().GetList<UserGradeConfig>(u => u.Id == user.GradeId);
            var userDetail = Resolve<IUserDetailService>().GetSingle(parameter.LoginUserId);

            var userOutput = AutoMapping.SetValue<UserOutput>(user);

            userOutput.GradeName = userGrade.Count > 0 ? userGrade[0]?.Name : string.Empty;
            userOutput.Sex = userDetail.Sex.GetDisplayName();
            userOutput.ParentUserName = Resolve<IUserService>().GetSingle(u => u.Id == user.ParentId)?.UserName;

            userOutput.RegionName = Resolve<IRegionService>().GetRegionNameById(userDetail.RegionId);
            if (!userDetail.Avator.IsNullOrEmpty())
                userOutput.Avator = Resolve<IApiService>().ApiImageUrl(userDetail.Avator);
            else
                userOutput.Avator = Resolve<IApiService>().ApiImageUrl(@"/wwwroot/static/images/avator/man_64.png");
            return ApiResult.Success(userOutput);
        }

        /// <summary>
        ///     确认支付密码  传入明文
        /// </summary>
        [HttpPost]
        [ApiAuth]
        [Display(Description = "确认支付密码，密码传入明文")]
        public ApiResult ConfirmPayPassword([FromBody] ConfirmPayPassword confirmPayPassword)
        {
            var result = Resolve<IUserDetailService>()
                .ConfirmPayPassword(confirmPayPassword.PayPassWord, confirmPayPassword.LoginUserId);
            return ToResult(result);
        }

        /// <summary>
        ///     删除s the 会员.
        /// </summary>
        [HttpDelete]
        [ApiAuth(Filter = FilterType.Admin)]
        public ApiResult Delete(long id)
        {
            if (Resolve<IUserAdminService>().DeleteUser(id)) return ApiResult.Success("会员删除成功");
            return ApiResult.Failure("会员删除失败,服务异常，请稍后再试");
        }

        [HttpGet]
        public ApiResult GetUserGrade()
        {
            var list = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var result = new List<KeyValue>();
            foreach (var item in list)
            {
                var keyValue = new KeyValue
                {
                    Key = item.Id,
                    Value = item.Name
                };
                result.Add(keyValue);
            }

            return ApiResult.Success(result);
        }
    }
}