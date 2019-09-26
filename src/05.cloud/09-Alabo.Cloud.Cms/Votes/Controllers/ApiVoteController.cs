using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Market.Votes.Domain.Entities;
using Alabo.App.Market.Votes.Domain.Services;
using Alabo.RestfulApi;

namespace Alabo.App.Market.Votes.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Vote/[action]")]
    public class ApiVoteController : ApiBaseController<Vote, ObjectId> {

        public ApiVoteController() : base()
        {
            BaseService = Resolve<IVoteService>();
        }
    }
}