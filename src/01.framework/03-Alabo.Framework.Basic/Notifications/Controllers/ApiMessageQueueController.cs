using Microsoft.AspNetCore.Mvc;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.App.Core.Common.Domain.Services;

namespace Alabo.App.Core.Common.Controllers {

    [ApiExceptionFilter]
    [Route("Api/MessageQueue/[action]")]
    public class ApiMessageQueueController : ApiBaseController<MessageQueue, long> {

        public ApiMessageQueueController() : base() {
            BaseService = Resolve<IMessageQueueService>();
        }
    }
}