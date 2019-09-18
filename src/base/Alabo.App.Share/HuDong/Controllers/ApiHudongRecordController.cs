using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Open.HuDong.Domain.Entities;
using Alabo.App.Open.HuDong.Domain.Services;

namespace Alabo.App.Open.HuDong.Controllers {

    [ApiExceptionFilter]
    [Route("Api/HudongRecord/[action]")]
    public class ApiHudongRecordController : ApiBaseController<HudongRecord, ObjectId> {

        public ApiHudongRecordController() : base() {
            //_userManager = userManager;
            BaseService = Resolve<IHudongRecordService>();
        }
    }
}