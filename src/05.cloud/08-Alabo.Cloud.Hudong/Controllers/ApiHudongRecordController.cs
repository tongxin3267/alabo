using Alabo.App.Share.HuDong.Domain.Entities;
using Alabo.App.Share.HuDong.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.App.Share.HuDong.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/HudongRecord/[action]")]
    public class ApiHudongRecordController : ApiBaseController<HudongRecord, ObjectId>
    {
        public ApiHudongRecordController()
        {
            //_userManager = userManager;
            BaseService = Resolve<IHudongRecordService>();
        }
    }
}