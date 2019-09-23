using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using MongoDB.Bson;
using Alabo.App.Core.User;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.App.Asset.Recharges.Domain.Entities;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Finance.Domain.Services;

namespace Alabo.App.Asset.Recharges.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Recharge/[action]")]
    public class ApiRechargeController : ApiBaseController<Recharge, Int64> {

        public ApiRechargeController() : base() {
            BaseService = Resolve<IRechargeService>();
        }
    }
}