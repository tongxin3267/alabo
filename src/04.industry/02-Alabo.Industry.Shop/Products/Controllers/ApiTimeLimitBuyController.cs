using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Shop.Product.Domain.Services;
using Alabo.App.Shop.Product.ViewModels;
using Alabo.Core.WebApis.Controller;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Product.Controllers
{
    /// <summary>
    /// ApiTimeLimitBuyController
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/TimeLimitBuy/[action]")]
    public class ApiTimeLimitBuyController : ApiBaseController
    {
        /// <summary>
        /// constructor
        /// </summary>
        public ApiTimeLimitBuyController() : base()
        {
        }

        /// <summary>
        /// 限时购列表
        /// </summary>
        [HttpGet]
        [Display(Description = "限时购列表")]
        public ApiResult<List<TimeLimitBuyItem>> GetList()
        {
            var result = Resolve<IProductService>().GetTimeLimitBuyList();
            return ApiResult.Success(result);
        }
    }
}
