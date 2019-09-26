using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.App.Share.Attach.Domain.Services;

namespace Alabo.App.Share.Attach.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Comment/[action]")]
    public class ApiCommentController : ApiBaseController<Comment, ObjectId> {

        public ApiCommentController() : base() {
            BaseService = Resolve<ICommentService>();
        }
    }
}