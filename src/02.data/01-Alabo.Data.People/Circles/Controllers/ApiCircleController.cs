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
using Alabo.App.Agent.Circle.Domain.Entities;
using Alabo.App.Agent.Circle.Domain.Services;
using Alabo.Core.WebApis.Controller;

namespace Alabo.App.Agent.Circle.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Circle/[action]")]
    public class ApiCircleController : ApiBaseController<Domain.Entities.Circle, ObjectId> {
        
       

        public ApiCircleController() : base() {
            BaseService = Resolve<ICircleService>();
       
        }
    }
}