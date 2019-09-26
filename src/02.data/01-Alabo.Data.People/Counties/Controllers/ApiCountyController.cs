using Alabo.Data.People.Counties.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Data.People.Counties.Controllers {

    [ApiExceptionFilter]
    [Route("Api/County/[action]")]
    public class ApiCountyController : ApiBaseController<Domain.Entities.County, ObjectId> {
        
       

        public ApiCountyController() : base() {
            BaseService = Resolve<ICountyService>();
       
        }
    }
}