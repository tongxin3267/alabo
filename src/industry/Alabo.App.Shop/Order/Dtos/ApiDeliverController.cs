using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using MongoDB.Bson;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.User;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.App.Shop.Order.Domain.Dtos;

namespace Alabo.App.Shop.Order.Dtos {

    [ApiExceptionFilter]
    [Route("Api/Deliver/[action]")]
    public class ApiDeliverController : ApiBaseController<Deliver, ObjectId> {
        
       

        public ApiDeliverController() : base() {
            BaseService = Resolve<IDeliverService>();
       
        }
    }
}