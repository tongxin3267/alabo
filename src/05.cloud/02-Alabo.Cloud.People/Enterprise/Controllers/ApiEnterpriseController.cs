using Alabo.Cloud.People.Enterprise.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Cloud.People.Enterprise.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Enterprise/[action]")]
    public class ApiEnterpriseController : ApiBaseController<Domain.Entities.Enterprise, ObjectId>
    {
        public ApiEnterpriseController()
        {
            BaseService = Resolve<IEnterpriseService>();
        }

        [HttpGet]
        [Display(Description = "��ҵ��֤")]
        public ApiResult<PagedList<Domain.Entities.Enterprise>> EnterpriseList([FromQuery] PagedInputDto parameter)
        {
            var model = Resolve<IEnterpriseService>().GetPagedList(Query);
            return ApiResult.Success(model);
        }

        /// <summary>
        ///     ��ҵ��֤��Ϣ
        /// </summary>
        /// <param name="enterprise"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Enterprise([FromBody] Domain.Entities.Enterprise enterprise)
        {
            if (enterprise == null) return ApiResult.Failure("ʵ�岻��Ϊ��");
            var result = Resolve<IEnterpriseService>().AddOrUpdate(enterprise);
            if (result) return ApiResult.Success("��֤�ɹ���");
            return ApiResult.Failure("��֤ʧ��");
        }
    }
}