using Alabo.Cloud.People.UserDigitals.Domain.Entities;
using Alabo.Cloud.People.UserDigitals.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Cloud.People.UserDigitals.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/UserDigital/[action]")]
    public class ApiUserDigitalController : ApiBaseController<UserDigital, ObjectId>
    {
        public ApiUserDigitalController()
        {
            BaseService = Resolve<IUserDigitalService>();
        }
    }
}