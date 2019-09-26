using Alabo.Data.People.Cities.Domain.Entities;
using Alabo.Data.People.Cities.Domain.Services;
using Alabo.Data.People.Employes.Dtos;
using Alabo.Data.People.Users.Dtos;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Data.People.Cities.Controllers
{

    [ApiExceptionFilter]
    [Route("Api/City/[action]")]
    public class ApiCityController : ApiBaseController<City, ObjectId> {
        
       

        public ApiCityController() : base() {
            BaseService = Resolve<ICityService>();
        }

        /// <summary>
        /// ���кϻ��˵�¼
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<RoleOuput> Login([FromBody]UserOutput userOutput) {
            return null;
            ////Todo ���кϻ��˵�¼
            //var result = Resolve<IEmployeeService>().Login(userOutput, () =>
            //{
            //    return (true, FilterType.City);
            //});
            //return Open.ApiBase.Models.ApiResult.Success(result);
        }

        [HttpGet]
        public ApiResult ChangeStatus([FromQuery]string UserId, string Status) {
            if (string.IsNullOrEmpty(UserId)) {
                return ApiResult.Failure("��������ʧ��");
            }
            var b = Resolve<ICityService>().ChangeUserStatus(UserId, Status);

            if (b.Succeeded) {
                return ApiResult.Success();
            } else {
                return ApiResult.Failure("����ʧ��");
            }
        }
    }
}