using System.Collections.Generic;
using Alabo.App.Kpis.Kpis.Domain.Configs;
using Alabo.App.Kpis.Kpis.Domain.Entities;
using Alabo.App.Kpis.Kpis.Domain.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Kpis.Kpis.Controllers
{
    /// <summary>
    ///     KPIøº∫À
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/Kpi/[action]")]
    public class ApiKpiController : ApiBaseController<Kpi, long>
    {
        public ApiKpiController()
        {
            BaseService = Resolve<IKpiService>();
        }

        /// <summary>
        ///     ªÒ»°Kpiøº∫À≈‰÷√
        /// </summary>
        [HttpGet]
        public ApiResult<Dictionary<string, object>> GetKpiConfigs()
        {
            var transfers = Resolve<IAutoConfigService>().GetList<KpiAutoConfig>();
            var dic = new Dictionary<string, object>();
            transfers.Foreach(r =>
                dic.Add(r.Id.ToString(), r.Name));

            if (dic != null) return ApiResult.Success(dic);

            return ApiResult.Failure<Dictionary<string, object>>("ªÒ»°Kpiøº∫À≈‰÷√", MessageCodes.ParameterValidationFailure);
        }
    }
}