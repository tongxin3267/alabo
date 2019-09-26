using System;
using Alabo.App.Asset.Recharges.Domain.Entities;
using Alabo.App.Asset.Recharges.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Alabo.App.Asset.Recharges.Domain.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Recharge/[action]")]
    public class ApiRechargeController : ApiBaseController<Recharge, Int64> {

        public ApiRechargeController() : base() {
            BaseService = Resolve<IRechargeService>();
        }
    }
}