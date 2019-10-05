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
using Alabo.Cloud.Targets.Iterations.Domain.Entities;
using Alabo.Cloud.Targets.Iterations.Domain.Services;

namespace Alabo.Cloud.Targets.Iterations.Controllers {
		[ApiExceptionFilter]
		[Route("Api/Iteration/[action]")]
		public class ApiIterationController : ApiBaseController<Iteration,ObjectId>  {
 public ApiIterationController() : base() 
	{ 
		BaseService = Resolve<IIterationService>();
	}

	}
}
