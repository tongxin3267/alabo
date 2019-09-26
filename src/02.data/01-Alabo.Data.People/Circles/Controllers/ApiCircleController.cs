using Alabo.Data.People.Circles.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Data.People.Circles.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Circle/[action]")]
    public class ApiCircleController : ApiBaseController<Domain.Entities.Circle, ObjectId> {
        
       

        public ApiCircleController() : base() {
            BaseService = Resolve<ICircleService>();
       
        }
    }
}