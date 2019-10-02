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
using Alabo.Data.People.Regionals.Domain.Entities;
using Alabo.Data.People.Regionals.Domain.Services;

namespace Alabo.Data.People.Regionals.Controllers {
		[ApiExceptionFilter]
		[Route("Api/Regional/[action]")]
		public class ApiRegionalController : ApiBaseController<Regional,ObjectId>  {
 public ApiRegionalController() : base() 
	{ 
		BaseService = Resolve<IRegionalService>();
	}

	}
}
