using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Core.Common;
using MongoDB.Bson;
using Alabo.App.Core.User;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.App.Asset.Refunds.Domain.Entities;
using Alabo.App.Asset.Refunds.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;

namespace Alabo.App.Asset.Refunds.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Refund/[action]")]
    public class ApiRefundController : ApiBaseController<Refund, long> {

        public ApiRefundController() : base() {
            BaseService = Resolve<IRefundService>();
        }
    }
}