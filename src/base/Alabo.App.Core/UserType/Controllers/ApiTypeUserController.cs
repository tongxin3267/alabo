using Microsoft.AspNetCore.Mvc;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.UserType.Domain.Entities;
using Alabo.App.Core.UserType.Domain.Services;

namespace Alabo.App.Core.UserType.Controllers {

    [ApiExceptionFilter]
    [Route("Api/TypeUser/[action]")]
    public class ApiTypeUserController : ApiBaseController<TypeUser, long> {

        public ApiTypeUserController() : base() {
            BaseService = Resolve<ITypeUserService>();
        }
    }
}