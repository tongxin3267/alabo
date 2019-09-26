using Alabo.Cloud.Cms.Votes.Domain.Entities;
using Alabo.Cloud.Cms.Votes.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Cloud.Cms.Votes.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Vote/[action]")]
    public class ApiVoteController : ApiBaseController<Vote, ObjectId>
    {
        public ApiVoteController()
        {
            BaseService = Resolve<IVoteService>();
        }
    }
}