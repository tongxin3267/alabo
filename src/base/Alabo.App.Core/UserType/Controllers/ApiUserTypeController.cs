using Microsoft.AspNetCore.Mvc;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.UserType.Domain.Services;

namespace Alabo.App.Core.UserType.Controllers {

    [ApiExceptionFilter]
    [Route("Api/UserType/[action]")]
    public class ApiUserTypeController : ApiBaseController<Domain.Entities.UserType, long> {

        public ApiUserTypeController() : base() {
            BaseService = Resolve<IUserTypeService>();
        }
    }
}