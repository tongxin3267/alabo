using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Alabo.Industry.Shop.Activitys.Modules.BuyPermision.Controller
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
