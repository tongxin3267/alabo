using _01_Alabo.Cloud.Core.Extends.Domain.Entities;
using _01_Alabo.Cloud.Core.Extends.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace _01_Alabo.Cloud.Core.Extends.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Extend/[action]")]
    public class ApiExtendController : ApiBaseController<Extend, ObjectId>
    {
        public ApiExtendController()
        {
            BaseService = Resolve<IExtendService>();
        }
    }
}