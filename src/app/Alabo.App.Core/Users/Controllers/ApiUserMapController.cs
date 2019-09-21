using Microsoft.AspNetCore.Mvc;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Services;

namespace Alabo.App.Core.User.Controllers {

    [ApiExceptionFilter]
    [Route("Api/UserMap/[action]")]
    public class ApiUserMapController : ApiBaseController<UserMap, long> {

        public ApiUserMapController() : base() {
            BaseService = Resolve<IUserMapService>();
        }
    }
}