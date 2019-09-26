using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.Common;
using MongoDB.Bson;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.User;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.App.Shop.AfterSale.Domain.Entities;
using Alabo.App.Shop.AfterSale.Domain.Services;

namespace Alabo.App.Shop.AfterSale.Controllers {

    [ApiExceptionFilter]
    [Route("Api/AfterSale/[action]")]
    public class ApiAfterSaleController : ApiBaseController<Domain.Entities.AfterSale, ObjectId> {
        
       

        public ApiAfterSaleController() : base() {
       
        }
    }
}