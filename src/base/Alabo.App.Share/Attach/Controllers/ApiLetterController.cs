using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.App.Share.Attach.Domain.Services;

namespace Alabo.App.Share.Attach.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Letter/[action]")]
    public class ApiLetterController : ApiBaseController<Letter, ObjectId> {

        public ApiLetterController() : base() {
            BaseService = Resolve<ILetterService>();
        }
    }
}