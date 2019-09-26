using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.Framework.Core.WebApis.Filter;

using MongoDB.Bson;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.Data.Things.Goodss.Domain.Entities;
using Alabo.Data.Things.Goodss.Domain.Services;

namespace Alabo.Data.Things.Goodss.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Goods/[action]")]
    public class ApiGoodsController : ApiBaseController<Goods, long> {

        public ApiGoodsController() : base() {
            BaseService = Resolve<IGoodsService>();
        }
    }
}