using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Shop.Store.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.App.Shop.Store.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Store/[action]")]
    public class ApiStoreController : ApiBaseController<Domain.Entities.Store, ObjectId> {

        public ApiStoreController() : base() {
            BaseService = Resolve<IStoreService>();
        }
    }
}