using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.Markets.EnterpriseCertification.Domain.Entities;
using Alabo.App.Core.Markets.EnterpriseCertification.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.Markets.EnterpriseCertification.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Enterprise/[action]")]
    public class ApiEnterpriseController : ApiBaseController<Enterprise, ObjectId> {

        public ApiEnterpriseController() : base() {
            BaseService = Resolve<IEnterpriseService>();
        }

        [HttpGet]
        [Display(Description = "��ҵ��֤")]
        public ApiResult<PagedList<Enterprise>> EnterpriseList([FromQuery] PagedInputDto parameter) {
            var model = Resolve<IEnterpriseService>().GetPagedList(Query);
            return ApiResult.Success(model);
        }

        /// <summary>
        /// ��ҵ��֤��Ϣ
        /// </summary>
        /// <param name="enterprise"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Enterprise([FromBody]Enterprise enterprise) {
            if (enterprise == null) {
                return ApiResult.Failure("ʵ�岻��Ϊ��");
            }
            var result = Resolve<IEnterpriseService>().AddOrUpdate(enterprise);
            if (result) {
                return ApiResult.Success("��֤�ɹ���");
            }
            return ApiResult.Failure("��֤ʧ��");
        }
    }
}