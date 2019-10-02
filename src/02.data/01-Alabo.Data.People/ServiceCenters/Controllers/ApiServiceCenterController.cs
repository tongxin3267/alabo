using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.Framework.Core.WebApis.Filter;

using MongoDB.Bson;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.RestfulApi;using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.Data.People.ServiceCenters.Domain.Entities;
using Alabo.Data.People.ServiceCenters.Domain.Services;

namespace Alabo.Data.People.ServiceCenters.Controllers {
		[ApiExceptionFilter]
		[Route("Api/ServiceCenter/[action]")]
		public class ApiServiceCenterController : ApiBaseController<ServiceCenter,ObjectId>  {
 public ApiServiceCenterController() : base() 
	{ 
		BaseService = Resolve<IServiceCenterService>();
	}

	}
}
