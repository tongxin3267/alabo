using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Offline.Merchants.Domain.Entities;
using Alabo.App.Offline.Merchants.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.App.Offline.Merchants.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Merchant/[action]")]
    public class ApiMerchantController : ApiBaseController<Merchant, ObjectId> {

        public ApiMerchantController()
            : base() {
            BaseService = Resolve<IMerchantService>();
        }
    }
}