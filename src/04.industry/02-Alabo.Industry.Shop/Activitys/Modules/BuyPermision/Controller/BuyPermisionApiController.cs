using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
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
