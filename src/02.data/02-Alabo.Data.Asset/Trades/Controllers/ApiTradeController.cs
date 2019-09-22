using Microsoft.AspNetCore.Mvc;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Services;

namespace Alabo.App.Core.Finance.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Trade/[action]")]
    public class ApiTradeController : ApiBaseController<Trade, long> {

        public ApiTradeController() : base() {
            BaseService = Resolve<ITradeService>();
        }
    }
}