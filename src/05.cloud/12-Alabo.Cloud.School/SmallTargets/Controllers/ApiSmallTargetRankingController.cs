using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.User;
using Alabo.App.Market.SmallTargets.Domain.Entities;
using Alabo.App.Market.SmallTargets.Domain.Services;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.RestfulApi;

namespace Alabo.App.Market.SmallTargets.Controllers {

    [ApiExceptionFilter]
    [Route("Api/SmallTargetRanking/[action]")]
    public class ApiSmallTargetRankingController : ApiBaseController<SmallTargetRanking, ObjectId> {

        public ApiSmallTargetRankingController() : base() {
            BaseService = Resolve<ISmallTargetRankingService>();

        }
    }
}