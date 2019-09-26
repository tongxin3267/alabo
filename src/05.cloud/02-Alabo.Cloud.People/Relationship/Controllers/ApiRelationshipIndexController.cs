using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.User;
using Alabo.App.Market.Relationship.Domain.Entities;
using Alabo.App.Market.Relationship.Domain.Services;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.RestfulApi;

namespace Alabo.App.Market.Relationship.Controllers {

    [ApiExceptionFilter]
    [Route("Api/RelationshipIndex/[action]")]
    public class ApiRelationshipIndexController : ApiBaseController<RelationshipIndex, ObjectId> {

        public ApiRelationshipIndexController() : base() {
            BaseService = Resolve<IRelationshipIndexService>();

        }
    }
}