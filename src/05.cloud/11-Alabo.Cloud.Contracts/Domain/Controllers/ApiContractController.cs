using Alabo.Cloud.Contracts.Domain.Entities;
using Alabo.Cloud.Contracts.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Cloud.Contracts.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Contract/[action]")]
    public class ApiContractController : ApiBaseController<Contract, ObjectId>
    {
        public ApiContractController()
        {
            BaseService = Resolve<IContractService>();
        }
    }
}