using Alabo.Data.People.Users.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Users.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Alabo.Data.People.Users.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/UserMap/[action]")]
    public class ApiUserMapController : ApiBaseController<UserMap, long>
    {
        public ApiUserMapController()
        {
            BaseService = Resolve<IUserMapService>();
        }
    }
}