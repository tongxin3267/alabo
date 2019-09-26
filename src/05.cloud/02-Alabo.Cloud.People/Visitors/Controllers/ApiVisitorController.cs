using Alabo.Cloud.People.Visitors.Domain.Entities;
using Alabo.Cloud.People.Visitors.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Cloud.People.Visitors.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Visitor/[action]")]
    public class ApiVisitorController : ApiBaseController<Visitor, ObjectId>
    {
        public ApiVisitorController()
        {
            BaseService = Resolve<IVisitorService>();
        }
    }
}