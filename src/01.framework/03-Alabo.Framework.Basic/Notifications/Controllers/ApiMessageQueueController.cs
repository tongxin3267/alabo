using Alabo.Framework.Basic.Notifications.Domain.Entities;
using Alabo.Framework.Basic.Notifications.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Alabo.Framework.Basic.Notifications.Controllers {

    [ApiExceptionFilter]
    [Route("Api/MessageQueue/[action]")]
    public class ApiMessageQueueController : ApiBaseController<MessageQueue, long> {

        public ApiMessageQueueController() {
            BaseService = Resolve<IMessageQueueService>();
        }
    }
}