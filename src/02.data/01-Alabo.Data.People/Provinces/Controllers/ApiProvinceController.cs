using Alabo.App.Agent.Province.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.App.Agent.Province.Controllers
{

    [ApiExceptionFilter]
    [Route("Api/Province/[action]")]
    public class ApiProvinceController : ApiBaseController<Domain.Entities.Province, ObjectId> {
        
       

        public ApiProvinceController() : base() {
            BaseService = Resolve<IProvinceService>();
       
        }
    }
}