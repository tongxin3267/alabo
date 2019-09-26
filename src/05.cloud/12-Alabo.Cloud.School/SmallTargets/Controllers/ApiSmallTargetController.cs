using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.User;
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