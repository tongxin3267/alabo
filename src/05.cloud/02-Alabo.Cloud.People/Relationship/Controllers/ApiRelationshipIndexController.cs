using Alabo.Cloud.People.Relationship.Domain.Entities;
using Alabo.Cloud.People.Relationship.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Cloud.People.Relationship.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/RelationshipIndex/[action]")]
    public class ApiRelationshipIndexController : ApiBaseController<RelationshipIndex, ObjectId>
    {
        public ApiRelationshipIndexController()
        {
            BaseService = Resolve<IRelationshipIndexService>();
        }
    }
}