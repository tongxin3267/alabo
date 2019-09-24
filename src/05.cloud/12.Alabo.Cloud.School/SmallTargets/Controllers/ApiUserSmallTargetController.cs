using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.User;
using Alabo.App.Market.SmallTargets.Domain.Entities;
using Alabo.App.Market.SmallTargets.Domain.Services;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.RestfulApi;

namespace Alabo.App.Market.SmallTargets.Controllers {

    [ApiExceptionFilter]
    [Route("Api/UserSmallTarget/[action]")]
    public class ApiUserSmallTargetController : ApiBaseController<UserSmallTarget, ObjectId> {

        public ApiUserSmallTargetController() : base() {
            BaseService = Resolve<IUserSmallTargetService>();

        }
    }
}