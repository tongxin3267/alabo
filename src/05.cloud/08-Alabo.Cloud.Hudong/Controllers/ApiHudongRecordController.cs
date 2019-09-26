using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Share.HuDong.Domain.Entities;
using Alabo.App.Share.HuDong.Domain.Services;

namespace Alabo.App.Share.HuDong.Controllers {

    [ApiExceptionFilter]
    [Route("Api/HudongRecord/[action]")]
    public class ApiHudongRecordController : ApiBaseController<HudongRecord, ObjectId> {

        public ApiHudongRecordController() : base() {
            //_userManager = userManager;
            BaseService = Resolve<IHudongRecordService>();
        }
    }
}