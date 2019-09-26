using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.Finance.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Core.Finance.Controllers {

    [ApiExceptionFilter]
    [Route("Api/BankCard/[action]")]
    public class ApiBankCardController : ApiBaseController<BankCard, ObjectId> {

        public ApiBankCardController() : base() {
            BaseService = Resolve<IBankCardService>();
        }

        /// <summary>
        ///     ���¡���ӡ��������п�
        /// </summary>
        [HttpPost]
        [Display(Description = "���¡���ӡ��������п���ַ")]
        [ApiAuth]
        public ApiResult AddBankCard([FromBody] ApiBankCardInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var result = Resolve<IBankCardService>().AddOrUpdateBankCard(parameter);
            return ToResult(result);
        }

        /// <summary>
        ///     ��ȡ�û��������п�
        /// </summary>
        [HttpGet]
        [Display(Description = "��ȡ�û��������п�")]
        [ApiAuth]
        public ApiResult<IList<KeyValue>> GetList([FromQuery] long loginUserId) {
            var userId = AutoModel.BasicUser.Id;
            if (userId < 0) {
                return ApiResult.Failure<IList<KeyValue>>("�û�������!");
            }

            var result = Resolve<IBankCardService>().GetUserBankCardList(userId);
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     ɾ�����п�
        /// </summary>
        [HttpPost]
        [Display(Description = "ɾ�����п�")]
        [ApiAuth]
        public ApiResult Delete([FromQuery] ApiBankCardInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var result = Resolve<IBankCardService>().RomoveBankCard(parameter);
            return ToResult(result);
        }

        /// <summary>
        /// �����û�ID��ȡ���п�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetBankCardListByUserId(long userId) {
            var result = Resolve<IBankCardService>().GetBankCardByUserId(userId);
            return ApiResult.Success(result);
        }

        /// <summary>
        /// ɾ�����п�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult DeleteBankCard(string id) {
            var result = Resolve<IBankCardService>().DeleteBankCard(id);
            if (result.Succeeded) {
                return ApiResult.Success("���ɹ�");
            }
            return ApiResult.Failure("���ʧ��");
        }
    }
}