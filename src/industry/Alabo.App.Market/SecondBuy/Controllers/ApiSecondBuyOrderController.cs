using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using MongoDB.Bson;
using Alabo.App.Core.User;
using Alabo.RestfulApi;using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.App.Market.SecondBuy.Domain.Entities;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Market.SecondBuy.Domain.Services;
using ZKCloud.Open.ApiBase.Models;
using Alabo.Extensions;
using System.Collections.Generic;

namespace Alabo.App.Market.SecondBuy.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/SecondBuyOrder/[action]")]
    public class ApiSecondBuyOrderController : ApiBaseController<SecondBuyOrder, ObjectId>
    {

        public ApiSecondBuyOrderController(): base()
        {

            BaseService = Resolve<ISecondBuyOrderService>();
        }

        /// <summary>
        ///下单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Buy([FromBody]SecondBuyOrder model) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(),
                    MessageCodes.ParameterValidationFailure);
            }
            var result = Resolve<ISecondBuyOrderService>().Buy(model);
            return ToResult(result);
        }

        /// <summary>
        /// 最近购买
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<string>> BuyList([FromQuery]long productId) {
           
            var result = Resolve<ISecondBuyOrderService>().BuyList(productId);
            return ApiResult.Success(result);
  
        }

        /// <summary>
        /// 最近购买
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<string>> BuyListRcently([FromQuery]long productId) {

            var result = Resolve<ISecondBuyOrderService>().BuyListRcently(productId);
            return ApiResult.Success(result);

        }

    }
}
