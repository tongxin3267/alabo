using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.Framework.Basic.Letters.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.App.Share.Attach.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Letter/[action]")]
    public class ApiLetterController : ApiBaseController<Letter, ObjectId> {

        public ApiLetterController() : base() {
            BaseService = Resolve<ILetterService>();
        }
    }
}