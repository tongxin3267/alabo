﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Shop.Product.Domain.Services;
using Alabo.App.Shop.Product.ViewModels;
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