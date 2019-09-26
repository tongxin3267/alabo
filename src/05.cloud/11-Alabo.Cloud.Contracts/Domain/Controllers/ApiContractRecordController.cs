using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Core.WebApis.Controller;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.Common;
using MongoDB.Bson;
using Alabo.App.Core.User;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.Cloud.Contracts.Domain.Entities;
using Alabo.Cloud.Contracts.Domain.Services;

namespace Alabo.Cloud.Contracts.Controllers {

    [ApiExceptionFilter]
    [Route("Api/ContractRecord/[action]")]
    public class ApiContractRecordController : ApiBaseController<ContractRecord, ObjectId> {

        public ApiContractRecordController() : base() {
            BaseService = Resolve<IContractRecordService>();
        }
    }
}