using System.Collections.Generic;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Industry.Shop.Activitys.Modules.MemberDiscount.Controller
{
    /// <summary>
    ///     预售API
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/MemberDiscount/[action]")]
    public class MemberDiscountApiController : ApiBaseController
    {
        /// <summary>
        ///     MemberDiscountList
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ApiResult<List<string>> MemberDiscountList([FromQuery] long productId)
        {
            return ApiResult.Success(new List<string> {"T1", "T2"});
        }
    }
}