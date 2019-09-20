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
using Alabo.App.Agent.Province.Domain.Entities;
using Alabo.App.Core.UI.Domain.Services;
using Alabo.App.Agent.Province.Domain.Services;
using Alabo.App.Core.Api.Controller;

namespace Alabo.App.Agent.Province.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Province/[action]")]
    public class ApiProvinceController : ApiBaseController<Domain.Entities.Province, ObjectId> {
        
       

        public ApiProvinceController() : base() {
            BaseService = Resolve<IProvinceService>();
       
        }
    }
}