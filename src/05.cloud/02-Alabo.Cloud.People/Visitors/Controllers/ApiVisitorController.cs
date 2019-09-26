using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Core.Markets.Visitors.Domain.Entities;
using Alabo.App.Core.Markets.Visitors.Domain.Services;

namespace Alabo.App.Core.Markets.Visitors.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Visitor/[action]")]
    public class ApiVisitorController : ApiBaseController<Visitor, ObjectId> {

        public ApiVisitorController() : base() {
            BaseService = Resolve<IVisitorService>();
        }
    }
}