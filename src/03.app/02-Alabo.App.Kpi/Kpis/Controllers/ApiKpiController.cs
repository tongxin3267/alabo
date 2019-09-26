using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Share.Kpi.Domain.CallBack;
using Alabo.App.Share.Kpi.Domain.Services;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Share.Kpi.Controllers {

    /// <summary>
    /// KPIøº∫À
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/Kpi/[action]")]
    public class ApiKpiController : ApiBaseController<Domain.Entities.Kpi, long> {

        public ApiKpiController() : base() {
            BaseService = Resolve<IKpiService>();
        }

        /// <summary>
        ///     ªÒ»°Kpiøº∫À≈‰÷√
        /// </summary>
        [HttpGet]
        public ApiResult<Dictionary<string, object>> GetKpiConfigs() {
            var transfers = Resolve<IAutoConfigService>().GetList<KpiAutoConfig>();
            var dic = new Dictionary<string, object>();
            transfers.Foreach(r =>
                dic.Add(r.Id.ToString(), r.Name));

            if (dic != null) {
                return ApiResult.Success(dic);
            }

            return ApiResult.Failure<Dictionary<string, object>>("ªÒ»°Kpiøº∫À≈‰÷√", MessageCodes.ParameterValidationFailure);
        }
    }
}