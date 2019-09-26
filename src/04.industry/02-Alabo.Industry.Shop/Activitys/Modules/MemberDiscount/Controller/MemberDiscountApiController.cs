using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.Core.WebApis.Controller;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Activitys.Modules.MemberDiscount.Controller
{
    /// <summary>
    /// 预售API
    /// </summary>
    [ApiExceptionFilter, Route("Api/MemberDiscount/[action]")]
    public class MemberDiscountApiController : ApiBaseController
    {
        /// <summary>
        /// ctor
        /// </summary>
        public MemberDiscountApiController() : base()
        {
        }

        /// <summary>
        /// MemberDiscountList
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ApiResult<List<String>> MemberDiscountList([FromQuery] long productId)
        {
            return ApiResult.Success(new List<String>() { "T1", "T2" });
        }
    }
}
