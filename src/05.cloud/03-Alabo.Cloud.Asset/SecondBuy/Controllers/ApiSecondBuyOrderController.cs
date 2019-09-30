using Alabo.Cloud.Asset.SecondBuy.Domain.Entities;
using Alabo.Cloud.Asset.SecondBuy.Domain.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Cloud.Asset.SecondBuy.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/SecondBuyOrder/[action]")]
    public class ApiSecondBuyOrderController : ApiBaseController<SecondBuyOrder, ObjectId>
    {
        public ApiSecondBuyOrderController()
        {
            BaseService = Resolve<ISecondBuyOrderService>();
        }

        /// <summary>
        ///     下单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Buy([FromBody] SecondBuyOrder model)
        {
            if (!this.IsFormValid())
                return ApiResult.Failure(this.FormInvalidReason(),
                    MessageCodes.ParameterValidationFailure);
            var result = Resolve<ISecondBuyOrderService>().Buy(model);
            return ToResult(result);
        }

        /// <summary>
        ///     最近购买
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<string>> BuyList([FromQuery] long productId)
        {
            var result = Resolve<ISecondBuyOrderService>().BuyList(productId);
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     最近购买
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<string>> BuyListRcently([FromQuery] long productId)
        {
            var result = Resolve<ISecondBuyOrderService>().BuyListRcently(productId);
            return ApiResult.Success(result);
        }
    }
}