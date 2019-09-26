using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.User;
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