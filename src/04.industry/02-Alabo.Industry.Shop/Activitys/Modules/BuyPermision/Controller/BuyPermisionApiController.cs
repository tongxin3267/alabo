using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Core.WebApis.Controller;
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
