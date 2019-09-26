using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.User;
using Alabo.App.Market.DataBackup.Domain.Services;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.RestfulApi;

namespace Alabo.App.Market.DataBackup.Controllers {

    [ApiExceptionFilter]
    [Route("Api/DataBackup/[action]")]
    public class ApiDataBackupController : ApiBaseController<Domain.Entities.DataBackup, ObjectId> {

        public ApiDataBackupController() : base() {
            BaseService = Resolve<IDataBackupService>();

        }
    }
}