using Alabo.Cloud.Cms.BookDonae.Domain.Entities;
using Alabo.Cloud.Cms.BookDonae.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Cloud.Cms.BookDonae.Controllers {

    [ApiExceptionFilter]
    [Route("Api/BooksClass/[action]")]
    public class ApiBooksClassController : ApiBaseController<BooksClass, ObjectId> {

        public ApiBooksClassController() : base() {
            BaseService = Resolve<IBooksClassService>();
        }
    }
}