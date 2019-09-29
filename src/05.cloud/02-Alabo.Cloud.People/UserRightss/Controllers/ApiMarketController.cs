using Alabo.Data.People.Employes.Dtos;
using Alabo.Data.People.Users.Dtos;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Cloud.People.UserRightss.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Market/[action]")]
    public class ApiMarketController : ApiBaseController
    {
        /// <summary>
        ///     营销中心登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<RoleOuput> Login([FromBody] UserOutput userOutput)
        {
            return null;
            //var result = Resolve<IEmployeeService>().Login(userOutput, () =>
            //{
            //    var isMarker = userOutput.GradeId == Guid.Parse("cc873faa-749b-449b-b85a-c7d26f626feb");
            //    return (isMarker, FilterType.Market);
            //});
            //return ZKCloud.Open.ApiBase.Models.ApiResult.Success(result);
        }
    }
}