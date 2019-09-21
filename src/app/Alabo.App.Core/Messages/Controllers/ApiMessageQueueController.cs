using Microsoft.AspNetCore.Mvc;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
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