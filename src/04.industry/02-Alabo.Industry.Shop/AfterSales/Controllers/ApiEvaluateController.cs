using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Shop.AfterSales.Domain.Entities;
using Alabo.Industry.Shop.AfterSales.Domain.Services;
using Alabo.RestfulApi;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.AfterSales.Controllers {
		[ApiExceptionFilter]
		[Route("Api/Evaluate/[action]")]
		public class ApiEvaluateController : ApiBaseController<Evaluate,ObjectId>  {

 public ApiEvaluateController(RestClientConfig restClientConfig ) : base() 
	{ 
	
		BaseService = Resolve<IEvaluateService>();

	}

	}
}
