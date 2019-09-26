using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Dtos;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Core.Enums.Enum;
using Alabo.Core.WebUis.Design.AutoForms;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.User.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Identity/[action]")]
    public class ApiIdentityController : ApiBaseController<Identity, ObjectId> {

        public ApiIdentityController() : base() {
            BaseService = Resolve<IIdentityService>();
        }

        /// <summary>
        ///     添加或修改
        ///     Identities the specified parameter.
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>ApiResult.</returns>
        //[HttpPost]
        //[Display(Description = "实名认证")]
        //[ApiAuth]
        //[ApiView(typeof(Identity), PageType = ViewPageType.Edit)]
        //public ApiResult Identity([FromBody] IdentityView parameter) {
        //    if (!this.IsFormValid()) {
        //        return ApiResult.Failure<UserOutput>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
        //    }

        //    parameter.Status = IdentityStatus.IsPost;
        //    var result = Resolve<IIdentityService>().AddOrUpdate(parameter);
        //    if (result.Succeeded) {
        //        return ApiResult.Success();
        //    }

        //    return ApiResult.Failure("认证失败");
        //}

        /// <summary>
        ///
        /// </summary>
        /// <param name="loginUserId"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "实名认证")]
        [ApiAuth]
        public ApiResult IdentityInfo(long loginUserId) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var identity = Resolve<IIdentityService>().GetSingle(u => u.UserId == loginUserId);
            identity.Status = IdentityStatus.Succeed;

            var result = Resolve<IIdentityService>().Update(identity);
            if (result) {
                return ApiResult.Success("认证成功");
            }

            return ApiResult.Failure("认证失败");
        }

        /// <summary>
        ///     获取实名认证信息
        ///     Gets the specified parameter.
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>ApiResult&lt;IdentityApiOutput&gt;.</returns>
        [HttpGet]
        [Display(Description = "获取实名认证信息")]
        [ApiAuth]
        public ApiResult<AutoForm> GetIdentity([FromQuery] ApiBaseInput parameter) {
            var model = Resolve<IIdentityService>().GetSingle(parameter.LoginUserId);
            var result = new AutoForm();
            if (model != null) {
                if (model.Status == IdentityStatus.Succeed) {
                    result.FromMessage.Type = FromMessageType.Success;
                    result.FromMessage.Message = "您已完成实名认证!";
                    return ApiResult.Success(result);
                }
                if (model.Status == IdentityStatus.Failed) {
                    result.FromMessage.Type = FromMessageType.Success;
                    result.FromMessage.Message = "您的实名认证失败，请再次尝试!";
                    return ApiResult.Success(result);
                }
            }

            return ApiResult.Success(result);
        }

        [HttpPost]
        public ApiResult Identity([FromBody]Identity identity) {
            if (identity == null) {
                return ApiResult.Failure("实体不能为空");
            }
            var result = Resolve<IIdentityService>().Identity(identity);
            if (result.Succeeded) {
                return ApiResult.Success("认证成功！");
            }
            return ApiResult.Failure("认证失败");
        }

        [HttpPost]
        [ApiAuth]
        public ApiResult FaceIdentity([FromQuery] Identity identity) {
            if (identity == null) {
                return ApiResult.Failure("实体不能为空");
            }

            var result = Resolve<IIdentityService>().FaceIdentity(identity);
            if (result.Succeeded) {
                return ApiResult.Success("认证成功！");
            }
            return ApiResult.Failure("认证失败");
        }
    }
}