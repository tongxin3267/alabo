using Alabo.App.Core.Common.Domain.Services;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Alabo.App.Core.Common.Controllers
{

    [ApiExceptionFilter]
    [Route("Api/MessageQueue/[action]")]
    public class ApiMessageQueueController : ApiBaseController<MessageQueue, long> {

        public ApiMessageQueueController() : base() {
            BaseService = Resolve<IMessageQueueService>();
        }
    }
}