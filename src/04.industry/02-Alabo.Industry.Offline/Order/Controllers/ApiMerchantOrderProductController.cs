using Microsoft.AspNetCore.Mvc;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.User;
using Alabo.App.Offline.Order.Domain.Entities;
using Alabo.App.Offline.Order.Domain.Services;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.RestfulApi;

namespace Alabo.App.Offline.Order.Controllers
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