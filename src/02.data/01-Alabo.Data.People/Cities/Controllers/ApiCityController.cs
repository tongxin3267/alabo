using Alabo.Data.People.Cities.Domain.Entities;
using Alabo.Data.People.Cities.Domain.Services;
using Alabo.Data.People.Employes.Dtos;
using Alabo.Data.People.Users.Dtos;
using Alabo.Framework.Basic.PostRoles.Dtos;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Data.People.Cities.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/City/[action]")]
    public class ApiCityController : ApiBaseController<City, ObjectId>
    {
        public ApiCityController()
        {
            BaseService = Resolve<ICityService>();
        }

        /// <summary>
        ///     城市合伙人登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<RoleOuput> Login([FromBody] UserOutput userOutput)
        {
            return null;
            ////Todo 城市合伙人登录
            //var result = Resolve<IEmployeeService>().Login(userOutput, () =>
            //{
            //    return (true, FilterType.City);
            //});
            //return Open.ApiBase.Models.ApiResult.Success(result);
        }

        [HttpGet]
        public ApiResult ChangeStatus([FromQuery] string userId, string status)
        {
            if (string.IsNullOrEmpty(userId)) {
                return ApiResult.Failure("参数传入失败");
            }

            var b = Resolve<ICityService>().ChangeUserStatus(userId, status);

            if (b.Succeeded) {
                return ApiResult.Success();
            }

            return ApiResult.Failure("操作失败");
        }
    }
}