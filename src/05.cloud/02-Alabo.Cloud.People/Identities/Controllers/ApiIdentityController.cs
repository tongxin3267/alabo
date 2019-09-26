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
        ///     ��ӻ��޸�
        ///     Identities the specified parameter.
        /// </summary>
        /// <param name="parameter">����</param>
        /// <returns>ApiResult.</returns>
        //[HttpPost]
        //[Display(Description = "ʵ����֤")]
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

        //    return ApiResult.Failure("��֤ʧ��");
        //}

        /// <summary>
        ///
        /// </summary>
        /// <param name="loginUserId"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "ʵ����֤")]
        [ApiAuth]
        public ApiResult IdentityInfo(long loginUserId) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var identity = Resolve<IIdentityService>().GetSingle(u => u.UserId == loginUserId);
            identity.Status = IdentityStatus.Succeed;

            var result = Resolve<IIdentityService>().Update(identity);
            if (result) {
                return ApiResult.Success("��֤�ɹ�");
            }

            return ApiResult.Failure("��֤ʧ��");
        }

        /// <summary>
        ///     ��ȡʵ����֤��Ϣ
        ///     Gets the specified parameter.
        /// </summary>
        /// <param name="parameter">����</param>
        /// <returns>ApiResult&lt;IdentityApiOutput&gt;.</returns>
        [HttpGet]
        [Display(Description = "��ȡʵ����֤��Ϣ")]
        [ApiAuth]
        public ApiResult<AutoForm> GetIdentity([FromQuery] ApiBaseInput parameter) {
            var model = Resolve<IIdentityService>().GetSingle(parameter.LoginUserId);
            var result = new AutoForm();
            if (model != null) {
                if (model.Status == IdentityStatus.Succeed) {
                    result.FromMessage.Type = FromMessageType.Success;
                    result.FromMessage.Message = "�������ʵ����֤!";
                    return ApiResult.Success(result);
                }
                if (model.Status == IdentityStatus.Failed) {
                    result.FromMessage.Type = FromMessageType.Success;
                    result.FromMessage.Message = "����ʵ����֤ʧ�ܣ����ٴγ���!";
                    return ApiResult.Success(result);
                }
            }

            return ApiResult.Success(result);
        }

        [HttpPost]
        public ApiResult Identity([FromBody]Identity identity) {
            if (identity == null) {
                return ApiResult.Failure("ʵ�岻��Ϊ��");
            }
            var result = Resolve<IIdentityService>().Identity(identity);
            if (result.Succeeded) {
                return ApiResult.Success("��֤�ɹ���");
            }
            return ApiResult.Failure("��֤ʧ��");
        }

        [HttpPost]
        [ApiAuth]
        public ApiResult FaceIdentity([FromQuery] Identity identity) {
            if (identity == null) {
                return ApiResult.Failure("ʵ�岻��Ϊ��");
            }

            var result = Resolve<IIdentityService>().FaceIdentity(identity);
            if (result.Succeeded) {
                return ApiResult.Success("��֤�ɹ���");
            }
            return ApiResult.Failure("��֤ʧ��");
        }
    }
}