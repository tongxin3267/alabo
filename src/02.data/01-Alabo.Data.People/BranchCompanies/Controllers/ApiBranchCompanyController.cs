using Alabo.Data.People.BranchCompanys.Domain.Entities;
using Alabo.Data.People.BranchCompanys.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Data.People.BranchCompanys.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/BranchCompany/[action]")]
    public class ApiBranchCompanyController : ApiBaseController<BranchCompany, ObjectId>
    {
        public ApiBranchCompanyController() : base()
        {
            BaseService = Resolve<IBranchCompanyService>();
        }
    }
}