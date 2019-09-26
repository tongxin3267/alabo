using Microsoft.AspNetCore.Mvc;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Services;
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