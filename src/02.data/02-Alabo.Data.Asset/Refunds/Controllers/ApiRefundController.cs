using Alabo.App.Asset.Refunds.Domain.Entities;
using Alabo.App.Asset.Refunds.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Alabo.App.Asset.Refunds.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Refund/[action]")]
    public class ApiRefundController : ApiBaseController<Refund, long>
    {
        public ApiRefundController()
        {
            BaseService = Resolve<IRefundService>();
        }
    }
}