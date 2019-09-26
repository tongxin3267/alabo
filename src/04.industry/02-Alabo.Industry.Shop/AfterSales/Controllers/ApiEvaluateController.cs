using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.Framework.Core.WebApis.Filter;

using MongoDB.Bson;
using Alabo.App.Core.User;
using Alabo.RestfulApi;using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.App.Shop.AfterSale.Domain.Entities;
using Alabo.App.Shop.AfterSale.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;

namespace Alabo.App.Shop.AfterSale.Controllers {
		[ApiExceptionFilter]
		[Route("Api/Evaluate/[action]")]
		public class ApiEvaluateController : ApiBaseController<Evaluate,ObjectId>  {

 public ApiEvaluateController(RestClientConfig restClientConfig ) : base() 
	{ 
	
		BaseService = Resolve<IEvaluateService>();

	}

	}
}
