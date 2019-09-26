using Alabo.Data.People.Users.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using UserDetail = Alabo.Users.Entities.UserDetail;

namespace Alabo.Data.People.Users.Controllers {

    /// <summary>
    ///     用户相关Api接口
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/UserDetail/[action]")]
    public class ApiUserDetailController : ApiBaseController<UserDetail, long> {

        /// <summary>
        /// </summary>
        public ApiUserDetailController(
            ) : base() {
            BaseService = Resolve<IUserDetailService>();
        }
    }
}