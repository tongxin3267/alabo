using Microsoft.AspNetCore.Mvc;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Users.Entities;

namespace Alabo.App.Core.User.Controllers {

    [ApiExceptionFilter]
    [Route("Api/UserMap/[action]")]
    public class ApiUserMapController : ApiBaseController<UserMap, long> {

        public ApiUserMapController() : base() {
            BaseService = Resolve<IUserMapService>();
        }
    }
}