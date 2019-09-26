using Alabo.App.Agent.Citys.Domain.Entities;
using Alabo.App.Agent.Citys.Domain.Services;
using Alabo.App.Core.Employes.Domain.Dtos;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Agent.Citys.Controllers
{

    [ApiExceptionFilter]
    [Route("Api/City/[action]")]
    public class ApiCityController : ApiBaseController<City, ObjectId> {
        
       

        public ApiCityController() : base() {
            BaseService = Resolve<ICityService>();
        }

        /// <summary>
        /// 城市合伙人登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<RoleOuput> Login([FromBody]UserOutput userOutput) {
            return null;
            ////Todo 城市合伙人登录
            //var result = Resolve<IEmployeeService>().Login(userOutput, () =>
            //{
            //    return (true, FilterType.City);
            //});
            //return Open.ApiBase.Models.ApiResult.Success(result);
        }

        [HttpGet]
        public ApiResult ChangeStatus([FromQuery]string UserId, string Status) {
            if (string.IsNullOrEmpty(UserId)) {
                return ApiResult.Failure("参数传入失败");
            }
            var b = Resolve<ICityService>().ChangeUserStatus(UserId, Status);

            if (b.Succeeded) {
                return ApiResult.Success();
            } else {
                return ApiResult.Failure("操作失败");
            }
        }
    }
}