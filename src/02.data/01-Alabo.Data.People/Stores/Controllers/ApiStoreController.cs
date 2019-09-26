using Alabo.Data.People.Stores.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Data.People.Stores.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Store/[action]")]
    public class ApiStoreController : ApiBaseController<Domain.Entities.Store, ObjectId> {

        public ApiStoreController() : base() {
            BaseService = Resolve<IStoreService>();
        }
    }
}