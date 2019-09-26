using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Market.SmallTargets.Domain.Entities;
using Alabo.App.Market.SmallTargets.Domain.Services;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.RestfulApi;

namespace Alabo.App.Market.SmallTargets.Controllers {

    [ApiExceptionFilter]
    [Route("Api/SmallTarget/[action]")]
    public class ApiSmallTargetController : ApiBaseController<SmallTarget, ObjectId> {

        public ApiSmallTargetController() : base() {
            BaseService = Resolve<ISmallTargetService>();

        }
    }
}