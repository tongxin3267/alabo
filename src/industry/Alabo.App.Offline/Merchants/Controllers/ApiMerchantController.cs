using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.User;
using Alabo.App.Core.UserType.Domain.Enums;
using Alabo.App.Offline.Merchants.Domain.Entities;
using Alabo.App.Offline.Merchants.Domain.Services;
using Alabo.App.Offline.Product.Domain.Entities;
using Alabo.App.Offline.Product.Domain.Services;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.RestfulApi;

namespace Alabo.App.Offline.Merchants.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Merchant/[action]")]
    public class ApiMerchantController : ApiBaseController<Merchant, ObjectId>
    {
        public ApiMerchantController()
            : base()
        {
            BaseService = Resolve<IMerchantService>();
        }
    }
}