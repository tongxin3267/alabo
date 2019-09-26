using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Offline.Order.Domain.Entities;
using Alabo.Industry.Offline.Order.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alabo.Industry.Offline.Order.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/MerchantOrderProduct/[action]")]
    public class ApiMerchantOrderProductController : ApiBaseController<MerchantOrderProduct, long>
    {

        public ApiMerchantOrderProductController() 
            : base() {
            BaseService = Resolve<IMerchantOrderProductService>();
        }
    }
}