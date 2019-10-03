using Alabo.Cloud.People.Identities.Domain.Entities;
using Alabo.Cloud.People.Identities.Domain.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Dtos;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.UI.Design.AutoForms;
using Alabo.Users.Enum;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Cloud.People.Identities.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Identity/[action]")]
    public class ApiIdentityController : ApiBaseController<Identity, ObjectId>
    {
        public ApiIdentityController()
        {
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
        /// </summary>
        /// <param name="loginUserId"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "ʵ����֤")]
        [ApiAuth]
        public ApiResult IdentityInfo(long loginUserId)
        {
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
        public ApiResult<AutoForm> GetIdentity([FromQuery] ApiBaseInput parameter)
        {
            var model = Resolve<IIdentityService>().GetSingle(parameter.LoginUserId);
            var result = new AutoForm();
            if (model != null)
            {
                if (model.Status == IdentityStatus.Succeed)
                {
                    result.FromMessage.Type = FromMessageType.Success;
                    result.FromMessage.Message = "�������ʵ����֤!";
                    return ApiResult.Success(result);
                }

                if (model.Status == IdentityStatus.Failed)
                {
                    result.FromMessage.Type = FromMessageType.Success;
                    result.FromMessage.Message = "����ʵ����֤ʧ�ܣ����ٴγ���!";
                    return ApiResult.Success(result);
                }
            }

            return ApiResult.Success(result);
        }

        [HttpPost]
        public ApiResult Identity([FromBody] Identity identity)
        {
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
        public ApiResult FaceIdentity([FromQuery] Identity identity)
        {
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