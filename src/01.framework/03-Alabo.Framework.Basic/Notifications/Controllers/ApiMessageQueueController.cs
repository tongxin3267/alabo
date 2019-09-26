using Alabo.App.Core.Common.Domain.Services;
using Alabo.Framework.Basic.Notifications.Domain.Entities;
using Alabo.Framework.Basic.Notifications.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
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