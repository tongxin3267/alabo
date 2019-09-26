using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Asset.BankCards.Domain.Entities;
using Alabo.App.Asset.BankCards.Domain.Services;
using Alabo.App.Asset.BankCards.Dtos;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Asset.BankCards.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/BankCard/[action]")]
    public class ApiBankCardController : ApiBaseController<BankCard, ObjectId>
    {
        public ApiBankCardController()
        {
            BaseService = Resolve<IBankCardService>();
        }

        /// <summary>
        ///     更新、添加、保存银行卡
        /// </summary>
        [HttpPost]
        [Display(Description = "更新、添加、保存银行卡地址")]
        [ApiAuth]
        public ApiResult AddBankCard([FromBody] ApiBankCardInput parameter)
        {
            if (!this.IsFormValid())
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);

            var result = Resolve<IBankCardService>().AddOrUpdateBankCard(parameter);
            return ToResult(result);
        }

        /// <summary>
        ///     获取用户所有银行卡
        /// </summary>
        [HttpGet]
        [Display(Description = "获取用户所有银行卡")]
        [ApiAuth]
        public ApiResult<IList<KeyValue>> GetList([FromQuery] long loginUserId)
        {
            var userId = AutoModel.BasicUser.Id;
            if (userId < 0) return ApiResult.Failure<IList<KeyValue>>("用户不存在!");

            var result = Resolve<IBankCardService>().GetUserBankCardList(userId);
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     删除银行卡
        /// </summary>
        [HttpPost]
        [Display(Description = "删除银行卡")]
        [ApiAuth]
        public ApiResult Delete([FromQuery] ApiBankCardInput parameter)
        {
            if (!this.IsFormValid())
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);

            var result = Resolve<IBankCardService>().RomoveBankCard(parameter);
            return ToResult(result);
        }

        /// <summary>
        ///     根据用户ID获取银行卡
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetBankCardListByUserId(long userId)
        {
            var result = Resolve<IBankCardService>().GetBankCardByUserId(userId);
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     删除银行卡
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult DeleteBankCard(string id)
        {
            var result = Resolve<IBankCardService>().DeleteBankCard(id);
            if (result.Succeeded) return ApiResult.Success("解绑成功");
            return ApiResult.Failure("解绑失败");
        }
    }
}