using Alabo.Cloud.Shop.Comments.Domain.Entities;
using Alabo.Cloud.Shop.Comments.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.Comments.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Comment/[action]")]
    public class ApiCommentController : ApiBaseController<Comment, ObjectId> {

        public ApiCommentController() : base() {
            BaseService = Resolve<ICommentService>();
        }
    }
}