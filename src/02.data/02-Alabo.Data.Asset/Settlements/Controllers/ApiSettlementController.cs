using Alabo.App.Asset.Settlements.Domain.Entities;
using Alabo.App.Asset.Settlements.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Alabo.App.Asset.Settlements.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Settlement/[action]")]
    public class ApiSettlementController : ApiBaseController<Settlement, long>
    {
        public ApiSettlementController()
        {
            BaseService = Resolve<ISettlementService>();
        }
    }
}