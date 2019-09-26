using _01_Alabo.Cloud.Core.DataBackup.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace _01_Alabo.Cloud.Core.DataBackup.Controllers {

    [ApiExceptionFilter]
    [Route("Api/DataBackup/[action]")]
    public class ApiDataBackupController : ApiBaseController<Domain.Entities.DataBackup, ObjectId> {

        public ApiDataBackupController() : base() {
            BaseService = Resolve<IDataBackupService>();

        }
    }
}