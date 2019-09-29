using Alabo.Data.People.Users.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Users.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Alabo.Data.People.Users.Controllers
{
    /// <summary>
    ///     用户相关Api接口
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/UserDetail/[action]")]
    public class ApiUserDetailController : ApiBaseController<UserDetail, long>
    {
        /// <summary>
        /// </summary>
        public ApiUserDetailController(
        )
        {
            BaseService = Resolve<IUserDetailService>();
        }
    }
}