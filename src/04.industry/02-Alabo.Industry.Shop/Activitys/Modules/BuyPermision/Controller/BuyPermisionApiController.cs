﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.Core.WebApis.Controller;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Activitys.Modules.BuyPermision.Controller
{
    /// <summary>
    /// 预售API
    /// </summary>
    [ApiExceptionFilter, Route("Api/BuyPermision/[action]")]
    public class BuyPermisionApiController : ApiBaseController
    {
        /// <summary>
        /// ctor
        /// </summary>
        public BuyPermisionApiController() : base()
        {
        }
    }
}
