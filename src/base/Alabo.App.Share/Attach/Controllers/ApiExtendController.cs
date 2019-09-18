using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.App.Share.Attach.Domain.Services;

namespace Alabo.App.Share.Attach.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Extend/[action]")]
    public class ApiExtendController : ApiBaseController<Extend, ObjectId> {

        public ApiExtendController() : base() {
            BaseService = Resolve<IExtendService>();
        }
    }
}