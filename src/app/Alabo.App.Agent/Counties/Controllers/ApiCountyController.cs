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
using Alabo.App.Agent.County.Domain.Entities;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Agent.County.Domain.Services;

namespace Alabo.App.Agent.County.Controllers {

    [ApiExceptionFilter]
    [Route("Api/County/[action]")]
    public class ApiCountyController : ApiBaseController<Domain.Entities.County, ObjectId> {
        
       

        public ApiCountyController() : base() {
            BaseService = Resolve<ICountyService>();
       
        }
    }
}