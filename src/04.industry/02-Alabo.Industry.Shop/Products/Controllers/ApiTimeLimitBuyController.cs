using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Shop.Products.Domain.Services;
using Alabo.Industry.Shop.Products.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Industry.Shop.Products.Controllers
{
    /// <summary>
    ///     ApiTimeLimitBuyController
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/TimeLimitBuy/[action]")]
    public class ApiTimeLimitBuyController : ApiBaseController
    {
        /// <summary>
        ///     限时购列表
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